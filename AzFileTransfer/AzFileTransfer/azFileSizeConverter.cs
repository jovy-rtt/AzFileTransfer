using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AzFileTransfer
{
    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long fileSizeInBytes)
            {
                return FormatFileSize(fileSizeInBytes);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string FormatFileSize(long fileSizeInBytes)
        {
            const int scale = 1024;
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            int unitIndex = 0;

            double fileSize = fileSizeInBytes;
            while (fileSize >= scale && unitIndex < units.Length - 1)
            {
                fileSize /= scale;
                unitIndex++;
            }

            return $"{fileSize:0.#} {units[unitIndex]}";
        }
    }
}
