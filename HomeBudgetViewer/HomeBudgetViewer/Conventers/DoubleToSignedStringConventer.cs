using System;
using System.Collections.Generic;
using System.Globalization;
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
                NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                nfi.NumberGroupSeparator = " ";
                if (dValue > 0)
                    return $"+{dValue.ToString("#,0.00", nfi)}";
                else if (dValue < 0)
                    return $"-{dValue.ToString("#,0.00", nfi)}";
                else
                    return $"{dValue.ToString("#,0.00", nfi)}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
