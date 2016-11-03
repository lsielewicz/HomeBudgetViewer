using System;
using Windows.UI.Xaml.Data;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;

namespace HomeBudgetViewer.Conventers
{
    public class ItemTypeToBooleanConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is ItemType)
            {
                var itemType = (ItemType) value;
                if (parameter != null)
                {
                    var paramType = (ItemType) parameter;
                    return paramType == itemType;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
