using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
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
        private bool stopFlag = false;
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
            if (stopFlag) return;
        }

        private void settingsComboBox(object sender, EventArgs e) {
            if (stopFlag) return;
        }

        private void helpComboBox(object sender, EventArgs e) {
            if (stopFlag) return;
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
                using (SolidBrush brush = new SolidBrush(Color.LightGray)) {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            }

            // Рисуем лошадей поверх карты
            DrawHorses(e.Graphics);
        }

        private void DrawHorses(Graphics g) {
            int trackY = 100; // Y-позиция трека
            int trackWidth = this.ClientSize.Width - 200;
            int trackStartX = 50; // Начало трека

            // Рисуем всех лошадей
            for (int i = 0; i < horses.Length; i++) {
                int horseTrackY = trackY + (i * 40);
                horses[i].Draw(g, horseTrackY, trackWidth, trackStartX);
            }

            for (int i = 0; i < winnerHorse.Count; i++) {
                int horseTrackY = trackY + (i * 40);
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
            if (stopFlag) return;
            base.OnResize(e);
            this.Invalidate(); // Перерисовываем при изменении размера
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e) {
            if (stopFlag) return;
        }
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e) {
            if (stopFlag) return;
            this.Close();
        }
        private void сохранитьCtrlSToolStripMenuItem_Click(object sender, EventArgs e) {
            if (stopFlag) return;
            saveSettings();
        }

        private void startButtonClick(object sender, EventArgs e) {
            if (Start.Text == "Старт") { 
                Start.Text = "Стоп";
                winnerHorse.Clear();
                Program.generationMassiveHorse();
                Program.SetTimer();
                this.Invalidate();
                this.stopFlag = false;
            }
            else { 
                Start.Text = "Старт";
                Program.aTimer.Stop();
                this.stopFlag = true;
            }
        }

        private void OpenSettingsFile() {
            if (stopFlag) return;
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
                        ShowAutoCloseMessage("Данные успешно загружены из файла!", "Уведомление", 3000);
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"Ошибка при открытии файла:\n{ex.Message}",
                                      "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) {
            if (stopFlag) return;
            OpenSettingsFile();
        }

        private void resetButtonClick(object sender, EventArgs e) {
            if (stopFlag) return;
            horses = new Horse[Map.sizeMassiveHorse];
            winnerHorse.Clear();
            Program.generationMassiveHorse();
            Program.mainForm.Invalidate();
        }

        private void Pausa_Click(object sender, EventArgs e) {
            if (stopFlag) return;
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
            if (stopFlag) return;
            if (e.Control && e.KeyCode == Keys.S) {
                saveSettings();
            }
        }
        private void saveSettings() {
            if (stopFlag) return;
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
            if (stopFlag) return;
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
            if (stopFlag) return;
            //Process.Start("https://github.com/Ershik286");
            MessageBox.Show("Разработчик - Никита, Ershik286. При желании можете подписаться на GitHub https://github.com/Ershik286");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e) {
            if (stopFlag) return;
            MessageBox.Show($"Как пользоваться программой\r\n" +
                                $"1. Кнопка Старт - запускает программу, Стоп - останавливает полностью программу.\n" +
                                $"2. Кнопка Пауза/продолжить по аналогии запускает или останавливает движение лошадок по экрану.\n" +
                                $"3. Ползунок в верхней-правой части экрана - регулирует скорость лошадей.\n" +
                                $"4. Кнопка сброс - запускает игру заного, сбрасывает все настройки у лошадей и заного их генерирует.\n" +
                                $"5. У каждой лошадки можно выбирать цвет, её номер и имя.\n  " +
                                $"5.1 Для выбора цвета - нажмите по лошадке Левой Кнопкой Мыши, либо нажмите кнопку \"Настройки\", затем \"Цвет\", затем выберайте желаемую лошадку, после откроется окно с выбором цвета.\n  " +
                                $"5.2 Для выбора номера - нажмите по кнопке \"Настройки\", затем \"Номер\", выберайте нужную лошадку и введите в появившееся серое окошечко желаемый номер\n  " +
                                $"5.3 Для выбора имени - нажмите по кнопке \"Настройки\", затем \"Имя\", выберайте нужную лошадку и введите в появившееся серое окошечко желаемый номер\n" +
                                $"6. Возможно сохранить настройки у лошадей. Возможно сохранение лишь одних настроек. При сохранении предыдущие настройки будут удалены. Сохранение происходит либо через комбинацию клавиш Ctrl + S, или: Нажатие на кнопку \"Файл\", \"Сохранить\"\n" +
                                $"7. Открытие настроек происходит через кнопки Ctrl + O, или через \"Файл\", \"Открыть\", затем выбирается текстовый файл settings.txt.");
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e) {
            // Нажатие кнопки мыши
            //string button = e.Button.ToString();
            //string location = $"X: {e.X}, Y: {e.Y}";
            //MessageBox.Show($"MouseDown: {button} at {location}");


        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e) {
            if (stopFlag) return;

            if (e.Button == MouseButtons.Left) {

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
            if (stopFlag) return;
            using (var colorPicker = new HorseColorPickerForm(horses)) {
                if (colorPicker.ShowDialog() == DialogResult.OK) {
                    Color selectedColor = colorPicker.SelectedColor;
                    int selectedHorseIndex = colorPicker.SelectedHorseIndex;

                    if (selectedHorseIndex >= 0 && selectedHorseIndex < horses.Length) {
                        horses[selectedHorseIndex].setColor(selectedColor);
                        this.Invalidate();
                        ShowAutoCloseMessage($"Цвет лошади {horses[selectedHorseIndex].getName()} изменен",
                                            "Уведомление", 3000);
                    }
                }
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e) {
            if (stopFlag) return;
        }

        private void toolStripComboBox3_Click(object sender, EventArgs e) {

        }

        private void toogleNames_SelectedIndexChanged(object sender, EventArgs e) {
            if (stopFlag) return;
            if (toogleNames.SelectedIndex != -1) {
                textNumberHorse.Visible = true;
                this.Invalidate();
            }
        }


        private void toogleNumber_SelectedIndexChanged(object sender, EventArgs e) {
            if (stopFlag) return;
            if (toogleNumber.SelectedIndex != -1) {
                textNumberHorse.Visible = true;
                this.Invalidate();
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e) {
            if (stopFlag) return;
            if (e.KeyCode == Keys.Enter) {
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Проверяем, что лошадь выбрана
                if (toogleNames.SelectedIndex == -1) {
                    textNumberHorse.Text = "";
                    ShowAutoCloseMessage("Сначала выберите лошадь из списка!", "Уведомление", 3000);
                    return;
                }

                int selectedHorseIndex = toogleNames.SelectedIndex;
                string newNumber = toolStripTextBox1.Text.Trim();

                if (string.IsNullOrEmpty(newNumber)) {
                    ShowAutoCloseMessage("Введите номер лошади!", "Уведомление", 3000);
                    return;
                }

                horses[selectedHorseIndex].setName(toolStripTextBox1.Text);

                int ind = selectedHorseIndex + 1;
                ShowAutoCloseMessage("Имя " + ind.ToString() + "-й лошади изменен на " + toolStripTextBox1.Text, "Уведомление", 3000);
                toolStripTextBox1.Text = "";
                toolStripTextBox1.Visible = false;
            }
        }

        private void textNumberHorse_TextChanged(object sender, EventArgs e) {
            if (stopFlag) return;
        }

        private void textNumberHorse_KeyPress(object sender, KeyPressEventArgs e) {
            if (stopFlag) return;
            if (char.IsControl(e.KeyChar)) {
                return;
            }
            if (!char.IsDigit(e.KeyChar)) {
                e.Handled = true; // Отменяем ввод
            }
        }

        private void textNumberHorse_KeyDown(object sender, KeyEventArgs e) {
            if (stopFlag) return;
            if (e.KeyCode == Keys.Enter) {
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Проверяем, что лошадь выбрана
                if (toogleNumber.SelectedIndex == -1) {
                    textNumberHorse.Text = "";
                    ShowAutoCloseMessage("Сначала выберите лошадь из списка!", "Уведомление", 3000);
                    return;
                }

                int selectedHorseIndex = toogleNumber.SelectedIndex;
                string newNumber = textNumberHorse.Text.Trim();

                if (string.IsNullOrEmpty(newNumber)) {
                    ShowAutoCloseMessage("Введите номер лошади!", "Уведомление", 3000);
                    return;
                }

                // Безопасное преобразование
                if (int.TryParse(newNumber, out int parsedNumber)) {
                    if (parsedNumber <= 0) {
                        textNumberHorse.Text = "";
                        ShowAutoCloseMessage("Номер должен быть положительным числом!", "Уведомление", 3000);
                        return;
                    }
                    horses[selectedHorseIndex].setNumber(parsedNumber);
                    int ind = selectedHorseIndex + 1;
                    ShowAutoCloseMessage("Номер " + ind.ToString() + "-й лошади изменен на " + parsedNumber.ToString(), "Уведомление", 3000);
                    textNumberHorse.Text = "";
                    textNumberHorse.Visible = false;
                }
                else {
                    textNumberHorse.Text = "";
                    ShowAutoCloseMessage("Введите корректное число!", "Уведомление", 3000);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            if (stopFlag) return;
            checkTrackBar();
        }

        public void checkTrackBar() {
            if (stopFlag) return;
            if (!Program.timerIsRunning) return;
            int selectedValue = trackBar1.Value;
            Program.aTimer.Interval = Program.intervalTimer - selectedValue * 100 + 10 - 100;
            label1.Text = "Частота хода: " + Program.aTimer.Interval.ToString() + " мс";
        }

        public static void ShowAutoCloseMessage(string message, string caption, int timeoutMs) {

            Timer timer = new Timer();
            timer.Interval = timeoutMs;
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();

                // Закрываем MessageBox
                IntPtr hwnd = FindWindow(null, caption);
                if (hwnd != IntPtr.Zero) {
                    SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
            };

            timer.Start();
            MessageBox.Show(message, caption);
        }

        // Импорт WinAPI функций
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_CLOSE = 0x0010;

        private void toolStripComboBox1_Click_1(object sender, EventArgs e) {

        }
    }
} 
