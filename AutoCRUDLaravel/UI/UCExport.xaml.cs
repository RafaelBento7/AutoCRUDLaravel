using AutoCRUDLaravel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoCRUDLaravel.UI {
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
            View,
            EDIT
        }

        public ExportData Data { get; set; }

        private readonly string templatesPath;
        private readonly string columnsFormsTemplatePath;
        private readonly string columnsViewTemplatePath;

        private View currentView;

        public UCExport(List<Column> columns, ObservableCollection<ReplaceVariable> variables) {
            InitializeComponent();

            templatesPath = Path.Combine(Environment.CurrentDirectory, "Templates");
            columnsFormsTemplatePath = Path.Combine(templatesPath, "ColumnsForm");
            columnsViewTemplatePath = Path.Combine(templatesPath, "ColumnsView");

            GenerateViews(columns, variables);
            SetView(currentView);
        }

        private void GenerateViews(List<Column> columns, ObservableCollection<ReplaceVariable> variables) {
            try {
                Data = new ExportData();
                Data.ReadColumnTemplates(columnsFormsTemplatePath, columnsViewTemplatePath);
                Data.ReadTemplates(templatesPath);
                Data.GenerateIndex(columns, variables);
                Data.GenerateEdit(columns, variables);
                Data.GenerateCreate(columns, variables);
                Data.GenerateView(columns, variables);
                Data.GenerateModel(columns, variables);
                Data.GenerateController(columns, variables);
                Data.GenerateJavaScript(columns, variables);
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
            colorView.Visibility = Visibility.Hidden;
            colorEdit.Visibility = Visibility.Hidden;

            switch (currentView) {
                case View.MODEL:
                    colorModel.Visibility = Visibility.Visible;
                    tbPreview.Text = Data.Model;
                    break;
                case View.CONTROLLER:
                    colorController.Visibility = Visibility.Visible;
                    tbPreview.Text = Data.Controller;
                    break;
                case View.JAVASCRIPT:
                    colorJavaScript.Visibility = Visibility.Visible;
                    tbPreview.Text = Data.JavaScript;
                    break;
                case View.INDEX:
                    colorIndex.Visibility = Visibility.Visible;
                    tbPreview.Text = Data.Index;
                    break;
                case View.CREATE:
                    colorCreate.Visibility = Visibility.Visible;
                    tbPreview.Text = Data.Create;
                    break;
                case View.View:
                    colorView.Visibility = Visibility.Visible;
                    tbPreview.Text = Data.View;
                    break;
                case View.EDIT:
                    colorEdit.Visibility = Visibility.Visible;
                    tbPreview.Text = Data.Edit;
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

        private void View_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.View);
        }

        private void Edit_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.EDIT);
        }

        #endregion
    }
}
