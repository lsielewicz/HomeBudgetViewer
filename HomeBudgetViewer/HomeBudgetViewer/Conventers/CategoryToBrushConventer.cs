using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;

namespace HomeBudgetViewer.Conventers
{
    public class CategoryToBrushConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is Category)
            {
                return Application.Current.Resources[$"{(Category)value}Brush"] as LinearGradientBrush;
            }
            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
