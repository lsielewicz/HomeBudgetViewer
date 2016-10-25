using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using HomeBudgetViewer.Models.Enum;

namespace HomeBudgetViewer.Conventers
{
    public class IntToVisibilityConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is int)
            {
                if (parameter != null)
                {
                    var eParameter = (ConventerDirection) parameter;
                    if (eParameter == ConventerDirection.Inverse)
                        return (int) value != 0 ? Visibility.Collapsed : Visibility.Visible;
                }
                return (int)value != 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
