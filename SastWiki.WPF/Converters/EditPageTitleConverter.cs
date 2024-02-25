using System.Globalization;
using System.Windows.Data;

namespace SastWiki.WPF.Converters;

public class EditPageTitleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is int
            ? (int)value == 0
                ? "Add new entry"
                : (object)$"Edit entry ID :{value}"
            : throw new NotImplementedException();

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    ) => throw new NotImplementedException();
}
