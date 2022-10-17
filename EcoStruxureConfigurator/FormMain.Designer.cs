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
            this.BtnGenXML = new System.Windows.Forms.Button();
            this.BtnGenExcel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LblObjectFile = new System.Windows.Forms.Label();
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
            // BtnGenXML
            // 
            this.BtnGenXML.Location = new System.Drawing.Point(12, 32);
            this.BtnGenXML.Name = "BtnGenXML";
            this.BtnGenXML.Size = new System.Drawing.Size(75, 23);
            this.BtnGenXML.TabIndex = 1;
            this.BtnGenXML.Text = "Gen XML";
            this.BtnGenXML.UseVisualStyleBackColor = true;
            this.BtnGenXML.Click += new System.EventHandler(this.BtnGenXML_Click);
            // 
            // BtnGenExcel
            // 
            this.BtnGenExcel.Location = new System.Drawing.Point(12, 61);
            this.BtnGenExcel.Name = "BtnGenExcel";
            this.BtnGenExcel.Size = new System.Drawing.Size(75, 23);
            this.BtnGenExcel.TabIndex = 3;
            this.BtnGenExcel.Text = "Gen Excel";
            this.BtnGenExcel.UseVisualStyleBackColor = true;
            this.BtnGenExcel.Click += new System.EventHandler(this.BtnGenExcel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Object file:";
            // 
            // LblObjectFile
            // 
            this.LblObjectFile.AutoSize = true;
            this.LblObjectFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LblObjectFile.Location = new System.Drawing.Point(62, 7);
            this.LblObjectFile.Name = "LblObjectFile";
            this.LblObjectFile.Size = new System.Drawing.Size(65, 13);
            this.LblObjectFile.TabIndex = 5;
            this.LblObjectFile.Text = "Object file";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LblObjectFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnGenExcel);
            this.Controls.Add(this.BtnGenXML);
            this.Controls.Add(this.log);
            this.Name = "FormMain";
            this.Text = "Configurator";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Button BtnGenXML;
        private System.Windows.Forms.Button BtnGenExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblObjectFile;
    }
}

