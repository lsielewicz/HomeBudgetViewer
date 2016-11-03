using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using HomeBudgetViewer.Models.Enum;

namespace HomeBudgetViewer.Conventers
{
    public class CoutOfCollectionItemsToVisibilityConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is int)
            {
                var intValue = (int)value;
                if (parameter != null && (ConventerDirection)parameter == ConventerDirection.Inverse)
                {
                    if (intValue == 0)
                        return Visibility.Visible;
                    return Visibility.Collapsed;
                }
                if (intValue == 0)
                    return Visibility.Collapsed;
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
