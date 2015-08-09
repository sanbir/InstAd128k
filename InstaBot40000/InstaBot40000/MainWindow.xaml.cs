using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;

namespace InstaBot40000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PhantomJSDriver _driver;
        private WebDriverWait _wait;



        public MainWindow()
        {
            InitializeComponent();
            _driver = new PhantomJSDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _driver.Navigate().GoToUrl("https://instagram.com/accounts/login/");

            var user = _wait.Until(driver=>driver.FindElement(By.Id("lfFieldInputUsername")));
            var pass = _driver.FindElement(By.Id("lfFieldInputPassword"));
            var button = _driver.FindElement(By.ClassName("-cx-PRIVATE-LoginForm__loginButton"));

            user.SendKeys("pivers_sib");
            pass.SendKeys("717453zx");

            button.Click();
            var a = FindElement(By.LinkText("Log out"), 60);
            if (a == null)
            {
                AuthError.Visibility = Visibility.Visible;
                return;
            }
            AuthError.Visibility = Visibility.Hidden;

            //_driver.Navigate().GoToUrl("https://instagram.com/explore/tags/Пиво/");
            //a = FindElement(By.LinkText("API"), 60);


            var config = new InstaSharp.InstagramConfig("f5f2c96a6bf948348de274b9168d05dc", "9ff514285aac43a6ac9c07257be3c3d2");
            var tags = new InstaSharp.Endpoints.Tags(config);

            var result = await tags.Recent("Cats");

            //foreach (var item in result.Data)
            //{
            _driver.Navigate().GoToUrl("https://instagram.com/p/47IICuwP7O/?tagged=cat");
                //button = FindElement(By.ClassName("-cx-PRIVATE-PostInfo__likeButton"), 60);
                //button.Click();
                var commentField = FindElement(By.ClassName("-cx-PRIVATE-PostInfo__commentCreatorInput"), 60);
                commentField.SendKeys("Amazing cat!!!");
                commentField.SendKeys(Keys.Return);
            //}
            var wr = new StreamWriter("1stFile.html");
            wr.WriteLine(a);
            wr.Close();
            _driver.Quit();
        }

        public IWebElement FindElement(By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return _driver.FindElement(by);
        }
    }
}
