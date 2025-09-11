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

        public Form1() {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Paint += Form1_Paint;
            this.KeyPreview = true; // Важно: форма будет получать события клавиш first
            this.KeyDown += Form1_KeyDown;
            this.KeyPress += Form1_KeyPress;
            this.KeyUp += Form1_KeyUp;
            this.MouseDown += MainForm_MouseDown;
            this.MouseUp += MainForm_MouseUp;
            this.MouseClick += MainForm_MouseClick;
            this.MouseMove += MainForm_MouseMove;
            this.MouseWheel += MainForm_MouseWheel;
        }
        private void fileComboBox(object sender, EventArgs e) { //File

        }

        private void settingsComboBox(object sender, EventArgs e) {

        }

        private void helpComboBox(object sender, EventArgs e) {

        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            DrawHorses(e.Graphics);
        }

        private void DrawHorses(Graphics g) {
            int trackY = 100; // Y-позиция трека
            int trackWidth = this.ClientSize.Width - 200; // Ширина трека
            int trackStartX = 50; // Начало трека

            // Очищаем фон
            g.Clear(Color.White);

            // Рисуем всех лошадей
            for (int i = 0; i < horses.Length; i++) {
                int horseTrackY = trackY + (i * 40); // Смещаем каждую лошадь по Y
                horses[i].Draw(g, horseTrackY, trackWidth, trackStartX);
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

                        if (lines.Length < 2) {
                            MessageBox.Show("Файл должен содержать две строки", "Ошибка",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        string[] horseNames = lines[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] numbersStrings = lines[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        int[] numbers = new int[numbersStrings.Length];
                        for (int i = 0; i < numbersStrings.Length; i++) {
                            if (!int.TryParse(numbersStrings[i], out numbers[i])) {
                                MessageBox.Show($"Ошибка преобразования позиции: {numbersStrings[i]}", "Ошибка",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        for (int i = 0; i < horses.Length; i++) {
                            horses[i].setName(horseNames[i]);
                            horses[i].setNumber(numbers[i]);
                        }
                        this.Invalidate();

                        MessageBox.Show($"Данные успешно загружены из файла!\nЛошадей: {horses.Length}",
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
            Program.toogleTimer();
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
            string button = e.Button.ToString();
            string location = $"X: {e.X}, Y: {e.Y}";
            MessageBox.Show($"MouseDown: {button} at {location}");
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e) {
            // Отпускание кнопки мыши
            Console.WriteLine($"MouseUp: {e.Button} at ({e.X}, {e.Y})");
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
    }
} 
