namespace EcoStruxureConfigurator
{
    partial class FormMain
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
            this.BtnGenIO = new System.Windows.Forms.Button();
            this.BtnGenIO_MB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(12, 203);
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(776, 199);
            this.log.TabIndex = 0;
            this.log.Text = "";
            // 
            // BtnGenIO
            // 
            this.BtnGenIO.Location = new System.Drawing.Point(259, 46);
            this.BtnGenIO.Name = "BtnGenIO";
            this.BtnGenIO.Size = new System.Drawing.Size(75, 23);
            this.BtnGenIO.TabIndex = 1;
            this.BtnGenIO.Text = "GenIO";
            this.BtnGenIO.UseVisualStyleBackColor = true;
            this.BtnGenIO.Click += new System.EventHandler(this.BtnGenIO_Click);
            // 
            // BtnGenIO_MB
            // 
            this.BtnGenIO_MB.Location = new System.Drawing.Point(259, 88);
            this.BtnGenIO_MB.Name = "BtnGenIO_MB";
            this.BtnGenIO_MB.Size = new System.Drawing.Size(75, 23);
            this.BtnGenIO_MB.TabIndex = 2;
            this.BtnGenIO_MB.Text = "GenIO->MB";
            this.BtnGenIO_MB.UseVisualStyleBackColor = true;
            this.BtnGenIO_MB.Click += new System.EventHandler(this.BtnGenIO_MB_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnGenIO_MB);
            this.Controls.Add(this.BtnGenIO);
            this.Controls.Add(this.log);
            this.Name = "FormMain";
            this.Text = "Configurator";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Button BtnGenIO;
        private System.Windows.Forms.Button BtnGenIO_MB;
    }
}

