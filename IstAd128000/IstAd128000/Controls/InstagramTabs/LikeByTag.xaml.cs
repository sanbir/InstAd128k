using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InstAd128000.Helpers;
using System.Collections.Generic;
using Instad128000.Core.Helpers.SocialNetworksUsers;
using Instad128000.Core.Common.Interfaces.Services;
using InstAd128000.ViewModels;

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
            LikeTag.Text = String.Join("; ", UserFactory.Insta.TagsToProcess);
            ViewModel = new LikeByTagViewModel();
            DataContext = ViewModel;
            ViewModel.RequestService = reqSRV;
            ViewModel.DataStringService = dataStrSRV;
        }

        public LikeByTagViewModel ViewModel { get; set; }

        private int MaxLikes { get; set; }

        private async void Like_OnClick(object sender, RoutedEventArgs e)
        {
            ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = false;
            SpinnerInstance.SetToMainWindow();
            LikeButton.IsEnabled = false;

            if (string.IsNullOrWhiteSpace(LikeTag.Text))
            {
                LikeTag.Text = "Please, enter valid text";
                LikeTag.Foreground = Brushes.Red;
                ResetMainWindow();
                return;
            }

            if (WorkTime.Value.HasValue)
            {
                var delta = WorkTime.Value.Value - DateTime.Now;
                if (delta.Hours < 0 || delta.Minutes <= 0)
                {
                    MessageBox.Show("Пожалуйста, введите валидный промежуток времени");
                    ResetMainWindow();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите валидный промежуток времени");
                ResetMainWindow();
                return;
            }

            var tags = new List<string>();
            tags.AddRange(LikeTag.Text.Split(';'));
            var result = await UserFactory.Insta.LikeByTagAsync(WorkTime.Value.Value - DateTime.Now);
           
            ResetMainWindow();
        }

        private void ResetMainWindow()
        {
            ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = true;
            SpinnerInstance.RemoveFromMainWindow();
            LikeButton.IsEnabled = true;
        }
    }
}
