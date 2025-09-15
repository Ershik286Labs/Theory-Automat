namespace TeoryLab1 {
    partial class Form1 {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьCtrlSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выйтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.цветToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.номерToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toogleNumber = new System.Windows.Forms.ToolStripComboBox();
            this.textNumberHorse = new System.Windows.Forms.ToolStripTextBox();
            this.имяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toogleNames = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оРазработчикеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Start = new System.Windows.Forms.Button();
            this.Pausa = new System.Windows.Forms.Button();
            this.Reset = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // открытьToolStripMenuItem
            // 
            открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            открытьToolStripMenuItem.Size = new System.Drawing.Size(205, 24);
            открытьToolStripMenuItem.Text = "Открыть Ctrl + O";
            открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьCtrlSToolStripMenuItem,
            открытьToolStripMenuItem,
            this.выйтиToolStripMenuItem});
            this.файлToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            this.файлToolStripMenuItem.Click += new System.EventHandler(this.файлToolStripMenuItem_Click);
            // 
            // сохранитьCtrlSToolStripMenuItem
            // 
            this.сохранитьCtrlSToolStripMenuItem.Name = "сохранитьCtrlSToolStripMenuItem";
            this.сохранитьCtrlSToolStripMenuItem.Size = new System.Drawing.Size(205, 24);
            this.сохранитьCtrlSToolStripMenuItem.Text = "Сохранить Ctrl + S";
            this.сохранитьCtrlSToolStripMenuItem.Click += new System.EventHandler(this.сохранитьCtrlSToolStripMenuItem_Click);
            // 
            // выйтиToolStripMenuItem
            // 
            this.выйтиToolStripMenuItem.Name = "выйтиToolStripMenuItem";
            this.выйтиToolStripMenuItem.Size = new System.Drawing.Size(205, 24);
            this.выйтиToolStripMenuItem.Text = "Выйти Esc";
            this.выйтиToolStripMenuItem.Click += new System.EventHandler(this.выйтиToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.цветToolStripMenuItem,
            this.номерToolStripMenuItem,
            this.имяToolStripMenuItem});
            this.настройкиToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // цветToolStripMenuItem
            // 
            this.цветToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.цветToolStripMenuItem.Name = "цветToolStripMenuItem";
            this.цветToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.цветToolStripMenuItem.Text = "Цвет";
            this.цветToolStripMenuItem.Click += new System.EventHandler(this.цветToolStripMenuItem_Click);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Лошадь 1",
            "Лошадь 2",
            "Лошадь 3",
            "Лошадь 4",
            "Лошадь 5",
            "Лошадь 6",
            "Лошадь 7",
            "Лошадь 8",
            "Лошадь 9",
            "Лошадь 10"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox1.Text = "Выбор лошади";
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            this.toolStripComboBox1.Click += new System.EventHandler(this.toolStripComboBox1_Click);
            // 
            // номерToolStripMenuItem
            // 
            this.номерToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toogleNumber,
            this.textNumberHorse});
            this.номерToolStripMenuItem.Name = "номерToolStripMenuItem";
            this.номерToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.номерToolStripMenuItem.Text = "Номер";
            // 
            // toogleNumber
            // 
            this.toogleNumber.Items.AddRange(new object[] {
            "Лошадь 1",
            "Лошадь 2",
            "Лошадь 3",
            "Лошадь 4",
            "Лошадь 5",
            "Лошадь 6",
            "Лошадь 7",
            "Лошадь 8",
            "Лошадь 9",
            "Лошадь 10"});
            this.toogleNumber.Name = "toogleNumber";
            this.toogleNumber.Size = new System.Drawing.Size(121, 23);
            this.toogleNumber.Text = "Выбор лошади";
            this.toogleNumber.SelectedIndexChanged += new System.EventHandler(this.toogleNumber_SelectedIndexChanged);
            // 
            // textNumberHorse
            // 
            this.textNumberHorse.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.textNumberHorse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textNumberHorse.Name = "textNumberHorse";
            this.textNumberHorse.Size = new System.Drawing.Size(100, 23);
            this.textNumberHorse.Visible = false;
            this.textNumberHorse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textNumberHorse_KeyDown);
            this.textNumberHorse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textNumberHorse_KeyPress);
            this.textNumberHorse.TextChanged += new System.EventHandler(this.textNumberHorse_TextChanged);
            // 
            // имяToolStripMenuItem
            // 
            this.имяToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toogleNames,
            this.toolStripTextBox1});
            this.имяToolStripMenuItem.Name = "имяToolStripMenuItem";
            this.имяToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.имяToolStripMenuItem.Text = "Имя";
            // 
            // toogleNames
            // 
            this.toogleNames.Items.AddRange(new object[] {
            "Лошадь 1",
            "Лошадь 2",
            "Лошадь 3",
            "Лошадь 4",
            "Лошадь 5",
            "Лошадь 6",
            "Лошадь 7",
            "Лошадь 8",
            "Лошадь 9",
            "Лошадь 10"});
            this.toogleNames.Name = "toogleNames";
            this.toogleNames.Size = new System.Drawing.Size(121, 23);
            this.toogleNames.Text = "Выбор лошади";
            this.toogleNames.SelectedIndexChanged += new System.EventHandler(this.toogleNames_SelectedIndexChanged);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.Visible = false;
            this.toolStripTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.оРазработчикеToolStripMenuItem});
            this.помощьToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // оРазработчикеToolStripMenuItem
            // 
            this.оРазработчикеToolStripMenuItem.Name = "оРазработчикеToolStripMenuItem";
            this.оРазработчикеToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.оРазработчикеToolStripMenuItem.Text = "О разработчике";
            this.оРазработчикеToolStripMenuItem.Click += new System.EventHandler(this.оРазработчикеToolStripMenuItem_Click);
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.Start.Location = new System.Drawing.Point(0, 31);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(63, 28);
            this.Start.TabIndex = 1;
            this.Start.Text = "Старт";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.startButtonClick);
            // 
            // Pausa
            // 
            this.Pausa.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.Pausa.Location = new System.Drawing.Point(81, 31);
            this.Pausa.Name = "Pausa";
            this.Pausa.Size = new System.Drawing.Size(135, 28);
            this.Pausa.TabIndex = 2;
            this.Pausa.Text = "Пауза";
            this.Pausa.UseVisualStyleBackColor = true;
            this.Pausa.Click += new System.EventHandler(this.Pausa_Click);
            // 
            // Reset
            // 
            this.Reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.Reset.Location = new System.Drawing.Point(222, 31);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(63, 28);
            this.Reset.TabIndex = 3;
            this.Reset.Text = "Сброс";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.resetButtonClick);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(1048, 56);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(216, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1101, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Скорость лошадок";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 961);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.Pausa);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьCtrlSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выйтиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem цветToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem номерToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оРазработчикеToolStripMenuItem;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Pausa;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem имяToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toogleNames;
        private System.Windows.Forms.ToolStripComboBox toogleNumber;
        private System.Windows.Forms.ToolStripTextBox textNumberHorse;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
    }
}

