using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Instad128000.Core.Common.Models;
using InstAd128000.Helpers;
using System.Text.RegularExpressions;
using InstaSharp.Models.Responses;
using System.Collections.Generic;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using InstAd128000.ViewModels;
using Instad128000.Core.Helpers.SocialNetworksUsers;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Enums;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for SearchTags.xaml
    /// </summary>
    public partial class SearchTags : UserControl
    {
        public SearchTags(IRequestService reqSRV, IDataStringService dataStrSRV)
        {
            InitializeComponent();
            ViewModel.RequestService = reqSRV;
            ViewModel.DataStringService = dataStrSRV;
        }

        private async void SearchTags_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TagsStringInput.Text)
                || new Regex("[^a-zA-Zа-яА-Я]").Match(TagsStringInput.Text).Length > 0)
            {
                TagsStringInput.Text = "Text is invalid";
                TagsStringInput.Foreground = Brushes.Red;
                return;
            }

            IsInProgress(true);
            ViewModel.Result = (await UserFactory.Insta.SearchForTagsAsync(TagsStringInput.Text)).Data.Select(data => new TagsCount()
            {
                Count = data.MediaCount, Tag = data.Name.Trim()
            }).ToList();
            IsInProgress(false);
        }

        private void TagsStringInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TagsStringInput.Foreground = Brushes.Black;
        }


        private void ListItem_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var text = UiHelper.FindChild<TextBlock>(sender as Grid, "Tag").DataContext as TagsCount;
            if (!ViewModel.Chosen.Any(x => x == text))
            {
                ViewModel.Chosen.Add(text);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var deleted = new List<TagsCount>();
            deleted.AddRange(UserFactory.Insta.TagsToProcess.Where(x => !ViewModel.Chosen.Any(y => y == x)));

            UserFactory.Insta.TagsToProcess = UserFactory.Insta.TagsToProcess.Except(deleted);
            UserFactory.Insta.TagsToProcess = UserFactory.Insta.TagsToProcess.Concat(ViewModel.Chosen.Where(x => !UserFactory.Insta.TagsToProcess.Any(y => y == x)));

            var result = MessageBox.Show("Тэги сохранены, доступны во вкладках \"Комменты\" и \"Лайки\"");
        }

        private void Chosen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var text = UiHelper.FindChild<TextBlock>(sender as Grid, "Tag").Text;
            if (ViewModel.Chosen.Any(x => x.Tag == text))
            {
                ViewModel.Chosen.Remove(ViewModel.Chosen.FirstOrDefault(x=>x.Tag == text));
            }
        }

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var str in ViewModel.Result)
            {
                if (!ViewModel.Chosen.Any(x => x.Tag == str.Tag))
                {
                    ViewModel.Chosen.Add(str);
                }
            }
        }

        protected void IsInProgress(bool isInProgress)
        {
            if (isInProgress)
            {
                UiHelper.InstaBusy(true);
                TagSearchButton.IsEnabled = false;
            }
            else
            {
                UiHelper.InstaBusy(false);
                TagSearchButton.IsEnabled = true;
            }
        }
    }
}
