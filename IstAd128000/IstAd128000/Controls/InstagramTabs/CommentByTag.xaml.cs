using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InstAd128000.Helpers;

namespace InstAd128000.Controls.InstagramTabs
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
            ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = false;
            SpinnerInstance.SetToMainWindow();
            CommentButton.IsEnabled = false;

            if (string.IsNullOrWhiteSpace(CommentTag.Text))
            {
                CommentTag.Text = "Please, enter valid text";
                CommentTag.Foreground = Brushes.Red;
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
                

            int commentsNumber = Convert.ToInt32(CommentsNumber.Text);
            //todo: добавить поиск последнего id в бд для пагинации ептэ
            string lastId = "0";

            var result = await ControlGetter.MainWindow.InstagramTab.User.CommentByTagAsync(CommentTag.Text, CommentText.Text, commentsNumber, lastId, Convert.ToBoolean(ShouldLikeCheckBox.IsChecked));
            CommentedPostsCount.Text = result.Count.ToString();

            ResetMainWindow();
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

        private void ResetMainWindow()
        {
            ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = true;
            SpinnerInstance.RemoveFromMainWindow();
            CommentButton.IsEnabled = true;
        }
    }
}
