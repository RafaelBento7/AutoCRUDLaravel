using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCRUDLaravel {
    internal class Settings {
        public bool GenerateIndex { get; set; }
        public bool GenerateView { get; set; }
        public bool GenerateEdit { get; set; }
        public bool GenerateDelete { get; set; }
        public bool ShowDeleteButtonIndex { get; set; }
        public bool ShowEditButtonIndex { get; set; }
        public bool ShowShowButtonIndex { get; set; }
    }
}
