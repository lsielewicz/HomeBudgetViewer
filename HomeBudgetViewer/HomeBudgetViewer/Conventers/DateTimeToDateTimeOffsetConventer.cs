using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace HomeBudgetViewer.Conventers
{
    public class DateTimeToDateTimeOffsetConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is DateTime)
            {
                DateTime date = (DateTime) value;
                if(date.Year < 1900)
                    return new DateTimeOffset(DateTime.Now);
                return new DateTimeOffset(date);
            }
                
            return DateTimeOffset.MinValue;     
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is DateTimeOffset)
            {
                    DateTimeOffset dto = (DateTimeOffset) value;
                    return dto.DateTime;
            }
                
            return DateTime.Now;
        }
    }
}
