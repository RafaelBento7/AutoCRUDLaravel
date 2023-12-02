namespace AutoCRUDLaravel.models {
    public class Column {
        public string Name { get; set; }
        public ColumnType Type { get; set; }
        public string FieldTitle { get; set; }
        public string PlaceHolder { get; set; }
        public bool IsNullable { get; set; }
        public bool IsVisibleIndex { get; set; }
        public bool IsVisibleEdit { get; set; }
        public bool IsVisibleCreate { get; set; }
        public bool IsVisibleShow { get; set; }
        public bool Unsigned { get; set; }
        public string DefaultValue { get; set; }
        public ushort Position { get; set; }
        public ushort? MaxLength { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string SelectArray { get; set; }
        public string SelectEnum { get; set; }
        public string DateFormat { get; set; }

        public Column() { }
    }
}
