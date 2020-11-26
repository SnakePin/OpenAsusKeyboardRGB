namespace RogArmouryKbRevengGUI
{
    partial class Form1
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.staticEffectColorPbox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.colorCycleFast = new System.Windows.Forms.RadioButton();
            this.colorCycleMedium = new System.Windows.Forms.RadioButton();
            this.colorCycleSlow = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.saveColorCycleBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.infoDeviceNameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.newProfileIndexNumBox = new System.Windows.Forms.NumericUpDown();
            this.forceExitAuraSyncModeBtn = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.breathingRandomColorCheckBox = new System.Windows.Forms.CheckBox();
            this.breathingDoubleColorCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.breathingSlow = new System.Windows.Forms.RadioButton();
            this.breathingFast = new System.Windows.Forms.RadioButton();
            this.breathingMedium = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.staticEffectColorPbox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newProfileIndexNumBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save static color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.staticChangeColorBtn_Click);
            // 
            // staticEffectColorPbox
            // 
            this.staticEffectColorPbox.BackColor = System.Drawing.Color.Black;
            this.staticEffectColorPbox.Location = new System.Drawing.Point(5, 32);
            this.staticEffectColorPbox.Name = "staticEffectColorPbox";
            this.staticEffectColorPbox.Size = new System.Drawing.Size(106, 69);
            this.staticEffectColorPbox.TabIndex = 1;
            this.staticEffectColorPbox.TabStop = false;
            this.staticEffectColorPbox.Click += new System.EventHandler(this.staticEffectColorPbox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Static color:";
            // 
            // colorCycleFast
            // 
            this.colorCycleFast.AutoSize = true;
            this.colorCycleFast.Location = new System.Drawing.Point(5, 13);
            this.colorCycleFast.Name = "colorCycleFast";
            this.colorCycleFast.Size = new System.Drawing.Size(45, 17);
            this.colorCycleFast.TabIndex = 3;
            this.colorCycleFast.TabStop = true;
            this.colorCycleFast.Text = "Fast";
            this.colorCycleFast.UseVisualStyleBackColor = true;
            // 
            // colorCycleMedium
            // 
            this.colorCycleMedium.AutoSize = true;
            this.colorCycleMedium.Checked = true;
            this.colorCycleMedium.Location = new System.Drawing.Point(5, 35);
            this.colorCycleMedium.Name = "colorCycleMedium";
            this.colorCycleMedium.Size = new System.Drawing.Size(62, 17);
            this.colorCycleMedium.TabIndex = 3;
            this.colorCycleMedium.TabStop = true;
            this.colorCycleMedium.Text = "Medium";
            this.colorCycleMedium.UseVisualStyleBackColor = true;
            // 
            // colorCycleSlow
            // 
            this.colorCycleSlow.AutoSize = true;
            this.colorCycleSlow.Location = new System.Drawing.Point(5, 56);
            this.colorCycleSlow.Name = "colorCycleSlow";
            this.colorCycleSlow.Size = new System.Drawing.Size(48, 17);
            this.colorCycleSlow.TabIndex = 3;
            this.colorCycleSlow.TabStop = true;
            this.colorCycleSlow.Text = "Slow";
            this.colorCycleSlow.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.staticEffectColorPbox);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(117, 167);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Static effect";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.saveColorCycleBtn);
            this.groupBox2.Controls.Add(this.colorCycleSlow);
            this.groupBox2.Controls.Add(this.colorCycleFast);
            this.groupBox2.Controls.Add(this.colorCycleMedium);
            this.groupBox2.Location = new System.Drawing.Point(132, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(117, 167);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Color cycle effect";
            // 
            // saveColorCycleBtn
            // 
            this.saveColorCycleBtn.Location = new System.Drawing.Point(6, 79);
            this.saveColorCycleBtn.Name = "saveColorCycleBtn";
            this.saveColorCycleBtn.Size = new System.Drawing.Size(105, 23);
            this.saveColorCycleBtn.TabIndex = 0;
            this.saveColorCycleBtn.Text = "Save color cycle";
            this.saveColorCycleBtn.UseVisualStyleBackColor = true;
            this.saveColorCycleBtn.Click += new System.EventHandler(this.saveColorCycleBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.infoDeviceNameLabel);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(10, 183);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(239, 87);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Information";
            // 
            // infoDeviceNameLabel
            // 
            this.infoDeviceNameLabel.AutoSize = true;
            this.infoDeviceNameLabel.Location = new System.Drawing.Point(99, 16);
            this.infoDeviceNameLabel.Name = "infoDeviceNameLabel";
            this.infoDeviceNameLabel.Size = new System.Drawing.Size(78, 13);
            this.infoDeviceNameLabel.TabIndex = 0;
            this.infoDeviceNameLabel.Text = "Not connected";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Connected device:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(255, 218);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(235, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "Set random multistatic";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(255, 276);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(235, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Profile save";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(10, 276);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(239, 23);
            this.button6.TabIndex = 8;
            this.button6.Text = "Set multistatic key via Aura Sync protocol";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(255, 305);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(120, 23);
            this.button8.TabIndex = 10;
            this.button8.Text = "Switch to profile:";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // newProfileIndexNumBox
            // 
            this.newProfileIndexNumBox.Location = new System.Drawing.Point(381, 307);
            this.newProfileIndexNumBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.newProfileIndexNumBox.Name = "newProfileIndexNumBox";
            this.newProfileIndexNumBox.Size = new System.Drawing.Size(109, 20);
            this.newProfileIndexNumBox.TabIndex = 11;
            // 
            // forceExitAuraSyncModeBtn
            // 
            this.forceExitAuraSyncModeBtn.Location = new System.Drawing.Point(255, 189);
            this.forceExitAuraSyncModeBtn.Name = "forceExitAuraSyncModeBtn";
            this.forceExitAuraSyncModeBtn.Size = new System.Drawing.Size(235, 23);
            this.forceExitAuraSyncModeBtn.TabIndex = 12;
            this.forceExitAuraSyncModeBtn.Text = "Force exit Aura Sync mode";
            this.forceExitAuraSyncModeBtn.UseVisualStyleBackColor = true;
            this.forceExitAuraSyncModeBtn.Click += new System.EventHandler(this.forceExitAuraSyncModeBtn_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(10, 305);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 13;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.breathingRandomColorCheckBox);
            this.groupBox4.Controls.Add(this.breathingDoubleColorCheckBox);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.breathingSlow);
            this.groupBox4.Controls.Add(this.breathingFast);
            this.groupBox4.Controls.Add(this.breathingMedium);
            this.groupBox4.Location = new System.Drawing.Point(255, 10);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(235, 167);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Breathing effect";
            // 
            // breathingRandomColorCheckBox
            // 
            this.breathingRandomColorCheckBox.AutoSize = true;
            this.breathingRandomColorCheckBox.Location = new System.Drawing.Point(7, 132);
            this.breathingRandomColorCheckBox.Name = "breathingRandomColorCheckBox";
            this.breathingRandomColorCheckBox.Size = new System.Drawing.Size(92, 17);
            this.breathingRandomColorCheckBox.TabIndex = 9;
            this.breathingRandomColorCheckBox.Text = "Random color";
            this.breathingRandomColorCheckBox.UseVisualStyleBackColor = true;
            // 
            // breathingDoubleColorCheckBox
            // 
            this.breathingDoubleColorCheckBox.AutoSize = true;
            this.breathingDoubleColorCheckBox.Location = new System.Drawing.Point(7, 109);
            this.breathingDoubleColorCheckBox.Name = "breathingDoubleColorCheckBox";
            this.breathingDoubleColorCheckBox.Size = new System.Drawing.Size(86, 17);
            this.breathingDoubleColorCheckBox.TabIndex = 8;
            this.breathingDoubleColorCheckBox.Text = "Double color";
            this.breathingDoubleColorCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "First color:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Second color:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(121, 104);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(55, 55);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(121, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 55);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Save breathing";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // breathingSlow
            // 
            this.breathingSlow.AutoSize = true;
            this.breathingSlow.Location = new System.Drawing.Point(5, 56);
            this.breathingSlow.Name = "breathingSlow";
            this.breathingSlow.Size = new System.Drawing.Size(48, 17);
            this.breathingSlow.TabIndex = 3;
            this.breathingSlow.TabStop = true;
            this.breathingSlow.Text = "Slow";
            this.breathingSlow.UseVisualStyleBackColor = true;
            // 
            // breathingFast
            // 
            this.breathingFast.AutoSize = true;
            this.breathingFast.Location = new System.Drawing.Point(5, 13);
            this.breathingFast.Name = "breathingFast";
            this.breathingFast.Size = new System.Drawing.Size(45, 17);
            this.breathingFast.TabIndex = 3;
            this.breathingFast.TabStop = true;
            this.breathingFast.Text = "Fast";
            this.breathingFast.UseVisualStyleBackColor = true;
            // 
            // breathingMedium
            // 
            this.breathingMedium.AutoSize = true;
            this.breathingMedium.Checked = true;
            this.breathingMedium.Location = new System.Drawing.Point(5, 35);
            this.breathingMedium.Name = "breathingMedium";
            this.breathingMedium.Size = new System.Drawing.Size(62, 17);
            this.breathingMedium.TabIndex = 3;
            this.breathingMedium.TabStop = true;
            this.breathingMedium.Text = "Medium";
            this.breathingMedium.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(255, 247);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(235, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "Set random multistatic via Aura Sync protocol";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 336);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.forceExitAuraSyncModeBtn);
            this.Controls.Add(this.newProfileIndexNumBox);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "OpenAsusKeyboardRGB GUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.staticEffectColorPbox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newProfileIndexNumBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox staticEffectColorPbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton colorCycleFast;
        private System.Windows.Forms.RadioButton colorCycleMedium;
        private System.Windows.Forms.RadioButton colorCycleSlow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button saveColorCycleBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label infoDeviceNameLabel;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.NumericUpDown newProfileIndexNumBox;
        private System.Windows.Forms.Button forceExitAuraSyncModeBtn;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton breathingSlow;
        private System.Windows.Forms.RadioButton breathingFast;
        private System.Windows.Forms.RadioButton breathingMedium;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox breathingRandomColorCheckBox;
        private System.Windows.Forms.CheckBox breathingDoubleColorCheckBox;
        private System.Windows.Forms.Button button3;
    }
}