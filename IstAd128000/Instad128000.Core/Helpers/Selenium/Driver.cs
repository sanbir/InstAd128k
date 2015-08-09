using OpenQA.Selenium.PhantomJS;

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
    }

}
