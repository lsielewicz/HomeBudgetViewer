using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;

namespace HomeBudgetViewer.Conventers
{
    public class CategoryModelToBrushConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var eCategory = value.ToString();
                return Application.Current.Resources[$"{eCategory}Brush"] as LinearGradientBrush;
            }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
