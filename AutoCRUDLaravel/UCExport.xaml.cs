using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoCRUDLaravel {
    /// <summary>
    /// Interação lógica para UCExport.xam
    /// </summary>
    public partial class UCExport : UserControl {
        private enum View {
            MODEL,
            CONTROLLER,
            JAVASCRIPT,
            INDEX,
            CREATE,
            SHOW,
            EDIT
        }

        private View currentView;

        public UCExport(List<Column> columns) {
            InitializeComponent();
            SetView(currentView);
        }

        private void SetView(View currentView) {
            this.currentView = currentView;

            colorModel.Visibility = Visibility.Hidden;
            colorController.Visibility = Visibility.Hidden;
            colorJavaScript.Visibility = Visibility.Hidden;
            colorIndex.Visibility = Visibility.Hidden;
            colorCreate.Visibility = Visibility.Hidden;
            colorShow.Visibility = Visibility.Hidden;
            colorEdit.Visibility = Visibility.Hidden;

            switch (currentView) {
                case View.MODEL:
                    colorModel.Visibility = Visibility.Visible;
                    break;
                case View.CONTROLLER:
                    colorController.Visibility = Visibility.Visible;
                    break;
                case View.JAVASCRIPT:
                    colorJavaScript.Visibility = Visibility.Visible;
                    break;
                case View.INDEX:
                    colorIndex.Visibility = Visibility.Visible;
                    break;
                case View.CREATE:
                    colorCreate.Visibility = Visibility.Visible;
                    break;
                case View.SHOW:
                    colorShow.Visibility = Visibility.Visible;
                    break;
                case View.EDIT:
                    colorEdit.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void LoadPreview(View currentView) {
            switch (currentView) {
                case View.MODEL:
                    break;
                case View.CONTROLLER:
                    break;
                case View.JAVASCRIPT:
                    break;
                case View.INDEX:
                    break;
                case View.CREATE:
                    break;
                case View.SHOW:
                    break;
                case View.EDIT:
                    break;
                default:
                    break;
            }
        }

        #region Right-Tab StackPanel Clicks

        private void Model_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.MODEL);
        }

        private void Controller_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.CONTROLLER);
        }

        private void JavaScript_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.JAVASCRIPT);
        }

        private void Index_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.INDEX);
        }

        private void Create_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.CREATE);
        }

        private void Show_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.SHOW);
        }

        private void Edit_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            SetView(View.EDIT);
        }

        #endregion
    }
}
