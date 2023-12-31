﻿using AutoCRUDLaravel.models;
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

namespace AutoCRUDLaravel {
    /// <summary>
    /// Interação lógica para UCGeneral.xam
    /// </summary>
    public partial class UCGeneral : UserControl {
        public string Table { get { return cbTable.Text; } }
        public string Server { get { return tbServer.Text; } }
        public string Port { get { return tbPort.Text; } }
        public string Username { get { return tbUsername.Text; } }
        public string Database { get { return tbDatabase.Text; } }

        public ObservableCollection<GeneratorVariables> Variables { get { return list; } }


        private ObservableCollection<GeneratorVariables> list;

        public UCGeneral() {
            InitializeComponent();
            this.Loaded += (sender, e) => {
                SnackbarError.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));
                SnackbarSuccess.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));
                tbServer.Text = Settings.Server;
                tbPort.Text = Settings.Port;
                tbUsername.Text = Settings.Username;
                tbDatabase.Text = Settings.Database;
                list = GeneratorVariables.GetVariables();
                list.Insert(0, new GeneratorVariables("{columns}", "Display the columns based on the templates (Create/Edit/Show views)", true));
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

            connection.Server = tbServer.Text;
            connection.Port = tbPort.Text.ToInt();
            connection.Username = tbUsername.Text;
            connection.DatabaseName = tbDatabase.Text;

            (bool isConnected, string errorConnecting) = connection.Connect(tbPassword.Password);
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
            if (dgVariables.SelectedItem == null || !(dgVariables.SelectedItem is GeneratorVariables item))
                return;

            list.Remove(item);
        }

        private void NewVariable_Click(object sender, RoutedEventArgs e) {
            list.Add(new GeneratorVariables("{variable}", "value"));
        }
    }
}
