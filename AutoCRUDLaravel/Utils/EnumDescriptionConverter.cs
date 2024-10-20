using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AutoCRUDLaravel.Utils {
    internal class EnumDescriptionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return string.Empty;

            return ((Enum)value).GetEnumDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return string.Empty;
        }
    }
}
