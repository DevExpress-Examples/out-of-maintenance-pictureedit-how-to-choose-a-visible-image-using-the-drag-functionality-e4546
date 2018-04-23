Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

Namespace CustomImageEditor
	<ToolboxItem(True)> _
	Public Class DragImageEdit
		Inherits BaseEdit
		Private _ImagePos As Point
		Private _MouseDownPoint As Point
		Private leftMouseButtonDowned As Boolean = False
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Private HandCursor, HandDragCursor As Cursor

		Private initPoint As Point
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overridable Property Image() As Image
            Get
                If TypeOf EditValue Is EditValueInstance Then
                    Dim ev As EditValueInstance = TryCast(EditValue, EditValueInstance)
                    If (ev Is Nothing) AndAlso (Me.Image Is Nothing) Then
                        Return Nothing
                    End If
                    If (ev Is Nothing) AndAlso (Me.Image IsNot Nothing) Then
                        Return Me.Image
                    End If
                    Return ByteArrayToImage(ev.Image)
                End If
                Return Nothing
            End Get
            Set(ByVal value As Image)
                TryCast(EditValue, EditValueInstance).Image = ImageToByteArray(value)
            End Set
        End Property

		Friend Function ByteArrayToImage(ByVal inp() As Byte) As Image
			Return ByteImageConverter.FromByteArray(ByteImageConverter.ToByteArray(inp))
		End Function

		Friend Function ImageToByteArray(ByVal inp As Image) As Byte()
			Return ByteImageConverter.ToByteArray(inp)
		End Function
		Protected Friend Shadows ReadOnly Property ViewInfo() As DragImageEditViewInfo
			Get
				Return TryCast(MyBase.ViewInfo, DragImageEditViewInfo)
			End Get
		End Property

		Shared Sub New()
			DragImagEditRepositoryItem.Register()
		End Sub

		Public Sub New()
			HandCursor = DevExpress.Utils.CursorsHelper.LoadFromResource("DevExpress.Utils.Cursors.CursorHand.cur", GetType(DevExpress.Utils.CursorsHelper).Assembly)
			HandDragCursor = DevExpress.Utils.CursorsHelper.LoadFromResource("DevExpress.Utils.Cursors.CursorHandDrag.cur", GetType(DevExpress.Utils.CursorsHelper).Assembly)

		End Sub
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return DragImagEditRepositoryItem.EditorName
			End Get
		End Property
		Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
			If e.Button = System.Windows.Forms.MouseButtons.Left Then
				_MouseDownPoint = e.Location
				Dim ev As EditValueInstance = TryCast(Me.EditValue, EditValueInstance)
				If ev Is Nothing Then
					Return
				End If
                If (ev Is Nothing) OrElse (IsNothing(ev.Offset)) Then
                    initPoint = New Point(0, 0)
                Else
                    initPoint = ev.Offset
                End If
				leftMouseButtonDowned = True
				If (Not Me.IsDesignMode) Then
					Me.Cursor = HandDragCursor
					Me.Parent.Cursor = HandDragCursor
				End If
			End If
			MyBase.OnMouseDown(e)

		End Sub

		Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
			MyBase.OnMouseMove(e)
			If (Not leftMouseButtonDowned) Then
				Return
			End If
			Dim nowX As Integer = initPoint.X
			Dim nowY As Integer = initPoint.Y
			_ImagePos = New Point(SetX(nowX, _MouseDownPoint.X, e.X), SetY(nowY, _MouseDownPoint.Y, e.Y))
			Me.EditValue = New EditValueInstance() With {.Image = (TryCast(Me.EditValue, EditValueInstance)).Image, .Offset = _ImagePos}
			Me.LayoutChanged()
		End Sub

		Private Function SetX(ByVal nowX As Integer, ByVal mouseDownY As Integer, ByVal ex As Integer) As Integer
			Dim res As Integer = nowX + (mouseDownY - ex)
			If (res < 0) OrElse (Me.Image.Width<Me.Width) Then
				Return 0
			ElseIf res > (Me.Image.Width - Me.Width) Then
				Return (Me.Image.Width - Me.Width)
			End If
			Return res
		End Function
		Private Function SetY(ByVal nowY As Integer, ByVal mouseDownY As Integer, ByVal ey As Integer) As Integer
			Dim res As Integer = nowY + (mouseDownY - ey)
			If res < 0 Then
				Return 0
			ElseIf res > (Me.Image.Height - Me.Height) Then
				Return (Me.Image.Height - Me.Height)
			End If
			Return res
		End Function

		Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
			If e.Button = System.Windows.Forms.MouseButtons.Left Then
				leftMouseButtonDowned = False
				_ImagePos = New Point(0, 0)
				If (Not Me.IsDesignMode) Then
					Me.Cursor = HandCursor
					Me.Parent.Cursor = Me.DefaultCursor
				End If
			End If
			MyBase.OnMouseUp(e)
		End Sub

	End Class
End Namespace
