namespace ShopManager
{
    partial class DateTimePicker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.DateLabel = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.SetButton = new System.Windows.Forms.Button();
            this.HourSetter = new System.Windows.Forms.NumericUpDown();
            this.MinuteSetter = new System.Windows.Forms.NumericUpDown();
            this.SecondSetter = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.HourSetter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinuteSetter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondSetter)).BeginInit();
            this.SuspendLayout();
            // 
            // DatePicker
            // 
            this.DatePicker.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText;
            this.DatePicker.Location = new System.Drawing.Point(13, 60);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(309, 23);
            this.DatePicker.TabIndex = 0;
            this.DatePicker.Value = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLabel.Location = new System.Drawing.Point(13, 35);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(43, 18);
            this.DateLabel.TabIndex = 2;
            this.DateLabel.Text = "Date:";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.Location = new System.Drawing.Point(13, 105);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(45, 18);
            this.TimeLabel.TabIndex = 3;
            this.TimeLabel.Text = "Time:";
            // 
            // SetButton
            // 
            this.SetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SetButton.AutoSize = true;
            this.SetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.SetButton.Location = new System.Drawing.Point(246, 222);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(75, 27);
            this.SetButton.TabIndex = 4;
            this.SetButton.Text = "Set";
            this.SetButton.UseVisualStyleBackColor = false;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // HourSetter
            // 
            this.HourSetter.Location = new System.Drawing.Point(13, 135);
            this.HourSetter.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.HourSetter.Name = "HourSetter";
            this.HourSetter.Size = new System.Drawing.Size(47, 23);
            this.HourSetter.TabIndex = 5;
            this.HourSetter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MinuteSetter
            // 
            this.MinuteSetter.Location = new System.Drawing.Point(93, 135);
            this.MinuteSetter.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.MinuteSetter.Name = "MinuteSetter";
            this.MinuteSetter.Size = new System.Drawing.Size(47, 23);
            this.MinuteSetter.TabIndex = 6;
            this.MinuteSetter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SecondSetter
            // 
            this.SecondSetter.Location = new System.Drawing.Point(173, 135);
            this.SecondSetter.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.SecondSetter.Name = "SecondSetter";
            this.SecondSetter.Size = new System.Drawing.Size(47, 23);
            this.SecondSetter.TabIndex = 7;
            this.SecondSetter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = ":";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(150, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = ":";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateTimePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(334, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SecondSetter);
            this.Controls.Add(this.MinuteSetter);
            this.Controls.Add(this.HourSetter);
            this.Controls.Add(this.SetButton);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.DatePicker);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "DateTimePicker";
            this.Text = "Select date and time";
            this.Shown += new System.EventHandler(this.DateTimePicker_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.HourSetter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinuteSetter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondSetter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DatePicker;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button SetButton;
        private System.Windows.Forms.NumericUpDown HourSetter;
        private System.Windows.Forms.NumericUpDown MinuteSetter;
        private System.Windows.Forms.NumericUpDown SecondSetter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}