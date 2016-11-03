using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using HomeBudgetViewer.Models.Enum;

namespace HomeBudgetViewer.Conventers
{
    public class DateFilterToVisibilityConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var eValue = (DateFilter) value;
                if (eValue == DateFilter.ByDay || eValue == DateFilter.ByMonth)
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
