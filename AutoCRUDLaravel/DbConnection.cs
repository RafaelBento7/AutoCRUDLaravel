using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCRUDLaravel {
    internal class DbConnection {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }

        public void SetConnection(string server, int port, string username, string password) {

        }

        public List<Column>GetColumns(string tableName) {
            return new List<Column>();
        }
    }
}
