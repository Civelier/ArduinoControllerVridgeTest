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
            this.RefreshButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.PortList = new System.Windows.Forms.ListBox();
            this.ControllerTypeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HandshakeButton = new System.Windows.Forms.Button();
            this.LastMillisLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PingButton = new System.Windows.Forms.Button();
            this.ConnectTimer = new System.Windows.Forms.Timer(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.StickLabel = new System.Windows.Forms.Label();
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
            this.panel1.Controls.Add(this.RefreshButton);
            this.panel1.Controls.Add(this.OpenButton);
            this.panel1.Controls.Add(this.PortList);
            this.panel1.Controls.Add(this.ControllerTypeLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.HandshakeButton);
            this.panel1.Controls.Add(this.LastMillisLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PingButton);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 452);
            this.panel1.TabIndex = 0;
            // 
            // StickPanel
            // 
            this.StickPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.StickPanel.Controls.Add(this.StickCross);
            this.StickPanel.Location = new System.Drawing.Point(50, 292);
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
            this.RawLabel.Location = new System.Drawing.Point(97, 216);
            this.RawLabel.Name = "RawLabel";
            this.RawLabel.Size = new System.Drawing.Size(14, 13);
            this.RawLabel.TabIndex = 22;
            this.RawLabel.Text = "Y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(71, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Raw";
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(97, 203);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(13, 13);
            this.YLabel.TabIndex = 20;
            this.YLabel.Text = "0";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(97, 190);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(13, 13);
            this.XLabel.TabIndex = 19;
            this.XLabel.Text = "0";
            // 
            // B4Label
            // 
            this.B4Label.AutoSize = true;
            this.B4Label.Location = new System.Drawing.Point(97, 177);
            this.B4Label.Name = "B4Label";
            this.B4Label.Size = new System.Drawing.Size(32, 13);
            this.B4Label.TabIndex = 18;
            this.B4Label.Text = "False";
            // 
            // B3Label
            // 
            this.B3Label.AutoSize = true;
            this.B3Label.Location = new System.Drawing.Point(97, 164);
            this.B3Label.Name = "B3Label";
            this.B3Label.Size = new System.Drawing.Size(32, 13);
            this.B3Label.TabIndex = 17;
            this.B3Label.Text = "False";
            // 
            // B2Label
            // 
            this.B2Label.AutoSize = true;
            this.B2Label.Location = new System.Drawing.Point(97, 151);
            this.B2Label.Name = "B2Label";
            this.B2Label.Size = new System.Drawing.Size(32, 13);
            this.B2Label.TabIndex = 16;
            this.B2Label.Text = "False";
            // 
            // B1Label
            // 
            this.B1Label.AutoSize = true;
            this.B1Label.Location = new System.Drawing.Point(97, 138);
            this.B1Label.Name = "B1Label";
            this.B1Label.Size = new System.Drawing.Size(32, 13);
            this.B1Label.TabIndex = 15;
            this.B1Label.Text = "False";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(71, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(71, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "B4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "B3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "B2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "B1";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(380, 45);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 8;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(586, 45);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 7;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // PortList
            // 
            this.PortList.FormattingEnabled = true;
            this.PortList.Location = new System.Drawing.Point(461, 50);
            this.PortList.Name = "PortList";
            this.PortList.Size = new System.Drawing.Size(119, 17);
            this.PortList.TabIndex = 6;
            this.PortList.SelectedIndexChanged += new System.EventHandler(this.PortList_SelectedIndexChanged);
            // 
            // ControllerTypeLabel
            // 
            this.ControllerTypeLabel.AutoSize = true;
            this.ControllerTypeLabel.Location = new System.Drawing.Point(186, 17);
            this.ControllerTypeLabel.Name = "ControllerTypeLabel";
            this.ControllerTypeLabel.Size = new System.Drawing.Size(73, 13);
            this.ControllerTypeLabel.TabIndex = 5;
            this.ControllerTypeLabel.Text = "Disconnected";
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Controller type:";
            // 
            // HandshakeButton
            // 
            this.HandshakeButton.Location = new System.Drawing.Point(31, 12);
            this.HandshakeButton.Name = "HandshakeButton";
            this.HandshakeButton.Size = new System.Drawing.Size(75, 23);
            this.HandshakeButton.TabIndex = 3;
            this.HandshakeButton.Text = "Handshake";
            this.HandshakeButton.UseVisualStyleBackColor = true;
            this.HandshakeButton.Click += new System.EventHandler(this.HandshakeButton_Click);
            // 
            // LastMillisLabel
            // 
            this.LastMillisLabel.AutoSize = true;
            this.LastMillisLabel.Location = new System.Drawing.Point(186, 55);
            this.LastMillisLabel.Name = "LastMillisLabel";
            this.LastMillisLabel.Size = new System.Drawing.Size(26, 13);
            this.LastMillisLabel.TabIndex = 2;
            this.LastMillisLabel.Text = "0ms";
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Milliseconds:";
            // 
            // PingButton
            // 
            this.PingButton.Location = new System.Drawing.Point(31, 50);
            this.PingButton.Name = "PingButton";
            this.PingButton.Size = new System.Drawing.Size(75, 23);
            this.PingButton.TabIndex = 0;
            this.PingButton.Text = "Ping";
            this.PingButton.UseVisualStyleBackColor = true;
            this.PingButton.Click += new System.EventHandler(this.PingButton_Click);
            // 
            // ConnectTimer
            // 
            this.ConnectTimer.Enabled = true;
            this.ConnectTimer.Interval = 10;
            this.ConnectTimer.Tick += new System.EventHandler(this.ConnectTimer_Tick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(71, 125);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Stick";
            // 
            // StickLabel
            // 
            this.StickLabel.AutoSize = true;
            this.StickLabel.Location = new System.Drawing.Point(97, 125);
            this.StickLabel.Name = "StickLabel";
            this.StickLabel.Size = new System.Drawing.Size(32, 13);
            this.StickLabel.TabIndex = 25;
            this.StickLabel.Text = "False";
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
        private System.Windows.Forms.Button PingButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LastMillisLabel;
        private System.Windows.Forms.Label ControllerTypeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button HandshakeButton;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.ListBox PortList;
        private System.Windows.Forms.Button RefreshButton;
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
    }
}

