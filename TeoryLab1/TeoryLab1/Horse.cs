using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TeoryLab1 {
    public class Horse {
        const int spriteWidth = 8;
        const int spriteHeight = 4;

        public Horse(Color color, int number, string name) {
            this.color = color;
            horseSprite = LoadHorseSprite(color);
            this.number = number;
            this.name = name;
            this.position = 0;
            this.reach = false;
        }

        // Поля
        private Color color;
        private int number;
        private string name;
        private int position;
        private bool reach;
        private Bitmap horseSprite;

        private Bitmap LoadHorseSprite(Color tintColor) {
            try {
                string spritePath = Path.Combine(Application.StartupPath, "resourse", "image", "horse.png");

                if (!File.Exists(spritePath)) {
                    return CreateDefaultSprite(tintColor);
                }

                Bitmap sprite = new Bitmap(spritePath);
                return ApplyColorTint(sprite, tintColor);
            }
            catch {
                return CreateDefaultSprite(tintColor);
            }
        }

        private Bitmap ApplyColorTint(Bitmap original, Color tintColor) {
            Bitmap tinted = new Bitmap(original);

            for (int x = 0; x < tinted.Width; x++) {
                for (int y = 0; y < tinted.Height; y++) {
                    Color pixel = tinted.GetPixel(x, y);

                    if (pixel.A > 0) {
                        // Используем метод ShouldRecolorPixel для определения нужно ли перекрашивать
                        if (ShouldRecolorPixel(pixel, x, y)) {
                            int r = (int)(pixel.R * (tintColor.R / 255f));
                            int g = (int)(pixel.G * (tintColor.G / 255f));
                            int b = (int)(pixel.B * (tintColor.B / 255f));
                            tinted.SetPixel(x, y, Color.FromArgb(pixel.A, r, g, b));
                        }
                    }
                }
            }
            return tinted;
        }

        private bool ShouldRecolorPixel(Color pixel, int x, int y) {
            // 1. Проверка по цвету-маркеру (magenta)
            if (pixel.R == 15 && pixel.G == 0 && pixel.B == 255) {
                return true;
            }

            // 3. Проверка по яркости (светлые области)
            if (pixel.GetBrightness() > 0.5f) {
                return true;
            }

            return false;
        }

        private Bitmap CreateDefaultSprite(Color color) {
            Bitmap sprite = new Bitmap(spriteWidth, spriteHeight);

            using (Graphics g = Graphics.FromImage(sprite))
            using (SolidBrush brush = new SolidBrush(color)) {
                g.Clear(Color.Transparent);
                g.FillEllipse(brush, 15, 25, 35, 20); // Тело
                g.FillEllipse(brush, 45, 20, 15, 15); // Голова

                g.FillRectangle(brush, 20, 45, 5, 15);
                g.FillRectangle(brush, 35, 45, 5, 15);
                g.FillRectangle(brush, 50, 45, 5, 15);
            }

            return sprite;
        }

        public void Draw(Graphics g, int trackY, int trackWidth, int trackStartX) {
            if (horseSprite == null) return;

            // Позиция на треке
            int x = trackStartX + (position * trackWidth) / Map.distance - spriteWidth / 2;
            int y = trackY - spriteHeight / 2;

            // Рисуем спрайт
            g.DrawImage(horseSprite, x, y);

            // Отладочная информация
            using (Font font = new Font("Arial", 8))
            using (SolidBrush brush = new SolidBrush(Color.Black)) {
                g.DrawString($"{number}: {name}", font, brush, x, y - 5);
                g.DrawString($"Pos: {position}", font, brush, x, y + spriteHeight + 5);
            }
        }

        public void moving(int move) {
            if (position + move >= Map.distance) {
                this.reach = true;
                this.position = Map.distance;
                Form1.winnerHorse.Add(this);
                if (Form1.winnerHorse.Count == 10) {
                    MessageBox.Show("Все лошадки добежали");
                }
            }
            else {
                this.position += move;
            }
        }

        public void Dispose() {
            horseSprite?.Dispose();
        }

        public int getNumber() { return number; }
        public string getName() { return name; }
        public int getPosition() { return position; }
        public bool hasReached() { return reach; }
        public Color GetColor() { return color; }

        public void setName(string name) { this.name = name; }
        public void setPosition(int pos) { this.position = pos; }
        public void setNumber(int number) { this.number = number; }
        public void setColor(Color color) { this.color = color; }
    }
}
