using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Instad128000.Core;
using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Interfaces.Data.Services;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Logger;
using Instad128000.Core.Helpers.Selenium;
using InstAd128000.Helpers;
using InstAd128000.Services;
using InstAd128000.SqlLite;
using Microsoft.Practices.Unity;
using InstAd128000.ViewModels;

namespace InstAd128000
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
            this.Exit += App_Exit; ;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            Driver.Close();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
        {
            Driver.Close();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var windowVM = Unity.Container.Resolve<MainWindowViewModel>();
            var window = Unity.Container.Resolve<MainWindow>();

            window.ViewModel = windowVM;
            window.Show();
        }
    }
}
