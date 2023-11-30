using System;
using System.Collections.Generic;

namespace AutoCRUDLaravel.Models {
    internal class GeneratorVariables {
        public string FileVariable { get; set; }
        public string EquivalenceVariable { get; set; }

        public GeneratorVariables(string file, string equivalence) {
            this.FileVariable = file;
            this.EquivalenceVariable = equivalence;
        }

        public static List<GeneratorVariables> GetVariables() {
            string path = $@"{Environment.CurrentDirectory}\FileVariables.json";
            //TODO Deserialize
            return null;
        }

        public static void SaveVariables(List<GeneratorVariables> list) {
            //TODO Serialize and Save
        }
    }
}
