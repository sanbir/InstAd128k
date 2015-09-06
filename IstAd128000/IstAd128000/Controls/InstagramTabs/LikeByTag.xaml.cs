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
                ResetMainWindow();
                return;
            }

            if (WorkTime.Value.HasValue)
            {
                var delta = WorkTime.Value.Value - DateTime.Now;
                if (delta.Hours <= 0 || delta.Minutes <= 0)
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

            //todo: use this parameter!!!!!!!
            var likes = Convert.ToInt32(LikesNumber.Text);

            string lastId = "0";

            var result = await ControlGetter.MainWindow.InstagramTab.User.LikeByTag(LikeTag.Text, likes, lastId);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Temp\result.txt", true))
            {
                foreach (var item in result)
                {
                    file.WriteLine("{0} url: {1}", item.Type.ToString("g"), item.Link);
                }
                
            }
            //todo: замути лайки

            ResetMainWindow();
        }

        private void ResetMainWindow()
        {
            ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = true;
            SpinnerInstance.RemoveFromMainWindow();
            LikeButton.IsEnabled = true;
        }
    }
}
