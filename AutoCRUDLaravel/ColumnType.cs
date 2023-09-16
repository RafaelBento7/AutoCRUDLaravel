using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCRUDLaravel {
    internal enum ColumnType {
        NONE,
        DOUBLE,
        FLOAT,
        INT,
        VARCHAR,
        DATE,
        DATETIME,
        TIME,
        SELECT_OPTION,
        BOOLEAN
    }
}
