using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace AutoCRUDLaravel {
    internal class DbConnection {

        public bool IsConnected {
            get {
                return Connection != null && (
                    Connection.State == System.Data.ConnectionState.Open ||
                    Connection.State == System.Data.ConnectionState.Fetching ||
                    Connection.State == System.Data.ConnectionState.Executing);
            }
        }
        public string Server { get; set; }
        public int Port { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public MySqlConnection Connection { get; set; }

        private static DbConnection _instance = null;

        public static DbConnection Instance() {
            if (_instance == null)
                _instance = new DbConnection();
            return _instance;
        }

        public (List<string> tables, string errorMessage) GetTables() {

            List<string> tables = new List<string>();
            string query = $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{DatabaseName}'; ";

            try {
                if (!IsConnected)
                    throw new Exception("Not connected");

                using (MySqlCommand command = new MySqlCommand(query, Connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            tables.Add(reader["TABLE_NAME"].ToString());
                        }
                    }
                }
                return (tables, null);
            } catch (Exception ex) {
                return (null, ex.Message);
            }

        }

        public (List<Column> columns, string errorMessage) GetColumns(string tableName) {
            string query = $"SELECT COLUMN_NAME, ORDINAL_POSITION, COLUMN_DEFAULT, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, COLUMN_KEY " +
                           $"FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{DatabaseName}' AND TABLE_NAME = '{tableName}' ORDER BY ORDINAL_POSITION";
            List<Column> columns = new List<Column>();
            try {
                if (!IsConnected)
                    throw new Exception("Not connected");

                using (MySqlCommand command = new MySqlCommand(query, Connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            Column column = new Column() {
                                Name = reader["COLUMN_NAME"].ToString(),
                                Position = reader["ORDINAL_POSITION"].ToString().ToUShort(),
                                DefaultValue = reader["COLUMN_DEFAULT"].ToString(),
                                IsNullable = reader["IS_NULLABLE"].ToString().ToBoolean(),
                                Type = reader["DATA_TYPE"].ToString().ToColumnType(),
                                MaxSize = reader["CHARACTER_MAXIMUM_LENGTH"].ToString().ToUShort(),
                                PlaceHolder = reader["COLUMN_NAME"].ToString(),
                            };

                            string columnKey = reader["COLUMN_KEY"].ToString();
                            if (string.IsNullOrEmpty(columnKey)) {
                                column.IsPrimaryKey = false;
                                column.IsForeignKey = false;
                            } else if (columnKey.ToUpper() == "PRI") {
                                column.IsForeignKey = false;
                                column.IsPrimaryKey = true;
                                column.IsVisibleEdit = false;
                                column.IsVisibleCreate = false;
                            } else if (columnKey.ToUpper() == "MUL" || (!string.IsNullOrEmpty(column.Name) && column.Name.ToUpper().EndsWith("_ID"))) {
                                column.IsPrimaryKey = false;
                                column.IsForeignKey = true;
                                column.Type = ColumnType.SELECT_OPTION;
                            }

                            string columnName = reader["COLUMN_NAME"].ToString();
                            columns.Add(column);
                        }
                    }
                }
                return (columns, null);
            } catch (Exception ex) {
                return (null, ex.Message);
            }
        }

        public (bool isValid, string errorMessage) Validate(string server, string port, string username, string database) {
            if (IsConnected && Server == server && Port == port.ToInt() && Username == username) {
                return (true, null);
            }

            if (string.IsNullOrEmpty(server)) {
                return (false, "The server can not be empty");
            }

            if (string.IsNullOrEmpty(port)) {
                return (false, "The port can not be empty");
            }

            if (!port.IsNumeric()) {
                return (false, "The port can only contain numbers");
            }

            if (string.IsNullOrEmpty(username)) {
                return (false, "The username can not be empty");
            }

            if (string.IsNullOrEmpty(database)) {
                return (false, "The database can not be empty");
            }

            return (true, null);
        }

        public (bool isConnected, string errorMessage) Connect(string password) {
            Connection = null;
            try {
                string connstring = string.Format("Server={0}; port={1}; database={2}; UID={3}; password={4}", Server, Port, DatabaseName, Username, password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            } catch (Exception ex) {
                Connection = null;
                return (false, ex.Message);
            }

            return (true, null);
        }

        public void Close() {
            if (Connection.State == System.Data.ConnectionState.Broken ||
                Connection.State == System.Data.ConnectionState.Closed ||
                Connection.State == System.Data.ConnectionState.Fetching) {
                Connection.Close();
            }
        }
    }
}
