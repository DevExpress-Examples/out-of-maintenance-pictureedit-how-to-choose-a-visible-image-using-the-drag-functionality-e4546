Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils.Drawing

Namespace CustomImageEditor
	Public Class DragImagEditPainter
		Inherits BaseEditPainter
		Protected Overrides Sub DrawContent(ByVal info As ControlGraphicsInfoArgs)
			MyBase.DrawContent(info)
			DrawImage(info)
		End Sub
		Protected Overridable Sub DrawImage(ByVal info As ControlGraphicsInfoArgs)
			Dim vi As DragImageEditViewInfo = TryCast(info.ViewInfo, DragImageEditViewInfo)
			If Not(TypeOf vi.EditValue Is EditValueInstance) Then
				vi.PaintAppearance.DrawString(info.Cache, "incorrect editvalue", New Rectangle(New Point(vi.Bounds.Location.X +5,vi.Bounds.Location.Y),vi.Bounds.Size), vi.PaintAppearance.GetStringFormat(vi.DefaultTextOptions))
				Return
			End If
			Dim imageSize As Size = Size.Empty
			Try
				If vi.Image IsNot Nothing Then
					imageSize = vi.Image.Size
				End If
			Catch
			End Try
			If vi.Image Is Nothing OrElse imageSize.IsEmpty Then
				Dim text As String = Localizer.Active.GetLocalizedString(StringId.DataEmpty)
				If vi.Item.NullText IsNot Nothing AndAlso vi.Item.NullText.Length > 0 Then
					text = vi.Item.NullText
				End If
				vi.PaintAppearance.DrawString(info.Cache, text, vi.Bounds, vi.PaintAppearance.GetStringFormat(vi.DefaultTextOptions))
				Return
			End If
			Dim x, y As Integer
			Dim r As Rectangle = vi.Bounds
			x = -vi.HorizontOffset
			y = -vi.VerticalOffset
			Dim state As GraphicsClipState = info.Cache.ClipInfo.SaveAndSetClip(vi.ContentRect)
			Try
				info.Cache.Paint.DrawImage(info.Graphics, vi.Image, New Rectangle(r.X + x, r.Y + y, vi.Image.Width, vi.Image.Height), New Rectangle(0, 0, vi.Image.Width, vi.Image.Height), True)
			Finally
				info.Cache.ClipInfo.RestoreClipRelease(state)
			End Try
		End Sub

	End Class
End Namespace
