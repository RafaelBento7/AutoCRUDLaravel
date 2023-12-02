using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace AutoCRUDLaravel.models {
    public class GeneratorVariables {
        public string FileVariable { get; set; }
        public string EquivalenceVariable { get; set; }
        [JsonIgnore]
        public bool IsDefault { get; set; }

        public GeneratorVariables(string file, string equivalence, bool isDefault = false) {
            this.FileVariable = file;
            this.EquivalenceVariable = equivalence;
            this.IsDefault = isDefault;
        }

        public static ObservableCollection<GeneratorVariables> GetVariables() {
            try {
                return new ObservableCollection<GeneratorVariables>(JsonConvert.DeserializeObject<List<GeneratorVariables>>(File.ReadAllText($@"{Environment.CurrentDirectory}\templates\FileVariables.json")));
            } catch (Exception ex) {
                MessageBox.Show("Error while deserializing variables from json.\r\nError: " + ex.Message);
                return null;
            }
        }

        public static void SaveVariables(ObservableCollection<GeneratorVariables> list) {
            try {
                List<GeneratorVariables> filteredList = list.Where(item => !item.IsDefault).ToList();
                string json = JsonConvert.SerializeObject(filteredList, Formatting.Indented);
                File.WriteAllText($@"{Environment.CurrentDirectory}\templates\FileVariables.json", json);
            } catch (Exception ex) {
                MessageBox.Show("Error while deserializing variables from json.\r\nError: " + ex.Message);
            }
        }
    }
}
