using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Controls;

namespace CustomImageEditor {
    public class DragImageEditViewInfo : BaseEditViewInfo {
        Image image;
        int _VerticalOffset, _HorizontOffset;
        public Image Image {
            get { return image; }
            set {
                image = value;
            }
        }
        public int VerticalOffset {
            get { return _VerticalOffset;}
            set{_VerticalOffset = value;}
        }
        public int HorizontOffset {
            get {return _HorizontOffset;}
            set{_HorizontOffset = value;}
        }
   
        protected override void OnEditValueChanged() {
            if(!(EditValue is EditValueInstance)) return;
            if((EditValue != null) && (EditValue as EditValueInstance).Image != null)
                this.image = ByteImageConverter.FromByteArray(ByteImageConverter.ToByteArray((EditValue as EditValueInstance).Image));
            Point offset = new Point(0, 0);
            if((EditValue as EditValueInstance) != null) { offset = (EditValue as EditValueInstance).Offset; }
            if((EditValue != null) && (offset != null)) {
                this.HorizontOffset = offset.X;
                this.VerticalOffset = offset.Y;
            }
        }

        protected override void Assign(BaseControlViewInfo info) {
            base.Assign(info);
            DragImageEditViewInfo be = info as DragImageEditViewInfo;
            if(be == null)
                return;
            this.Image = be.Image;
            this._HorizontOffset = be._HorizontOffset;
            this._VerticalOffset = be._VerticalOffset;
        }

        public override void Reset() {
            if(EditValue != null) {
                _HorizontOffset = 0;
                _VerticalOffset = 0;
            }
            base.Reset();
            this.image = null;
           
        }
        public new DragImagEditRepositoryItem Item { get { return base.Item as DragImagEditRepositoryItem; } }
        public DragImageEditViewInfo(RepositoryItem item)
            : base(item) {
        }
    }
}
