using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Medical_Studio.Converters
{
    public class RangeMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double min = System.Convert.ToDouble(values[0]);
            double val = System.Convert.ToDouble(values[1]);
            double max = System.Convert.ToDouble(values[2]);


            if(parameter != null)
            {
                string str = System.Convert.ToString(parameter);
                var parts = str.Split(':');

                double tgmin = System.Convert.ToDouble(values[3]);
                double tgmax = System.Convert.ToDouble(values[4]);
            }


            double lgmin = Math.Log10(min);
            double lgval = Math.Log10(val);
            double lgmax = Math.Log10(max);

            double lgdelta = lgmax - lgmin;
            if (lgdelta == 0)
                lgdelta = 1;
            double tgdelta = tgmax - tgmin;
            if (tgdelta == 0)
                tgdelta = 1;
            double K = tgdelta / lgdelta;

            return (lgval - lgmin) * K + tgmin;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            double min = System.Convert.ToDouble(values[0]);
            double val = System.Convert.ToDouble(values[1]);
            double max = System.Convert.ToDouble(values[2]);
            double tgmin = System.Convert.ToDouble(values[3]);
            double tgmax = System.Convert.ToDouble(values[4]);

            double lgmin = Math.Log10(min);
            double lgval = Math.Log10(val);
            double lgmax = Math.Log10(max);

            double lgdelta = lgmax - lgmin;
            if (lgdelta == 0)
                lgdelta = 1;
            double tgdelta = tgmax - tgmin;
            if (tgdelta == 0)
                tgdelta = 1;
            double K = tgdelta / lgdelta;

            return (lgval - lgmin) * K + tgmin;
        }
    }
}
