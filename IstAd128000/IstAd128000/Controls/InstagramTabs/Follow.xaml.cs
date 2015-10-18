using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InstAd128000.Helpers;
using InstaSharp.Models;
using System.Linq;
using Instad128000.Core.Helpers.SocialNetworksUsers;
using Instad128000.Core.Common.Interfaces.Services;
using InstAd128000.ViewModels;
using Instad128000.Core.Common.Exceptions;
using System;
using System.Threading;
using System.ComponentModel;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for Follow.xaml
    /// </summary>
    public partial class Follow : UserControl
    {
        public Follow(IRequestService reqSRV, IDataStringService dataStrSRV)
        {
            InitializeComponent();
            ViewModel.RequestService = reqSRV;
            ViewModel.DataStringService = dataStrSRV;
        }

        private async void Follow_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ViewModel.TypedUserName))
            {
                FollowUsernameBox.Text = "Text is invalid";
                FollowUsernameBox.Foreground = Brushes.Red;
                return;
            }

            try
            {
                IsInProgress(true);
                ViewModel.UserList = (await UserFactory.Insta.AddToContactsAllContactsOfUserAsync(ViewModel.TypedUserName)).ToList();
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

            if (ViewModel.UserList == null)
            {
                FollowedPeopleCount.Foreground = Brushes.Red;
                FollowedPeopleCount.Text = "Sorry, this user locked his page or user do not exist.";
            }
        }

        protected void IsInProgress(bool isInProgress)
        {
            if (isInProgress)
            {
                UiHelper.InstaBusy(true);
                FollowButton.IsEnabled = false;
            }
            else
            {
                UiHelper.InstaBusy(false);
                FollowButton.IsEnabled = true;
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
