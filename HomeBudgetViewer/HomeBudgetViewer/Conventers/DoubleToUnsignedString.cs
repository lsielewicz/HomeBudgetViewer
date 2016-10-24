using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace HomeBudgetViewer.Conventers
{
    public class DoubleToUnsignedString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var dValue = (double)value;
                NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                nfi.NumberGroupSeparator = " ";
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