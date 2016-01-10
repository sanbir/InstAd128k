using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using System;

namespace Instad128000.Core.Helpers.Selenium
{
    public class Driver
    {
        private static IWebDriver _pD;

        public static IWebDriver PhantomInstance 
        {
            get
            {
                var driverService = PhantomJSDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                AppDomain.CurrentDomain.UnhandledException += Close;
                AppDomain.CurrentDomain.ProcessExit += Close;
                return _pD ?? (_pD = new PhantomJSDriver(driverService));
            }
        }

        ~Driver()
        {
            Close();
        }

        public static void Close()
        {
            if (_pD != null)
            {
                _pD.Quit();
                _pD.Dispose();
                _pD = null;
            }
        }

        private static void Close(object sender, UnhandledExceptionEventArgs args)
        {
            Close();
        }

        private static void Close(object sender, EventArgs args)
        {
            Close();
        }
    }
}
