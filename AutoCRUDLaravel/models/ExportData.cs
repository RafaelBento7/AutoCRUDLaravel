using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace AutoCRUDLaravel.Models {
    public class ExportData {
        // Main Files
        public string Index { get; set; }
        public string Create { get; set; }
        public string View { get; set; }
        public string Edit { get; set; }
        public string JavaScript { get; set; }
        public string Model { get; set; }
        public string Controller { get; set; }

        // Form Columns Templates
        public string FormColumnCheckBox { get; set; }
        public string FormColumnDate { get; set; }
        public string FormColumnDateTime { get; set; }
        public string FormColumnTime { get; set; }
        public string FormColumnNumericDouble { get; set; }
        public string FormColumnNumericInt { get; set; }
        public string FormColumnSelect { get; set; }
        public string FormColumnText { get; set; }

        // View Columns Templates
        public string ViewColumnCheckBox { get; set; }
        public string ViewColumnDate { get; set; }
        public string ViewColumnDateTime { get; set; }
        public string ViewColumnTime { get; set; }
        public string ViewColumnNumericDouble { get; set; }
        public string ViewColumnNumericInt { get; set; }
        public string ViewColumnSelect { get; set; }
        public string ViewColumnText { get; set; }

        public void GenerateIndex(List<Column> columns, ObservableCollection<ReplaceVariable> variables) {
            Index = ReplaceText(Index, variables);
        }

        public void GenerateView(List<Column> columns, ObservableCollection<ReplaceVariable> variables) {
            View = ReplaceText(View, variables);
        }

        public void GenerateCreate(List<Column> columns, ObservableCollection<ReplaceVariable> variables) {
            Create = ReplaceText(Create, variables);
        }

        public void GenerateEdit(List<Column> columns, ObservableCollection<ReplaceVariable> variables) {
            Edit = ReplaceText(Edit, variables);
        }

        public void GenerateController(List<Column> columns, ObservableCollection<ReplaceVariable> variables) {
            Controller = ReplaceText(Controller, variables);
        }

        public void GenerateModel(List<Column> columns, ObservableCollection<ReplaceVariable> variables) {
            Model = ReplaceText(Model, variables);
        }

        public void GenerateJavaScript(List<Column> columns, ObservableCollection<ReplaceVariable> variables) {
            JavaScript = ReplaceText(JavaScript, variables);
        }

        private string ReplaceText(string text, ObservableCollection<ReplaceVariable> variables) {
            if (string.IsNullOrEmpty(text))
                return "";

            foreach (ReplaceVariable variable in variables) {
                if (string.IsNullOrEmpty(variable.Variable))
                    continue;

                text.Replace(variable.Variable, variable.Equivalence);
            }
            return text;
        }

        public void ReadColumnTemplates(string main_path_form, string main_path_view) {
            try {
                FormColumnCheckBox = File.ReadAllText($@"{main_path_form}/Checkbox.txt");
                FormColumnDate = File.ReadAllText($@"{main_path_form}/Date.txt");
                FormColumnDateTime = File.ReadAllText($@"{main_path_form}/Datetime.txt");
                FormColumnTime = File.ReadAllText($@"{main_path_form}/Time.txt");
                FormColumnNumericDouble = File.ReadAllText($@"{main_path_form}/NumericDouble.txt");
                FormColumnNumericInt = File.ReadAllText($@"{main_path_form}/NumericInt.txt");
                FormColumnSelect = File.ReadAllText($@"{main_path_form}/Select.txt");
                FormColumnText = File.ReadAllText($@"{main_path_form}/Text.txt");

                ViewColumnCheckBox = File.ReadAllText($@"{main_path_view}/Checkbox.txt");
                ViewColumnDate = File.ReadAllText($@"{main_path_view}/Date.txt");
                ViewColumnDateTime = File.ReadAllText($@"{main_path_view}/Datetime.txt");
                ViewColumnTime = File.ReadAllText($@"{main_path_view}/Time.txt");
                ViewColumnNumericDouble = File.ReadAllText($@"{main_path_view}/NumericDouble.txt");
                ViewColumnNumericInt = File.ReadAllText($@"{main_path_view}/NumericInt.txt");
                ViewColumnSelect = File.ReadAllText($@"{main_path_view}/Select.txt");
                ViewColumnText = File.ReadAllText($@"{main_path_view}/Text.txt");
            } catch (Exception ex) {
                MessageBox.Show("Error while reading the column templates.\r\nError:" + ex.Message);
            }
        }

        public void ReadTemplates(string main_path) {
            try {
                Controller = File.ReadAllText($@"{main_path}/Controller.txt");
                Model = File.ReadAllText($@"{main_path}/Model.txt");
                Create = File.ReadAllText($@"{main_path}/Create.txt");
                Index = File.ReadAllText($@"{main_path}/Index.txt");
                View = File.ReadAllText($@"{main_path}/View.txt");
                Edit = File.ReadAllText($@"{main_path}/Edit.txt");
            } catch (Exception ex) {
                MessageBox.Show("Error while reading the column templates.\r\nError:" + ex.Message);
            }
        }
    }
}