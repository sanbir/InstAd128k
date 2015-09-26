using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InstAd128000.Helpers
{
    public static class ControlGetter
    {
        //todo: ибавиться от этого уродства
        public static MainWindow MainWindow
        {
            get { return (MainWindow) Application.Current.MainWindow; }
        }
    }
}
