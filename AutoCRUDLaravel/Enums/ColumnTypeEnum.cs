using System.ComponentModel;

namespace AutoCRUDLaravel.Enums {
    public enum ColumnTypeEnum {
        [Description("None")]
        NONE,
        [Description("Numeric (Double)")]
        NUMERIC_DOUBLE,
        [Description("Numeric (Int)")]
        NUMERIC_INT,
        [Description("Text")]
        TEXT,
        [Description("DateTime")]
        DATETIME,
        [Description("Date")]
        DATE,
        [Description("Time")]
        TIME,
        [Description("Select (from array)")]
        SELECT_ARRAY,
        [Description("Select (from enum)")]
        SELECT_ENUM,
        [Description("CheckBox")]
        BOOLEAN
    }
}
