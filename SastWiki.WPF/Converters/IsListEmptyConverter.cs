using System.Globalization;
using System.Windows.Data;

namespace SastWiki.WPF.Converters;

internal class IsListEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is IEnumerable<object> list
            ? (object)list.Any()
            : throw new NotImplementedException();

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    ) => throw new NotImplementedException();
}
