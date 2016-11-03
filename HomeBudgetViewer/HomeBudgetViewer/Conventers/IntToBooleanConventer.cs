using System;
using Windows.UI.Xaml.Data;

namespace HomeBudgetViewer.Conventers
{
    public class IntToBooleanConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is int)
            {
                return (int) value != 0;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
