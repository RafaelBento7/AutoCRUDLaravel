using System;
using System.Collections.Generic;

namespace AutoCRUDLaravel {
    internal class Table {

        public string Name { get; set; }
        public List<Column> Columns { get; set; }
    }
}
