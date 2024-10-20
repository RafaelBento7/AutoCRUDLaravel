using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCRUDLaravel.Models {
    public class Connection {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Table { get; set; }
    }
}
