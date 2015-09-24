using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;

namespace Instad128000.Core.Extensions
{
    public static class WebDriverExtensions
    {
        public static IWebElement WaitUntil(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                try
                {
                    wait.Until(ExpectedConditions.ElementExists(by));
                }
                catch(Exception e)
                {
                    return null;
                }
            }
            return driver.FindElement(by);
        }
    }
}
