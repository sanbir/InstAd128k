using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Instad128000.Core.Common.Models;
using InstAd128000.Helpers;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for SearchTags.xaml
    /// </summary>
    public partial class SearchTags : UserControl
    {
        public SearchTags()
        {
            InitializeComponent();
        }

        private async void SearchTags_OnClick(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TagsStringInput.Text))
            {
                TagsStringInput.Text = "Please, enter valid text";
                TagsStringInput.Foreground = Brushes.Red;
                return;
            }

            ControlGetter.MainWindow.IsNoProcessPerformed = false;
            SpinnerInstance.SetToMainWindow();
            TagSearchButton.IsEnabled = false;

            var result = await ControlGetter.MainWindow.User.SearchForTags(TagsStringInput.Text);
            var list = result.Data.Select(data => new TagsCount()
            {
                Count = data.MediaCount, Tag = data.Name.Trim()
            }).ToList();

            FoundTagsContainerGrid.ItemsSource = list;

            ControlGetter.MainWindow.IsNoProcessPerformed = true;
            SpinnerInstance.RemoveFromMainWindow();
            TagSearchButton.IsEnabled = true;
        }

        private void TagsStringInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TagsStringInput.Foreground = Brushes.Black;
        }


        private void ListItem_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Use this tag to leave comments(Yes) or likes(No)?","",MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                var tag = ControlGetter.MainWindow.CommentByTag.Tag as string;
                ControlGetter.MainWindow.CommentByTag.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                var keyIsPresent = false;
                do
                {
                    keyIsPresent = ControlGetter.MainWindow.ControlsList.ContainsKey(tag);
                } while (keyIsPresent == false);
                (ControlGetter.MainWindow.ControlsList[tag] as CommentByTag).CommentTag.Text = UiHelper.FindChild<TextBlock>(sender as Grid,"Tag").Text;
            }
            if (result == MessageBoxResult.No)
            {
                var tag = ControlGetter.MainWindow.LikeByTag.Tag as string;
                ControlGetter.MainWindow.LikeByTag.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                var keyIsPresent = false;
                do
                {
                    keyIsPresent = ControlGetter.MainWindow.ControlsList.ContainsKey(tag);
                } while (keyIsPresent == false);
                (ControlGetter.MainWindow.ControlsList[tag] as LikeByTag).LikeTag.Text = UiHelper.FindChild<TextBlock>(sender as Grid, "Tag").Text;
            }
        }
    }
}
