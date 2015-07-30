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

namespace InstAd128000.Tabs
{
    /// <summary>
    /// Interaction logic for LikeByTag.xaml
    /// </summary>
    public partial class LikeByTag : UserControl
    {
        public LikeByTag()
        {
            InitializeComponent();
        }

        private void Like_OnClick(object sender, RoutedEventArgs e)
        {
            ControlGetter.MainWindow.IsNoProcessPerformed = false;
            SpinnerInstance.SetToMainWindow();
            LikeButton.IsEnabled = false;

            if (string.IsNullOrWhiteSpace(LikeTag.Text))
            {
                LikeTag.Text = "Please, enter valid text";
                LikeTag.Foreground = Brushes.Red;
                return;
            }

            //todo: use this parameter!!!!!!!
            var commentsNumber = LikesNumber.Text;

            //todo: замути лайки

            ControlGetter.MainWindow.IsNoProcessPerformed = true;
            SpinnerInstance.RemoveFromMainWindow();
            LikeButton.IsEnabled = true;
        }
    }
}
