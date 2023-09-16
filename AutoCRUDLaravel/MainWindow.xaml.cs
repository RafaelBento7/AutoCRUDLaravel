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

namespace AutoCRUDLaravel
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var dbCon = DbConnection.Instance();
            dbCon.Server = "localhost";
            dbCon.Port = 3306;
            dbCon.DatabaseName = "test";
            dbCon.Username = "root";
            if (dbCon.Connect("")) {
                dbCon.GetColumns("test");
                dbCon.Close();
            }
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
    }
}
