Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Grid

Namespace Default_MVVM
	Public NotInheritable Class ColumnBehavior
		Public Shared ReadOnly IsRenameEditorActivatedProperty As DependencyProperty = DependencyProperty.RegisterAttached("IsRenameEditorActivated", GetType(Boolean), GetType(ColumnBehavior), New PropertyMetadata(False))

		Private Sub New()
		End Sub
		Public Shared Sub SetIsRenameEditorActivated(ByVal element As GridColumn, ByVal value As Boolean)
			If element.Header Is Nothing AndAlso value Then
				element.Header = element.FieldName
			End If
			element.SetValue(IsRenameEditorActivatedProperty, value)
		End Sub
		Public Shared Function GetIsRenameEditorActivated(ByVal element As GridColumn) As Boolean
			Return CBool(element.GetValue(IsRenameEditorActivatedProperty))
		End Function
	End Class
End Namespace