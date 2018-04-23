Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DXWpfApplication
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Bars
Imports System.Windows.Markup
Imports DevExpress.Xpf.Editors
Imports System.Windows.Media.Animation

Namespace Default_MVVM
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
			DataContext = New TestDataViewsModel()
		End Sub

		Private Sub OnShowGridMenu(ByVal sender As Object, ByVal e As GridMenuEventArgs)
			If e.MenuType = GridMenuType.Column Then
				Dim columnHeader As GridColumnHeader = TryCast(e.TargetElement, GridColumnHeader)
				Dim column As GridColumn = TryCast(columnHeader.DataContext, GridColumn)
				Dim showColumnHeaderEditor As Boolean = ColumnBehavior.GetIsRenameEditorActivated(column)
				Dim item As New BarButtonItem()
				If showColumnHeaderEditor Then
					item.Content = "Hide ColumnHeader Editor"
				Else
					item.Content = "Show ColumnHeader Editor"
				End If
				AddHandler item.ItemClick, AddressOf OnItemClick
				item.Tag = column
				e.Customizations.Add(item)
			End If
		End Sub

		Private Shared Sub OnItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
			Dim column As GridColumn = TryCast(e.Item.Tag, GridColumn)
			ColumnBehavior.SetIsRenameEditorActivated(column, (Not ColumnBehavior.GetIsRenameEditorActivated(column)))
		End Sub

		Private Sub OnRenameEditorLostFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim edit As TextEdit = TryCast(sender, TextEdit)
			edit.Visibility = System.Windows.Visibility.Hidden
		End Sub
	End Class

End Namespace