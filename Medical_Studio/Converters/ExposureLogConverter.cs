using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Medical_Studio.Converters
{
    public class ExposureLogConverter : IValueConverter
    {
        static double shift = 6;
        static double scale = 400;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = System.Convert.ToDouble(value);

            if (v != 0)
            {
                v = Math.Log10(v);
                v += shift;
                v *= scale;
                return v;
            }
            else
                return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = System.Convert.ToDouble(value) / scale - shift;
            return Math.Pow(10, v);
        }
    }
}
