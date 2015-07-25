using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;

namespace Instad128000.Logic.Core
{
    public class WaitTimer
    {
        public WaitTimer(PhantomJSDriver driver)
        {
            Driver = driver;
        }

        public PhantomJSDriver Driver { get; set; }

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
