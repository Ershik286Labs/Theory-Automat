using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeoryLab1 {
    public partial class Form1 : Form {
        private List<Horse> horses = new List<Horse>();
        private Timer animationTimer;

        public Form1() {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Paint += Form1_Paint;

            // Настраиваем таймер для анимации
            animationTimer = new Timer();
            animationTimer.Interval = 100;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        // Метод для добавления лошадей из Program.cs
        public void AddHorse(Horse horse) {
            horses.Add(horse);
        }

        private void fileComboBox(object sender, EventArgs e) { //File

        }

        private void settingsComboBox(object sender, EventArgs e) {

        }

        private void helpComboBox(object sender, EventArgs e) {

        }

        private void сохранитьCtrlSToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            DrawHorses(e.Graphics);
        }

        private void DrawHorses(Graphics g) {
            int trackY = 100; // Y-позиция трека
            int trackWidth = this.ClientSize.Width - 100; // Ширина трека
            int trackStartX = 50; // Начало трека

            // Очищаем фон
            g.Clear(Color.White);

            // Рисуем трек
            using (Pen trackPen = new Pen(Color.Black, 3)) {
                g.DrawLine(trackPen, trackStartX, trackY, trackStartX + trackWidth, trackY);
            }

            // Рисуем всех лошадей
            for (int i = 0; i < horses.Count; i++) {
                int horseTrackY = trackY + (i * 40); // Смещаем каждую лошадь по Y
                horses[i].Draw(g, horseTrackY, trackWidth, trackStartX);
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e) {
            // Обновляем позиции лошадей
            //Random rand = new Random();
            //foreach (var horse in horses) {
            //    if (!horse.hasReached()) {
            //        horse.moving(rand.Next(1, 5));
            //    }
            //}

            // Перерисовываем форму
            this.Invalidate();
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            this.Invalidate(); // Перерисовываем при изменении размера
        }

    }
}
