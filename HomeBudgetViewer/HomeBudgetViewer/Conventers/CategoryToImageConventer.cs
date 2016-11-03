using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;

namespace HomeBudgetViewer.Conventers
{
    public class CategoryToImageConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is Category)
            {
                return Application.Current.Resources[$"{(Category) value}"] as BitmapImage;             
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
