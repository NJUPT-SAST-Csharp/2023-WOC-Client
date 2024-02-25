using System.Globalization;
using System.Windows.Data;
using SastWiki.WPF.UserControl;

namespace SastWiki.WPF.Converters;

internal class TagViewerItemConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is List<string> tagNames
            ? (object)tagNames.Select(x => new TagViewerTagItem() { Name = x })
            : throw new NotImplementedException();

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    ) => throw new NotImplementedException();
}
