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
        public bool IsVisibleIndex { get; set; }
        public bool IsVisibleEdit { get; set; }
        public bool IsVisibleCreate { get; set; }
        /// <summary>
        /// If varchar sets the max length of the input
        /// </summary>
        public ushort MaxSize { get; set; }


        public Column() { }
    }
}
