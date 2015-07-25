using System;
using System.Collections.Generic;
using System.Linq;
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
            get { return _spinner ?? new Spinner(); }
        }

        public static void SetToMainWindow()
        {
            ControlGetter.MainWindow.Panel.Children.Add(Get);
        }
    }
}
