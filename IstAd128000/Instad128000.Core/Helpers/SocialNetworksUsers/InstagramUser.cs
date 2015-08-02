using System.Collections.Generic;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces;
using Instad128000.Core.Extensions;
using Instad128000.Core.Helpers.Selenium;
using InstAd128000.SqlLite;
using InstaSharp;
using InstaSharp.Models;
using InstaSharp.Models.Responses;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Instad128000.Core.Helpers.SocialNetworksUsers
{
    public class InstagramUser : ISocialNetworkUser<User>
    {
        public string ClientKey { get; set; }

        public string ClientId { get; set; }

        public PhantomJSDriver Driver { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        private InstagramConfig ApiConfig { get; set; }

        public WaitTimer WaitTimer { get; set; }

        public InstagramUser(string clientKey, string clientId, PhantomJSDriver driver, string userName, string userPassword)
        {
            var aaa = new DataBaseHandler();
            ClientId = clientId;
            ClientKey = clientKey;
            Driver = driver;
            WaitTimer = new WaitTimer(driver);
            UserName = userName;
            UserPassword = userPassword;
        }

        public bool Authorize()
        {
            return SeleniumAuth() && ApiAuth();
        }

        private bool SeleniumAuth()
        {
            Driver.Navigate().GoToUrl("https://instagram.com/accounts/login/");

            var user = WaitTimer.FindElement(By.Id("lfFieldInputUsername"), 60);
            var pass = Driver.FindElement(By.Id("lfFieldInputPassword"));
            var button = Driver.FindElement(By.ClassName("-cx-PRIVATE-LoginForm__loginButton"));

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

        /// <summary>
        /// Returns list of followers for selected user
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns></returns>
        public async Task<List<User>> GetContactsListOf(string userName)
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
        public async Task<List<User>> AddToContactsAllContactsOf(string userName)
        {
            List<User> followers = await GetContactsListOf(userName);
            if (followers == null) return null;
            var users = new InstaSharp.Endpoints.Users(ApiConfig);
            foreach (var item in followers)
            {
                //todo: убрать break
                break;
                Driver.Navigate().GoToUrl("https://instagram.com/" + item.Username);
                var followButton = Driver.FindElement(By.ClassName("-cx-PRIVATE-FollowButton__button"));
                followButton.Click();
            }
            return followers;
        }

        public async Task<MediasResponse> CommentByTag(string tag, string commentText)
        {
            var tags = new InstaSharp.Endpoints.Tags(ApiConfig);
            var result = await tags.Recent(tag,"500","10000000",1000);

            foreach (var res in result.Data.ToArray())
            {
                break;
                Driver.Navigate().GoToUrl("https://instagram.com/p/" + "47IICuwP7O/?tagged=" + tag);
                var commentField = Driver.WaitUntil(By.ClassName("-cx-PRIVATE-PostInfo__commentCreatorInput"), 60);
                commentField.SendKeys(commentText);
                commentField.SendKeys(Keys.Return);
            }
            return result;
        }

        public async Task<TagsResponse> SearchForTags(string tagPart)
        {
            var tags = new InstaSharp.Endpoints.Tags(ApiConfig);
            var results = await tags.Search(tagPart);

            return results;
        }
    }
}
