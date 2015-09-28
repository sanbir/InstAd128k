using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Helpers.SocialNetworksUsers;
using InstAd128000.Services;
using Microsoft.Practices.Unity;
using Instad128000.Core.Common.Interfaces;
using InstAd128000.Helpers;

namespace InstAd128000.Controls
{
    /// <summary>
    /// Interaction logic for InstagramTabsContainer.xaml
    /// </summary>
    public partial class InstagramTabsContainer : UserControl, INotifyPropertyChanged
    {
        public InstagramTabsContainer()
        {
            InitializeComponent();
            _noProcessPerformed = true;
            _controlsList = new Dictionary<string, UserControl>();
        }

        private bool _loggedIn;
        private bool _noProcessPerformed;
        public bool IsLogged
        {
            get { return _loggedIn; }
            set
            {
                _loggedIn = value;
                NotifyPropertyChanged("IsLogged");
            }
        }
        public bool IsNoProcessPerformed
        {
            get { return _noProcessPerformed; }
            set
            {
                _noProcessPerformed = value;
                NotifyPropertyChanged("IsNoProcessPerformed");
            }
        }
        public bool IsUiFreeForUser
        {
            get { return IsLogged && !IsNoProcessPerformed; }
        }

        private Dictionary<string, UserControl> _controlsList;
        public Dictionary<string, UserControl> ControlsList
        {
            get { return _controlsList; }
        }
        
        private void AnyButton_OnClick(object sender, RoutedEventArgs e)
        {
            UserControl tab;

            var button = sender as Button;
            if (button == null) return;
            var tag = Convert.ToString(button.Tag);

            if (_controlsList.ContainsKey(tag))
            {
                tab = _controlsList[tag];
                tab.Width = double.NaN;
                tab.Height = double.NaN;
                Panel.Children.Clear();
                Panel.Children.Add(tab);
                return;
            }
            try
            {
                tab = (UserControl)Activator.CreateInstance(Type.GetType("InstAd128000.Controls.InstagramTabs." + tag), new object[] { Unity.Container.Resolve<IRequestService>(), Unity.Container.Resolve<IDataStringService>() });
                _controlsList.Add(tag, tab);
            }
            catch (Exception ex)
            {
                MessageBox.Show("This functionality is under development");
                return;
            }

            tab.Width = double.NaN;
            tab.Height = double.NaN;
            Panel.Children.Clear();
            Panel.Children.Add(tab);
        }

        //public static readonly DependencyProperty RequestServiceProperty =
        //          DependencyProperty.Register(nameof(RequestSRV), typeof(IRequestService), typeof(InstagramTabsContainer), 
        //              new FrameworkPropertyMetadata(null, OnRequestServiceChanged));

        //public IRequestService RequestSRV
        //{
        //    get { return GetValue(RequestServiceProperty) as IRequestService; }
        //    set { SetValue(RequestServiceProperty, value); }
        //}

        //private static void OnRequestServiceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs eventArgs)
        //{
        //    var control = obj as InstagramTabsContainer;
        //    control.RequestSRV = (IRequestService)eventArgs.NewValue;
        //}

        //public static readonly DependencyProperty DataStringServiceProperty =
        //          DependencyProperty.Register(nameof(DataStringSRV), typeof(IDataStringService), typeof(InstagramTabsContainer),
        //              new FrameworkPropertyMetadata(null, OnDataStringServiceChanged));
        //public IDataStringService DataStringSRV
        //{
        //    get { return GetValue(DataStringServiceProperty) as IDataStringService; }
        //    set { SetValue(DataStringServiceProperty, value); }
        //}

        //private static void OnDataStringServiceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs eventArgs)
        //{
        //    var control = obj as InstagramTabsContainer;
        //    control.DataStringSRV = (IDataStringService)eventArgs.NewValue;
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
