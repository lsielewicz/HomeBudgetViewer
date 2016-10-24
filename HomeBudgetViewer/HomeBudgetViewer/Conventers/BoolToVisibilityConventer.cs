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
    public class BoolToVisibilityConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is bool)
            {
                var bValue = (bool) value;
                if (parameter != null)
                {
                    var eParameter = (ConventerDirection) parameter;
                    if (eParameter == ConventerDirection.Inverse)
                    {
                        return bValue ? Visibility.Collapsed : Visibility.Visible;
                    }
                }
                return bValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
