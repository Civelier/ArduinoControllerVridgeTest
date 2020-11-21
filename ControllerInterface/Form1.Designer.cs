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
            this.RightController = new System.IO.Ports.SerialPort(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.PortList = new System.Windows.Forms.ListBox();
            this.ControllerTypeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HandshakeButton = new System.Windows.Forms.Button();
            this.LastMillisLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PingButton = new System.Windows.Forms.Button();
            this.MyErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.LeftController = new System.IO.Ports.SerialPort(this.components);
            this.MainThreadDispatcher = new System.Windows.Forms.Timer(this.components);
            this.ConnectTimer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.B1Label = new System.Windows.Forms.Label();
            this.B2Label = new System.Windows.Forms.Label();
            this.B3Label = new System.Windows.Forms.Label();
            this.B4Label = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.RawLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MyErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // RightController
            // 
            this.RightController.BaudRate = 115200;
            this.RightController.PortName = "COM4";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
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
            // MyErrorProvider
            // 
            this.MyErrorProvider.ContainerControl = this;
            // 
            // LeftController
            // 
            this.LeftController.BaudRate = 115200;
            this.LeftController.ReadTimeout = 500;
            this.LeftController.WriteTimeout = 500;
            // 
            // MainThreadDispatcher
            // 
            this.MainThreadDispatcher.Interval = 2;
            this.MainThreadDispatcher.Tick += new System.EventHandler(this.MainThreadDispatcher_Tick);
            // 
            // ConnectTimer
            // 
            this.ConnectTimer.Enabled = true;
            this.ConnectTimer.Tick += new System.EventHandler(this.ConnectTimer_Tick);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "B2";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "B4";
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(71, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Y";
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
            // B2Label
            // 
            this.B2Label.AutoSize = true;
            this.B2Label.Location = new System.Drawing.Point(97, 151);
            this.B2Label.Name = "B2Label";
            this.B2Label.Size = new System.Drawing.Size(32, 13);
            this.B2Label.TabIndex = 16;
            this.B2Label.Text = "False";
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
            // B4Label
            // 
            this.B4Label.AutoSize = true;
            this.B4Label.Location = new System.Drawing.Point(97, 177);
            this.B4Label.Name = "B4Label";
            this.B4Label.Size = new System.Drawing.Size(32, 13);
            this.B4Label.TabIndex = 18;
            this.B4Label.Text = "False";
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
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(97, 203);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(13, 13);
            this.YLabel.TabIndex = 20;
            this.YLabel.Text = "0";
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
            // RawLabel
            // 
            this.RawLabel.AutoSize = true;
            this.RawLabel.Location = new System.Drawing.Point(97, 216);
            this.RawLabel.Name = "RawLabel";
            this.RawLabel.Size = new System.Drawing.Size(14, 13);
            this.RawLabel.TabIndex = 22;
            this.RawLabel.Text = "Y";
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
            ((System.ComponentModel.ISupportInitialize)(this.MyErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort RightController;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider MyErrorProvider;
        private System.IO.Ports.SerialPort LeftController;
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
        private System.Windows.Forms.Timer MainThreadDispatcher;
    }
}

