using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InstAd128000.Helpers;
using InstaSharp.Models;

namespace InstAd128000.Tabs
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

            var followString = FollowUsernameBox.Text;
            _returnList = await ControlGetter.MainWindow.User.FollowAllFollowersOfSelectedUser(followString);

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
