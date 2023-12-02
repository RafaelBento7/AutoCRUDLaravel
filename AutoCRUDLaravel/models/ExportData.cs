using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace AutoCRUDLaravel.models {
    public class ExportData {
        // Main Files
        public string Index { get; set; }
        public string Create { get; set; }
        public string Show { get; set; }
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

        // Show Columns Templates
        public string ShowColumnCheckBox { get; set; }
        public string ShowColumnDate { get; set; }
        public string ShowColumnDateTime { get; set; }
        public string ShowColumnTime { get; set; }
        public string ShowColumnNumericDouble { get; set; }
        public string ShowColumnNumericInt { get; set; }
        public string ShowColumnSelect { get; set; }
        public string ShowColumnText { get; set; }

        public void GenerateIndex(List<Column> columns) {

        }

        public void GenerateShow(List<Column> columns) {

        }

        public void GenerateCreate(List<Column> columns) {

        }

        public void GenerateEdit(List<Column> columns) {

        }

        public void GenerateController(List<Column> columns) {

        }

        public void GenerateModel(List<Column> columns) {

        }

        public void GenerateJavaScript(List<Column> columns) {

        }

        public void ReadColumnTemplates(string main_path_form, string main_path_show) {
            try {
                FormColumnCheckBox = File.ReadAllText($@"{main_path_form}/checkbox.txt");
                FormColumnDate = File.ReadAllText($@"{main_path_form}/date.txt");
                FormColumnDateTime = File.ReadAllText($@"{main_path_form}/datetime.txt");
                FormColumnTime = File.ReadAllText($@"{main_path_form}/time.txt");
                FormColumnNumericDouble = File.ReadAllText($@"{main_path_form}/numeric_double.txt");
                FormColumnNumericInt = File.ReadAllText($@"{main_path_form}/numeric_int.txt");
                FormColumnSelect = File.ReadAllText($@"{main_path_form}/select.txt");
                FormColumnText = File.ReadAllText($@"{main_path_form}/text.txt");

                ShowColumnCheckBox = File.ReadAllText($@"{main_path_show}/checkbox.txt");
                ShowColumnDate = File.ReadAllText($@"{main_path_show}/date.txt");
                ShowColumnDateTime = File.ReadAllText($@"{main_path_show}/datetime.txt");
                ShowColumnTime = File.ReadAllText($@"{main_path_show}/time.txt");
                ShowColumnNumericDouble = File.ReadAllText($@"{main_path_show}/numeric_double.txt");
                ShowColumnNumericInt = File.ReadAllText($@"{main_path_show}/numeric_int.txt");
                ShowColumnSelect = File.ReadAllText($@"{main_path_show}/select.txt");
                ShowColumnText = File.ReadAllText($@"{main_path_show}/text.txt");
            } catch (Exception ex) {
                MessageBox.Show("Error while reading the column templates.\r\nError:"+ex.Message);
            }
        }

        public void ReadTemplates(string main_path) {
            Controller = File.ReadAllText($@"{main_path}/Controller.txt");
            Model = File.ReadAllText($@"{main_path}/Model.txt");
            Create = File.ReadAllText($@"{main_path}/Create.txt");
            Index = File.ReadAllText($@"{main_path}/Index.txt");
            Show = File.ReadAllText($@"{main_path}/Show.txt");
            Edit = File.ReadAllText($@"{main_path}/Edit.txt");
        }
    }
}