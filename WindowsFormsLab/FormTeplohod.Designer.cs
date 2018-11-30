namespace WindowsFormsLab
{
    partial class FormTeplohod
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
            this.pictureBoxTeplohod = new System.Windows.Forms.PictureBox();
            this.plusLokomativ = new System.Windows.Forms.Button();
            this.plusTep = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.pictureBoxTake = new System.Windows.Forms.PictureBox();
            this.Take = new System.Windows.Forms.Button();
            this.maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTeplohod)).BeginInit();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTake)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxTeplohod
            // 
            this.pictureBoxTeplohod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBoxTeplohod.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTeplohod.Name = "pictureBoxTeplohod";
            this.pictureBoxTeplohod.Size = new System.Drawing.Size(735, 461);
            this.pictureBoxTeplohod.TabIndex = 0;
            this.pictureBoxTeplohod.TabStop = false;
            // 
            // plusLokomativ
            // 
            this.plusLokomativ.Location = new System.Drawing.Point(741, 115);
            this.plusLokomativ.Name = "plusLokomativ";
            this.plusLokomativ.Size = new System.Drawing.Size(130, 61);
            this.plusLokomativ.TabIndex = 1;
            this.plusLokomativ.Text = "Добавить Локомотив";
            this.plusLokomativ.UseVisualStyleBackColor = true;
            this.plusLokomativ.Click += new System.EventHandler(this.plusLokomativ_Click);
            // 
            // plusTep
            // 
            this.plusTep.Location = new System.Drawing.Point(742, 182);
            this.plusTep.Name = "plusTep";
            this.plusTep.Size = new System.Drawing.Size(130, 66);
            this.plusTep.TabIndex = 2;
            this.plusTep.Text = "Добавить Теплоход";
            this.plusTep.UseVisualStyleBackColor = true;
            this.plusTep.Click += new System.EventHandler(this.plusTep_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.pictureBoxTake);
            this.groupBox.Controls.Add(this.Take);
            this.groupBox.Controls.Add(this.maskedTextBox);
            this.groupBox.Controls.Add(this.label);
            this.groupBox.Location = new System.Drawing.Point(741, 254);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(138, 207);
            this.groupBox.TabIndex = 3;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Забрать вагон";
            // 
            // pictureBoxTake
            // 
            this.pictureBoxTake.Location = new System.Drawing.Point(7, 125);
            this.pictureBoxTake.Name = "pictureBoxTake";
            this.pictureBoxTake.Size = new System.Drawing.Size(131, 82);
            this.pictureBoxTake.TabIndex = 3;
            this.pictureBoxTake.TabStop = false;
            // 
            // Take
            // 
            this.Take.Location = new System.Drawing.Point(32, 97);
            this.Take.Name = "Take";
            this.Take.Size = new System.Drawing.Size(75, 23);
            this.Take.TabIndex = 2;
            this.Take.Text = "Забрать";
            this.Take.UseVisualStyleBackColor = true;
            this.Take.Click += new System.EventHandler(this.Take_Click);
            // 
            // maskedTextBox
            // 
            this.maskedTextBox.Location = new System.Drawing.Point(78, 42);
            this.maskedTextBox.Name = "maskedTextBox";
            this.maskedTextBox.Size = new System.Drawing.Size(36, 20);
            this.maskedTextBox.TabIndex = 1;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(29, 42);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(42, 13);
            this.label.TabIndex = 0;
            this.label.Text = "Место:";
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(741, 0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(138, 108);
            this.listBox.TabIndex = 4;
            this.listBox.Click += new System.EventHandler(this.listBoxs_SelectedIndexChanged);
            // 
            // FormTeplohod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.plusTep);
            this.Controls.Add(this.plusLokomativ);
            this.Controls.Add(this.pictureBoxTeplohod);
            this.Name = "FormTeplohod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Теплоход";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTeplohod)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTake)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxTeplohod;
        private System.Windows.Forms.Button plusLokomativ;
        private System.Windows.Forms.Button plusTep;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button Take;
        private System.Windows.Forms.MaskedTextBox maskedTextBox;
        private System.Windows.Forms.PictureBox pictureBoxTake;
        private System.Windows.Forms.ListBox listBox;
    }
}

