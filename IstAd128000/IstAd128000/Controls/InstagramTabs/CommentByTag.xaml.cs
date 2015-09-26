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
            CommentTag.Text = String.Join("; ", UserFactory.Insta.TagsToProcess);
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

            var result = await UserFactory.Insta.CommentByTagAsync(CommentText.Text, WorkTime.Value.Value - DateTime.Now);
            CommentedPostsCount.Text = result.Count().ToString();

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
