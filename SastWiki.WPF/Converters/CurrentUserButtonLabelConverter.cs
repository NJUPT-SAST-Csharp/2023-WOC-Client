using System.Globalization;
using System.Windows.Data;
using SastWiki.Core.Models.Dto;

namespace SastWiki.WPF.Converters;

public class CurrentUserButtonLabelConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is UserDto user
            ? (object)(
                (user.Email is null or "") // 因为后端并不会返回用户名，这里就拿邮箱替代了
                    ? "Not Logged in"
                    : user.Email
            )
            : throw new NotImplementedException();

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    ) => throw new NotImplementedException();
}
