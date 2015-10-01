﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourSquare.SharpSquare.Entities;
using Instad128000.Core.Common.Enums;
using Instad128000.Core.Common.Interfaces;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Models;
using Instad128000.Core.Extensions;
using Instad128000.Core.Helpers.Selenium;
using InstaSharp;
using InstaSharp.Models;
using InstaSharp.Models.Responses;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Instad128000.Core.Helpers.SocialNetworksUsers
{
    public class InstagramUser : BaseInstaUser
    {
        public IWebDriver WebDriver { get; set; }

        private InstagramConfig ApiConfig { get; set; }

        public WaitTimer WaitTimer { get; set; }

        public InstagramUser(string clientKey, string clientId, IWebDriver webDriver, string userName, string userPassword, 
            IRequestService requestService, IDataStringService dataStringService) 
                : base(userName, userPassword, clientKey, clientId, requestService, dataStringService)
        {
            WebDriver = webDriver;
            WaitTimer = new WaitTimer(webDriver);
        }

        public override bool Authorize()
        {
            return SeleniumAuth() && ApiAuth();
        }

        private bool SeleniumAuth()
        {
            WebDriver.Navigate().GoToUrl("https://instagram.com/accounts/login/");

            var user = WaitTimer.FindElement(By.Id("lfFieldInputUsername"), 60);
            var pass = WebDriver.FindElement(By.Id("lfFieldInputPassword"));
            var button = WebDriver.FindElement(By.ClassName("-cx-PRIVATE-LoginForm__loginButton"));

            user.SendKeys(UserName);
            pass.SendKeys(UserPassword);

            button.SendKeys(Keys.Enter);

            return WaitTimer.FindElement(By.CssSelector("span[class*='-cx-PRIVATE-SearchBox__inactiveSearchIcon coreSpriteSearchIcon']"), 60) != null;
        }

        private bool ApiAuth()
        {
            ApiConfig = new InstagramConfig(ClientId, ClientKey);
            return ApiConfig != null;
        }

        private async Task<bool> GetSeleniumUserId()
        {
            var users = new InstaSharp.Endpoints.Users(ApiConfig);
            UserId = (await users.Search(UserName, 1)).Data[0].Id;
            return UserId > 0;
        }

        /// <summary>
        /// Returns list of followers for selected user
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns></returns>
        public override async Task<IEnumerable<InstaSharp.Models.User>> GetContactsListAsync(string userName)
        {
            var users = new InstaSharp.Endpoints.Users(ApiConfig);
            var foundUser = await users.Search(userName, 1);
            var relationships = new InstaSharp.Endpoints.Relationships(ApiConfig);
            var followers = await relationships.FollowedBy(foundUser.Data[0].Id);
            return followers.Data;
        }

        /// <summary>
        /// Follow All Followers Of User
        /// </summary>
        /// <param name="userName">Name of user of which followers to follow</param>
        public override async Task<IEnumerable<InstaSharp.Models.User>> AddToContactsAllContactsOfUserAsync(string userName)
        {
            List<InstaSharp.Models.User> followers = (await GetContactsListAsync(userName)).ToList();
            if (followers == null) return null;
            var users = new InstaSharp.Endpoints.Users(ApiConfig);
            foreach (var item in followers)
            {
                //todo: убрать break
                break;
                WebDriver.Navigate().GoToUrl("https://instagram.com/" + item.Username);
                var followButton = WebDriver.FindElement(By.ClassName("-cx-PRIVATE-FollowButton__button"));
                followButton.Click();
            }
            return followers;
        }

        public override async Task<IEnumerable<RequestResult>> LikeByTagAsync(TimeSpan workPeriod)
        {
            _currentActionResultsList = new ObservableCollection<RequestResult>();
            
            var start = DateTime.Now;
            var end = DateTime.Now.Add(workPeriod);
            var random = new Random();

            if (UserId == 0)
            {
                await GetSeleniumUserId();
            }
            var mediaEndpoint = new InstaSharp.Endpoints.Media(ApiConfig);
            var tagsEndpoint = new InstaSharp.Endpoints.Tags(ApiConfig);
            var waitSeconds = 40; //todo: userSetting
            var likeFrequency = 2; //todo: userSetting // это типа каждое n-е фото только лайкает, чтоб никто ни о чём не догадался ]:->
            var banCount = 0;
            var count = 0;
            var banCountSetting = 10; //todo: userSettin
            var lastId = RequestService.GetAll()?.OrderByDescending(c => c.ModifyDate).Select(c => c.PostId)?.FirstOrDefault();

            do
            {
                //todo: проверочка на TagsToProcess нулл
                var tag = TagsToProcess.ToArray()[random.Next(0, TagsToProcess.Count() - 1)];
                var result = await tagsEndpoint.Recent(tag.Tag.NormalizeIt(), lastId, null, 50);
                
                foreach (var res in result.Data.ToArray())
                {
                    var timer = random.Next(10, waitSeconds);
                    if (random.Next(0, waitSeconds) <= waitSeconds/likeFrequency)
                    {
                        var likeResult = await Task.Run(() => AddLike(res));
                        if (likeResult != null)
                        {
                            if (likeResult.VictimsId == 0)
                            {
                                count++;
                            }
                            else
                            {
                                _currentActionResultsList.Add(likeResult);
                            }
                            
                        }
                        else
                        {
                            banCount++;
                            if (banCount == banCountSetting)
                                break;
                        }
                    }
                    else
                    {
                        count++;
                        WebDriver.Navigate().GoToUrl(res.Link);
                    }
                    if(DateTime.Now > end)
                    {
                        if (_currentActionResultsList != null && _currentActionResultsList.Count > 0)
                        {
                            lastId = _currentActionResultsList.Last().PostId;
                            SaveToDb();
                        }
                        return _currentActionResultsList.ToList();
                    }
                    await Task.Run(() => Thread.Sleep(new TimeSpan(0, 0, timer)));

                }

                lastId = result.Pagination.NextMaxTagId;
                SaveToDb();

            } while (DateTime.Now > end);


            return _currentActionResultsList.ToList();
        }

        public void WaitAjax()
        {
            while (true) // Handle timeout somewhere
            {
                var ready = new Func<bool>(() => (bool)ExecuteJavaScript("return (typeof($) === 'undefined') ? true : !$.active;"));
                if (ready())
                    break;
                Thread.Sleep(100);
            }
            
        }

        public object ExecuteJavaScript(string javaScript, params object[] args)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)WebDriver;

            return javaScriptExecutor.ExecuteScript(javaScript, args);
        }

        private RequestResult AddLike(Media media)
        {
            RequestResult result = null;
            
            WebDriver.Navigate().GoToUrl(media.Link);
            var likeButton = WebDriver.WaitUntil(By.ClassName("coreSpriteHeartOpen"), 5);
            if (likeButton != null)
            {
                string id = media.Id.Substring(0, media.Id.IndexOf("_"));
                string script = "window.vazr = ''; $.ajax({ url: 'https://instagram.com/web/likes/" + id + "/like/', type: 'POST'}).done(function( msg ) { window.vazr = msg.status == 'ok'? '1' : '0'; }).fail(function( jqXHR, textStatus ) {window.vazr = '0';});";
                ExecuteJavaScript(script);                                                      //1047724894729627550
                WaitAjax();
                string vazr = (string)ExecuteJavaScript("return window.vazr;");
                if(vazr == "1")
                    result = new RequestResult("", media.User.Id, UserId, media.Link, RequestType.Like,media.Id);
            }
            else
            {
                return new RequestResult("", 0, 0, "", RequestType.Like,media.Id);
            }
            
            return result;
        }

        private RequestResult AddComment(Media media, string commentText)
        {
            RequestResult result = null;
            WebDriver.Navigate().GoToUrl(media.Link);
            var commentResult = WebDriver.WaitUntil(By.XPath("//a[@title='" + UserName + "' and contains(text(), '" + UserName + "')] "), 5);
            int counter = 0;
            do
            {
                if (commentResult == null) // if comment not exists
                {
                    var commentField = WebDriver.WaitUntil(By.ClassName("-cx-PRIVATE-PostInfo__commentCreatorInput"), 10);
                    if (commentField == null)
                    {
                        if (counter != 2)
                        {
                            counter++;
                            continue;
                        }
                        break;
                    }

                    commentField.SendKeys(commentText.Trim());
                    commentField.SendKeys(Keys.Return);
                    commentResult = WebDriver.WaitUntil(By.XPath("//a[@title='" + UserName.ToLower() + "' and contains(text(), '" + UserName.ToLower() + "')] "), 5);

                    if (commentResult != null)
                    {
                        result = new RequestResult(commentText, media.User.Id, UserId, media.Link, RequestType.Comment, media.Id);
                        break;
                    }
                }
            } while (counter != 2); //todo: сделать колличество попыток настраиваемым для юзера
            
            return result;
        } 

        public override async Task<IEnumerable<RequestResult>> CommentByTagAsync(string commentText, TimeSpan workPeriod)
        {
            _currentActionResultsList = new ObservableCollection<RequestResult>();

            var start = DateTime.Now;
            var end = DateTime.Now.Add(workPeriod);
            var random = new Random();
            
            do
            {
                if (UserId == 0)
                {
                    await GetSeleniumUserId();
                }

                var tag = TagsToProcess.ToArray()[random.Next(0, TagsToProcess.Count() - 1)];

                var tagsEndpoint = new InstaSharp.Endpoints.Tags(ApiConfig);

                var lastId = RequestService.GetAll()?.OrderByDescending(c => c.ModifyDate).Select(c => c.PostId)?.FirstOrDefault();

                var result = await tagsEndpoint.Recent(tag.Tag.NormalizeIt(), lastId ?? "0", null, null);

                if (result == null)
                {
                    return null;
                }

                if (result.Data == null || result.Data.Count == 0)
                {
                    return null;
                }

                foreach (var res in result.Data.ToArray())
                {
                    var timer = random.Next(0, 20);

                    var requestResult = await Task.Run(() => AddComment(res, commentText));

                    if (requestResult != null)
                    {
                        _currentActionResultsList.Add(requestResult);
                    }

                    if (DateTime.Now > end)
                    {
                        if (_currentActionResultsList != null && _currentActionResultsList.Count > 0)
                        {
                            lastId = _currentActionResultsList.Last().PostId;
                            SaveToDb();
                        }
                        return _currentActionResultsList.ToList();
                    }

                    await Task.Run(() => Thread.Sleep(new TimeSpan(0, 0, timer)));
                };

                lastId = result.Pagination.NextMaxTagId;
                SaveToDb();

            } while (DateTime.Now > end);

            return _currentActionResultsList.ToList();
        }

        public override async Task<TagsResponse> SearchForTagsAsync(string tagPart)
        {
            var tags = new InstaSharp.Endpoints.Tags(ApiConfig);
            var results = await tags.Search(tagPart);

            return results;
        }
    }
}
