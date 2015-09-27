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
using Instad128000.Core.Common.Interfaces;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Models;
using InstAd128000.Helpers;
using InstAd128000.ViewModels;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for HistoryOfActions.xaml
    /// </summary>
    public partial class HistoryOfActions : UserControl
    {
        public HistoryOfActions(IRequestService reqSRV, IDataStringService dataStrSRV)
        {
            InitializeComponent();
            ViewModel = new HistoryOfActionsViewModel();
            DataContext = ViewModel;
            ViewModel.RequestService = reqSRV;
            ViewModel.DataStringService = dataStrSRV;
        }

        public HistoryOfActionsViewModel ViewModel { get; set; }

        private void Link_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as TextBlock).Text);
        }

        private async Task SetItems()
        {
            ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = false;
            Helpers.SpinnerInstance.SetToMainWindow();

            Refresh.IsEnabled = false;
            HistoryContainerGrid.ItemsSource = await Task.Run(() => ViewModel.RequestService.GetAll().Select(x => new HistoryAction() { Comment = x.CommentText, Link = x.Link, Type = x.Type }));
            Refresh.IsEnabled = true;

            ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = true;
            Helpers.SpinnerInstance.RemoveFromMainWindow();
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await SetItems();
        }
    }
}
