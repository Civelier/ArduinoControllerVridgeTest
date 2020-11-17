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
            this.MainThreadDispatcher.Tick += new System.EventHandler(this.MainThreadDispatcher_Tick);
            // 
            // ConnectTimer
            // 
            this.ConnectTimer.Enabled = true;
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
        private System.Windows.Forms.Timer MainThreadDispatcher;
        private System.Windows.Forms.Timer ConnectTimer;
    }
}

