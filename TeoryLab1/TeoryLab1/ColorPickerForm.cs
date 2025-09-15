using System;
using System.Drawing;
using System.Windows.Forms;

namespace TeoryLab1 {
    public partial class ColorPickerForm : Form {
        public Color SelectedColor { get; private set; } = Color.Black;

        public ColorPickerForm() {
            InitializeComponent();
            InitializeColorButtons();
        }

        private void InitializeColorButtons() {
            this.Text = "Выберите цвет";
            this.Size = new Size(375, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int buttonSize = 40;
            int margin = 10;
            int buttonsPerRow = 7;

            for (int i = 0; i < Program.horseColor.Length; i++) {
                var colorButton = new Button {
                    BackColor = Program.horseColor[i],
                    Size = new Size(buttonSize, buttonSize),
                    Location = new Point(
                        margin + (i % buttonsPerRow) * (buttonSize + margin),
                        margin + (i / buttonsPerRow) * (buttonSize + margin)
                    ),
                    Tag = Program.horseColor[i],
                    FlatStyle = FlatStyle.Flat
                };

                colorButton.FlatAppearance.BorderSize = 2;
                colorButton.FlatAppearance.BorderColor = Color.Black;
                colorButton.Click += ColorButton_Click;

                this.Controls.Add(colorButton);
            }

            // Кнопка отмены
            var cancelButton = new Button {
                Text = "Отмена",
                Size = new Size(100, 30),
                Location = new Point(125, 180)
            };
            cancelButton.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.Add(cancelButton);
        }

        private void ColorButton_Click(object sender, EventArgs e) {
            if (sender is Button button && button.Tag is Color color) {
                SelectedColor = color;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ColorPickerForm_Load(object sender, EventArgs e) {
            // Пустая реализация, если не нужна
        }
    }
}