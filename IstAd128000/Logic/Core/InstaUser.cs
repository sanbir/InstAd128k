using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Logic.Core
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

    }
}
