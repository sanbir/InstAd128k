using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InstAd128000.Helpers;
using System.Collections.Generic;
using Instad128000.Core.Helpers.SocialNetworksUsers;
using Instad128000.Core.Common.Interfaces.Services;
using InstAd128000.ViewModels;
using System.Collections.Specialized;
using System.ComponentModel;
using Instad128000.Core.Common.Exceptions;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for LikeByTag.xaml
    /// </summary>
    public partial class LikeByTag : UserControl
    {
        public LikeByTag(IRequestService reqSRV, IDataStringService dataStrSRV)
        {
            InitializeComponent();
            MaxLikes = Convert.ToInt32(Properties.Settings.Default.MaxTransactionNumber);
            ViewModel.RequestService = reqSRV;
            ViewModel.DataStringService = dataStrSRV;
            UserFactory.Insta.TagsChanged += () => {
                ViewModel.Tags = UserFactory.Insta.TagsToProcess;
            };
            ViewModel.Tags = UserFactory.Insta.TagsToProcess;
            UserFactory.Insta.LocationsChanged += () => {
                ViewModel.Locations = UserFactory.Insta.LocationsToProcess;
            };
            ViewModel.Locations = UserFactory.Insta.LocationsToProcess;
        }

        private int MaxLikes { get; set; }

        private async void Like_OnClick(object sender, RoutedEventArgs e)
        {
            if (WorkTime.Value.HasValue)
            {
                var delta = WorkTime.Value.Value - DateTime.Now;
                if (delta.Hours < 0 || delta.Minutes <= 0)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите валидный промежуток времени");
                return;
            }

            try
            {
                IsInProgress(true);
                var result = await UserFactory.Insta.DoActionAsync(WorkTime.Value.Value - DateTime.Now);
            }
            catch (InstAdException IAe)
            {
                MessageBox.Show(IAe.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show("System error: " + exc.Message + ". Please, try again.");
            }
            finally
            {
                IsInProgress(false);
            }
            IsInProgress(false);
        }

        protected void IsInProgress(bool isInProgress)
        {
            if (isInProgress)
            {
                UiHelper.InstaBusy(true);
                LikeButton.IsEnabled = false;
            }
            else
            {
                UiHelper.InstaBusy(false);
                LikeButton.IsEnabled = true;
            }
        }
    }
}
