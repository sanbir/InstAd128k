using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace InstAd128000.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolArrToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a boolean");

            if ((bool)value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}