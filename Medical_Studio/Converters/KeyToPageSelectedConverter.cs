using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Medical_Studio.Converters
{
    public class KeyToPageSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string param = System.Convert.ToString(parameter);
                string configKey = System.Convert.ToString(value ?? "");
                if(configKey == "")
                {
                    return param == "0";
                }
                string[] parts = configKey.Split('_');
                return param == parts[0];
            }
            catch (Exception) { }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
