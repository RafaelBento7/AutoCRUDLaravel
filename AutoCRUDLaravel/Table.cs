using System;
using System.Collections.Generic;

namespace AutoCRUDLaravel {
    internal class Table {

        public string Name { get; set; }
        public DateTime LastTimeExecuted { get; set; }

        public List<Column> Columns { get; set; }
    }
}
