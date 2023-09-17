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

        public static bool GenerateIndex { get; set; }
        public static bool GenerateShow { get; set; }
        public static bool GenerateEdit { get; set; }
        public static bool ShowDeleteButtonIndex { get; set; }
        public static bool ShowEditButtonIndex { get; set; }
        public static bool ShowShowButtonIndex { get; set; }
        public static bool IsVisibleIndex { get; set; }
        public static bool IsVisibleEdit { get; set; }
        public static bool IsVisibleCreate { get; set; }
        public static bool IsVisibleShow { get; set; }


        private static void CreateConfigFile() {
            try {
                string pathName = $@"{Environment.CurrentDirectory}\config.ini";
                FileStream fs = File.Create(pathName);
                fs.Close();
            } catch (Exception ex) {
                MessageBox.Show($"Error creating save file.\nError:{ex.Message}");
            }
        }

        public static void Save(string server, string port, string username, string database,
                                bool generateIndex, bool generateShow, bool generateEdit,
                                bool showDeleteButton, bool showEditButton, bool showShowButton,
                                bool visibleIndex, bool visibleEdit, bool visibleCreate, bool visibleShow) {
            if (!File.Exists(Environment.CurrentDirectory + @"\config.ini"))
                CreateConfigFile();
            try {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile("config.ini");
                data["Database"]["Server"] = server;
                data["Database"]["Port"] = port;
                data["Database"]["Username"] = username;
                data["Database"]["Database"] = database;
                data["General"]["GenerateIndex"] = generateIndex.ToString();
                data["General"]["GenerateShow"] = generateShow.ToString();
                data["General"]["GenerateEdit"] = generateEdit.ToString();
                data["General"]["ShowDeleteButton"] = showDeleteButton.ToString();
                data["General"]["ShowEditButton"] = showEditButton.ToString();
                data["General"]["ShowShowButton"] = showShowButton.ToString();
                data["General"]["VisibleIndex"] = visibleIndex.ToString();
                data["General"]["VisibleEdit"] = visibleEdit.ToString();
                data["General"]["VisibleCreate"] = visibleCreate.ToString();
                data["General"]["VisibleShow"] = visibleShow.ToString();
                parser.WriteFile("config.ini", data);
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
                GenerateIndex = data["General"]["GenerateIndex"].ToBoolean();
                GenerateShow = data["General"]["GenerateShow"].ToBoolean();
                GenerateEdit = data["General"]["GenerateEdit"].ToBoolean();
                ShowDeleteButtonIndex = data["General"]["ShowDeleteButton"].ToBoolean();
                ShowEditButtonIndex = data["General"]["ShowEditButton"].ToBoolean();
                ShowShowButtonIndex = data["General"]["ShowShowButton"].ToBoolean();
                IsVisibleIndex = data["General"]["VisibleIndex"].ToBoolean();
                IsVisibleEdit = data["General"]["VisibleEdit"].ToBoolean();
                IsVisibleCreate = data["General"]["VisibleCreate"].ToBoolean();
                IsVisibleShow = data["General"]["VisibleShow"].ToBoolean();
            } catch (Exception ex) {
                MessageBox.Show($"Error loading settings\nError:{ex.Message}");
            }
        }
    }
}
