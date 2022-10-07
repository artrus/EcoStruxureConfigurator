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
            this.btnGenIO = new System.Windows.Forms.Button();
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
            // btnGenIO
            // 
            this.btnGenIO.Location = new System.Drawing.Point(259, 46);
            this.btnGenIO.Name = "btnGenIO";
            this.btnGenIO.Size = new System.Drawing.Size(75, 23);
            this.btnGenIO.TabIndex = 1;
            this.btnGenIO.Text = "GenIO";
            this.btnGenIO.UseVisualStyleBackColor = true;
            this.btnGenIO.Click += new System.EventHandler(this.BtnGenIO_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGenIO);
            this.Controls.Add(this.log);
            this.Name = "FormMain";
            this.Text = "Configurator";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Button btnGenIO;
    }
}

