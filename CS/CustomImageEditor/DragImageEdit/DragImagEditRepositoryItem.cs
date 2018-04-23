using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils.Drawing;
using System.Drawing.Imaging;
using DevExpress.Utils;
using DevExpress.XtraPrinting;

namespace CustomImageEditor {
    [UserRepositoryItem("Register")]
    public class DragImagEditRepositoryItem : RepositoryItem {
   
        static DragImagEditRepositoryItem() {
            Register();
        }
        public static void Register() {
            EditorClassInfo myEditorCI = new EditorClassInfo(EditorName, typeof(DragImageEdit), typeof(DragImagEditRepositoryItem), typeof(DragImageEditViewInfo), new DragImagEditPainter(), true);
            EditorRegistrationInfo.Default.Editors.Add(myEditorCI);
        }
        internal const string EditorName = "myImageEdit";
        public override string EditorTypeName {
            get { return EditorName; }
        }
    }
}
