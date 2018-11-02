using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace T686902 {
    class EditableColumnHeaderBehavior : Behavior<GridControl> {
        public static bool GetAllowEditHeader(DependencyObject obj) {
            return (bool)obj.GetValue(AllowEditHeaderProperty);
        }
        public static void SetAllowEditHeader(DependencyObject obj, bool value) {
            obj.SetValue(AllowEditHeaderProperty, value);
        }
        public static readonly DependencyProperty AllowEditHeaderProperty = DependencyProperty.RegisterAttached("AllowEditHeader", typeof(bool), typeof(EditableColumnHeaderBehavior), new PropertyMetadata(false, OnAllowEditHeaderChanged));

        static void OnAllowEditHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            GridColumn column = d as GridColumn;
            if (column != null && column.Header == null && (bool)e.NewValue)
                column.Header = column.FieldName;
        }
        GridControl Grid => AssociatedObject;
        TableView View => (TableView)Grid.View;

        protected override void OnAttached() {
            base.OnAttached();
            View.ColumnHeaderTemplate = (DataTemplate)Grid.FindResource("EditableHeaderTemplate");
            View.ShowGridMenu += View_ShowGridMenu;
        }

        private void View_ShowGridMenu(object sender, GridMenuEventArgs e) {
            if (e.MenuType == GridMenuType.Column) {
                GridColumnHeader columnHeader = e.TargetElement as GridColumnHeader;
                GridColumn column = columnHeader.DataContext as GridColumn;
                bool allowEditColumnHeader = GetAllowEditHeader(column);
                BarButtonItem item = new BarButtonItem();
                item.Content = allowEditColumnHeader ? "Hide ColumnHeader Editor" : "Show ColumnHeader Editor";
                item.ItemClick += Item_ItemClick;
                item.Tag = new object[] { column, columnHeader };
                e.Customizations.Add(item);
            }
        }

        private void Item_ItemClick(object sender, ItemClickEventArgs e) {
            object[] objs = (object[])e.Item.Tag;
            GridColumn column = (GridColumn)objs[0];
            SetAllowEditHeader(column, !GetAllowEditHeader(column));
            if (GetAllowEditHeader(column)) {
                GridColumnHeader header = (GridColumnHeader)objs[1];
                TextEdit te = LayoutTreeHelper.GetVisualChildren(header).OfType<TextEdit>().FirstOrDefault();
                Dispatcher.BeginInvoke(new Action(() => {
                    te.Focus();
                    te.SelectAll();
                    te.LostFocus += Te_LostFocus;
                }), DispatcherPriority.Normal);
            }
        }

        private void Te_LostFocus(object sender, RoutedEventArgs e) {
            TextEdit te = (TextEdit)sender;
            te.Visibility = Visibility.Hidden;
            te.LostFocus -= Te_LostFocus;
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            View.ShowGridMenu -= View_ShowGridMenu;
        }
    }
}
