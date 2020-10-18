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
            this.label11 = new System.Windows.Forms.Label();
            this.num_BrightFactor = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label_Status = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label_CallsSec = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label_CallsMin = new System.Windows.Forms.Label();
            this.panel_PreviewColor = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.check_allPreview = new System.Windows.Forms.CheckBox();
            this.panel_dominantColor = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.panel_averageColor = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.panel_colorThief = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.panel_kMeans = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.panel_weightedDominant = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.num_colorBrightness = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Interval)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_Brightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_BrightFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_colorBrightness)).BeginInit();
            this.SuspendLayout();
            // 
            // combo_Displays
            // 
            this.combo_Displays.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.combo_Displays.ForeColor = System.Drawing.Color.White;
            this.combo_Displays.FormattingEnabled = true;
            this.combo_Displays.Location = new System.Drawing.Point(106, 12);
            this.combo_Displays.Name = "combo_Displays";
            this.combo_Displays.Size = new System.Drawing.Size(179, 21);
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
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Light IP";
            // 
            // txt_IP
            // 
            this.txt_IP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_IP.ForeColor = System.Drawing.Color.White;
            this.txt_IP.Location = new System.Drawing.Point(106, 39);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(179, 20);
            this.txt_IP.TabIndex = 3;
            // 
            // picture_Preview
            // 
            this.picture_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picture_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture_Preview.Location = new System.Drawing.Point(12, 247);
            this.picture_Preview.Name = "picture_Preview";
            this.picture_Preview.Size = new System.Drawing.Size(339, 190);
            this.picture_Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture_Preview.TabIndex = 4;
            this.picture_Preview.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Screenshot";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Light Color";
            // 
            // panel_Color
            // 
            this.panel_Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Color.Location = new System.Drawing.Point(360, 247);
            this.panel_Color.Name = "panel_Color";
            this.panel_Color.Size = new System.Drawing.Size(139, 84);
            this.panel_Color.TabIndex = 7;
            // 
            // btn_Start
            // 
            this.btn_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Start.Location = new System.Drawing.Point(450, 205);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(101, 23);
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
            this.combo_Algorithms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.combo_Algorithms.ForeColor = System.Drawing.Color.White;
            this.combo_Algorithms.FormattingEnabled = true;
            this.combo_Algorithms.Location = new System.Drawing.Point(106, 65);
            this.combo_Algorithms.Name = "combo_Algorithms";
            this.combo_Algorithms.Size = new System.Drawing.Size(179, 21);
            this.combo_Algorithms.TabIndex = 9;
            // 
            // check_Preview
            // 
            this.check_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.check_Preview.AutoSize = true;
            this.check_Preview.Checked = true;
            this.check_Preview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_Preview.Location = new System.Drawing.Point(15, 207);
            this.check_Preview.Name = "check_Preview";
            this.check_Preview.Size = new System.Drawing.Size(94, 17);
            this.check_Preview.TabIndex = 11;
            this.check_Preview.Text = "Show Preview";
            this.check_Preview.UseVisualStyleBackColor = true;
            // 
            // numeric_Interval
            // 
            this.numeric_Interval.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numeric_Interval.ForeColor = System.Drawing.Color.White;
            this.numeric_Interval.Location = new System.Drawing.Point(106, 92);
            this.numeric_Interval.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numeric_Interval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numeric_Interval.Name = "numeric_Interval";
            this.numeric_Interval.Size = new System.Drawing.Size(148, 20);
            this.numeric_Interval.TabIndex = 12;
            this.numeric_Interval.Value = new decimal(new int[] {
            128,
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
            this.btn_Detect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txt_Gateway);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btn_Detect);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(291, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 100);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP Lookup";
            // 
            // txt_Gateway
            // 
            this.txt_Gateway.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Gateway.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_Gateway.ForeColor = System.Drawing.Color.White;
            this.txt_Gateway.Location = new System.Drawing.Point(59, 26);
            this.txt_Gateway.Name = "txt_Gateway";
            this.txt_Gateway.Size = new System.Drawing.Size(194, 20);
            this.txt_Gateway.TabIndex = 19;
            this.txt_Gateway.Text = "192.168.15.1";
            // 
            // track_Brightness
            // 
            this.track_Brightness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.track_Brightness.Location = new System.Drawing.Point(506, 247);
            this.track_Brightness.Maximum = 100;
            this.track_Brightness.Name = "track_Brightness";
            this.track_Brightness.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.track_Brightness.Size = new System.Drawing.Size(45, 190);
            this.track_Brightness.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(503, 231);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Brightness";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 119);
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
            this.check_UpdateBrightness.Location = new System.Drawing.Point(106, 118);
            this.check_UpdateBrightness.Name = "check_UpdateBrightness";
            this.check_UpdateBrightness.Size = new System.Drawing.Size(113, 17);
            this.check_UpdateBrightness.TabIndex = 22;
            this.check_UpdateBrightness.Text = "Update Brightness";
            this.check_UpdateBrightness.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 143);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Brightness Factor";
            // 
            // num_BrightFactor
            // 
            this.num_BrightFactor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.num_BrightFactor.DecimalPlaces = 3;
            this.num_BrightFactor.ForeColor = System.Drawing.Color.White;
            this.num_BrightFactor.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.num_BrightFactor.Location = new System.Drawing.Point(106, 141);
            this.num_BrightFactor.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num_BrightFactor.Name = "num_BrightFactor";
            this.num_BrightFactor.Size = new System.Drawing.Size(174, 20);
            this.num_BrightFactor.TabIndex = 23;
            this.num_BrightFactor.Value = new decimal(new int[] {
            13,
            0,
            0,
            65536});
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 663);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Status: ";
            // 
            // label_Status
            // 
            this.label_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Status.AutoSize = true;
            this.label_Status.Location = new System.Drawing.Point(52, 663);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(22, 13);
            this.label_Status.TabIndex = 26;
            this.label_Status.Text = "OK";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(358, 663);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Calls:";
            // 
            // label_CallsSec
            // 
            this.label_CallsSec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_CallsSec.AutoSize = true;
            this.label_CallsSec.Location = new System.Drawing.Point(396, 663);
            this.label_CallsSec.Name = "label_CallsSec";
            this.label_CallsSec.Size = new System.Drawing.Size(23, 13);
            this.label_CallsSec.TabIndex = 28;
            this.label_CallsSec.Text = "0/s";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(448, 663);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Total:";
            // 
            // label_CallsMin
            // 
            this.label_CallsMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_CallsMin.AutoSize = true;
            this.label_CallsMin.Location = new System.Drawing.Point(488, 663);
            this.label_CallsMin.Name = "label_CallsMin";
            this.label_CallsMin.Size = new System.Drawing.Size(49, 13);
            this.label_CallsMin.TabIndex = 30;
            this.label_CallsMin.Text = "0/minute";
            // 
            // panel_PreviewColor
            // 
            this.panel_PreviewColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_PreviewColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_PreviewColor.Location = new System.Drawing.Point(361, 353);
            this.panel_PreviewColor.Name = "panel_PreviewColor";
            this.panel_PreviewColor.Size = new System.Drawing.Size(139, 84);
            this.panel_PreviewColor.TabIndex = 9;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(358, 337);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "Preview Color";
            // 
            // check_allPreview
            // 
            this.check_allPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.check_allPreview.AutoSize = true;
            this.check_allPreview.Location = new System.Drawing.Point(15, 443);
            this.check_allPreview.Name = "check_allPreview";
            this.check_allPreview.Size = new System.Drawing.Size(118, 17);
            this.check_allPreview.TabIndex = 31;
            this.check_allPreview.Text = "Show All Algorithms";
            this.check_allPreview.UseVisualStyleBackColor = true;
            // 
            // panel_dominantColor
            // 
            this.panel_dominantColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_dominantColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_dominantColor.Location = new System.Drawing.Point(15, 476);
            this.panel_dominantColor.Name = "panel_dominantColor";
            this.panel_dominantColor.Size = new System.Drawing.Size(94, 184);
            this.panel_dominantColor.TabIndex = 9;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 460);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "Dominant Color";
            // 
            // panel_averageColor
            // 
            this.panel_averageColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_averageColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_averageColor.Location = new System.Drawing.Point(115, 476);
            this.panel_averageColor.Name = "panel_averageColor";
            this.panel_averageColor.Size = new System.Drawing.Size(94, 184);
            this.panel_averageColor.TabIndex = 33;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(112, 460);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 13);
            this.label17.TabIndex = 32;
            this.label17.Text = "Average Color";
            // 
            // panel_colorThief
            // 
            this.panel_colorThief.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_colorThief.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_colorThief.Location = new System.Drawing.Point(215, 476);
            this.panel_colorThief.Name = "panel_colorThief";
            this.panel_colorThief.Size = new System.Drawing.Size(94, 184);
            this.panel_colorThief.TabIndex = 35;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(212, 460);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 13);
            this.label18.TabIndex = 34;
            this.label18.Text = "Color Thief";
            // 
            // panel_kMeans
            // 
            this.panel_kMeans.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_kMeans.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_kMeans.Location = new System.Drawing.Point(315, 476);
            this.panel_kMeans.Name = "panel_kMeans";
            this.panel_kMeans.Size = new System.Drawing.Size(94, 184);
            this.panel_kMeans.TabIndex = 37;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(312, 460);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(73, 13);
            this.label19.TabIndex = 36;
            this.label19.Text = "Color KMeans";
            // 
            // panel_weightedDominant
            // 
            this.panel_weightedDominant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_weightedDominant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_weightedDominant.Location = new System.Drawing.Point(415, 476);
            this.panel_weightedDominant.Name = "panel_weightedDominant";
            this.panel_weightedDominant.Size = new System.Drawing.Size(94, 184);
            this.panel_weightedDominant.TabIndex = 39;
            this.panel_weightedDominant.Visible = false;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(412, 460);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(101, 13);
            this.label20.TabIndex = 38;
            this.label20.Text = "Weighted Dominant";
            this.label20.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 169);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(83, 13);
            this.label21.TabIndex = 41;
            this.label21.Text = "Color Brightness";
            // 
            // num_colorBrightness
            // 
            this.num_colorBrightness.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.num_colorBrightness.DecimalPlaces = 3;
            this.num_colorBrightness.ForeColor = System.Drawing.Color.White;
            this.num_colorBrightness.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.num_colorBrightness.Location = new System.Drawing.Point(106, 167);
            this.num_colorBrightness.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num_colorBrightness.Name = "num_colorBrightness";
            this.num_colorBrightness.Size = new System.Drawing.Size(174, 20);
            this.num_colorBrightness.TabIndex = 40;
            this.num_colorBrightness.Value = new decimal(new int[] {
            12,
            0,
            0,
            65536});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(561, 685);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.num_colorBrightness);
            this.Controls.Add(this.panel_weightedDominant);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.panel_kMeans);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.panel_colorThief);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.panel_averageColor);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.panel_dominantColor);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.check_allPreview);
            this.Controls.Add(this.panel_PreviewColor);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label_CallsMin);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.picture_Preview);
            this.Controls.Add(this.label_CallsSec);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label_Status);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.num_BrightFactor);
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
            this.Controls.Add(this.txt_IP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combo_Displays);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MainForm";
            this.Text = "Ambipoor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picture_Preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Interval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_Brightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_BrightFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_colorBrightness)).EndInit();
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
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown num_BrightFactor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label_CallsSec;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label_CallsMin;
        private System.Windows.Forms.Panel panel_PreviewColor;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox check_allPreview;
        private System.Windows.Forms.Panel panel_dominantColor;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel_averageColor;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel_colorThief;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel_kMeans;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel_weightedDominant;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown num_colorBrightness;
    }
}

