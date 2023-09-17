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
        public MainWindow() {
            InitializeComponent();
            Settings.Load();
            content.Content = new UCGeneral();
        }

        private void General_Click(object sender, MouseButtonEventArgs e) {
            colorGeneral.Visibility = Visibility.Visible;
            colorColumns.Visibility = Visibility.Hidden;
            colorExport.Visibility = Visibility.Hidden;
        }

        private void Columns_Click(object sender, MouseButtonEventArgs e) {
            colorGeneral.Visibility = Visibility.Hidden;
            colorColumns.Visibility = Visibility.Visible;
            colorExport.Visibility = Visibility.Hidden;
        }

        private void Export_Click(object sender, MouseButtonEventArgs e) {
            colorGeneral.Visibility = Visibility.Hidden;
            colorColumns.Visibility = Visibility.Hidden;
            colorExport.Visibility = Visibility.Visible;
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            if (!(content.Content is UCGeneral))
                return;

            Settings.Save(((UCGeneral)content.Content).tbServer.Text, ((UCGeneral)content.Content).tbPort.Text, ((UCGeneral)content.Content).tbUsername.Text, ((UCGeneral)content.Content).tbDatabase.Text,
                          ((UCGeneral)content.Content).cbGenerateIndex.IsChecked == true, ((UCGeneral)content.Content).cbGenerateShow.IsChecked == true, ((UCGeneral)content.Content).cbGenerateEdit.IsChecked == true,
                          ((UCGeneral)content.Content).cbDisplayButtonDelete.IsChecked == true, ((UCGeneral)content.Content).cbDisplayButtonEdit.IsChecked == true, ((UCGeneral)content.Content).cbDisplayButtonShow.IsChecked == true,
                          ((UCGeneral)content.Content).cbVisibleIndex.IsChecked == true, ((UCGeneral)content.Content).cbVisibleEdit.IsChecked == true, ((UCGeneral)content.Content).cbVisibleCreate.IsChecked == true, ((UCGeneral)content.Content).cbVisibleShow.IsChecked == true);
        }
    }
}
