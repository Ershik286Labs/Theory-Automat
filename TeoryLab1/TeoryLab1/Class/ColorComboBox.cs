using System.Drawing;
using System.Windows.Forms;

namespace TeoryLab1 {
    public class ColorComboBox : ComboBox {
        public ColorComboBox() {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ItemHeight = 30;
        }

        protected override void OnDrawItem(DrawItemEventArgs e) {
            if (e.Index < 0) return;

            e.DrawBackground();

            if (this.Items[e.Index] is ColorItem colorItem) {
                // Рисуем цветной квадрат
                Rectangle colorRect = new Rectangle(e.Bounds.Left + 2, e.Bounds.Top + 2,
                                                  25, e.Bounds.Height - 4);
                using (var brush = new SolidBrush(colorItem.Color)) {
                    e.Graphics.FillRectangle(brush, colorRect);
                }
                e.Graphics.DrawRectangle(Pens.Black, colorRect);

                // Рисуем текст
                Rectangle textRect = new Rectangle(e.Bounds.Left + 35, e.Bounds.Top,
                                                 e.Bounds.Width - 35, e.Bounds.Height);
                using (var brush = new SolidBrush(e.ForeColor)) {
                    e.Graphics.DrawString(colorItem.Name, e.Font, brush, textRect,
                                        StringFormat.GenericDefault);
                }
            }

            e.DrawFocusRectangle();
        }
    }

    public class ColorItem {
        public Color Color { get; set; }
        public string Name { get; set; }

        public ColorItem(Color color, string name) {
            Color = color;
            Name = name;
        }

        public override string ToString() {
            return Name;
        }
    }
}