using MaterialDesignThemes.Wpf;
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
    /// Interação lógica para UCGeneral.xam
    /// </summary>
    public partial class UCGeneral : UserControl {
        public UCGeneral() {
            InitializeComponent();
            this.Loaded += (sender, e) => {
                SnackbarError.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));
                SnackbarSuccess.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));
                tbServer.Text = Settings.Server;
                tbPort.Text = Settings.Port;
                tbUsername.Text = Settings.Username;
                tbDatabase.Text = Settings.Database;
                cbGenerateIndex.IsChecked = Settings.GenerateIndex;
                cbGenerateEdit.IsChecked = Settings.GenerateEdit;
                cbGenerateShow.IsChecked = Settings.GenerateShow;
                cbDisplayButtonShow.IsChecked = Settings.ShowShowButtonIndex;
                cbDisplayButtonEdit.IsChecked = Settings.ShowEditButtonIndex;
                cbDisplayButtonDelete.IsChecked = Settings.ShowDeleteButtonIndex;
                cbVisibleIndex.IsChecked = Settings.IsVisibleIndex;
                cbVisibleEdit.IsChecked = Settings.IsVisibleEdit;
                cbVisibleShow.IsChecked = Settings.IsVisibleShow;
                cbVisibleCreate.IsChecked= Settings.IsVisibleCreate;
            };
        }

        private void Connect_Click(object sender, RoutedEventArgs e) {
            DbConnection connection = DbConnection.Instance();
            (bool isValid, string errorValidations) = connection.Validate(tbServer.Text, tbPort.Text, tbUsername.Text, tbDatabase.Text);
            if (!isValid) {
                SnackbarError.MessageQueue.Enqueue(errorValidations);
                return;
            }

            connection.Server = tbServer.Text;
            connection.Port = tbPort.Text.ToInt();
            connection.Username = tbUsername.Text;
            connection.DatabaseName = tbDatabase.Text;

            (bool isConnected, string errorConnecting) = connection.Connect(tbPassword.Password);
            if (isConnected) {
                tbTable.IsEnabled = true;
                btUpdateTables.IsEnabled = true;
                tbTable.ItemsSource = null;
                SnackbarSuccess.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));
                SnackbarSuccess.MessageQueue.Enqueue("Connected");
                UpdateTables_Click(btUpdateTables, null);
            } else {
                tbTable.IsEnabled = false;
                btUpdateTables.IsEnabled = false;
                tbTable.ItemsSource = null;
                SnackbarError.MessageQueue.Enqueue(errorConnecting);
            }
        }

        private void UpdateTables_Click(object sender, RoutedEventArgs e) {
            var (tables, errorMessage) = DbConnection.Instance().GetTables();
            

            if (tables == null) {
                SnackbarError.MessageQueue.Enqueue(errorMessage);
                return;
            }

            if (tables.Count == 0) {
                SnackbarError.MessageQueue.Enqueue("Não existem tabelas disponíveis a mostrar");
                return;
            }

            tbTable.ItemsSource = null;
            tbTable.ItemsSource = tables;
        }
    }
}
