using AutoCRUDLaravel.Enums;
using AutoCRUDLaravel.Models;
using AutoCRUDLaravel.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AutoCRUDLaravel.UI {
    /// <summary>
    /// Interação lógica para UCColumns.xam
    /// </summary>
    public partial class UCColumns : UserControl {
        public List<Column> Columns {
            get {
                if (dgColumns.ItemsSource == null)
                    return null;

                return (List<Column>)dgColumns.ItemsSource;
            }
        }

        public UCColumns(Connection table, List<Column> columns) {
            InitializeComponent();

            tbTable.Text = table.Table;
            tbUsername.Text = DbConnection.Instance().Connection.Username;
            tbDatabase.Text = DbConnection.Instance().Connection.Database;

            if (columns.Count > 0) {
                dgColumns.ItemsSource = columns;
            } else {
                var (columnsDB, errorMessage) = DbConnection.Instance().GetColumns(table.Table);
                if (columnsDB == null)
                    MessageBox.Show(errorMessage);
                dgColumns.ItemsSource = columnsDB;
            }

            this.Loaded += (sender, e) => {
                cbType.ItemsSource = Enum.GetValues(typeof(ColumnTypeEnum));
                if (dgColumns.Items != null && dgColumns.Items.Count > 0) {
                    dgColumns.SelectedItem = dgColumns.Items[0];
                    View_Click(this, null);
                }
            };
        }

        private void View_Click(object sender, RoutedEventArgs e) {
            if (dgColumns.SelectedItem == null)
                return;

            Column selectedItem = dgColumns.SelectedItem as Column;

            lblName.Text = selectedItem.Name;
            cbVisibleCreate.IsChecked = selectedItem.IsVisibleCreate;
            cbVisibleIndex.IsChecked = selectedItem.IsVisibleIndex;
            cbVisibleEdit.IsChecked = selectedItem.IsVisibleEdit;
            cbVisibleView.IsChecked = selectedItem.IsVisibleView;
            tbFieldTitle.Text = selectedItem.FieldTitle;
            tbPlaceholder.Text = selectedItem.PlaceHolder;
            cbNullable.IsChecked = selectedItem.IsNullable;
            cbUnsigned.IsChecked = selectedItem.Unsigned;
            tbDefaultValue.Text = selectedItem.DefaultValue;
            tbMaxLength.Text = selectedItem.MaxLength.ToString();
            tbSelectEnum.Text = selectedItem.SelectEnum;
            tbSelectArray.Text = selectedItem.SelectArray;
            tbDateFormat.Text = selectedItem.DateFormat;

            foreach (ColumnTypeEnum type in cbType.ItemsSource) {
                if (type == selectedItem.Type)
                    cbType.SelectedItem = type;
            }
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (!(sender is ComboBox))
                return;

            ComboBox cbType = sender as ComboBox;

            if (cbType.SelectedItem == null)
                return;

            cbUnsigned.Visibility = Visibility.Collapsed;
            tbSelectEnum.Visibility = Visibility.Collapsed;
            lblSelectEnum.Visibility = Visibility.Collapsed;
            lblMaxLength.Visibility = Visibility.Collapsed;
            tbMaxLength.Visibility = Visibility.Collapsed;
            lblSelectArray.Visibility = Visibility.Collapsed;
            tbSelectArray.Visibility = Visibility.Collapsed;
            lblDateFormat.Visibility = Visibility.Collapsed;
            tbDateFormat.Visibility = Visibility.Collapsed;

            switch ((ColumnTypeEnum)cbType.SelectedItem) {
                case ColumnTypeEnum.NUMERIC_DOUBLE:
                case ColumnTypeEnum.NUMERIC_INT:
                    cbUnsigned.Visibility = Visibility.Visible;
                    break;
                case ColumnTypeEnum.SELECT_ENUM:
                    tbSelectEnum.Visibility = Visibility.Visible;
                    lblSelectEnum.Visibility = Visibility.Visible;
                    break;
                case ColumnTypeEnum.TEXT:
                    lblMaxLength.Visibility = Visibility.Visible;
                    tbMaxLength.Visibility = Visibility.Visible;
                    break;
                case ColumnTypeEnum.SELECT_ARRAY:
                    lblSelectArray.Visibility = Visibility.Visible;
                    tbSelectArray.Visibility = Visibility.Visible;
                    break;
                case ColumnTypeEnum.DATETIME:
                case ColumnTypeEnum.DATE:
                case ColumnTypeEnum.TIME:
                    lblDateFormat.Visibility = Visibility.Visible;
                    tbDateFormat.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e) {
            if (cbType.SelectedItem == null)
                return;

            if (dgColumns.ItemsSource == null)
                return;

            foreach (Column item in dgColumns.ItemsSource) {
                if (item.Name == lblName.Text) {
                    item.IsVisibleCreate = cbVisibleCreate.IsChecked == true;
                    item.IsVisibleIndex = cbVisibleIndex.IsChecked == true;
                    item.IsVisibleEdit = cbVisibleEdit.IsChecked == true;
                    item.IsVisibleView = cbVisibleView.IsChecked == true;
                    item.FieldTitle = tbFieldTitle.Text;
                    item.PlaceHolder = tbPlaceholder.Text;
                    if (cbType.SelectedItem != null)
                        if (cbType.SelectedItem is ColumnTypeEnum)
                            item.Type = (ColumnTypeEnum)cbType.SelectedItem;
                    item.IsNullable = cbNullable.IsChecked == true;
                    item.Unsigned = cbUnsigned.IsChecked == true;
                    item.DefaultValue = tbDefaultValue.Text;
                    item.MaxLength = tbMaxLength.Text.ToUShort();
                    item.SelectEnum = tbSelectEnum.Text;
                    item.SelectArray = tbSelectArray.Text;
                    item.DateFormat = tbDateFormat.Text;
                    break;
                }
            }

            var columns = dgColumns.ItemsSource;
            dgColumns.ItemsSource = null;
            dgColumns.ItemsSource = columns;
        }
    }
}
