using IniParser.Model;
using IniParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using AutoCRUDLaravel.Models;

namespace AutoCRUDLaravel.Utils {
    internal static class IniHelper {
        private static void CreateConfigFile() {
            try {
                string pathName = $@"{Environment.CurrentDirectory}\config.ini";
                FileStream fs = File.Create(pathName);
                fs.Close();
            } catch (Exception ex) {
                MessageBox.Show($"Error creating save file.\nError:{ex.Message}");
            }
        }

        public static void SaveSettings(string server, string port, string username, string password, string database) {
            if (!File.Exists(Environment.CurrentDirectory + @"\config.ini"))
                CreateConfigFile();
            try {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile("config.ini");
                data["Database"]["Server"] = server;
                data["Database"]["Port"] = port;
                data["Database"]["Username"] = username;
                data["Database"]["Password"] = password;
                data["Database"]["Database"] = database;
                parser.WriteFile("config.ini", data);
                LoadSettings();
                MessageBox.Show($"Settings Saved!");
            } catch (Exception ex) {
                MessageBox.Show($"Error Saving.\nError:{ex.Message}");
            }
        }

        public static Connection LoadSettings() {
            if (!File.Exists(Environment.CurrentDirectory + @"\config.ini")) {
                CreateConfigFile();
                return new Connection();
            }

            try {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile("config.ini");
                return new Connection() {
                    Server = data["Database"]["Server"],
                    Port = data["Database"]["Port"].ToInt(),
                    Username = data["Database"]["Username"],
                    Password = data["Database"]["Password"],
                    Database = data["Database"]["Database"],
                };
            } catch (Exception ex) {
                MessageBox.Show($"Error loading settings\nError:{ex.Message}");
                return new Connection();
            }
        }
    }
}
