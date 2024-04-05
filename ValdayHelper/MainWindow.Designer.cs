namespace ValdayHelper
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.log = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.ButtonGen = new System.Windows.Forms.Button();
            this.textBoxStation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonWeintekTags = new System.Windows.Forms.Button();
            this.textBoxWeintekPLC = new System.Windows.Forms.TextBox();
            this.buttonWeintekAlarms = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(12, 186);
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(776, 252);
            this.log.TabIndex = 1;
            this.log.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Open Modbus";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonOpenModbus_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(238, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Prefix";
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Location = new System.Drawing.Point(277, 29);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(171, 20);
            this.textBoxPrefix.TabIndex = 4;
            this.textBoxPrefix.TextChanged += new System.EventHandler(this.TextBoxPrefix_TextChanged);
            // 
            // ButtonGen
            // 
            this.ButtonGen.Location = new System.Drawing.Point(12, 66);
            this.ButtonGen.Name = "ButtonGen";
            this.ButtonGen.Size = new System.Drawing.Size(149, 23);
            this.ButtonGen.TabIndex = 5;
            this.ButtonGen.Text = "Gen";
            this.ButtonGen.UseVisualStyleBackColor = true;
            this.ButtonGen.Click += new System.EventHandler(this.ButtonGen_Click);
            // 
            // textBoxStation
            // 
            this.textBoxStation.Location = new System.Drawing.Point(277, 66);
            this.textBoxStation.Name = "textBoxStation";
            this.textBoxStation.Size = new System.Drawing.Size(171, 20);
            this.textBoxStation.TabIndex = 7;
            this.textBoxStation.TextChanged += new System.EventHandler(this.TextBoxStation_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Station";
            // 
            // buttonWeintekTags
            // 
            this.buttonWeintekTags.Location = new System.Drawing.Point(498, 27);
            this.buttonWeintekTags.Name = "buttonWeintekTags";
            this.buttonWeintekTags.Size = new System.Drawing.Size(149, 23);
            this.buttonWeintekTags.TabIndex = 8;
            this.buttonWeintekTags.Text = "WeintekTags";
            this.buttonWeintekTags.UseVisualStyleBackColor = true;
            this.buttonWeintekTags.Click += new System.EventHandler(this.ButtonWeintekTags_Click);
            // 
            // textBoxWeintekPLC
            // 
            this.textBoxWeintekPLC.Location = new System.Drawing.Point(667, 27);
            this.textBoxWeintekPLC.Name = "textBoxWeintekPLC";
            this.textBoxWeintekPLC.Size = new System.Drawing.Size(85, 20);
            this.textBoxWeintekPLC.TabIndex = 9;
            // 
            // buttonWeintekAlarms
            // 
            this.buttonWeintekAlarms.Location = new System.Drawing.Point(498, 63);
            this.buttonWeintekAlarms.Name = "buttonWeintekAlarms";
            this.buttonWeintekAlarms.Size = new System.Drawing.Size(149, 23);
            this.buttonWeintekAlarms.TabIndex = 10;
            this.buttonWeintekAlarms.Text = "WeintekAlarms";
            this.buttonWeintekAlarms.UseVisualStyleBackColor = true;
            this.buttonWeintekAlarms.Click += new System.EventHandler(this.ButtonWeintekAlarms_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonWeintekAlarms);
            this.Controls.Add(this.textBoxWeintekPLC);
            this.Controls.Add(this.buttonWeintekTags);
            this.Controls.Add(this.textBoxStation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ButtonGen);
            this.Controls.Add(this.textBoxPrefix);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.log);
            this.Name = "MainWindow";
            this.Text = "Valday Helper";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.Button ButtonGen;
        private System.Windows.Forms.TextBox textBoxStation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonWeintekTags;
        private System.Windows.Forms.TextBox textBoxWeintekPLC;
        private System.Windows.Forms.Button buttonWeintekAlarms;
    }
}

