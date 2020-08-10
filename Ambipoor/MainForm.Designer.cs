namespace Ambipoor {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.combo_Displays = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.picture_Preview = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel_Color = new System.Windows.Forms.Panel();
            this.btn_Start = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_Algorithms = new System.Windows.Forms.ComboBox();
            this.check_Preview = new System.Windows.Forms.CheckBox();
            this.numeric_Interval = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timer_Preview = new System.Windows.Forms.Timer(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.btn_Detect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Gateway = new System.Windows.Forms.TextBox();
            this.track_Brightness = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.check_UpdateBrightness = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Interval)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_Brightness)).BeginInit();
            this.SuspendLayout();
            // 
            // combo_Displays
            // 
            this.combo_Displays.FormattingEnabled = true;
            this.combo_Displays.Location = new System.Drawing.Point(80, 12);
            this.combo_Displays.Name = "combo_Displays";
            this.combo_Displays.Size = new System.Drawing.Size(205, 21);
            this.combo_Displays.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Display";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Light IP";
            // 
            // txt_IP
            // 
            this.txt_IP.Location = new System.Drawing.Point(80, 39);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(205, 20);
            this.txt_IP.TabIndex = 3;
            // 
            // picture_Preview
            // 
            this.picture_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture_Preview.Location = new System.Drawing.Point(12, 182);
            this.picture_Preview.Name = "picture_Preview";
            this.picture_Preview.Size = new System.Drawing.Size(339, 190);
            this.picture_Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture_Preview.TabIndex = 4;
            this.picture_Preview.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Screenshot";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Color";
            // 
            // panel_Color
            // 
            this.panel_Color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Color.Location = new System.Drawing.Point(360, 182);
            this.panel_Color.Name = "panel_Color";
            this.panel_Color.Size = new System.Drawing.Size(139, 190);
            this.panel_Color.TabIndex = 7;
            // 
            // btn_Start
            // 
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Start.Location = new System.Drawing.Point(474, 378);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 8;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Algorithm";
            // 
            // combo_Algorithms
            // 
            this.combo_Algorithms.FormattingEnabled = true;
            this.combo_Algorithms.Location = new System.Drawing.Point(80, 65);
            this.combo_Algorithms.Name = "combo_Algorithms";
            this.combo_Algorithms.Size = new System.Drawing.Size(205, 21);
            this.combo_Algorithms.TabIndex = 9;
            // 
            // check_Preview
            // 
            this.check_Preview.AutoSize = true;
            this.check_Preview.Checked = true;
            this.check_Preview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_Preview.Location = new System.Drawing.Point(12, 146);
            this.check_Preview.Name = "check_Preview";
            this.check_Preview.Size = new System.Drawing.Size(94, 17);
            this.check_Preview.TabIndex = 11;
            this.check_Preview.Text = "Show Preview";
            this.check_Preview.UseVisualStyleBackColor = true;
            // 
            // numeric_Interval
            // 
            this.numeric_Interval.Location = new System.Drawing.Point(80, 92);
            this.numeric_Interval.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numeric_Interval.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numeric_Interval.Name = "numeric_Interval";
            this.numeric_Interval.Size = new System.Drawing.Size(174, 20);
            this.numeric_Interval.TabIndex = 12;
            this.numeric_Interval.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Interval";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(260, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "ms";
            // 
            // timer_Preview
            // 
            this.timer_Preview.Tick += new System.EventHandler(this.timer_Preview_Tick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Gateway";
            // 
            // btn_Detect
            // 
            this.btn_Detect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Detect.Location = new System.Drawing.Point(178, 51);
            this.btn_Detect.Name = "btn_Detect";
            this.btn_Detect.Size = new System.Drawing.Size(75, 23);
            this.btn_Detect.TabIndex = 17;
            this.btn_Detect.Text = "Detect";
            this.btn_Detect.UseVisualStyleBackColor = true;
            this.btn_Detect.Click += new System.EventHandler(this.btn_Detect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Gateway);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btn_Detect);
            this.groupBox1.Location = new System.Drawing.Point(291, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 100);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP Lookup";
            // 
            // txt_Gateway
            // 
            this.txt_Gateway.Location = new System.Drawing.Point(59, 26);
            this.txt_Gateway.Name = "txt_Gateway";
            this.txt_Gateway.Size = new System.Drawing.Size(194, 20);
            this.txt_Gateway.TabIndex = 19;
            this.txt_Gateway.Text = "192.168.15.1";
            // 
            // track_Brightness
            // 
            this.track_Brightness.Location = new System.Drawing.Point(506, 182);
            this.track_Brightness.Maximum = 100;
            this.track_Brightness.Name = "track_Brightness";
            this.track_Brightness.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.track_Brightness.Size = new System.Drawing.Size(45, 190);
            this.track_Brightness.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(503, 166);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Brightness";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Brightness";
            // 
            // check_UpdateBrightness
            // 
            this.check_UpdateBrightness.AutoSize = true;
            this.check_UpdateBrightness.Checked = true;
            this.check_UpdateBrightness.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_UpdateBrightness.Enabled = false;
            this.check_UpdateBrightness.Location = new System.Drawing.Point(80, 118);
            this.check_UpdateBrightness.Name = "check_UpdateBrightness";
            this.check_UpdateBrightness.Size = new System.Drawing.Size(113, 17);
            this.check_UpdateBrightness.TabIndex = 22;
            this.check_UpdateBrightness.Text = "Update Brightness";
            this.check_UpdateBrightness.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 409);
            this.Controls.Add(this.check_UpdateBrightness);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.track_Brightness);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numeric_Interval);
            this.Controls.Add(this.check_Preview);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.combo_Algorithms);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.panel_Color);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.picture_Preview);
            this.Controls.Add(this.txt_IP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combo_Displays);
            this.Name = "MainForm";
            this.Text = "Ambipoor";
            ((System.ComponentModel.ISupportInitialize)(this.picture_Preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Interval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_Brightness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combo_Displays;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.PictureBox picture_Preview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel_Color;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combo_Algorithms;
        private System.Windows.Forms.CheckBox check_Preview;
        private System.Windows.Forms.NumericUpDown numeric_Interval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timer_Preview;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_Detect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Gateway;
        private System.Windows.Forms.TrackBar track_Brightness;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox check_UpdateBrightness;
    }
}

