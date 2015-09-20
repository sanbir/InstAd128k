using OpenQA.Selenium.PhantomJS;
using System;

namespace Instad128000.Core.Helpers.Selenium
{
    public static class Driver
    {
        private static PhantomJSDriver _driver;

        public static PhantomJSDriver Instance 
        {
            get
            {
                var driverService = PhantomJSDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                AppDomain.CurrentDomain.UnhandledException += Close;
                AppDomain.CurrentDomain.ProcessExit += Close;
                return _driver ?? (_driver = new PhantomJSDriver(driverService));
            }
        }

        public static void Close()
        {
            if (_driver != null)
            {
                _driver.Quit();
            }
        }

        private static void Close(object sender, UnhandledExceptionEventArgs args)
        {
            if (_driver != null)
            {
                _driver.Quit();
            }
        }

        private static void Close(object sender, EventArgs args)
        {
            if (_driver != null)
            {
                _driver.Quit();
            }
        }
    }
}
