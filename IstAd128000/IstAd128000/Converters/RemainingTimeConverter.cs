using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace InstAd128000.Converters
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class RemainingTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(String))
                throw new InvalidOperationException("The target must be a DateTime");

            if (value == null) return null;

            var date = (DateTime)value;
            var now = DateTime.Now;
            var delta = date - now;

            var result = delta.Hours + " hours, " + delta.Minutes + " minutes";
            return result;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}