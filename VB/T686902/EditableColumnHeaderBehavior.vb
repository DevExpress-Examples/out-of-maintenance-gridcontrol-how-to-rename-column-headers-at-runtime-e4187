Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Threading

Namespace T686902
    Friend Class EditableColumnHeaderBehavior
        Inherits Behavior(Of GridControl)

        Public Shared Function GetAllowEditHeader(ByVal obj As DependencyObject) As Boolean
            Return DirectCast(obj.GetValue(AllowEditHeaderProperty), Boolean)
        End Function
        Public Shared Sub SetAllowEditHeader(ByVal obj As DependencyObject, ByVal value As Boolean)
            obj.SetValue(AllowEditHeaderProperty, value)
        End Sub
        Public Shared ReadOnly AllowEditHeaderProperty As DependencyProperty = DependencyProperty.RegisterAttached("AllowEditHeader", GetType(Boolean), GetType(EditableColumnHeaderBehavior), New PropertyMetadata(False, AddressOf OnAllowEditHeaderChanged))

        Private Shared Sub OnAllowEditHeaderChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim column As GridColumn = TryCast(d, GridColumn)
            If column IsNot Nothing AndAlso column.Header Is Nothing AndAlso DirectCast(e.NewValue, Boolean) Then
                column.Header = column.FieldName
            End If
        End Sub
        Private ReadOnly Property Grid() As GridControl
            Get
                Return AssociatedObject
            End Get
        End Property
        Private ReadOnly Property View() As TableView
            Get
                Return CType(Grid.View, TableView)
            End Get
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            View.ColumnHeaderTemplate = CType(Grid.FindResource("EditableHeaderTemplate"), DataTemplate)
            AddHandler View.ShowGridMenu, AddressOf View_ShowGridMenu
        End Sub

        Private Sub View_ShowGridMenu(ByVal sender As Object, ByVal e As GridMenuEventArgs)
            If e.MenuType = GridMenuType.Column Then
                Dim columnHeader As GridColumnHeader = TryCast(e.TargetElement, GridColumnHeader)
                Dim column As GridColumn = TryCast(columnHeader.DataContext, GridColumn)
                Dim allowEditColumnHeader As Boolean = GetAllowEditHeader(column)
                Dim item As New BarButtonItem()
                item.Content = If(allowEditColumnHeader, "Hide ColumnHeader Editor", "Show ColumnHeader Editor")
                AddHandler item.ItemClick, AddressOf Item_ItemClick
                item.Tag = New Object() { column, columnHeader }
                e.Customizations.Add(item)
            End If
        End Sub

        Private Sub Item_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            Dim objs() As Object = CType(e.Item.Tag, Object())
            Dim column As GridColumn = DirectCast(objs(0), GridColumn)
            SetAllowEditHeader(column, Not GetAllowEditHeader(column))
            If GetAllowEditHeader(column) Then
                Dim header As GridColumnHeader = DirectCast(objs(1), GridColumnHeader)
                Dim te As TextEdit = LayoutTreeHelper.GetVisualChildren(header).OfType(Of TextEdit)().FirstOrDefault()
                Dispatcher.BeginInvoke(New Action(Sub()
                    te.Focus()
                    te.SelectAll()
                    AddHandler te.LostFocus, AddressOf Te_LostFocus
                End Sub), DispatcherPriority.Normal)
            End If
        End Sub

        Private Sub Te_LostFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim te As TextEdit = DirectCast(sender, TextEdit)
            te.Visibility = Visibility.Hidden
            RemoveHandler te.LostFocus, AddressOf Te_LostFocus
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler View.ShowGridMenu, AddressOf View_ShowGridMenu
        End Sub
    End Class
End Namespace
