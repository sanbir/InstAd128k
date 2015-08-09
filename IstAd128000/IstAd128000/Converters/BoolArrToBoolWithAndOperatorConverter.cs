using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace InstAd128000.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolArrToBoolWithAndOperatorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");
            
            var result = true;
            for (var i=0;i<values.Count()-1;i++)
            {
                result = (bool) values[i] && (bool) values[i+1];
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}