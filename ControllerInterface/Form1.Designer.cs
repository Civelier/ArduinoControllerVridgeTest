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
            // InitMPUButton
            // 
            this.InitMPUButton.Location = new System.Drawing.Point(160, 36);
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
            this.QuatWLabel.Location = new System.Drawing.Point(185, 101);
            this.QuatWLabel.Name = "QuatWLabel";
            this.QuatWLabel.Size = new System.Drawing.Size(32, 13);
            this.QuatWLabel.TabIndex = 34;
            this.QuatWLabel.Text = "False";
            // 
            // QuatZLabel
            // 
            this.QuatZLabel.AutoSize = true;
            this.QuatZLabel.Location = new System.Drawing.Point(185, 88);
            this.QuatZLabel.Name = "QuatZLabel";
            this.QuatZLabel.Size = new System.Drawing.Size(32, 13);
            this.QuatZLabel.TabIndex = 33;
            this.QuatZLabel.Text = "False";
            // 
            // QuatYLabel
            // 
            this.QuatYLabel.AutoSize = true;
            this.QuatYLabel.Location = new System.Drawing.Point(185, 75);
            this.QuatYLabel.Name = "QuatYLabel";
            this.QuatYLabel.Size = new System.Drawing.Size(32, 13);
            this.QuatYLabel.TabIndex = 32;
            this.QuatYLabel.Text = "False";
            // 
            // QuatXLabel
            // 
            this.QuatXLabel.AutoSize = true;
            this.QuatXLabel.Location = new System.Drawing.Point(185, 62);
            this.QuatXLabel.Name = "QuatXLabel";
            this.QuatXLabel.Size = new System.Drawing.Size(32, 13);
            this.QuatXLabel.TabIndex = 31;
            this.QuatXLabel.Text = "False";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(159, 101);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(18, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "W";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(159, 88);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "Z";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(159, 75);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "Y";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(159, 62);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 13);
            this.label18.TabIndex = 27;
            this.label18.Text = "X";
            // 
            // CalibrateButton
            // 
            this.CalibrateButton.Location = new System.Drawing.Point(132, 241);
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
            this.StickLabel.Location = new System.Drawing.Point(73, 49);
            this.StickLabel.Name = "StickLabel";
            this.StickLabel.Size = new System.Drawing.Size(32, 13);
            this.StickLabel.TabIndex = 25;
            this.StickLabel.Text = "False";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Stick";
            // 
            // StickPanel
            // 
            this.StickPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.StickPanel.Controls.Add(this.StickCross);
            this.StickPanel.Location = new System.Drawing.Point(26, 216);
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
            this.RawLabel.Location = new System.Drawing.Point(73, 140);
            this.RawLabel.Name = "RawLabel";
            this.RawLabel.Size = new System.Drawing.Size(14, 13);
            this.RawLabel.TabIndex = 22;
            this.RawLabel.Text = "Y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(47, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Raw";
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(73, 127);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(13, 13);
            this.YLabel.TabIndex = 20;
            this.YLabel.Text = "0";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(73, 114);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(13, 13);
            this.XLabel.TabIndex = 19;
            this.XLabel.Text = "0";
            // 
            // B4Label
            // 
            this.B4Label.AutoSize = true;
            this.B4Label.Location = new System.Drawing.Point(73, 101);
            this.B4Label.Name = "B4Label";
            this.B4Label.Size = new System.Drawing.Size(32, 13);
            this.B4Label.TabIndex = 18;
            this.B4Label.Text = "False";
            // 
            // B3Label
            // 
            this.B3Label.AutoSize = true;
            this.B3Label.Location = new System.Drawing.Point(73, 88);
            this.B3Label.Name = "B3Label";
            this.B3Label.Size = new System.Drawing.Size(32, 13);
            this.B3Label.TabIndex = 17;
            this.B3Label.Text = "False";
            // 
            // B2Label
            // 
            this.B2Label.AutoSize = true;
            this.B2Label.Location = new System.Drawing.Point(73, 75);
            this.B2Label.Name = "B2Label";
            this.B2Label.Size = new System.Drawing.Size(32, 13);
            this.B2Label.TabIndex = 16;
            this.B2Label.Text = "False";
            // 
            // B1Label
            // 
            this.B1Label.AutoSize = true;
            this.B1Label.Location = new System.Drawing.Point(73, 62);
            this.B1Label.Name = "B1Label";
            this.B1Label.Size = new System.Drawing.Size(32, 13);
            this.B1Label.TabIndex = 15;
            this.B1Label.Text = "False";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(47, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "B4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "B3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "B2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 62);
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
    }
}

