Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Namespace CustomImageEditor
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub
		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
            Dim ds As New BindingList(Of ImageWithOffsetItem)()
            Dim t As Image = My.Resources.Penguins
            Dim converter As New ImageConverter()
			For i As Integer = 0 To 9
				Dim item As ImageWithOffsetItem = CreateItem(t)
				ds.Add(item)
				dragImageEdit1.EditValue = item
			Next i

			gridControl1.DataSource = ds
			Dim newCustomImageEditReposioryItem As New DragImagEditRepositoryItem()
			gridControl1.RepositoryItems.Add(newCustomImageEditReposioryItem)
			AddHandler TryCast(gridControl1.MainView, GridView).CalcRowHeight, AddressOf Form1_CalcRowHeight
			TryCast(gridControl1.MainView, GridView).Columns("InfoImage").ColumnEdit = newCustomImageEditReposioryItem
			dragImageEdit1.EditValue = CreateEditvalue(t)
			MyBase.OnLoad(e)
		End Sub

		Private Sub Form1_CalcRowHeight(ByVal sender As Object, ByVal e As RowHeightEventArgs)
			e.RowHeight = CInt(Fix(e.RowHeight * 1.5))
		End Sub

		Private Function CreateEditvalue(ByVal t As Image) As EditValueInstance
			Dim converter As New ImageConverter()
			Dim evi As New EditValueInstance()
			Dim res() As Byte = CType(converter.ConvertTo(t, GetType(Byte())), Byte())
			evi.Image = res
			evi.Offset = New Point(0, 0)
			Return evi
		End Function

		Private Function CreateItem(ByVal t As Image) As ImageWithOffsetItem
			Dim item As New ImageWithOffsetItem()
			Dim evi As EditValueInstance = CreateEditvalue(t)
			item.InfoImage = evi
			Return item
		End Function

	End Class

	Public Class EditValueInstance
		Private _Offset As Point
		Private _Image() As Byte

		Public Property Image() As Byte()
			Get
				Return _Image
			End Get
			Set(ByVal value As Byte())
				_Image = value
			End Set
		End Property

		Public Property Offset() As Point
			Get
				Return _Offset
			End Get
			Set(ByVal value As Point)
				_Offset = value
			End Set
		End Property
	End Class

	Public Class ImageWithOffsetItem
		Private _Image As EditValueInstance

		Public Property InfoImage() As EditValueInstance
			Get
				Return _Image
			End Get
			Set(ByVal value As EditValueInstance)
				_Image = value
			End Set
		End Property

		Public Sub New()

		End Sub
	End Class
End Namespace