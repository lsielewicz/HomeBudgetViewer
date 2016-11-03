using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;

namespace HomeBudgetViewer.Conventers
{
    public class ItemTypeToForegroundConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is ItemType)
            {
                var itemType = (ItemType)value;
                if (parameter != null)
                {
                    var paramType = (ItemType)parameter;
                    return paramType == itemType ?
                        new SolidColorBrush(Colors.White) :
                        new SolidColorBrush(Colors.Black);
                }
            }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
