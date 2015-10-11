using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InstAd128000.Helpers;
using System.Collections.Generic;
using Instad128000.Core.Helpers.SocialNetworksUsers;
using Instad128000.Core.Common.Interfaces.Services;
using InstAd128000.ViewModels;
using System.Collections.Specialized;
using System.ComponentModel;
using Instad128000.Core.Common.Interfaces;
using Instad128000.Core.Common.Exceptions;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for tAGS.xaml
    /// </summary>
    public partial class CommentByTag : UserControl
    {
        public CommentByTag(IRequestService reqSRV, IDataStringService dataStrSRV)
        {
            InitializeComponent();
            ViewModel.RequestService = reqSRV;
            ViewModel.DataStringService = dataStrSRV;
            UserFactory.Insta.PropertyChanged += (object sender, PropertyChangedEventArgs args) => {
                ViewModel.Tags = UserFactory.Insta.TagsToProcess;
                ViewModel.Locations = UserFactory.Insta.LocationsToProcess;
            };
            ViewModel.Tags = UserFactory.Insta.TagsToProcess;
            ViewModel.Locations = UserFactory.Insta.LocationsToProcess;
        }
        
        private async void Comment_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CommentText.Text))
            {
                MessageBox.Show("Пожалуйста, введите комментарий");
                return;
            }
            if (ViewModel.EndTime.HasValue)
            {
                var delta = ViewModel.EndTime.Value - DateTime.Now;
                if (delta.Hours < 0 || delta.Minutes <= 0)
                {
                    MessageBox.Show("Пожалуйста, введите валидный промежуток времени");
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
                var result = await UserFactory.Insta.DoActionAsync(ViewModel.EndTime.Value - DateTime.Now, CommentText.Text);
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

        private void CommentsNumber_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            var regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        protected void IsInProgress(bool isInProgress)
        {
            if (isInProgress)
            {
                UiHelper.InstaBusy(true);
                CommentButton.IsEnabled = false;
            }
            else
            {
                UiHelper.InstaBusy(false);
                CommentButton.IsEnabled = true;
            }
        }
    }
}
