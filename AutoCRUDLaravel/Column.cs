using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCRUDLaravel {
    internal class Column {
        public string Name { get; set; }
        public ColumnType Type { get; set; }
        public string PlaceHolder { get; set; }
        public bool IsNullable { get; set; }
        public bool IsVisibleIndex { get; set; }
        public bool IsVisibleEdit { get; set; }
        public bool IsVisibleCreate { get; set; }
        public bool Unsigned { get; set; }
        public string DefaultValue { get; set; }
        public ushort Position { get; set; }
        public ushort? MaxSize { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }

        public Column() { }
    }
}
