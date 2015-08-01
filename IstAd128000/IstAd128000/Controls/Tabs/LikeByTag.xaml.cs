using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InstAd128000.Helpers;

namespace InstAd128000.Controls.Tabs
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

        private void Like_OnClick(object sender, RoutedEventArgs e)
        {
            ControlGetter.MainWindow.IsNoProcessPerformed = false;
            SpinnerInstance.SetToMainWindow();
            LikeButton.IsEnabled = false;

            if (string.IsNullOrWhiteSpace(LikeTag.Text))
            {
                LikeTag.Text = "Please, enter valid text";
                LikeTag.Foreground = Brushes.Red;
                ControlGetter.MainWindow.IsNoProcessPerformed = true;
                SpinnerInstance.RemoveFromMainWindow();
                LikeButton.IsEnabled = true;
                return;
            }

            //todo: use this parameter!!!!!!!
            var commentsNumber = LikesNumber.Number;

            //todo: замути лайки

            ControlGetter.MainWindow.IsNoProcessPerformed = true;
            SpinnerInstance.RemoveFromMainWindow();
            LikeButton.IsEnabled = true;
        }
    }
}
