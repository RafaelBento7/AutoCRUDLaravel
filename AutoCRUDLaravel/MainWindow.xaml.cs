using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoCRUDLaravel {
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window {
        string table;
        List<Column> columns;
        public MainWindow() {
            InitializeComponent();
            Settings.Load();
            content.Content = new UCGeneral();
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            if (!(content.Content is UCGeneral))
                return;

            Settings.Save(((UCGeneral)content.Content).tbServer.Text, ((UCGeneral)content.Content).tbPort.Text, ((UCGeneral)content.Content).tbUsername.Text, ((UCGeneral)content.Content).tbDatabase.Text,
                          ((UCGeneral)content.Content).cbGenerateIndex.IsChecked == true, ((UCGeneral)content.Content).cbGenerateShow.IsChecked == true, ((UCGeneral)content.Content).cbGenerateEdit.IsChecked == true, ((UCGeneral)content.Content).cbGenerateCreate.IsChecked == true,
                          ((UCGeneral)content.Content).cbDisplayButtonDelete.IsChecked == true, ((UCGeneral)content.Content).cbDisplayButtonEdit.IsChecked == true, ((UCGeneral)content.Content).cbDisplayButtonShow.IsChecked == true,
                          ((UCGeneral)content.Content).cbVisibleIndex.IsChecked == true, ((UCGeneral)content.Content).cbVisibleEdit.IsChecked == true, ((UCGeneral)content.Content).cbVisibleCreate.IsChecked == true, ((UCGeneral)content.Content).cbVisibleShow.IsChecked == true);
        }

        private void Next_Click(object sender, RoutedEventArgs e) {
            if (content.Content is UCGeneral ucGeneral) {
                table = ucGeneral.Table;

                if (string.IsNullOrEmpty(table))
                    return;
                btPrevious.Visibility = Visibility.Visible;
                content.Content = new UCColumns(this.table);
                ChangeTabColorFocus(false, true, false);
                btSave.Visibility = Visibility.Collapsed;
                btExportJson.Visibility = Visibility.Visible;
                btAddColumn.Visibility = Visibility.Visible;

                return;
            }

            if (content.Content is UCColumns ucColumns) {
                columns = ucColumns.Columns;

                if (columns == null)
                    return;

                ChangeTabColorFocus(false, false, true);
                btNext.Visibility = Visibility.Collapsed;

                content.Content = new UCExport();
            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e) {
            if (content.Content is UCGeneral)
                return;

            btNext.Visibility = Visibility.Visible;

            if (content.Content is UCColumns ucColumns) {
                btPrevious.Visibility = Visibility.Collapsed;

                columns = ucColumns.Columns;
                ChangeTabColorFocus(true, false, false);
                btSave.Visibility = Visibility.Visible;
                btExportJson.Visibility = Visibility.Collapsed;
                btAddColumn.Visibility = Visibility.Collapsed;

                content.Content = new UCGeneral();
            }

            if (content.Content is UCExport ucExport) {
                ChangeTabColorFocus(false, true, false);
                content.Content = new UCColumns(table);
            }
        }

        private void ChangeTabColorFocus(bool general, bool columns, bool export) {
            if (general)
                colorGeneral.Visibility = Visibility.Visible;
            else colorGeneral.Visibility = Visibility.Hidden;

            if (columns)
                colorColumns.Visibility = Visibility.Visible;
            else colorColumns.Visibility = Visibility.Hidden;

            if (export)
                colorExport.Visibility = Visibility.Visible;
            else colorExport.Visibility = Visibility.Hidden;
        }
    }
}
