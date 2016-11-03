using System;
using Windows.UI.Xaml.Data;

namespace HomeBudgetViewer.Conventers
{
    public class NegativeBooleanConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                return !(bool) value;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
