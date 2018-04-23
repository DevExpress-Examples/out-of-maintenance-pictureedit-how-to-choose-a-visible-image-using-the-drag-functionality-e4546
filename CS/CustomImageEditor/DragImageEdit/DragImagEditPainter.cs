using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils.Drawing;

namespace CustomImageEditor {
    public class DragImagEditPainter : BaseEditPainter {
        protected override void DrawContent(ControlGraphicsInfoArgs info) {
            base.DrawContent(info);
            DrawImage(info);
        }
        protected virtual void DrawImage(ControlGraphicsInfoArgs info) {
            DragImageEditViewInfo vi = info.ViewInfo as DragImageEditViewInfo;
            if(!(vi.EditValue is EditValueInstance)) {
                vi.PaintAppearance.DrawString(info.Cache, "incorrect editvalue", new Rectangle(new Point(vi.Bounds.Location.X +5,vi.Bounds.Location.Y),vi.Bounds.Size), vi.PaintAppearance.GetStringFormat(vi.DefaultTextOptions));
                return;
            }
            Size imageSize = Size.Empty;
            try { if(vi.Image != null) imageSize = vi.Image.Size; }
            catch { }
            if(vi.Image == null || imageSize.IsEmpty) {
                string text = Localizer.Active.GetLocalizedString(StringId.DataEmpty);
                if(vi.Item.NullText != null && vi.Item.NullText.Length > 0) text = vi.Item.NullText;
                vi.PaintAppearance.DrawString(info.Cache, text, vi.Bounds, vi.PaintAppearance.GetStringFormat(vi.DefaultTextOptions));
                return;
            }
            int x, y;
            Rectangle r = vi.Bounds;
            x = -vi.HorizontOffset;
            y = -vi.VerticalOffset;
            GraphicsClipState state = info.Cache.ClipInfo.SaveAndSetClip(vi.ContentRect);
            try {
                info.Cache.Paint.DrawImage(info.Graphics, vi.Image,
                    new Rectangle(r.X + x, r.Y + y, vi.Image.Width, vi.Image.Height),
                    new Rectangle(0, 0, vi.Image.Width, vi.Image.Height), true);
            }
            finally {
                info.Cache.ClipInfo.RestoreClipRelease(state);
            }
        }

    }
}
