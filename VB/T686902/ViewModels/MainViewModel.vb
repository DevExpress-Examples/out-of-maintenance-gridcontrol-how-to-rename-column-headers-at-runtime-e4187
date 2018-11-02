Imports DevExpress.Mvvm.DataAnnotations
Imports System.Collections.ObjectModel

Namespace T686902.ViewModels
    <POCOViewModel> _
    Public Class MainViewModel
        Protected Sub New()
            Items = New ObservableCollection(Of Item)()
            For i As Integer = 0 To 9
                Items.Add(New Item() With { _
                    .ID = i, _
                    .Name = "Item " & i _
                })
            Next i
        End Sub
        Public Overridable Property Items() As ObservableCollection(Of Item)
    End Class

    Public Class Item
        Public Property ID() As Integer
        Public Property Name() As String
    End Class
End Namespace