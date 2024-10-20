using AutoCRUDLaravel.Models;
using AutoCRUDLaravel.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace AutoCRUDLaravel.UI {
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window {
        private enum Screen {
            GENERAL,
            COLUMNS,
            EXPORT
        }
        private Connection connection;
        List<Column> columns;
        ObservableCollection<ReplaceVariable> variables;

        public MainWindow() {
            InitializeComponent();
            connection = IniHelper.LoadSettings();
            variables = new ObservableCollection<ReplaceVariable>();
            columns = new List<Column>();
            content.Content = new UCGeneral(connection, variables);
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            if (!(content.Content is UCGeneral uc))
                return;

            IniHelper.SaveSettings(uc.Server, uc.Port, uc.Username, uc.Password, uc.Database);
            ReplaceVariable.SaveVariables(uc.Variables);
        }

        private void Next_Click(object sender, RoutedEventArgs e) {
            if (content.Content is UCGeneral ucGeneral) {
                // if connections changes reset columns
                if (!connection.Server.Equals(ucGeneral.Server) &&
                    !connection.Port.Equals(ucGeneral.Port) &&
                    !connection.Username.Equals(ucGeneral.Username) &&
                    !connection.Password.Equals(ucGeneral.Password) &&
                    !connection.Table.Equals(ucGeneral.Table) &&
                    !connection.Database.Equals(ucGeneral.Database))
                    columns = new List<Column>();

                connection.Server = ucGeneral.Server;
                connection.Port = ucGeneral.Port.ToInt();
                connection.Username = ucGeneral.Username;
                connection.Password = ucGeneral.Password;
                connection.Database = ucGeneral.Database;
                connection.Table = ucGeneral.Table;
                variables = ucGeneral.Variables;

                if (string.IsNullOrEmpty(connection.Table))
                    return;

                content.Content = new UCColumns(connection, columns);
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
                content.Content = new UCGeneral(connection, variables);
            } else if (content.Content is UCExport ucExport) {
                ChangeScreen(Screen.COLUMNS);
                content.Content = new UCColumns(connection, columns);
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
