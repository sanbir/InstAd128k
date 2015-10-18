using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Instad128000.Core.Common.Interfaces;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Helpers.Selenium;
using Instad128000.Core.Helpers.SocialNetworksUsers;
using InstAd128000.Helpers;
using Instad128000.Core.Common.Enums;
using System;
using InstAd128000.ViewModels;
using Instad128000.Core.Common.Exceptions;
using System.Threading;
using System.ComponentModel;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login(IRequestService reqSRV, IDataStringService dataStrSRV)
        { 
            InitializeComponent();
            ViewModel.RequestService = reqSRV;
            ViewModel.DataStringService = dataStrSRV;
            PasswordBox.Password = ViewModel.Password;
        }

        private async void Login_OnClick(object sender, RoutedEventArgs e)
        {
            var error = false;

            if (string.IsNullOrWhiteSpace(ViewModel.Login))
            {
                UsernameBox.Background = new SolidColorBrush(Color.FromArgb(60, 255, 100, 0));
                UsernameBox.Text = "Please, provide username!";
                error = true;
            }
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordBox.Background = new SolidColorBrush(Color.FromArgb(60, 255, 100, 0));
                PasswordBox.Password = "Please, provide password!";
                error = true;
            }

            if (ViewModel.Login != Properties.Settings.Default.Username ||
               PasswordBox.Password != Properties.Settings.Default.Password)
            {
                var result = MessageBox.Show("Do you want to save credentials?","",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Properties.Settings.Default.Username = ViewModel.Login;
                    Properties.Settings.Default.Password = PasswordBox.Password;
                    Properties.Settings.Default.Save();
                }
            }
            if (error) return;

            try
            {
                IsInProgress(true);
                if (!(await DoLoginTaskAsync()))
                {
                    var warnText = new TextBlock();
                    warnText.Style = (Style)Application.Current.MainWindow.FindResource("OnStartDisclaimer");
                    warnText.Text = "Credentials provided are incorrect or do not exist. Please, ckeck it and try again.";
                    warnText.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
                    warnText.VerticalAlignment = VerticalAlignment.Top;

                    ControlGetter.MainWindow.InstagramTab.Panel.Children.Add(warnText);
                    ControlGetter.MainWindow.InstagramTab.Panel.Children.Add(this);
                }
                else
                {
                    ControlGetter.MainWindow.InstagramTab.Panel.Children.Clear();
                }
            }
            catch (InstAdException IAe)
            {
                MessageBox.Show(IAe.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Системная ошибка: " + exc.Message + ". Пожалуйста, попробуйте еще раз.");
            }
            finally
            {
                IsInProgress(false);
            }
        }

        private async Task<bool> DoLoginTaskAsync()
        {
            var user = await Task.Run(() => UserFactory.InitInsta(SocialUserType.Instagram, ViewModel.Login, ViewModel.Password,
                 ViewModel.RequestService, ViewModel.DataStringService));

            UiHelper.FindVisualParent<InstagramTabsContainer>(this).ViewModel.SetInstaUserModelChangedEventHandler();

            return await Task.Run(() => user.Authorize());
        }

        protected void IsInProgress(bool isInProgress)
        {
            if (isInProgress)
            {
                UiHelper.InstaBusy(true);
                LoginButton.IsEnabled = false;
            }
            else
            {
                UiHelper.InstaBusy(false);
                LoginButton.IsEnabled = true;
            }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            var MainParent = UiHelper.FindVisualParent<InstagramTabsContainer>(this);
            if (MainParent != null)
            {
                MainParent.ViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs args) =>
                {
                    ViewModel.CancelToken = MainParent.ViewModel.CancelToken.Token;
                };
            }
            base.OnVisualParentChanged(oldParent);
        }
    }
}
