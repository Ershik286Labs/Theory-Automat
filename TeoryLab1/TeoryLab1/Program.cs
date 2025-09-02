using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeoryLab1 {
    internal static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 mainForm = new Form1();

            // Создаем и добавляем лошадей
            Horse horse1 = new Horse(Color.Red, 1, "Конь");
            Horse horse2 = new Horse(Color.Blue, 2, "Скакун");
            Horse horse3 = new Horse(Color.Green, 3, "Быстрый");

            mainForm.AddHorse(horse1);
            mainForm.AddHorse(horse2);
            mainForm.AddHorse(horse3);

            Application.Run(mainForm);
        }
    }
}
