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
            this.BtnGenWeintek = new System.Windows.Forms.Button();
            this.chListSysGen = new System.Windows.Forms.CheckedListBox();
            this.BtnGenSEP = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnGenWeintekAlarms = new System.Windows.Forms.Button();
            this.textBox_SEPPrefix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnParseIO = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(12, 386);
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(1102, 218);
            this.log.TabIndex = 0;
            this.log.Text = "";
            // 
            // BtnGenXML
            // 
            this.BtnGenXML.Location = new System.Drawing.Point(3, 26);
            this.BtnGenXML.Name = "BtnGenXML";
            this.BtnGenXML.Size = new System.Drawing.Size(129, 23);
            this.BtnGenXML.TabIndex = 1;
            this.BtnGenXML.Text = "PLC";
            this.BtnGenXML.UseVisualStyleBackColor = true;
            this.BtnGenXML.Click += new System.EventHandler(this.BtnGenXML_Click);
            // 
            // BtnGenExcel
            // 
            this.BtnGenExcel.Location = new System.Drawing.Point(3, 55);
            this.BtnGenExcel.Name = "BtnGenExcel";
            this.BtnGenExcel.Size = new System.Drawing.Size(129, 23);
            this.BtnGenExcel.TabIndex = 3;
            this.BtnGenExcel.Text = "Table regs";
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
            // BtnGenWeintek
            // 
            this.BtnGenWeintek.Location = new System.Drawing.Point(3, 84);
            this.BtnGenWeintek.Name = "BtnGenWeintek";
            this.BtnGenWeintek.Size = new System.Drawing.Size(129, 23);
            this.BtnGenWeintek.TabIndex = 6;
            this.BtnGenWeintek.Text = "Weintek tags";
            this.BtnGenWeintek.UseVisualStyleBackColor = true;
            this.BtnGenWeintek.Click += new System.EventHandler(this.BtnGenWeintek_Click);
            // 
            // chListSysGen
            // 
            this.chListSysGen.CheckOnClick = true;
            this.chListSysGen.FormattingEnabled = true;
            this.chListSysGen.Location = new System.Drawing.Point(12, 31);
            this.chListSysGen.Name = "chListSysGen";
            this.chListSysGen.Size = new System.Drawing.Size(266, 349);
            this.chListSysGen.TabIndex = 7;
            // 
            // BtnGenSEP
            // 
            this.BtnGenSEP.Location = new System.Drawing.Point(3, 142);
            this.BtnGenSEP.Name = "BtnGenSEP";
            this.BtnGenSEP.Size = new System.Drawing.Size(129, 23);
            this.BtnGenSEP.TabIndex = 10;
            this.BtnGenSEP.Text = "SEP";
            this.BtnGenSEP.UseVisualStyleBackColor = true;
            this.BtnGenSEP.Click += new System.EventHandler(this.BtnGenSEP_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.BtnGenXML);
            this.flowLayoutPanel1.Controls.Add(this.BtnGenExcel);
            this.flowLayoutPanel1.Controls.Add(this.BtnGenWeintek);
            this.flowLayoutPanel1.Controls.Add(this.BtnGenWeintekAlarms);
            this.flowLayoutPanel1.Controls.Add(this.BtnGenSEP);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(299, 31);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(137, 175);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Generate";
            // 
            // BtnGenWeintekAlarms
            // 
            this.BtnGenWeintekAlarms.Location = new System.Drawing.Point(3, 113);
            this.BtnGenWeintekAlarms.Name = "BtnGenWeintekAlarms";
            this.BtnGenWeintekAlarms.Size = new System.Drawing.Size(129, 23);
            this.BtnGenWeintekAlarms.TabIndex = 12;
            this.BtnGenWeintekAlarms.Text = "Weintek alarms";
            this.BtnGenWeintekAlarms.UseVisualStyleBackColor = true;
            this.BtnGenWeintekAlarms.Click += new System.EventHandler(this.BtnGenWeintekAlarms_Click);
            // 
            // textBox_SEPPrefix
            // 
            this.textBox_SEPPrefix.Location = new System.Drawing.Point(3, 16);
            this.textBox_SEPPrefix.Name = "textBox_SEPPrefix";
            this.textBox_SEPPrefix.Size = new System.Drawing.Size(166, 20);
            this.textBox_SEPPrefix.TabIndex = 13;
            this.textBox_SEPPrefix.TextChanged += new System.EventHandler(this.TextBox_SEPPrefix_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Prefix";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Controls.Add(this.textBox_SEPPrefix);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(442, 160);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(173, 46);
            this.flowLayoutPanel2.TabIndex = 15;
            // 
            // btnParseIO
            // 
            this.btnParseIO.Location = new System.Drawing.Point(448, 58);
            this.btnParseIO.Name = "btnParseIO";
            this.btnParseIO.Size = new System.Drawing.Size(129, 23);
            this.btnParseIO.TabIndex = 13;
            this.btnParseIO.Text = "Parse IO";
            this.btnParseIO.UseVisualStyleBackColor = true;
            this.btnParseIO.Click += new System.EventHandler(this.BtnParseIO_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 638);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.chListSysGen);
            this.Controls.Add(this.LblObjectFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.log);
            this.Controls.Add(this.btnParseIO);
            this.Name = "FormMain";
            this.Text = "Configurator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Button BtnGenXML;
        private System.Windows.Forms.Button BtnGenExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblObjectFile;
        private System.Windows.Forms.Button BtnGenWeintek;
        private System.Windows.Forms.CheckedListBox chListSysGen;
        private System.Windows.Forms.Button BtnGenSEP;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_SEPPrefix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button BtnGenWeintekAlarms;
        private System.Windows.Forms.Button btnParseIO;
    }
}

