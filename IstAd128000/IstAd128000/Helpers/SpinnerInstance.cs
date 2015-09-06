using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using InstAd128000.Controls;

namespace InstAd128000.Helpers
{
    public static class SpinnerInstance
    {
        private static Spinner _spinner;

        public static Spinner Get
        {
            get { return _spinner ?? (_spinner = new Spinner()); }
        }

        public static void SetToMainWindow()
        {
            //todo: сделать универсально
            ControlGetter.MainWindow.InstagramTab.Panel.Children.Add(Get);
        }

        public static void RemoveFromMainWindow()
        {
            //todo: сделать универсально
            ControlGetter.MainWindow.InstagramTab.Panel.Children.Remove(Get);
        }
    }
}
