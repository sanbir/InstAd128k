using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Extensions;
using InstaSharp;
using InstaSharp.Endpoints;
using InstaSharp.Models;
using InstaSharp.Models.Responses;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Instad128000.Logic.Core
{
    public class InstaUser
    {
        public string ClientKey { get; set; }

        public string ClientId { get; set; }

        public PhantomJSDriver Driver { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        private InstagramConfig ApiConfig { get; set; }

        public WaitTimer WaitTimer { get; set; }


        public InstaUser(string clientKey, string clientId, PhantomJSDriver driver, string userName, string userPassword)
        {
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
        private async Task<List<User>> GetFollowersList(string userName)
        {
            var users = new InstaSharp.Endpoints.Users(ApiConfig);
            var foundUser = await users.Search(userName, 1);
            var relationships = new InstaSharp.Endpoints.Relationships(ApiConfig);
            var followers = await relationships.FollowedBy(foundUser.Data[0].Id);
            return followers.Data;
        }

        /// <summary>
        /// Follow For All Followers Of SelectedUser
        /// </summary>
        /// <param name="userName">User Name</param>
        public async Task<List<User>> FollowAllFollowersOfSelectedUser(string userName)
        {
            List<User> followers = await GetFollowersList(userName);
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

        public async Task<List<Tags>> CommentByTag(string tag)
        {
            var tags = new InstaSharp.Endpoints.Tags(ApiConfig);
            var results = await tags.Recent("Cats","","",100);

            foreach (var result in results.Data.ToArray())
            {
                break;
                Driver.Navigate().GoToUrl("https://instagram.com/p/47IICuwP7O/?tagged=cat");
                var commentField = Driver.WaitUntil(By.ClassName("-cx-PRIVATE-PostInfo__commentCreatorInput"), 60);
                commentField.SendKeys("Amazing cat!!!");
                commentField.SendKeys(Keys.Return);
            }
            return null;
        }

        public async Task<TagsResponse> SearchForTags(string tagPart)
        {
            var tags = new InstaSharp.Endpoints.Tags(ApiConfig);
            var results = await tags.Search(tagPart);

            return results;
        }
    }
}
