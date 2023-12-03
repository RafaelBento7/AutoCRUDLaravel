using AutoCRUDLaravel.models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace AutoCRUDLaravel {
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window {
        private enum Screen {
            GENERAL,
            COLUMNS,
            EXPORT
        }
        string table;
        List<Column> columns;
        ObservableCollection<GeneratorVariables> variables;

        public MainWindow() {
            InitializeComponent();
            Settings.Load();
            content.Content = new UCGeneral();
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            if (!(content.Content is UCGeneral uc))
                return;

            Settings.Save(uc.Server, uc.Port, uc.Username, uc.Database);
            GeneratorVariables.SaveVariables(uc.Variables);
        }

        private void Next_Click(object sender, RoutedEventArgs e) {
            if (content.Content is UCGeneral ucGeneral) {
                table = ucGeneral.Table;
                variables = ucGeneral.Variables;
                if (string.IsNullOrEmpty(table))
                    return;

                content.Content = new UCColumns(this.table);
                ChangeScreen(Screen.COLUMNS);
                return;
            } else if (content.Content is UCColumns ucColumns) {
                columns = ucColumns.Columns;

                if (columns == null)
                    return;

                ChangeScreen(Screen.EXPORT);
                content.Content = new UCExport(columns, variables);
            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e) {
            if (content.Content is UCGeneral)
                return;

            if (content.Content is UCColumns ucColumns) {
                columns = ucColumns.Columns;
                ChangeScreen(Screen.GENERAL);
                content.Content = new UCGeneral();
            } else if (content.Content is UCExport ucExport) {
                ChangeScreen(Screen.COLUMNS);
                content.Content = new UCColumns(table);
            }
        }

        private void ChangeScreen(Screen currentScreen) {
            if (currentScreen == Screen.GENERAL) {
                colorGeneral.Visibility = Visibility.Visible;
                colorColumns.Visibility = Visibility.Hidden;
                colorExport.Visibility = Visibility.Hidden;
                btNext.Visibility = Visibility.Visible;
                btPrevious.Visibility = Visibility.Collapsed;
                stackPanelExport.Visibility = Visibility.Collapsed;
                btAddColumn.Visibility = Visibility.Collapsed;
                btSave.Visibility = Visibility.Visible;
            } else if (currentScreen == Screen.COLUMNS) {
                colorGeneral.Visibility = Visibility.Hidden;
                colorColumns.Visibility = Visibility.Visible;
                colorExport.Visibility = Visibility.Hidden;
                btNext.Visibility = Visibility.Visible;
                btPrevious.Visibility = Visibility.Visible;
                stackPanelExport.Visibility = Visibility.Collapsed;
                btAddColumn.Visibility = Visibility.Visible;
                btSave.Visibility = Visibility.Collapsed;
            } else {
                colorGeneral.Visibility = Visibility.Hidden;
                colorColumns.Visibility = Visibility.Hidden;
                colorExport.Visibility = Visibility.Visible;
                btNext.Visibility = Visibility.Collapsed;
                btPrevious.Visibility = Visibility.Visible;
                stackPanelExport.Visibility = Visibility.Visible;
                btAddColumn.Visibility = Visibility.Collapsed;
                btSave.Visibility = Visibility.Collapsed;
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e) {

        }

        private void ExportAll_Click(object sender, RoutedEventArgs e) {

        }
    }
}
