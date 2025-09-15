using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace TeoryLab1 {
    public class Horse {
        const int spriteWidth = 8;
        const int spriteHeight = 4;
        const int shiftY = 275;//ыыыы
        const int shiftX = 50;
        const int sizePixel = 3;
        public Horse(Color color, int number, string name) {
            this.color = color;
            this.number = number;
            this.name = name;
            this.position = 0;
            this.positionX = position + shiftX;
            this.positionY = number * 70 + shiftY;
            activeRectClick = new Rectangle(positionX, positionY - 8 * sizePixel, 27 * sizePixel, sizePixel * 15);
            this.reach = false;

            animationTimer = new Timer();
            animationTimer.Interval = 20; // 20 мс = 50 FPS
            animationTimer.Tick += AnimationTimer_Tick;
        }

        // Поля
        private Rectangle activeRectClick;
        private Color color;
        private int number;
        private string name;
        private int position;
        private int positionX;
        private int positionY;
        private bool reach;
        private Timer animationTimer; // Таймер анимации

        private int animationStep;  // Шаг анимации
        private int targetPosition; // Целевая позиция
        private const int animationSteps = 10; // Количество шагов анимации

        public void drawMedal(Graphics g, Color color) {
            drawPixel(g, positionX + sizePixel * 29, positionY, color);
            drawPixel(g, positionX + sizePixel * 29, positionY + sizePixel * 1, color);

            drawPixel(g, positionX + sizePixel * 28, positionY, Color.DarkGray);
            drawPixel(g, positionX + sizePixel * 27, positionY - sizePixel, Color.DarkGray);
            drawPixel(g, positionX + sizePixel * 26, positionY - sizePixel, Color.DarkGray);
            drawPixel(g, positionX + sizePixel * 25, positionY - 2 * sizePixel, Color.DarkGray);
        }

        public void drawMovingHorse(Graphics g) {
            drawTail(g);
            drawBody(g);

            //ноги
            //первая нога
            drawPixel(g, positionX + 13 * sizePixel, positionY + sizePixel * 6, color);
            drawPixel(g, positionX + 13 * sizePixel, positionY + sizePixel * 7, color);
            for (int j = 7; j < 12; j++) {
                drawPixel(g, positionX + 12 * sizePixel, positionY + sizePixel * j, color);
            }
            drawPixel(g, positionX + 12 * sizePixel, positionY + sizePixel * 12, Color.Black);

            //вторая
            drawPixel(g, positionX + 15 * sizePixel, positionY + sizePixel * 6, color);
            drawPixel(g, positionX + 15 * sizePixel, positionY + sizePixel * 7, color);
            for (int j = 7; j < 12; j++) {
                drawPixel(g, positionX + 14 * sizePixel, positionY + sizePixel * j, color);
            }
            drawPixel(g, positionX + 14 * sizePixel, positionY + sizePixel * 12, Color.Black);

            //четвертая
            for (int j = 6; j < 12; j++) {
                drawPixel(g, positionX + 25 * sizePixel, positionY + sizePixel * j, color);
            }
            drawPixel(g, positionX + 25 * sizePixel, positionY + sizePixel * 12, Color.Black);

            //четвертая
            for (int j = 5; j < 11; j++) {
                drawPixel(g, positionX + 27 * sizePixel, positionY + sizePixel * j, color);
            }
            drawPixel(g, positionX + 27 * sizePixel, positionY + sizePixel * 11, Color.Black);

            drawHead(g);
        }

        public void drawStandartHorse(Graphics g) {
            drawTail(g);

            drawBody(g);

            //ноги
            //первая нога
            drawPixel(g, positionX + 13 * sizePixel, positionY + sizePixel * 6, color);
            drawPixel(g, positionX + 13 * sizePixel, positionY + sizePixel * 7, color);
            for (int j = 7; j < 12; j++) {
                drawPixel(g, positionX + 12 * sizePixel, positionY + sizePixel * j, color);
            }
            drawPixel(g, positionX + 12 * sizePixel, positionY + sizePixel * 12, Color.Black);

            //вторая
            drawPixel(g, positionX + 15 * sizePixel, positionY + sizePixel * 6, color);
            drawPixel(g, positionX + 15 * sizePixel, positionY + sizePixel * 7, color);
            for (int j = 7; j < 12; j++) {
                drawPixel(g, positionX + 14 * sizePixel, positionY + sizePixel * j, color);
            }
            drawPixel(g, positionX + 14 * sizePixel, positionY + sizePixel * 12, Color.Black);

            //четвертая
            for (int j = 6; j < 12; j++) {
                drawPixel(g, positionX + 25 * sizePixel, positionY + sizePixel * j, color);
            }
            drawPixel(g, positionX + 25 * sizePixel, positionY + sizePixel * 12, Color.Black);

            //четвертая
            for (int j = 5; j < 11; j++) {
                drawPixel(g, positionX + 27 * sizePixel, positionY + sizePixel * j, color);
            }
            drawPixel(g, positionX + 27 * sizePixel, positionY + sizePixel * 11, Color.Black);

            //голова (heeead)
            drawHead(g);

            //drawMedal(g, Color.Gold);

        }

        public void drawTail(Graphics g) {
            //хвост
            drawPixel(g, positionX, positionY, Color.Black);
            drawPixel(g, positionX, positionY - sizePixel, Color.Black);

            for (int i = 1; i < 5; i++) {
                drawPixel(g, positionX + sizePixel * i, positionY - 2 * sizePixel, Color.Black);
                drawPixel(g, positionX + sizePixel * i, positionY - sizePixel, Color.Black);
            }

            drawPixel(g, positionX + 5 * sizePixel, positionY - sizePixel, Color.Black);
            drawPixel(g, positionX + 6 * sizePixel, positionY - sizePixel, Color.Black);
            drawPixel(g, positionX + 5 * sizePixel, positionY, Color.Black);
            drawPixel(g, positionX + 6 * sizePixel, positionY, Color.Black);

            drawPixel(g, positionX + 7 * sizePixel, positionY, Color.Black);
            drawPixel(g, positionX + 6 * sizePixel, positionY + sizePixel, Color.Black);
            drawPixel(g, positionX + 7 * sizePixel, positionY + sizePixel, Color.Black);

            for (int i = 1; i < 5; i++) {
                drawPixel(g, positionX + 7 * sizePixel + sizePixel * i, positionY + sizePixel, Color.Black);
                drawPixel(g, positionX + 7 * sizePixel + sizePixel * i, positionY + 2 * sizePixel, Color.Black);
            }
            drawPixel(g, positionX + 12 * sizePixel, positionY + sizePixel, Color.Black);
        }

        public void drawBody(Graphics g) {
            for (int i = 13; i < 27; i++) {
                for (int j = 1; j < 6; j++) {
                    drawPixel(g, positionX + i * sizePixel, positionY + sizePixel * j, color);
                }
            }
            for (int i = 14; i < 27; i++) {
                drawPixel(g, positionX + i * sizePixel, positionY, color);
            }
            SolidBrush brushName = new SolidBrush(Color.Yellow);
            using (Font font = new Font("Arial", 12))
            using (SolidBrush brush = new SolidBrush(Color.Black)) {
                g.DrawString(name, font, brushName, positionX - shiftX, positionY + 5);
                g.DrawString(number.ToString(), font, brush, positionX + 55, positionY);
            }
        }

        public void drawHead(Graphics g) {
            for (int i = 1; i < 5; i++) {
                for (int j = 1; j < 12; j++) {
                    drawPixel(g, positionX + 24 * sizePixel + sizePixel * i, positionY - sizePixel * 8 + sizePixel * j, color);
                }
            }
            drawPixel(g, positionX + 24 * sizePixel + sizePixel, positionY - sizePixel * 8, color);
            drawPixel(g, positionX + 24 * sizePixel + sizePixel, positionY - sizePixel * 9, color);

            drawPixel(g, positionX + 26 * sizePixel + sizePixel, positionY - sizePixel * 8, Color.Black);

            drawPixel(g, positionX + 26 * sizePixel + sizePixel, positionY - sizePixel * 9, color);
            drawPixel(g, positionX + 26 * sizePixel, positionY - sizePixel * 8, Color.Black);

            for (int i = 28; i < 28 + 4; i++) {
                drawPixel(g, positionX + i * sizePixel, positionY - sizePixel * 5, color);
                drawPixel(g, positionX + i * sizePixel, positionY - sizePixel * 6, color);
            }
            drawPixel(g, positionX + 26 * sizePixel + sizePixel, positionY - sizePixel * 6, Color.Black);

            for (int i = 1; i < 4; i++) {
                drawPixel(g, positionX + i * sizePixel + sizePixel * 21, positionY - sizePixel * 7, Color.Black);
            }
            drawPixel(g, positionX + 24 * sizePixel, positionY - sizePixel * 6, Color.Black);

            for (int i = 1; i < 4; i++) {
                for (int j = 1; j < 6; j++) {
                    drawPixel(g, positionX + i * sizePixel + sizePixel * 21, positionY - sizePixel * 6 + sizePixel * j, Color.Black);
                }
            }
            drawPixel(g, positionX + sizePixel * 20, positionY - sizePixel, Color.Black);
            drawPixel(g, positionX + sizePixel * 21, positionY - sizePixel, Color.Black);
        }

        public void Draw(Graphics g, int trackY, int trackWidth, int trackStartX) {
            drawStandartHorse(g);
        }

        private void AnimationTimer_Tick(object sender, EventArgs e) {
            if (animationStep < animationSteps) {
                // Плавное перемещение
                int newPosition = position + (targetPosition - position) * (animationStep + 1) / animationSteps;
                positionX = newPosition * 35 + 15;
                animationStep++;

                // Запрос на перерисовку формы
                Program.mainForm.Invalidate();
            }
            else {
                // Завершение анимации
                animationTimer.Stop();
                position = targetPosition;
                positionX = position * 35 + 15;
            }
        }

        public void StartAnimation(int newTargetPosition) {
            targetPosition = newTargetPosition;
            animationStep = 0;
            animationTimer.Start();
        }

        private void drawPixel(Graphics g, int x, int y, Color color) {
            Pen pen = new Pen(color);
            Rectangle rect = new Rectangle(x, y, sizePixel, sizePixel);
            using (SolidBrush brush = new SolidBrush(color)) { 
                g.FillRectangle(brush, rect);
            }
        }

        public void moving(int move, ref Horse[] horses, int horseIndex) {
            if (position + move >= Map.distance) {
                if (reach == false) {
                    this.reach = true;
                    this.position = Map.distance;
                    this.positionX = position * 35 + 15;
                    Form1.winnerHorse.Add(this);

                    // Удаляем текущую лошадь из массива
                    Horse[] newHorsesArray = new Horse[horses.Length - 1];
                    int newIndex = 0;

                    for (int i = 0; i < horses.Length; i++) {
                        if (i != horseIndex) { // Пропускаем текущую лошадь
                            newHorsesArray[newIndex] = horses[i];
                            newIndex++;
                        }
                    }

                    horses = newHorsesArray;

                    if (Form1.winnerHorse.Count == horses.Length) {
                        MessageBox.Show("Все лошадки добежали\n Победители: 1 место - " +
                                      Form1.winnerHorse[0].name + ", 2 место - " +
                                      Form1.winnerHorse[1].name + ", 3 место - " +
                                      Form1.winnerHorse[2].name);
                    }
                }
            }
            else {
                Timer timer1 = new Timer();

                this.position += move;
                this.positionX = position * 35 + 15;
            }
        }

        //public void Dispose() {
        //    horseSprite?.Dispose();
        //}
        public int getPosition() { return position; }
        public int getNumber() { return number; }
        public string getName() { return name; }
        public int getPositionX() { return positionX; }
        public int getPositionY() { return positionY; }
        public bool hasReached() { return reach; }
        public Color GetColor() { return color; }

        public void setName(string name) { this.name = name; }
        public void setPositionX(int posX) { this.positionX = posX; }
        public void setPositionY(int posY) { this.positionY = posY; }
        public void setPosition(int pos) { this.position = pos; }
        public void setNumber(int number) { this.number = number; }
        public void setColor(Color color) { this.color = color; }

        public bool isCollising(MouseEventArgs e) {
            if ((e.X <= activeRectClick.X + activeRectClick.Width && e.X >= activeRectClick.X) && 
                (e.Y <= activeRectClick.Y + activeRectClick.Height && e.Y >= activeRectClick.Y)) {
                return true;
            }
            else { return false; }
        }
    }
}