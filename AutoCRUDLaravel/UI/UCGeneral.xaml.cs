using AutoCRUDLaravel.Models;
using AutoCRUDLaravel.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AutoCRUDLaravel.UI {
    /// <summary>
    /// Interação lógica para UCGeneral.xam
    /// </summary>
    public partial class UCGeneral : UserControl {
        public string Table => cbTable.Text;
        public string Server => tbServer.Text;
        public string Port => tbPort.Text;
        public string Username => tbUsername.Text;
        public string Password => tbPassword.Password;
        public string Database => tbDatabase.Text;

        public ObservableCollection<ReplaceVariable> Variables { get { return list; } }


        private ObservableCollection<ReplaceVariable> list;

        public UCGeneral(Connection connection, ObservableCollection<ReplaceVariable> variables) {
            InitializeComponent();
            this.Loaded += (sender, e) => {
                SnackbarError.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));
                SnackbarSuccess.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));
                tbServer.Text = connection.Server;
                tbPort.Text = connection.Port == 0 ? string.Empty : connection.Port.ToString();
                tbUsername.Text = connection.Username;
                tbPassword.Password = connection.Password;
                tbDatabase.Text = connection.Database;


                if (DbConnection.Instance().IsConnected) {
                    cbTable.IsEnabled = true;
                    btUpdateTables.IsEnabled = true;
                    cbTable.ItemsSource = null;
                    UpdateTables_Click(btUpdateTables, null);
                    if (!string.IsNullOrEmpty(connection.Table)) {
                        foreach (string table in cbTable.Items) 
                            if (table.Equals(connection.Table))
                                cbTable.SelectedItem = table;
                    }
                    list = variables;
                } else {
                    list = ReplaceVariable.GetVariables();
                    list.Insert(0, new ReplaceVariable("{columns}", "Display the columns based on the templates (Create/Edit/View views)", true));
                }
                dgVariables.ItemsSource = list;
            };
        }

        private void Connect_Click(object sender, RoutedEventArgs e) {
            DbConnection connection = DbConnection.Instance();
            (bool isValid, string errorValidations) = connection.Validate(tbServer.Text, tbPort.Text, tbUsername.Text, tbDatabase.Text);
            if (!isValid) {
                SnackbarError.MessageQueue.Enqueue(errorValidations);
                return;
            }

            connection.Connection = new Connection {
                Server = tbServer.Text,
                Port = tbPort.Text.ToInt(),
                Username = tbUsername.Text,
                Password = tbPassword.Password,
                Database = tbDatabase.Text
            };

            (bool isConnected, string errorConnecting) = connection.Connect();
            if (isConnected) {
                cbTable.IsEnabled = true;
                btUpdateTables.IsEnabled = true;
                cbTable.ItemsSource = null;
                SnackbarSuccess.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));
                SnackbarSuccess.MessageQueue.Enqueue("Connected");
                UpdateTables_Click(btUpdateTables, null);
            } else {
                cbTable.IsEnabled = false;
                btUpdateTables.IsEnabled = false;
                cbTable.ItemsSource = null;
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

            cbTable.ItemsSource = null;
            cbTable.ItemsSource = tables;
        }

        private void Delete_Click(object sender, RoutedEventArgs e) {
            if (dgVariables.SelectedItem == null || !(dgVariables.SelectedItem is ReplaceVariable item))
                return;

            list.Remove(item);
        }

        private void NewVariable_Click(object sender, RoutedEventArgs e) {
            list.Add(new ReplaceVariable("{variable}", "value"));
            dgVariables.ScrollIntoView(dgVariables.Items[dgVariables.Items.Count - 1]);
        }
    }
}
