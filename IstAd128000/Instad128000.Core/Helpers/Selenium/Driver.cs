using OpenQA.Selenium;
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
            }
        }

        private static void Close(object sender, UnhandledExceptionEventArgs args)
        {
            if (_pD != null)
            {
                _pD.Quit();
            }
        }

        private static void Close(object sender, EventArgs args)
        {
            if (_pD != null)
            {
                _pD.Quit();
            }
        }
    }
}
