using IniParser.Model;
using IniParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace AutoCRUDLaravel {
    internal static class Settings {
        public static string Server { get; set; }
        public static string Port { get; set; }
        public static string Username { get; set; }
        public static string Database { get; set; }


        private static void CreateConfigFile() {
            try {
                string pathName = $@"{Environment.CurrentDirectory}\config.ini";
                FileStream fs = File.Create(pathName);
                fs.Close();
            } catch (Exception ex) {
                MessageBox.Show($"Error creating save file.\nError:{ex.Message}");
            }
        }

        public static void Save(string server, string port, string username, string database) {
            if (!File.Exists(Environment.CurrentDirectory + @"\config.ini"))
                CreateConfigFile();
            try {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile("config.ini");
                data["Database"]["Server"] = server;
                data["Database"]["Port"] = port;
                data["Database"]["Username"] = username;
                data["Database"]["Database"] = database;
                parser.WriteFile("config.ini", data);
                Load();
                MessageBox.Show($"Settings Saved!");
            } catch (Exception ex) {
                MessageBox.Show($"Error Saving.\nError:{ex.Message}");
            }
        }

        public static void Load() {
            if (!File.Exists(Environment.CurrentDirectory + @"\config.ini")) {
                CreateConfigFile();
                return;
            }

            try {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile("config.ini");
                Server = data["Database"]["Server"];
                Port = data["Database"]["Port"];
                Username = data["Database"]["Username"];
                Database = data["Database"]["Database"];
            } catch (Exception ex) {
                MessageBox.Show($"Error loading settings\nError:{ex.Message}");
            }
        }
    }
}
