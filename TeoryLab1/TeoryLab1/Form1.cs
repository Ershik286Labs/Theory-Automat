using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TeoryLab1 {
    public partial class Form1 : Form {
        public Horse[] horses = new Horse[Map.sizeMassiveHorse];
        public static List<Horse> winnerHorse = new List<Horse>();

        public string fileSettingsName = "settings.txt";
        private Image mapImage;
        public Form1() {
            InitializeComponent();
            InitializeMap();
            this.DoubleBuffered = true;
            this.Paint += Form1_Paint;
            this.KeyPreview = true; // Важно: форма будет получать события клавиш first
            this.KeyDown += Form1_KeyDown;
            this.KeyPress += Form1_KeyPress;
            this.KeyUp += Form1_KeyUp;
            this.MouseDown += MainForm_MouseDown;
            this.MouseUp += MainForm_MouseUp;
            this.MouseClick += MainForm_MouseClick;
        }
        private void fileComboBox(object sender, EventArgs e) { //File

        }

        private void settingsComboBox(object sender, EventArgs e) {

        }

        private void helpComboBox(object sender, EventArgs e) {

        }

        private void InitializeMap() {
            try {
                string imagePath = Path.Combine(Application.StartupPath, "resourse", "image", "map.png");
                if (File.Exists(imagePath)) {
                    mapImage = Image.FromFile(imagePath);
                }
                else {
                    MessageBox.Show("Файл карты не найден: " + imagePath);
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Ошибка загрузки карты: {ex.Message}");
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            // Рисуем карту как фон
            if (mapImage != null) {
                e.Graphics.DrawImage(mapImage, 0, 0, this.Width, this.Height);
            }
            else {
                // Запасной фон если карта не загружена
                using (SolidBrush brush = new SolidBrush(Color.LightGray)) {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            }

            // Рисуем лошадей поверх карты
            DrawHorses(e.Graphics);
        }

        private void DrawHorses(Graphics g) {
            int trackY = 100; // Y-позиция трека
            int trackWidth = this.ClientSize.Width - 200; // Ширина трека
            int trackStartX = 50; // Начало трека

            // Рисуем всех лошадей
            for (int i = 0; i < horses.Length; i++) {
                int horseTrackY = trackY + (i * 40); // Смещаем каждую лошадь по Y
                horses[i].Draw(g, horseTrackY, trackWidth, trackStartX);
            }

            for (int i = 0; i < winnerHorse.Count; i++) {
                int horseTrackY = trackY + (i * 40); // Смещаем каждую лошадь по Y
                winnerHorse[i].Draw(g, horseTrackY, trackWidth, trackStartX);
                if (i == 0) {
                    winnerHorse[i].drawMedal(g, Color.Gold);
                }
                if (i == 1) {
                    winnerHorse[i].drawMedal(g, Color.DarkGray);
                }
                if (i == 2) {
                    winnerHorse[i].drawMedal(g, Color.RosyBrown);
                }
            }
        }

        //private void AnimationTimer_Tick(object sender, EventArgs e) {
        //    //this.Invalidate();
        //}

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            this.Invalidate(); // Перерисовываем при изменении размера
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e) {

        }
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }
        private void сохранитьCtrlSToolStripMenuItem_Click(object sender, EventArgs e) {
            saveSettings();
        }

        private void startButtonClick(object sender, EventArgs e) {
            Program.SetTimer();
        }

        private void OpenSettingsFile() {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Title = "Выберите файл с данными лошадей";
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.InitialDirectory = Application.StartupPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    try {
                        string filePath = openFileDialog.FileName;
                        string[] lines = File.ReadAllLines(filePath);

                        // Проверяем, что файл содержит минимум 3 строки
                        if (lines.Length < 3) {
                            MessageBox.Show("Файл должен содержать три строки: имена, номера, цвета", "Ошибка",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string[] horseNames = lines[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] numbersStrings = lines[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] colorStrings = lines[2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // Проверяем согласованность данных
                        if (horseNames.Length != numbersStrings.Length || horseNames.Length != colorStrings.Length) {
                            MessageBox.Show("Количество имен, номеров и цветов должно совпадать", "Ошибка",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        int[] numbers = new int[numbersStrings.Length];
                        int[] colorIndices = new int[colorStrings.Length];

                        // Парсим номера
                        for (int i = 0; i < numbersStrings.Length; i++) {
                            if (!int.TryParse(numbersStrings[i], out numbers[i])) {
                                MessageBox.Show($"Ошибка преобразования номера: {numbersStrings[i]}", "Ошибка",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        for (int i = 0; i < colorStrings.Length; i++) {
                            if (!int.TryParse(colorStrings[i], out colorIndices[i])) {
                                MessageBox.Show($"Ошибка преобразования индекса цвета: {colorStrings[i]}", "Ошибка",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (colorIndices[i] < 0 || colorIndices[i] >= Program.horseColor.Length) {
                                MessageBox.Show($"Неверный индекс цвета: {colorIndices[i]}. Допустимые значения: 0-{Program.horseColor.Length - 1}", "Ошибка",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        for (int i = 0; i < horses.Length && i < horseNames.Length; i++) {
                            horses[i].setName(horseNames[i]);
                            horses[i].setNumber(numbers[i]);
                            horses[i].setColor(Program.horseColor[colorIndices[i]]);
                        }

                        this.Invalidate();

                        MessageBox.Show($"Данные успешно загружены из файла!\nЛошадей: {Math.Min(horses.Length, horseNames.Length)}",
                                      "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"Ошибка при открытии файла:\n{ex.Message}",
                                      "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenSettingsFile();
        }

        private void resetButtonClick(object sender, EventArgs e) {
            horses = new Horse[Map.sizeMassiveHorse];
            Program.mainForm.Invalidate();
            Program.generationMassiveHorse();
        }

        private void Pausa_Click(object sender, EventArgs e) {
            if (!Program.timerIsRunning) return;
            Program.toogleTimer();
            if (Program.aTimer.Enabled) {
                Pausa.Text = "Пауза";
            }
            else {
                Pausa.Text = "Продолжить";
            }
            Program.mainForm.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.S) {
                saveSettings();
            }
        }
        private void saveSettings() {

            using (StreamWriter writer = new StreamWriter(fileSettingsName)) {
                for (int i = 0; i < horses.Length; i++) {
                    writer.Write(horses[i].getName() + " ");
                }
                writer.WriteLine();
                for (int i = 0; i < horses.Length; i++) {
                    writer.Write(horses[i].getNumber().ToString() + " ");
                }
                writer.WriteLine();
                for (int i = 0; i < horses.Length; i++) {
                    for (int j = 0; j < Program.horseColor.Length; j++) {
                        if (horses[i].GetColor() == Program.horseColor[j]) {
                            writer.Write(j.ToString() + " ");
                        }
                    }
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {
            // Обработка отпускания клавиши
            if (e.Control && e.KeyCode == Keys.S) {
                saveSettings();
            }
            if (e.Control && e.KeyCode == Keys.O) {
                OpenSettingsFile();
            }
            if (e.KeyCode == Keys.Escape) {
                this.Close();
            }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
            //// Обработка символов
            //if (e.KeyChar == 'a' || e.KeyChar == 'A') {
            //    MessageBox.Show("Нажата клавиша A");
            //    e.Handled = true;
            //}
        }

        private void оРазработчикеToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/Ershik286");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/Ershik286Labs/Theory-Automat/blob/main/TeoryLab1/Inf.txt");
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e) {
            // Нажатие кнопки мыши
            //string button = e.Button.ToString();
            //string location = $"X: {e.X}, Y: {e.Y}";
            //MessageBox.Show($"MouseDown: {button} at {location}");


        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e) {
            // Отпускание кнопки мыши
            if (e.Button == MouseButtons.Left) {
                for (int i = 0; i < horses.Length; i++) {
                    if (horses[i].isCollising(e)) {
                        using (var colorPicker = new ColorPickerForm()) {
                            if (colorPicker.ShowDialog() == DialogResult.OK) {
                                Color col = colorPicker.SelectedColor;
                                bool checkColor = false;
                                for (int hors = 0; hors < horses.Length; hors++) {
                                    if (col == horses[hors].GetColor()) {
                                        checkColor = true;
                                        MessageBox.Show($"Ошибка, выбранный цвет уже используется");
                                    }
                                }
                                if (!checkColor) {
                                    horses[i].setColor(col);
                                    this.Invalidate();
                                    MessageBox.Show($"Цвет лошади {horses[i].getName()} изменен на {colorPicker.SelectedColor.Name}");
                                }
                            }
                        }
                        break;
                    }
                }
            }
            if (e.Button == MouseButtons.Right) {

            }
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e) {
            // Одиночный клик
            //if (e.Button == MouseButtons.Left) {
            //    LeftClickAction(e.Location);
            //}
            //else if (e.Button == MouseButtons.Right) {
            //    RightClickAction(e.Location);
            //}
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e) {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            // Получаем выбранный индекс из ComboBox
            int selectedIndex = toolStripComboBox1.SelectedIndex;
            using (var colorPicker = new ColorPickerForm()) {
                if (colorPicker.ShowDialog() == DialogResult.OK) {
                    Color col = colorPicker.SelectedColor;
                    bool checkColor = false;

                    // Проверяем, не используется ли цвет другими лошадьми
                    for (int hors = 0; hors < horses.Length; hors++) {
                        if (hors != selectedIndex && col == horses[hors].GetColor()) {
                            checkColor = true;
                            MessageBox.Show($"Ошибка: выбранный цвет уже используется лошадью {horses[hors].getName()}");
                            break;
                        }
                    }

                    if (!checkColor) {
                        horses[selectedIndex].setColor(col);
                        this.Invalidate();
                        MessageBox.Show($"Цвет лошади {horses[selectedIndex].getName()} изменен на {col.Name}");
                    }
                }
            }
        }

        private void toolStripComboBox3_Click(object sender, EventArgs e) {

        }

        private void toogleNames_SelectedIndexChanged(object sender, EventArgs e) {
            if (toogleNames.SelectedIndex != -1) {
                textNumberHorse.Visible = true;
                this.Invalidate();
            }
        }


        private void toogleNumber_SelectedIndexChanged(object sender, EventArgs e) {
            if (toogleNumber.SelectedIndex != -1) {
                textNumberHorse.Visible = true;
                this.Invalidate();
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Проверяем, что лошадь выбрана
                if (toogleNames.SelectedIndex == -1) {
                    textNumberHorse.Text = "";
                    MessageBox.Show("Сначала выберите лошадь из списка!");
                    return;
                }

                int selectedHorseIndex = toogleNames.SelectedIndex;
                string newNumber = toolStripTextBox1.Text.Trim();

                if (string.IsNullOrEmpty(newNumber)) {
                    MessageBox.Show("Введите номер лошади!");
                    return;
                }

                horses[selectedHorseIndex].setName(toolStripTextBox1.Text);
                MessageBox.Show($"Имя {selectedHorseIndex + 1}-й лошади изменен на {toolStripComboBox1.Text}");
                toolStripTextBox1.Text = "";
                toolStripTextBox1.Visible = false;
            }
        }

        private void textNumberHorse_TextChanged(object sender, EventArgs e) {

        }

        private void textNumberHorse_KeyPress(object sender, KeyPressEventArgs e) {
            if (char.IsControl(e.KeyChar)) {
                return;
            }
            if (!char.IsDigit(e.KeyChar)) {
                e.Handled = true; // Отменяем ввод
            }
        }

        private void textNumberHorse_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Проверяем, что лошадь выбрана
                if (toogleNumber.SelectedIndex == -1) {
                    textNumberHorse.Text = "";
                    MessageBox.Show("Сначала выберите лошадь из списка!");
                    return;
                }

                int selectedHorseIndex = toogleNumber.SelectedIndex;
                string newNumber = textNumberHorse.Text.Trim();

                if (string.IsNullOrEmpty(newNumber)) {
                    MessageBox.Show("Введите номер лошади!");
                    return;
                }

                // Безопасное преобразование
                if (int.TryParse(newNumber, out int parsedNumber)) {
                    // Дополнительные проверки
                    if (parsedNumber <= 0) {
                        textNumberHorse.Text = "";
                        MessageBox.Show("Номер должен быть положительным числом!");
                        return;
                    }
                    horses[selectedHorseIndex].setNumber(parsedNumber);
                    MessageBox.Show($"Номер {selectedHorseIndex + 1}-й лошади изменен на {parsedNumber}");
                    textNumberHorse.Text = "";
                    textNumberHorse.Visible = false;
                }
                else {
                    textNumberHorse.Text = "";
                    MessageBox.Show("Введите корректное число!");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            checkTrackBar();
        }

        public void checkTrackBar() {
            if (!Program.timerIsRunning) return;
            int selectedValue = trackBar1.Value + 1;
            Program.aTimer.Interval = selectedValue * 100;
        }
    }
} 
