using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestApplication.View
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class LocalDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var dt = new DateTime(System.Convert.ToInt64(value));
                    var dtValue = System.Convert.ToDateTime(dt).ToLocalTime();
                    return dtValue;
            }           
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var dtValue = System.Convert.ToDateTime(value).ToUniversalTime();
                return dtValue;
            }
            return null;
        }
    }
}
