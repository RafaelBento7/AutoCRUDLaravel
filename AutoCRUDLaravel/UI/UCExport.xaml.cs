using AutoCRUDLaravel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoCRUDLaravel {
    /// <summary>
    /// Interação lógica para UCExport.xam
    /// </summary>
    public partial class UCExport : UserControl {
        private enum View {
            MODEL,
            CONTROLLER,
            JAVASCRIPT,
            INDEX,
            CREATE,
            SHOW,
            EDIT
        }

        public ExportData Data { get; }

        private string templatesPath;
        private string columnsFormsTemplatePath;
        private string columnsShowTemplatePath;

        private View currentView;

        private ExportData data;

        public UCExport(List<Column> columns, ObservableCollection<GeneratorVariables> variables) {
            InitializeComponent();
            SetView(currentView);

            templatesPath = Path.Combine(Environment.CurrentDirectory, "templates");
            columnsFormsTemplatePath = Path.Combine(templatesPath, "columns_template_create_edit");
            columnsShowTemplatePath = Path.Combine(templatesPath, "columns_template_show");

            GenerateViews(columns);
        }

        private void GenerateViews(List<Column> columns) {
            try {
                ExportData data = new ExportData();
                data.ReadColumnTemplates(columnsFormsTemplatePath, columnsShowTemplatePath);
                data.ReadTemplates(templatesPath);
                data.GenerateIndex(columns);
                data.GenerateEdit(columns);
                data.GenerateCreate(columns);
                data.GenerateShow(columns);
                data.GenerateModel(columns);
                data.GenerateController(columns);
                data.GenerateJavaScript(columns);
            } catch (Exception ex) {
                MessageBox.Show("Error while generating the views.\r\nError:" + ex.Message);
            }
        }

        private void SetView(View currentView) {
            this.currentView = currentView;

            colorModel.Visibility = Visibility.Hidden;
            colorController.Visibility = Visibility.Hidden;
            colorJavaScript.Visibility = Visibility.Hidden;
            colorIndex.Visibility = Visibility.Hidden;
            colorCreate.Visibility = Visibility.Hidden;
            colorShow.Visibility = Visibility.Hidden;
            colorEdit.Visibility = Visibility.Hidden;

            switch (currentView) {
                case View.MODEL:
                    colorModel.Visibility = Visibility.Visible;
                    tbPreview.Text = data.Model;
                    break;
                case View.CONTROLLER:
                    colorController.Visibility = Visibility.Visible;
                    tbPreview.Text = data.Controller;
                    break;
                case View.JAVASCRIPT:
                    colorJavaScript.Visibility = Visibility.Visible;
                    tbPreview.Text = data.JavaScript;
                    break;
                case View.INDEX:
                    colorIndex.Visibility = Visibility.Visible;
                    tbPreview.Text = data.Index;
                    break;
                case View.CREATE:
                    colorCreate.Visibility = Visibility.Visible;
                    tbPreview.Text = data.Create;
                    break;
                case View.SHOW:
                    colorShow.Visibility = Visibility.Visible;
                    tbPreview.Text = data.Show;
                    break;
                case View.EDIT:
                    colorEdit.Visibility = Visibility.Visible;
                    tbPreview.Text = data.Edit;
                    break;
            }
        }

        #region Right-Tab StackPanel Clicks

        private void Model_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.MODEL);
        }

        private void Controller_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.CONTROLLER);
        }

        private void JavaScript_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.JAVASCRIPT);
        }

        private void Index_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.INDEX);
        }

        private void Create_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.CREATE);
        }

        private void Show_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.SHOW);
        }

        private void Edit_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.EDIT);
        }

        #endregion
    }
}
