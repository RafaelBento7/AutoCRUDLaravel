using MaterialDesignThemes.Wpf;
using MySqlX.XDevAPI.Relational;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoCRUDLaravel {
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

        public UCColumns(string table) {
            InitializeComponent();

            tbTable.Text = table;
            tbUsername.Text = DbConnection.Instance().Username;
            tbDatabase.Text = DbConnection.Instance().DatabaseName;

            var (columns, errorMessage) = DbConnection.Instance().GetColumns(table);
            if (columns == null)
                MessageBox.Show(errorMessage);

            this.Loaded += (sender, e) => {
                dgColumns.ItemsSource = columns;
                cbType.ItemsSource = ColumnType.GetAllColumnTypes();
                if (dgColumns.Items != null && dgColumns.Items.Count > 0) {
                    dgColumns.SelectedItem = dgColumns.Items[0];
                    Show_Click(this, null);
                }
            };
        }

        private void Show_Click(object sender, RoutedEventArgs e) {
            if (dgColumns.SelectedItem == null)
                return;

            Column selectedItem = dgColumns.SelectedItem as Column;

            lblName.Text = selectedItem.Name;
            cbVisibleCreate.IsChecked = selectedItem.IsVisibleCreate;
            cbVisibleIndex.IsChecked = selectedItem.IsVisibleIndex;
            cbVisibleEdit.IsChecked = selectedItem.IsVisibleEdit;
            cbVisibleShow.IsChecked = selectedItem.IsVisibleShow;
            tbFieldTitle.Text = selectedItem.FieldTitle;
            tbPlaceholder.Text = selectedItem.PlaceHolder;
            cbNullable.IsChecked = selectedItem.IsNullable;
            cbUnsigned.IsChecked = selectedItem.Unsigned;
            tbDefaultValue.Text = selectedItem.DefaultValue;
            tbMaxLength.Text = selectedItem.MaxLength.ToString();
            tbSelectEnum.Text = selectedItem.SelectEnum;
            tbSelectArray.Text = selectedItem.SelectArray;
            tbDateFormat.Text = selectedItem.DateFormat;

            foreach (ColumnType type in cbType.ItemsSource) {
                if (type.Type == selectedItem.Type.Type)
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

            switch (((ColumnType)cbType.SelectedItem).Type) {
                case ColumnType.TypeEnum.NUMERIC_DOUBLE:
                case ColumnType.TypeEnum.NUMERIC_INT:
                    cbUnsigned.Visibility = Visibility.Visible;
                    break;
                case ColumnType.TypeEnum.SELECT_ENUM:
                    tbSelectEnum.Visibility = Visibility.Visible;
                    lblSelectEnum.Visibility = Visibility.Visible;
                    break;
                case ColumnType.TypeEnum.TEXT:
                    lblMaxLength.Visibility = Visibility.Visible;
                    tbMaxLength.Visibility = Visibility.Visible;
                    break;
                case ColumnType.TypeEnum.SELECT_ARRAY:
                    lblSelectArray.Visibility = Visibility.Visible;
                    tbSelectArray.Visibility = Visibility.Visible;
                    break;
                case ColumnType.TypeEnum.DATETIME:
                case ColumnType.TypeEnum.DATE:
                case ColumnType.TypeEnum.TIME:
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
                    item.IsVisibleShow = cbVisibleShow.IsChecked == true;
                    item.FieldTitle = tbFieldTitle.Text;
                    item.PlaceHolder = tbPlaceholder.Text;
                    if (cbType.SelectedItem != null)
                        if (cbType.SelectedItem is ColumnType)
                            item.Type = (ColumnType)cbType.SelectedItem;
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
