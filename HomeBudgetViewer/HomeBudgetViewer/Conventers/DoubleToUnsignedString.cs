using System;
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