using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace TeoryLab1 {
    public class Horse {
        const int spriteWidth = 8;
        const int spriteHeight = 4;
        const int shiftY = 100;//ыыыы
        const int sizePixel = 3;
        public Horse(Color color, int number, string name) {
            this.color = color;
            //horseSprite = LoadHorseSprite(color);
            this.number = number;
            this.name = name;
            this.position = 0;
            this.positionX = position + 15;
            this.positionY = number * 45 + shiftY;
            activeRectClick = new Rectangle(positionX, positionY - 8 * sizePixel, 27 * sizePixel, sizePixel * 15);
            this.reach = false;
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
        //private Bitmap horseSprite;

        //private Bitmap LoadHorseSprite(Color tintColor) {
        //    try {
        //        string spritePath = Path.Combine(Application.StartupPath, "resourse", "image", "horse.png");

        //        if (!File.Exists(spritePath)) {
        //            return CreateDefaultSprite(tintColor);
        //        }

        //        Bitmap sprite = new Bitmap(spritePath);
        //        return ApplyColorTint(sprite, tintColor);
        //    }
        //    catch {
        //        return CreateDefaultSprite(tintColor);
        //    }
        //}

        //private Bitmap ApplyColorTint(Bitmap original, Color tintColor) {
        //    Bitmap tinted = new Bitmap(original);

        //    for (int x = 0; x < tinted.Width; x++) {
        //        for (int y = 0; y < tinted.Height; y++) {
        //            Color pixel = tinted.GetPixel(x, y);

        //            if (pixel.A > 0) {
        //                // Используем метод ShouldRecolorPixel для определения нужно ли перекрашивать
        //                if (ShouldRecolorPixel(pixel, x, y)) {
        //                    int r = (int)(pixel.R * (tintColor.R / 255f));
        //                    int g = (int)(pixel.G * (tintColor.G / 255f));
        //                    int b = (int)(pixel.B * (tintColor.B / 255f));
        //                    tinted.SetPixel(x, y, Color.FromArgb(pixel.A, r, g, b));
        //                }
        //            }
        //        }
        //    }
        //    return tinted;
        //}

        //private bool ShouldRecolorPixel(Color pixel, int x, int y) {
        //    // 1. Проверка по цвету-маркеру (magenta)
        //    if (pixel.R == 15 && pixel.G == 0 && pixel.B == 255) {
        //        return true;
        //    }

        //    // 3. Проверка по яркости (светлые области)
        //    if (pixel.GetBrightness() > 0.5f) {
        //        return true;
        //    }

        //    return false;
        //}

        //private Bitmap CreateDefaultSprite(Color color) {
        //    Bitmap sprite = new Bitmap(spriteWidth, spriteHeight);

        //    using (Graphics g = Graphics.FromImage(sprite))
        //    using (SolidBrush brush = new SolidBrush(color)) {
        //        g.Clear(Color.Transparent);
        //        g.FillEllipse(brush, 15, 25, 35, 20); // Тело
        //        g.FillEllipse(brush, 45, 20, 15, 15); // Голова

        //        g.FillRectangle(brush, 20, 45, 5, 15);
        //        g.FillRectangle(brush, 35, 45, 5, 15);
        //        g.FillRectangle(brush, 50, 45, 5, 15);
        //    }

        //    return sprite;
        //}

        //public void Draw(Graphics g, int trackY, int trackWidth, int trackStartX) {
        //    if (horseSprite == null) return;

        //    // Позиция на треке
        //    int x = trackStartX + (position * trackWidth) / Map.distance - spriteWidth / 2;
        //    int y = trackY - spriteHeight / 2;

        //    // Рисуем спрайт
        //    g.DrawImage(horseSprite, x, y);

        //    // Отладочная информация
        //    using (Font font = new Font("Arial", 8))
        //    using (SolidBrush brush = new SolidBrush(Color.Black)) {
        //        g.DrawString($"{number}: {name}", font, brush, x, y - 5);
        //        g.DrawString($"Pos: {position}", font, brush, x, y + spriteHeight + 5);
        //    }
        //}
        public void drawStandartHorse(Graphics g) {
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
            //хвост

            //тело
            for (int i = 13; i < 27; i++) {
                for (int j = 1; j < 6; j++) {
                    drawPixel(g, positionX + i * sizePixel, positionY + sizePixel * j, color);
                }
            }
            for (int i = 14; i < 27; i++) {
                drawPixel(g, positionX + i * sizePixel, positionY, color);
            }
            //тело

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

            //глаз
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

        public void drawMovingHorse(Graphics g) {

        }

        public void Draw(Graphics g, int trackY, int trackWidth, int trackStartX) {
            drawStandartHorse(g);
        }

        private void drawPixel(Graphics g, int x, int y, Color color) {
            Pen pen = new Pen(color);
            Rectangle rect = new Rectangle(x, y, sizePixel, sizePixel);
            using (SolidBrush brush = new SolidBrush(color)) { 
                g.FillRectangle(brush, rect);
            }
        }

        public void moving(int move) {
            if (position + move >= Map.distance) {
                this.reach = true;
                this.position = Map.distance;
                this.positionX = position * 10 + 15;
                Form1.winnerHorse.Add(this);
                if (Form1.winnerHorse.Count == 10) {
                    MessageBox.Show("Все лошадки добежали");
                }
            }
            else {
                this.position += move;
                this.positionX = position * 10 + 15;
            }
            activeRectClick = new Rectangle(positionX, positionY - 8 * sizePixel, 27 * sizePixel, sizePixel * 15);
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