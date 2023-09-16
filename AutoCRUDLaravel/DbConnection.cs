using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace AutoCRUDLaravel {
    internal class DbConnection {
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public MySqlConnection Connection { get; set; }

        private static DbConnection _instance = null;

        public static DbConnection Instance() {
            if (_instance == null)
                _instance = new DbConnection();
            return _instance;
        }

        public List<Column>GetColumns(string tableName) {
            string query = $"SELECT COLUMN_NAME, ORDINAL_POSITION, COLUMN_DEFAULT, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, COLUMN_KEY " +
                           $"FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{DatabaseName}' AND TABLE_NAME = '{tableName}' ORDER BY ORDINAL_POSITION";
            List<Column> columns = new List<Column>();
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
                        } else if (columnKey.ToUpper() == "MUL" || (!string.IsNullOrEmpty(column.Name) && column.Name.ToUpper().EndsWith("_ID"))){
                            column.IsPrimaryKey = false;
                            column.IsForeignKey = true;
                        }

                        string columnName = reader["COLUMN_NAME"].ToString();
                        columns.Add(column);
                        Console.WriteLine($"Column Name: {columnName}");
                    }
                }
            }
            return columns;
        }

        public bool Connect(string password) {
            try {
                if (Connection == null) {
                    if (string.IsNullOrEmpty(DatabaseName))
                        return false;
                    string connstring = string.Format("Server={0}; port={1}; database={2}; UID={3}; password={4}", Server, Port, DatabaseName, Username, password);
                    Connection = new MySqlConnection(connstring);
                    Connection.Open();
                }
            } catch (Exception ex) {

                return false;
            }
            
            return true;
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
