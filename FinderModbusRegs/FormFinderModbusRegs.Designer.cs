namespace FinderModbusRegs
{
    partial class FormFinderModbusRegs
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
            this.textBox_Path = new System.Windows.Forms.TextBox();
            this.Btn_Find = new System.Windows.Forms.Button();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textBox_Path
            // 
            this.textBox_Path.Location = new System.Drawing.Point(12, 24);
            this.textBox_Path.Name = "textBox_Path";
            this.textBox_Path.Size = new System.Drawing.Size(578, 20);
            this.textBox_Path.TabIndex = 0;
            // 
            // Btn_Find
            // 
            this.Btn_Find.Location = new System.Drawing.Point(611, 24);
            this.Btn_Find.Name = "Btn_Find";
            this.Btn_Find.Size = new System.Drawing.Size(75, 23);
            this.Btn_Find.TabIndex = 1;
            this.Btn_Find.Text = "Find";
            this.Btn_Find.UseVisualStyleBackColor = true;
            this.Btn_Find.Click += new System.EventHandler(this.Btn_Find_Click);
            // 
            // rtb_log
            // 
            this.rtb_log.Location = new System.Drawing.Point(12, 50);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(973, 688);
            this.rtb_log.TabIndex = 2;
            this.rtb_log.Text = "";
            // 
            // FormFinderModbusRegs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 739);
            this.Controls.Add(this.rtb_log);
            this.Controls.Add(this.Btn_Find);
            this.Controls.Add(this.textBox_Path);
            this.Name = "FormFinderModbusRegs";
            this.Text = "FinderModbusRegs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Path;
        private System.Windows.Forms.Button Btn_Find;
        private System.Windows.Forms.RichTextBox rtb_log;
    }
}

