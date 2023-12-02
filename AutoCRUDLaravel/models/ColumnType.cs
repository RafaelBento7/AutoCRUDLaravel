using System.Collections.Generic;

namespace AutoCRUDLaravel.models {
    public class ColumnType {
        public enum TypeEnum {
            NONE,
            NUMERIC_DOUBLE,
            NUMERIC_INT,
            TEXT,
            DATETIME,
            DATE,
            TIME,
            SELECT_ARRAY,
            SELECT_ENUM,
            BOOLEAN
        }

        public TypeEnum Type { get; set; }
        public string Description { get; set; }

        public ColumnType(TypeEnum type, string description) {
            this.Type = type;
            this.Description = description;
        }

        public override string ToString() {
            return this.Description;
        }

        public static List<ColumnType> GetAllColumnTypes() {
            return new List<ColumnType>() {
                new ColumnType(TypeEnum.NUMERIC_INT, "Numeric (Int)"),
                new ColumnType(TypeEnum.NUMERIC_DOUBLE, "Numeric (Double)"),
                new ColumnType(TypeEnum.TEXT, "Text"),
                new ColumnType(TypeEnum.DATETIME, "DateTime"),
                new ColumnType(TypeEnum.DATE, "Date"),
                new ColumnType(TypeEnum.TIME, "Time"),
                new ColumnType(TypeEnum.SELECT_ARRAY, "Select (from array)"),
                new ColumnType(TypeEnum.SELECT_ENUM, "Select (from enum)"),
                new ColumnType(TypeEnum.BOOLEAN, "CheckBox")
            };
        }

        public static ColumnType GetColumnType(TypeEnum type) {
            switch (type) {
                case TypeEnum.NUMERIC_DOUBLE:
                    return new ColumnType(type, "Numeric (Double)");
                case TypeEnum.NUMERIC_INT:
                    return new ColumnType(type, "Numeric (INT)");
                case TypeEnum.TEXT:
                    return new ColumnType(type, "Text");
                case TypeEnum.DATETIME:
                    return new ColumnType(type, "DateTime");
                case TypeEnum.DATE:
                    return new ColumnType(type, "Date");
                case TypeEnum.TIME:
                    return new ColumnType(type, "Time");
                case TypeEnum.SELECT_ARRAY:
                    return new ColumnType(type, "Select (from array)");
                case TypeEnum.SELECT_ENUM:
                    return new ColumnType(type, "Select (from enum)");
                case TypeEnum.BOOLEAN:
                    return new ColumnType(type, "CheckBox");
                case TypeEnum.NONE:
                default: 
                    return new ColumnType(type, "None");
            }
        }
    }
}
