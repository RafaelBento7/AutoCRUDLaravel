using AutoCRUDLaravel.Models;
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
                return ColumnType.GetColumnType(ColumnType.TypeEnum.NONE);

            switch (value.ToUpper()) {
                case "VARCHAR":
                    return ColumnType.GetColumnType(ColumnType.TypeEnum.TEXT);
                case "FLOAT":
                case "DOUBLE":
                    return ColumnType.GetColumnType(ColumnType.TypeEnum.NUMERIC_INT);
                case "SELECT":
                    return ColumnType.GetColumnType(ColumnType.TypeEnum.SELECT_ARRAY);
                case "ENUM":
                    return ColumnType.GetColumnType(ColumnType.TypeEnum.SELECT_ENUM);
                case "DATETIME":
                    return ColumnType.GetColumnType(ColumnType.TypeEnum.DATETIME);
                case "DATE":
                    return ColumnType.GetColumnType(ColumnType.TypeEnum.DATE);
                case "TIME":
                    return ColumnType.GetColumnType(ColumnType.TypeEnum.TIME);
                case "INT":
                    return ColumnType.GetColumnType(ColumnType.TypeEnum.NUMERIC_INT);
                case "TINYINT":
                    return ColumnType.GetColumnType(ColumnType.TypeEnum.BOOLEAN);
                default:
                    return ColumnType.GetColumnType(ColumnType.TypeEnum.NONE);
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