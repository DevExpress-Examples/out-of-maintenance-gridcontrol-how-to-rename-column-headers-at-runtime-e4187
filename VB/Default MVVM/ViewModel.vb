Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Namespace DXWpfApplication
	'Model
	Public Class TestData
		Private privateText As String
		Public Property Text() As String
			Get
				Return privateText
			End Get
			Set(ByVal value As String)
				privateText = value
			End Set
		End Property
		Private privateNumber As Integer
		Public Property Number() As Integer
			Get
				Return privateNumber
			End Get
			Set(ByVal value As Integer)
				privateNumber = value
			End Set
		End Property
	End Class

	'Base View Model

	Public MustInherit Class BaseViewModel
		Implements INotifyPropertyChanged
		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Protected Sub OnPropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
	End Class

	'View Model
	Public Class TestDataViewModel
		Inherits BaseViewModel

		Private data As TestData
		Public Sub New(ByVal data As TestData)
			Me.data = data
		End Sub

		Public Sub New()
			Me.New(New TestData())
		End Sub

		Public Property Text() As String
			Get
				Return data.Text
			End Get
			Set(ByVal value As String)
				If data.Text <> value Then
					data.Text = value
					OnPropertyChanged("Text")
				End If
			End Set
		End Property

		Public Property Number() As Integer
			Get
				Return data.Number
			End Get
			Set(ByVal value As Integer)
				If data.Number <> value Then
					data.Number = value
					OnPropertyChanged("Number")
				End If
			End Set
		End Property
	End Class

	'Views Model
	Public Class TestDataViewsModel
		Inherits BaseViewModel
		Public Sub New()
			Records = New ObservableCollection(Of TestDataViewModel)()
			Dim list As New List(Of TestData)()
			For i As Integer = 0 To 9
				Records.Add(New TestDataViewModel(New TestData() With {.Text = "Row" & i, .Number = i}))
			Next i
		End Sub

		Private records_Renamed As ObservableCollection(Of TestDataViewModel)

		Public Property Records() As ObservableCollection(Of TestDataViewModel)
			Get
				Return records_Renamed
			End Get
			Set(ByVal value As ObservableCollection(Of TestDataViewModel))
				If records_Renamed IsNot value Then
					records_Renamed = value
					OnPropertyChanged("Records")
				End If
			End Set
		End Property
	End Class
End Namespace
