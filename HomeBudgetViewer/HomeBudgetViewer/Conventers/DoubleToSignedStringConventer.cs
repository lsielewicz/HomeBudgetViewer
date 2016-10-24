using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace HomeBudgetViewer.Conventers
{
    public class DoubleToSignedStringConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var dValue = (double)value;
                if (dValue > 0)
                    return $"+{dValue.ToString("F")}";
                else if (dValue < 0)
                    return $"-{dValue.ToString("F")}";
                else
                    return $"{dValue.ToString("F")}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
