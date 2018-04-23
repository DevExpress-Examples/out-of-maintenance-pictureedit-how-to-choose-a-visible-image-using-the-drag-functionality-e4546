Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Controls

Namespace CustomImageEditor
	Public Class DragImageEditViewInfo
		Inherits BaseEditViewInfo
		Private image_Renamed As Image
		Private _VerticalOffset, _HorizontOffset As Integer
		Public Property Image() As Image
			Get
				Return image_Renamed
			End Get
			Set(ByVal value As Image)
				image_Renamed = value
			End Set
		End Property
		Public Property VerticalOffset() As Integer
			Get
				Return _VerticalOffset
			End Get
			Set(ByVal value As Integer)
				_VerticalOffset = value
			End Set
		End Property
		Public Property HorizontOffset() As Integer
			Get
				Return _HorizontOffset
			End Get
			Set(ByVal value As Integer)
				_HorizontOffset = value
			End Set
		End Property

		Protected Overrides Sub OnEditValueChanged()
			If Not(TypeOf EditValue Is EditValueInstance) Then
				Return
			End If
			If (EditValue IsNot Nothing) AndAlso (TryCast(EditValue, EditValueInstance)).Image IsNot Nothing Then
				Me.image_Renamed = ByteImageConverter.FromByteArray(ByteImageConverter.ToByteArray((TryCast(EditValue, EditValueInstance)).Image))
			End If
			Dim offset As New Point(0, 0)
			If (TryCast(EditValue, EditValueInstance)) IsNot Nothing Then
				offset = (TryCast(EditValue, EditValueInstance)).Offset
			End If
            If (EditValue IsNot Nothing) AndAlso (Not IsNothing(offset)) Then
                Me.HorizontOffset = offset.X
                Me.VerticalOffset = offset.Y
            End If
		End Sub

		Protected Overrides Sub Assign(ByVal info As BaseControlViewInfo)
			MyBase.Assign(info)
			Dim be As DragImageEditViewInfo = TryCast(info, DragImageEditViewInfo)
			If be Is Nothing Then
				Return
			End If
			Me.Image = be.Image
			Me._HorizontOffset = be._HorizontOffset
			Me._VerticalOffset = be._VerticalOffset
		End Sub

		Public Overrides Sub Reset()
			If EditValue IsNot Nothing Then
				_HorizontOffset = 0
				_VerticalOffset = 0
			End If
			MyBase.Reset()
			Me.image_Renamed = Nothing

		End Sub
		Public Shadows ReadOnly Property Item() As DragImagEditRepositoryItem
			Get
				Return TryCast(MyBase.Item, DragImagEditRepositoryItem)
			End Get
		End Property
		Public Sub New(ByVal item As RepositoryItem)
			MyBase.New(item)
		End Sub
	End Class
End Namespace
