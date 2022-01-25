using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Medical_Studio.Converters
{
    public class MultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double m = 1;
            double val = 1;
            try
            {
                m = System.Convert.ToDouble(parameter);
                val = System.Convert.ToDouble(value);
            }
            catch (Exception) { }
            
            return val * m;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double m = 1;
            double val = 1;
            try
            {
                m = System.Convert.ToDouble(parameter);
                val = System.Convert.ToDouble(value);
            }
            catch (Exception) { }

            return val / m;
        }
    }
}
