using AutoCRUDLaravel.Enums;
using AutoCRUDLaravel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoCRUDLaravel.Utils {
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

        internal static ColumnTypeEnum ToColumnType(this string value) {
            if (string.IsNullOrEmpty(value))
                return ColumnTypeEnum.NONE;

            switch (value.ToUpper()) {
                case "VARCHAR":
                case "CHAR":
                    return ColumnTypeEnum.TEXT;
                case "FLOAT":
                case "DOUBLE":
                    return ColumnTypeEnum.NUMERIC_INT;
                case "SELECT":
                    return ColumnTypeEnum.SELECT_ARRAY;
                case "ENUM":
                    return ColumnTypeEnum.SELECT_ENUM;
                case "DATETIME":
                    return ColumnTypeEnum.DATETIME;
                case "DATE":
                    return ColumnTypeEnum.DATE;
                case "TIME":
                    return ColumnTypeEnum.TIME;
                case "INT":
                    return ColumnTypeEnum.NUMERIC_INT;
                case "TINYINT":
                    return ColumnTypeEnum.BOOLEAN;
                default:
                    return ColumnTypeEnum.NONE;
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

        internal static string GetEnumDescription(this Enum value) {
            if (value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any()) {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}