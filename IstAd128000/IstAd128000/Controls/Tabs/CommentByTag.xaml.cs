using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InstAd128000.Helpers;

namespace InstAd128000.Controls.Tabs
{
    /// <summary>
    /// Interaction logic for tAGS.xaml
    /// </summary>
    public partial class CommentByTag : UserControl
    {
        public CommentByTag()
        {
            InitializeComponent();
        }

        private async void Comment_OnClick(object sender, RoutedEventArgs e)
        {
            ControlGetter.MainWindow.IsNoProcessPerformed = false;
            SpinnerInstance.SetToMainWindow();
            CommentButton.IsEnabled = false;

            if (string.IsNullOrWhiteSpace(CommentTag.Text))
            {
                CommentTag.Text = "Please, enter valid text";
                CommentTag.Foreground = Brushes.Red;
                return;
            }

            //todo: use this parameter!!!!!!!
            var commentsNumber = CommentsNumber.Text;

            var result = await ControlGetter.MainWindow.User.CommentByTag(CommentTag.Text, CommentText.Text);
            CommentedPostsCount.Text = result.Data.Count.ToString();

            if (Convert.ToBoolean(ShouldLikeCheckBox.IsChecked))
            {
                //todo: запили по-браццки
            }

            ControlGetter.MainWindow.IsNoProcessPerformed = true;
            SpinnerInstance.RemoveFromMainWindow();
            CommentButton.IsEnabled = true;
        }
    }
}
