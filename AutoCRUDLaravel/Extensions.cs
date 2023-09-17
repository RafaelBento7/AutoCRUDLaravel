using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCRUDLaravel {
    internal static class Extensions {
        internal static int ToInt(this string value) {
            if (int.TryParse(value, out int result))
                return result;
            return 0;
        }

        internal static short ToShort(this string value) {
            if (short.TryParse(value, out short result))
                return result;
            return 0;
        }
        internal static ushort ToUShort(this string value) {
            if (ushort.TryParse(value, out ushort result))
                return result;
            return 0;
        }

        internal static double ToDouble(this string value) {
            if (double.TryParse(value, out double result))
                return result;
            return 0;
        }

        internal static bool ToBoolean(this string value) {
            if (value == null)
                return false;

            if (value.ToUpper() == "TRUE" || value == "1")
                return true;

            return false;
        }

        internal static ColumnType ToColumnType(this string value) {
            if (string.IsNullOrEmpty(value))
                return ColumnType.NONE;

            switch (value.ToUpper()) {
                case "VARCHAR":
                    return ColumnType.VARCHAR;
                case "FLOAT":
                    return ColumnType.FLOAT;
                case "DOUBLE":
                    return ColumnType.DOUBLE;
                case "SELECT":
                    return ColumnType.SELECT_OPTION;
                case "DATETIME":
                    return ColumnType.DATETIME;
                case "DATE":
                    return ColumnType.DATE;
                case "TIME":
                    return ColumnType.TIME;
                case "INT":
                    return ColumnType.INT;
                case "TINYINT":
                    return ColumnType.BOOLEAN;
                default:
                    return ColumnType.NONE;
            }
        }

        internal static bool IsNumeric(this string value) {
            int len = value.Length;
            for (int i = 0; i < len; ++i) {
                char c = value[i];
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}