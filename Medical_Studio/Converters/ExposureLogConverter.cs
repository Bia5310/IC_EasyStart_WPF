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
        static double shift = 10;
        static double scale = 500;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = System.Convert.ToDouble(value);

            v += shift;
            v *= scale;

            if (v != 0)
                return Math.Log10(v);
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
