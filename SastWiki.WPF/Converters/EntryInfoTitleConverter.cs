using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SastWiki.Core.Models.Dto;

namespace SastWiki.WPF.Converters
{
    public class EntryInfoTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EntryDto entry)
            {
                var sb = new StringBuilder();
                if (!string.IsNullOrEmpty(entry.CategoryName))
                {
                    sb.Append($"[{entry.CategoryName}] - ");
                }
                sb.Append(entry.Title ?? "Untitled");
                if (entry.TagNames.Count > 0)
                {
                    sb.Append($" - Tags:");
                    foreach (var tag in entry.TagNames)
                    {
                        sb.Append($" {tag}");
                    }
                }
                return sb.ToString();
            }
            return "Untitled";
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
