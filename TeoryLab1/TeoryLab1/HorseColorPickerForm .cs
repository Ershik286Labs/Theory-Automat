using System;
using System.Drawing;
using System.Windows.Forms;

namespace TeoryLab1 {
    public partial class HorseColorPickerForm : Form {
        public Color SelectedColor { get; private set; } = Color.Black;
        public int SelectedHorseIndex { get; private set; } = -1;
        private Horse[] horses;

        private ColorComboBox[] colorComboBoxes;

        public HorseColorPickerForm(Horse[] horses) {
            this.horses = horses;
            InitializeForm();
        }

        private void InitializeForm() {
            this.Text = "Выбор цветов для лошадей";
            this.Size = new Size(400, 450);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            colorComboBoxes = new ColorComboBox[horses.Length];

            var panel = new Panel {
                Location = new Point(10, 10),
                Size = new Size(365, 350),
                AutoScroll = true
            };

            // Заполняем доступные цвета
            ColorItem[] availableColors = new ColorItem[Program.horseColor.Length];
            for (int i = 0; i < Program.horseColor.Length; i++) {
                availableColors[i] = new ColorItem(Program.horseColor[i],
                                                 Program.horseColor[i].Name);
            }

            // Создаем комбобоксы для каждой лошади
            for (int i = 0; i < horses.Length; i++) {
                // Label с именем лошади
                var label = new Label {
                    Text = horses[i].getName() + ":",
                    Location = new Point(10, 10 + i * 45),
                    Size = new Size(80, 20),
                    TextAlign = ContentAlignment.MiddleRight
                };

                // ComboBox для выбора цвета
                var comboBox = new ColorComboBox {
                    Location = new Point(100, 10 + i * 45),
                    Size = new Size(200, 30),
                    Tag = i // храним индекс лошади
                };

                // Заполняем доступными цветами
                comboBox.Items.AddRange(availableColors);

                // Устанавливаем текущий цвет лошади
                for (int j = 0; j < availableColors.Length; j++) {
                    if (availableColors[j].Color == horses[i].GetColor()) {
                        comboBox.SelectedIndex = j;
                        break;
                    }
                }

                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                colorComboBoxes[i] = comboBox;

                panel.Controls.Add(label);
                panel.Controls.Add(comboBox);
            }

            this.Controls.Add(panel);

            // Кнопки
            var okButton = new Button {
                Text = "OK",
                Size = new Size(80, 30),
                Location = new Point(120, 370),
                DialogResult = DialogResult.OK
            };
            okButton.Click += OkButton_Click;

            var cancelButton = new Button {
                Text = "Отмена",
                Size = new Size(80, 30),
                Location = new Point(210, 370),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (sender is ColorComboBox comboBox && comboBox.Tag is int horseIndex) {
                // Проверяем, не занят ли цвет другой лошадью
                if (comboBox.SelectedItem is ColorItem selectedColor) {
                    for (int i = 0; i < colorComboBoxes.Length; i++) {
                        if (i != horseIndex && colorComboBoxes[i].SelectedItem is ColorItem currentColor) {
                            if (currentColor.Color == selectedColor.Color) {
                                // Цвет уже занят, возвращаем предыдущий выбор
                                MessageBox.Show($"Этот цвет уже используется лошадью {horses[i].getName()}",
                                              "Цвет занят", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                // Восстанавливаем предыдущий цвет
                                for (int j = 0; j < comboBox.Items.Count; j++) {
                                    if (comboBox.Items[j] is ColorItem item && item.Color == horses[horseIndex].GetColor()) {
                                        comboBox.SelectedIndex = j;
                                        break;
                                    }
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e) {
            // Применяем все изменения цветов
            for (int i = 0; i < colorComboBoxes.Length; i++) {
                if (colorComboBoxes[i].SelectedItem is ColorItem selectedColor) {
                    horses[i].setColor(selectedColor.Color);
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void HorseColorPickerForm_Load(object sender, EventArgs e) {
            // Пустая реализация
        }
    }
}