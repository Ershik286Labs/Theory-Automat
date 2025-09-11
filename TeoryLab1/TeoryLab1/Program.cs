using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeoryLab1 {
    internal static class Program {
        private static System.Timers.Timer aTimer;
        private const int intervalTimer = 10;
        public static Form1 mainForm;
        private const int move = 1;

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainForm = new Form1();

            generationMassiveHorse();

            Application.Run(mainForm);
            aTimer?.Stop();
            aTimer?.Dispose();
        }

        public static void SetTimer() {
            // Настраиваем таймер (если он еще не создан)
            if (aTimer == null) {
                aTimer = new System.Timers.Timer(intervalTimer);
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = true;
            }

            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e) {
            mainForm.Invoke(new Action(() => {
                Random rnd = new Random();
                // Проверяем, что в списке есть лошади
                if (mainForm.horses.Length > 0) {
                    int numberMovingHorse = rnd.Next(0, mainForm.horses.Length);
                    mainForm.horses[numberMovingHorse].moving(move);
                    mainForm.Invalidate();
                }
            }));
        }

        public static void toogleTimer() {
            if (aTimer != null) {

                if (aTimer.Enabled)
                    aTimer.Stop();
                else aTimer.Start();
            }
        }

        public static void generationMassiveHorse() {
            string[] horseNames = new string[]{
                "Звёздный Ветер",
                "Альбус",
                "Гром",
                "Изумруд",
                "Багира",
                "Скиф",
                "Валькирия",
                "Эклипс",
                "Сапфир",
                "Буян",
                "Церера",
                "Атака",
                "Лира",
                "Одиссей",
                "Лаванда",
                "Борей",
                "Грация",
                "Барон",
                "Секвойя",
                "Заря"
            };

            Color[] horseColor = new Color[14]
            {
                Color.White,
                Color.Gray,
                Color.DarkGray,
                Color.Black,
                Color.DarkRed,
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.Green,
                Color.Cyan,
                Color.LightBlue,
                Color.Blue,
                Color.Purple,
                Color.Pink
            };

            Random rnd = new Random();

            // Создаем новых лошадей
            for (int i = 0; i < Map.sizeMassiveHorse; i++) {
                int numberName = rnd.Next(0, horseNames.Length);
                int numberColor = rnd.Next(0, horseColor.Length);
                mainForm.horses[i] = new Horse(horseColor[numberColor], i, horseNames[numberColor]);
            }
        }
    }
}
