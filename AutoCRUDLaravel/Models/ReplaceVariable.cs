using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace AutoCRUDLaravel.Models {
    public class ReplaceVariable {
        public string Variable { get; set; }
        public string Equivalence { get; set; }
        [JsonIgnore]
        public bool IsDefault { get; set; }

        public ReplaceVariable(string file, string equivalence, bool isDefault = false) {
            this.Variable = file;
            this.Equivalence = equivalence;
            this.IsDefault = isDefault;
        }

        public static ObservableCollection<ReplaceVariable> GetVariables() {
            try {
                return new ObservableCollection<ReplaceVariable>(JsonConvert.DeserializeObject<List<ReplaceVariable>>(File.ReadAllText($@"{Environment.CurrentDirectory}\Templates\ReplaceVariables.json")));
            } catch (Exception ex) {
                MessageBox.Show("Error while deserializing variables from json.\r\nError: " + ex.Message);
                return null;
            }
        }

        public static void SaveVariables(ObservableCollection<ReplaceVariable> list) {
            try {
                List<ReplaceVariable> filteredList = list.Where(item => !item.IsDefault).ToList();
                string json = JsonConvert.SerializeObject(filteredList, Formatting.Indented);
                File.WriteAllText($@"{Environment.CurrentDirectory}\Templates\ReplaceVariables.json", json);
            } catch (Exception ex) {
                MessageBox.Show("Error while deserializing variables from json.\r\nError: " + ex.Message);
            }
        }
    }
}
