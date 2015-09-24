using Instad128000.Core.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Instad128000.Core.Helpers.Selenium
{
    public class WaitTimer
    {
        public WaitTimer(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebDriver Driver { get; set; }

        public IWebElement FindElement(By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                return Driver.WaitUntil(by,60);
            }
            return Driver.FindElement(by);
        }
    }
}
