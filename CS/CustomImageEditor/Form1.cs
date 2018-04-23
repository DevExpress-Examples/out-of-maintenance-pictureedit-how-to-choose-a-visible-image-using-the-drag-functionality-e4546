using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace CustomImageEditor {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e) {
            BindingList<ImageWithOffsetItem> ds = new BindingList<ImageWithOffsetItem>();
            Image t = CustomImageEditor.Properties.Resources.Penguins as Image;
            ImageConverter converter = new ImageConverter();
            for(int i = 0; i < 10; i++) {
                ImageWithOffsetItem item = CreateItem(t);
                ds.Add(item);
                dragImageEdit1.EditValue = item;
            }
            
            gridControl1.DataSource = ds;
            DragImagEditRepositoryItem newCustomImageEditReposioryItem = new DragImagEditRepositoryItem();
            gridControl1.RepositoryItems.Add(newCustomImageEditReposioryItem);
            (gridControl1.MainView as GridView).CalcRowHeight += Form1_CalcRowHeight;
            (gridControl1.MainView as GridView).Columns["InfoImage"].ColumnEdit = newCustomImageEditReposioryItem;
            dragImageEdit1.EditValue = CreateEditvalue(t);
            base.OnLoad(e);
        }

        void Form1_CalcRowHeight(object sender, RowHeightEventArgs e) {
            e.RowHeight = (int)(e.RowHeight * 1.5);
        }

        EditValueInstance CreateEditvalue(Image t) {
            ImageConverter converter = new ImageConverter();
            EditValueInstance evi = new EditValueInstance();
            byte[] res = (byte[])converter.ConvertTo(t, typeof(byte[]));
            evi.Image = res;
            evi.Offset = new Point(0, 0);
            return evi;
        }

        ImageWithOffsetItem CreateItem(Image t) {
            ImageWithOffsetItem item = new ImageWithOffsetItem();
            EditValueInstance evi = CreateEditvalue(t);
            item.InfoImage = evi;
            return item;
        }

    }

    public class EditValueInstance {
        private Point _Offset;
        private byte[] _Image;

        public byte[] Image {
            get { return _Image; }
            set { _Image = value; }
        }

        public Point Offset {
            get { return _Offset; }
            set { _Offset = value; }
        }
    }

    public class ImageWithOffsetItem {
        private EditValueInstance _Image;

        public EditValueInstance InfoImage {
            get { return _Image; }
            set { _Image = value; }
        }

        public ImageWithOffsetItem() {
            
        }
    }
}