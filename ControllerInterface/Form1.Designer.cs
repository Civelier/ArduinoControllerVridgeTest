namespace ControllerInterface
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
            this.components = new System.ComponentModel.Container();
            this.ControllerPort = new System.IO.Ports.SerialPort(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.LQuatWLabel = new System.Windows.Forms.Label();
            this.LQuatZLabel = new System.Windows.Forms.Label();
            this.LQuatYLabel = new System.Windows.Forms.Label();
            this.LQuatXLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.LStickLabel = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.LYLabel = new System.Windows.Forms.Label();
            this.LXLabel = new System.Windows.Forms.Label();
            this.LB4Label = new System.Windows.Forms.Label();
            this.LB3Label = new System.Windows.Forms.Label();
            this.LB2Label = new System.Windows.Forms.Label();
            this.LB1Label = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.LeftStickPanel = new System.Windows.Forms.Panel();
            this.LeftStickCross = new System.Windows.Forms.PictureBox();
            this.InitMPUButton = new System.Windows.Forms.Button();
            this.QuatWLabel = new System.Windows.Forms.Label();
            this.QuatZLabel = new System.Windows.Forms.Label();
            this.QuatYLabel = new System.Windows.Forms.Label();
            this.QuatXLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.CalibrateButton = new System.Windows.Forms.Button();
            this.StickLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.StickPanel = new System.Windows.Forms.Panel();
            this.StickCross = new System.Windows.Forms.PictureBox();
            this.RawLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.B4Label = new System.Windows.Forms.Label();
            this.B3Label = new System.Windows.Forms.Label();
            this.B2Label = new System.Windows.Forms.Label();
            this.B1Label = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ConnectTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.LeftStickPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftStickCross)).BeginInit();
            this.StickPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StickCross)).BeginInit();
            this.SuspendLayout();
            // 
            // ControllerPort
            // 
            this.ControllerPort.BaudRate = 115200;
            this.ControllerPort.PortName = "COM7";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.LQuatWLabel);
            this.panel1.Controls.Add(this.LQuatZLabel);
            this.panel1.Controls.Add(this.LQuatYLabel);
            this.panel1.Controls.Add(this.LQuatXLabel);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.LStickLabel);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.LYLabel);
            this.panel1.Controls.Add(this.LXLabel);
            this.panel1.Controls.Add(this.LB4Label);
            this.panel1.Controls.Add(this.LB3Label);
            this.panel1.Controls.Add(this.LB2Label);
            this.panel1.Controls.Add(this.LB1Label);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Controls.Add(this.label33);
            this.panel1.Controls.Add(this.label34);
            this.panel1.Controls.Add(this.label35);
            this.panel1.Controls.Add(this.label36);
            this.panel1.Controls.Add(this.LeftStickPanel);
            this.panel1.Controls.Add(this.InitMPUButton);
            this.panel1.Controls.Add(this.QuatWLabel);
            this.panel1.Controls.Add(this.QuatZLabel);
            this.panel1.Controls.Add(this.QuatYLabel);
            this.panel1.Controls.Add(this.QuatXLabel);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.CalibrateButton);
            this.panel1.Controls.Add(this.StickLabel);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.StickPanel);
            this.panel1.Controls.Add(this.RawLabel);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.YLabel);
            this.panel1.Controls.Add(this.XLabel);
            this.panel1.Controls.Add(this.B4Label);
            this.panel1.Controls.Add(this.B3Label);
            this.panel1.Controls.Add(this.B2Label);
            this.panel1.Controls.Add(this.B1Label);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 452);
            this.panel1.TabIndex = 0;
            // 
            // LQuatWLabel
            // 
            this.LQuatWLabel.AutoSize = true;
            this.LQuatWLabel.Location = new System.Drawing.Point(198, 109);
            this.LQuatWLabel.Name = "LQuatWLabel";
            this.LQuatWLabel.Size = new System.Drawing.Size(32, 13);
            this.LQuatWLabel.TabIndex = 60;
            this.LQuatWLabel.Text = "False";
            // 
            // LQuatZLabel
            // 
            this.LQuatZLabel.AutoSize = true;
            this.LQuatZLabel.Location = new System.Drawing.Point(198, 96);
            this.LQuatZLabel.Name = "LQuatZLabel";
            this.LQuatZLabel.Size = new System.Drawing.Size(32, 13);
            this.LQuatZLabel.TabIndex = 59;
            this.LQuatZLabel.Text = "False";
            // 
            // LQuatYLabel
            // 
            this.LQuatYLabel.AutoSize = true;
            this.LQuatYLabel.Location = new System.Drawing.Point(198, 83);
            this.LQuatYLabel.Name = "LQuatYLabel";
            this.LQuatYLabel.Size = new System.Drawing.Size(32, 13);
            this.LQuatYLabel.TabIndex = 58;
            this.LQuatYLabel.Text = "False";
            // 
            // LQuatXLabel
            // 
            this.LQuatXLabel.AutoSize = true;
            this.LQuatXLabel.Location = new System.Drawing.Point(198, 70);
            this.LQuatXLabel.Name = "LQuatXLabel";
            this.LQuatXLabel.Size = new System.Drawing.Size(32, 13);
            this.LQuatXLabel.TabIndex = 57;
            this.LQuatXLabel.Text = "False";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(172, 109);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 13);
            this.label13.TabIndex = 56;
            this.label13.Text = "W";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(172, 96);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 13);
            this.label14.TabIndex = 55;
            this.label14.Text = "Z";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(172, 83);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 13);
            this.label19.TabIndex = 54;
            this.label19.Text = "Y";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(172, 70);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 13);
            this.label20.TabIndex = 53;
            this.label20.Text = "X";
            // 
            // LStickLabel
            // 
            this.LStickLabel.AutoSize = true;
            this.LStickLabel.Location = new System.Drawing.Point(86, 57);
            this.LStickLabel.Name = "LStickLabel";
            this.LStickLabel.Size = new System.Drawing.Size(32, 13);
            this.LStickLabel.TabIndex = 52;
            this.LStickLabel.Text = "False";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(60, 57);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(31, 13);
            this.label22.TabIndex = 51;
            this.label22.Text = "Stick";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(86, 148);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(14, 13);
            this.label23.TabIndex = 50;
            this.label23.Text = "Y";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(60, 148);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 13);
            this.label24.TabIndex = 49;
            this.label24.Text = "Raw";
            // 
            // LYLabel
            // 
            this.LYLabel.AutoSize = true;
            this.LYLabel.Location = new System.Drawing.Point(86, 135);
            this.LYLabel.Name = "LYLabel";
            this.LYLabel.Size = new System.Drawing.Size(13, 13);
            this.LYLabel.TabIndex = 48;
            this.LYLabel.Text = "0";
            // 
            // LXLabel
            // 
            this.LXLabel.AutoSize = true;
            this.LXLabel.Location = new System.Drawing.Point(86, 122);
            this.LXLabel.Name = "LXLabel";
            this.LXLabel.Size = new System.Drawing.Size(13, 13);
            this.LXLabel.TabIndex = 47;
            this.LXLabel.Text = "0";
            // 
            // LB4Label
            // 
            this.LB4Label.AutoSize = true;
            this.LB4Label.Location = new System.Drawing.Point(86, 109);
            this.LB4Label.Name = "LB4Label";
            this.LB4Label.Size = new System.Drawing.Size(32, 13);
            this.LB4Label.TabIndex = 46;
            this.LB4Label.Text = "False";
            // 
            // LB3Label
            // 
            this.LB3Label.AutoSize = true;
            this.LB3Label.Location = new System.Drawing.Point(86, 96);
            this.LB3Label.Name = "LB3Label";
            this.LB3Label.Size = new System.Drawing.Size(32, 13);
            this.LB3Label.TabIndex = 45;
            this.LB3Label.Text = "False";
            // 
            // LB2Label
            // 
            this.LB2Label.AutoSize = true;
            this.LB2Label.Location = new System.Drawing.Point(86, 83);
            this.LB2Label.Name = "LB2Label";
            this.LB2Label.Size = new System.Drawing.Size(32, 13);
            this.LB2Label.TabIndex = 44;
            this.LB2Label.Text = "False";
            // 
            // LB1Label
            // 
            this.LB1Label.AutoSize = true;
            this.LB1Label.Location = new System.Drawing.Point(86, 70);
            this.LB1Label.Name = "LB1Label";
            this.LB1Label.Size = new System.Drawing.Size(32, 13);
            this.LB1Label.TabIndex = 43;
            this.LB1Label.Text = "False";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(60, 135);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(14, 13);
            this.label31.TabIndex = 42;
            this.label31.Text = "Y";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(60, 122);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(14, 13);
            this.label32.TabIndex = 41;
            this.label32.Text = "X";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(60, 109);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(20, 13);
            this.label33.TabIndex = 40;
            this.label33.Text = "B4";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(60, 96);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(20, 13);
            this.label34.TabIndex = 39;
            this.label34.Text = "B3";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(60, 83);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(20, 13);
            this.label35.TabIndex = 38;
            this.label35.Text = "B2";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(60, 70);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(20, 13);
            this.label36.TabIndex = 37;
            this.label36.Text = "B1";
            // 
            // LeftStickPanel
            // 
            this.LeftStickPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.LeftStickPanel.Controls.Add(this.LeftStickCross);
            this.LeftStickPanel.Location = new System.Drawing.Point(63, 251);
            this.LeftStickPanel.Name = "LeftStickPanel";
            this.LeftStickPanel.Size = new System.Drawing.Size(100, 100);
            this.LeftStickPanel.TabIndex = 36;
            // 
            // LeftStickCross
            // 
            this.LeftStickCross.Image = global::ControllerInterface.Properties.Resources.Cross;
            this.LeftStickCross.Location = new System.Drawing.Point(34, 34);
            this.LeftStickCross.Name = "LeftStickCross";
            this.LeftStickCross.Size = new System.Drawing.Size(32, 32);
            this.LeftStickCross.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LeftStickCross.TabIndex = 0;
            this.LeftStickCross.TabStop = false;
            // 
            // InitMPUButton
            // 
            this.InitMPUButton.Location = new System.Drawing.Point(263, 23);
            this.InitMPUButton.Name = "InitMPUButton";
            this.InitMPUButton.Size = new System.Drawing.Size(75, 23);
            this.InitMPUButton.TabIndex = 35;
            this.InitMPUButton.Text = "Init";
            this.InitMPUButton.UseVisualStyleBackColor = true;
            this.InitMPUButton.Click += new System.EventHandler(this.InitMPUButton_Click);
            // 
            // QuatWLabel
            // 
            this.QuatWLabel.AutoSize = true;
            this.QuatWLabel.Location = new System.Drawing.Point(494, 109);
            this.QuatWLabel.Name = "QuatWLabel";
            this.QuatWLabel.Size = new System.Drawing.Size(32, 13);
            this.QuatWLabel.TabIndex = 34;
            this.QuatWLabel.Text = "False";
            // 
            // QuatZLabel
            // 
            this.QuatZLabel.AutoSize = true;
            this.QuatZLabel.Location = new System.Drawing.Point(494, 96);
            this.QuatZLabel.Name = "QuatZLabel";
            this.QuatZLabel.Size = new System.Drawing.Size(32, 13);
            this.QuatZLabel.TabIndex = 33;
            this.QuatZLabel.Text = "False";
            // 
            // QuatYLabel
            // 
            this.QuatYLabel.AutoSize = true;
            this.QuatYLabel.Location = new System.Drawing.Point(494, 83);
            this.QuatYLabel.Name = "QuatYLabel";
            this.QuatYLabel.Size = new System.Drawing.Size(32, 13);
            this.QuatYLabel.TabIndex = 32;
            this.QuatYLabel.Text = "False";
            // 
            // QuatXLabel
            // 
            this.QuatXLabel.AutoSize = true;
            this.QuatXLabel.Location = new System.Drawing.Point(494, 70);
            this.QuatXLabel.Name = "QuatXLabel";
            this.QuatXLabel.Size = new System.Drawing.Size(32, 13);
            this.QuatXLabel.TabIndex = 31;
            this.QuatXLabel.Text = "False";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(468, 109);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(18, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "W";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(468, 96);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "Z";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(468, 83);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "Y";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(468, 70);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 13);
            this.label18.TabIndex = 27;
            this.label18.Text = "X";
            // 
            // CalibrateButton
            // 
            this.CalibrateButton.Location = new System.Drawing.Point(217, 285);
            this.CalibrateButton.Name = "CalibrateButton";
            this.CalibrateButton.Size = new System.Drawing.Size(75, 23);
            this.CalibrateButton.TabIndex = 26;
            this.CalibrateButton.Text = "Calibrate";
            this.CalibrateButton.UseVisualStyleBackColor = true;
            this.CalibrateButton.Click += new System.EventHandler(this.CalibrateButton_Click);
            // 
            // StickLabel
            // 
            this.StickLabel.AutoSize = true;
            this.StickLabel.Location = new System.Drawing.Point(382, 57);
            this.StickLabel.Name = "StickLabel";
            this.StickLabel.Size = new System.Drawing.Size(32, 13);
            this.StickLabel.TabIndex = 25;
            this.StickLabel.Text = "False";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(356, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Stick";
            // 
            // StickPanel
            // 
            this.StickPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.StickPanel.Controls.Add(this.StickCross);
            this.StickPanel.Location = new System.Drawing.Point(359, 251);
            this.StickPanel.Name = "StickPanel";
            this.StickPanel.Size = new System.Drawing.Size(100, 100);
            this.StickPanel.TabIndex = 23;
            // 
            // StickCross
            // 
            this.StickCross.Image = global::ControllerInterface.Properties.Resources.Cross;
            this.StickCross.Location = new System.Drawing.Point(34, 34);
            this.StickCross.Name = "StickCross";
            this.StickCross.Size = new System.Drawing.Size(32, 32);
            this.StickCross.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.StickCross.TabIndex = 0;
            this.StickCross.TabStop = false;
            // 
            // RawLabel
            // 
            this.RawLabel.AutoSize = true;
            this.RawLabel.Location = new System.Drawing.Point(382, 148);
            this.RawLabel.Name = "RawLabel";
            this.RawLabel.Size = new System.Drawing.Size(14, 13);
            this.RawLabel.TabIndex = 22;
            this.RawLabel.Text = "Y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(356, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Raw";
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(382, 135);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(13, 13);
            this.YLabel.TabIndex = 20;
            this.YLabel.Text = "0";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(382, 122);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(13, 13);
            this.XLabel.TabIndex = 19;
            this.XLabel.Text = "0";
            // 
            // B4Label
            // 
            this.B4Label.AutoSize = true;
            this.B4Label.Location = new System.Drawing.Point(382, 109);
            this.B4Label.Name = "B4Label";
            this.B4Label.Size = new System.Drawing.Size(32, 13);
            this.B4Label.TabIndex = 18;
            this.B4Label.Text = "False";
            // 
            // B3Label
            // 
            this.B3Label.AutoSize = true;
            this.B3Label.Location = new System.Drawing.Point(382, 96);
            this.B3Label.Name = "B3Label";
            this.B3Label.Size = new System.Drawing.Size(32, 13);
            this.B3Label.TabIndex = 17;
            this.B3Label.Text = "False";
            // 
            // B2Label
            // 
            this.B2Label.AutoSize = true;
            this.B2Label.Location = new System.Drawing.Point(382, 83);
            this.B2Label.Name = "B2Label";
            this.B2Label.Size = new System.Drawing.Size(32, 13);
            this.B2Label.TabIndex = 16;
            this.B2Label.Text = "False";
            // 
            // B1Label
            // 
            this.B1Label.AutoSize = true;
            this.B1Label.Location = new System.Drawing.Point(382, 70);
            this.B1Label.Name = "B1Label";
            this.B1Label.Size = new System.Drawing.Size(32, 13);
            this.B1Label.TabIndex = 15;
            this.B1Label.Text = "False";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(356, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(356, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(356, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "B4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(356, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "B3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(356, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "B2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "B1";
            // 
            // ConnectTimer
            // 
            this.ConnectTimer.Enabled = true;
            this.ConnectTimer.Interval = 20;
            this.ConnectTimer.Tick += new System.EventHandler(this.ConnectTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.LeftStickPanel.ResumeLayout(false);
            this.LeftStickPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftStickCross)).EndInit();
            this.StickPanel.ResumeLayout(false);
            this.StickPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StickCross)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort ControllerPort;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer ConnectTimer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label B1Label;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.Label B4Label;
        private System.Windows.Forms.Label B3Label;
        private System.Windows.Forms.Label B2Label;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label RawLabel;
        private System.Windows.Forms.Panel StickPanel;
        private System.Windows.Forms.PictureBox StickCross;
        private System.Windows.Forms.Label StickLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button CalibrateButton;
        private System.Windows.Forms.Label QuatWLabel;
        private System.Windows.Forms.Label QuatZLabel;
        private System.Windows.Forms.Label QuatYLabel;
        private System.Windows.Forms.Label QuatXLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button InitMPUButton;
        private System.Windows.Forms.Panel LeftStickPanel;
        private System.Windows.Forms.PictureBox LeftStickCross;
        private System.Windows.Forms.Label LQuatWLabel;
        private System.Windows.Forms.Label LQuatZLabel;
        private System.Windows.Forms.Label LQuatYLabel;
        private System.Windows.Forms.Label LQuatXLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label LStickLabel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label LYLabel;
        private System.Windows.Forms.Label LXLabel;
        private System.Windows.Forms.Label LB4Label;
        private System.Windows.Forms.Label LB3Label;
        private System.Windows.Forms.Label LB2Label;
        private System.Windows.Forms.Label LB1Label;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
    }
}

