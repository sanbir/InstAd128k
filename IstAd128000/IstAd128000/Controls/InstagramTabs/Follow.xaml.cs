using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InstAd128000.Helpers;
using InstaSharp.Models;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for Follow.xaml
    /// </summary>
    public partial class Follow : UserControl
    {
        public Follow()
        {
            InitializeComponent();
        }

        private List<User> _returnList;
        public List<User> UserList {
            get { return _returnList; }
        }

        private async void Follow_OnClick(object sender, RoutedEventArgs e)
        {
            _returnList = null;

            ControlGetter.MainWindow.IsNoProcessPerformed = false;
            SpinnerInstance.SetToMainWindow();
            FollowButton.IsEnabled = false;

            //todo: check text is not empty
            var followString = FollowUsernameBox.Text;
            _returnList = await ControlGetter.MainWindow.User.AddToContactsAllContactsOf(followString);

            if (_returnList != null)
            {
                FollowedPeopleContainerGrid.ItemsSource = _returnList;
                FollowedPeopleCount.Text = "Followed Count (" + _returnList.Count + "):";
            }
            else
            {
                FollowedPeopleCount.Foreground = Brushes.Red;
                FollowedPeopleCount.Text = "Sorry, this user locked his page or user do not exist.";
            }
            ControlGetter.MainWindow.IsNoProcessPerformed = true;
            SpinnerInstance.RemoveFromMainWindow();
            FollowButton.IsEnabled = true;
        }
    }
}
