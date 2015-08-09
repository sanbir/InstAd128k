using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InstAd128000.Helpers;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for LikeByTag.xaml
    /// </summary>
    public partial class LikeByTag : UserControl
    {
        public LikeByTag()
        {
            InitializeComponent();
            MaxLikes = Convert.ToInt32(Properties.Settings.Default.MaxTransactionNumber);
        }

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
                ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = true;
                SpinnerInstance.RemoveFromMainWindow();
                LikeButton.IsEnabled = true;
                return;
            }

            //todo: use this parameter!!!!!!!
            var likes = Convert.ToInt32(LikesNumber.Text);

            string lastId = "0";

            var result = await ControlGetter.MainWindow.InstagramTab.User.LikeByTag(LikeTag.Text, likes, lastId);
            //todo: замути лайки

            ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = true;
            SpinnerInstance.RemoveFromMainWindow();
            LikeButton.IsEnabled = true;
        }
    }
}
