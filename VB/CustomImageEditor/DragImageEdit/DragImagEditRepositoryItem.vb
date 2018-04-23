Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils.Drawing
Imports System.Drawing.Imaging
Imports DevExpress.Utils
Imports DevExpress.XtraPrinting

Namespace CustomImageEditor
	<UserRepositoryItem("Register")> _
	Public Class DragImagEditRepositoryItem
		Inherits RepositoryItem

		Shared Sub New()
			Register()
		End Sub
		Public Shared Sub Register()
			Dim myEditorCI As New EditorClassInfo(EditorName, GetType(DragImageEdit), GetType(DragImagEditRepositoryItem), GetType(DragImageEditViewInfo), New DragImagEditPainter(), True)
			EditorRegistrationInfo.Default.Editors.Add(myEditorCI)
		End Sub
		Friend Const EditorName As String = "myImageEdit"
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return EditorName
			End Get
		End Property
	End Class
End Namespace
