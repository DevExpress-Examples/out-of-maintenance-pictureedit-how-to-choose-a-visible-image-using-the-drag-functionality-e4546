using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace CustomImageEditor {
    [ToolboxItem(true)]
    public class DragImageEdit : BaseEdit {
        private Point _ImagePos;
        private Point _MouseDownPoint;
        private bool leftMouseButtonDowned = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private Cursor HandCursor, HandDragCursor;

        private Point initPoint;
        [DXCategory(CategoryName.Appearance), DefaultValue(null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Image Image {
            get {
                if(EditValue is EditValueInstance) {
                    EditValueInstance ev = EditValue as EditValueInstance;
                    if((ev == null) && (this.Image == null)) return null;
                    if((ev == null) && (this.Image != null)) return this.Image;
                    return ByteArrayToImage(ev.Image);
                }
                return null;
            }
            set {
                (EditValue as EditValueInstance).Image = ImageToByteArray(value);
            }
        }

        internal Image ByteArrayToImage(byte[] inp) {
            return ByteImageConverter.FromByteArray(ByteImageConverter.ToByteArray(inp));
        }

        internal byte[] ImageToByteArray(Image inp) {
            return ByteImageConverter.ToByteArray(inp);
        }
        protected internal new DragImageEditViewInfo ViewInfo { get { return base.ViewInfo as DragImageEditViewInfo; } }
        
        static DragImageEdit() {
            DragImagEditRepositoryItem.Register();
        }
        
        public DragImageEdit() {
            HandCursor = DevExpress.Utils.CursorsHelper.LoadFromResource("DevExpress.Utils.Cursors.CursorHand.cur", typeof(DevExpress.Utils.CursorsHelper).Assembly);
            HandDragCursor = DevExpress.Utils.CursorsHelper.LoadFromResource("DevExpress.Utils.Cursors.CursorHandDrag.cur", typeof(DevExpress.Utils.CursorsHelper).Assembly);
		
        }
        public override string EditorTypeName {
            get { return DragImagEditRepositoryItem.EditorName; }
        }
        protected override void OnMouseDown(MouseEventArgs e) {
            if(e.Button == System.Windows.Forms.MouseButtons.Left) {
                _MouseDownPoint = e.Location;
                EditValueInstance ev = this.EditValue as EditValueInstance;
                if(ev == null) return;
                if((ev == null) || (ev.Offset == null)) {
                    initPoint = new Point(0, 0);
                }
                else { initPoint = ev.Offset; }
                leftMouseButtonDowned = true;
                if(!this.IsDesignMode) {
                    this.Cursor = HandDragCursor;
                    this.Parent.Cursor = HandDragCursor;
                }
            }
            base.OnMouseDown(e);
            
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if(!leftMouseButtonDowned) {
                return; }
            int nowX = initPoint.X;
            int nowY = initPoint.Y;
            _ImagePos = new Point(SetX(nowX, _MouseDownPoint.X, e.X), SetY(nowY, _MouseDownPoint.Y, e.Y));
            this.EditValue = new EditValueInstance() { Image = (this.EditValue as EditValueInstance).Image, Offset = _ImagePos };
            this.LayoutChanged();
        }

        private int SetX(int nowX, int mouseDownY, int ex) {
            int res = nowX + (mouseDownY - ex);
            if((res < 0) || (this.Image.Width<this.Width)) {
                return 0;
            }
            else if(res > (this.Image.Width - this.Width))
                return (this.Image.Width - this.Width);
            return res;
        }
        private int SetY(int nowY, int mouseDownY, int ey) {
            int res = nowY + (mouseDownY - ey);
            if(res < 0) { return 0; }
            else if(res > (this.Image.Height - this.Height)) { return (this.Image.Height - this.Height); }
            return res;
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            if(e.Button == System.Windows.Forms.MouseButtons.Left) {
                leftMouseButtonDowned = false;
                _ImagePos = new Point(0, 0);
                if(!this.IsDesignMode) {
                    this.Cursor = HandCursor;
                    this.Parent.Cursor = this.DefaultCursor;
                }
            }
            base.OnMouseUp(e);
        }

    }
}
