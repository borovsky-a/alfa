using System;
using System.Globalization;
using System.Windows.Data;
namespace prototype.UserEritor.Desktop.Converters
{
    public class PaginationSelectedItemInverseConverster : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
            {
                return false;
            }
            if(!int.TryParse(values[0]?.ToString(), out var pageIndex) || !int.TryParse(values[1]?.ToString(), out var refIndex))
            {
                return false;
            }
            return pageIndex != refIndex;
        }

      

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}