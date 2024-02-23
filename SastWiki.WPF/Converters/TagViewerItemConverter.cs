using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using SastWiki.Core.Models.Dto;
using SastWiki.WPF.UserControl;

namespace SastWiki.WPF.Converters
{
    class TagViewerItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<string> tagNames)
            {
                return tagNames.Select(x => new TagViewerTagItem() { Name = x });
            }
            else
                throw new NotImplementedException();
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            throw new NotImplementedException();
        }
    }
}
