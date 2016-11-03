using System;
using Windows.UI.Xaml.Data;

namespace HomeBudgetViewer.Conventers
{
     public class IntToLenghtConventer : IValueConverter
    {
         public object Convert(object value, Type targetType, object parameter, string language)
         {
             if (value != null && value is int)
             {
                 double lenght = (int) value*35;
                 if (lenght <= 250)
                     return double.NaN;
                 return lenght;
             }
             return 0.0;
         }

         public object ConvertBack(object value, Type targetType, object parameter, string language)
         {
             throw new NotImplementedException();
         }
    }
}
