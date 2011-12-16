using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhys2_5_WPF
{
    class BitRuleConverter : System.Windows.Data.IValueConverter
    {
        // byte to bit
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte v;
            if (!byte.TryParse(value.ToString(), out v))
                return System.Windows.DependencyProperty.UnsetValue;
            return System.Convert.ToString(v, 2).PadLeft(8, '0');
        }

        // bit to byte
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return System.Windows.DependencyProperty.UnsetValue;
            string s = value.ToString();
            if (string.IsNullOrWhiteSpace(s))
                return System.Windows.DependencyProperty.UnsetValue;
            if (s.Length > 8)
                s = s.Remove(8);
            try
            {
                return System.Convert.ToInt32(s, 2).ToString();
            }
            catch (FormatException)
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }
        }
    }
}
