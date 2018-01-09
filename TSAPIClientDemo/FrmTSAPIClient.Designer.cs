namespace TSAPIClientDemo
{
    partial class FrmTSAPIClient
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblServerID = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtLoginID = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblLoginID = new System.Windows.Forms.Label();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.btnGetServerNames = new System.Windows.Forms.Button();
            this.btnGetDevices = new System.Windows.Forms.Button();
            this.btnMonitorDevice = new System.Windows.Forms.Button();
            this.cboServerNames = new System.Windows.Forms.ComboBox();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this.btnQueryDevice = new System.Windows.Forms.Button();
            this.lblDeviceID = new System.Windows.Forms.Label();
            this.lblNumberToDial = new System.Windows.Forms.Label();
            this.txtNumberToDial = new System.Windows.Forms.TextBox();
            this.btnDial = new System.Windows.Forms.Button();
            this.btnHangUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(143, 119);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblServerID
            // 
            this.lblServerID.AutoSize = true;
            this.lblServerID.Location = new System.Drawing.Point(36, 44);
            this.lblServerID.Name = "lblServerID";
            this.lblServerID.Size = new System.Drawing.Size(55, 13);
            this.lblServerID.TabIndex = 1;
            this.lblServerID.Text = "Server ID:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(97, 93);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(121, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "Avaya123";
            // 
            // txtLoginID
            // 
            this.txtLoginID.Location = new System.Drawing.Point(97, 67);
            this.txtLoginID.Name = "txtLoginID";
            this.txtLoginID.Size = new System.Drawing.Size(121, 20);
            this.txtLoginID.TabIndex = 4;
            this.txtLoginID.Text = "voiceivr";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(36, 96);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password:";
            // 
            // lblLoginID
            // 
            this.lblLoginID.AutoSize = true;
            this.lblLoginID.Location = new System.Drawing.Point(36, 70);
            this.lblLoginID.Name = "lblLoginID";
            this.lblLoginID.Size = new System.Drawing.Size(50, 13);
            this.lblLoginID.TabIndex = 6;
            this.lblLoginID.Text = "Login ID:";
            // 
            // rtbOutput
            // 
            this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOutput.Location = new System.Drawing.Point(12, 345);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(718, 124);
            this.rtbOutput.TabIndex = 7;
            this.rtbOutput.Text = "";
            // 
            // btnGetServerNames
            // 
            this.btnGetServerNames.Location = new System.Drawing.Point(12, 12);
            this.btnGetServerNames.Name = "btnGetServerNames";
            this.btnGetServerNames.Size = new System.Drawing.Size(206, 23);
            this.btnGetServerNames.TabIndex = 8;
            this.btnGetServerNames.Text = "Get Server Names";
            this.btnGetServerNames.UseVisualStyleBackColor = true;
            this.btnGetServerNames.Click += new System.EventHandler(this.btnGetServerNames_Click);
            // 
            // btnGetDevices
            // 
            this.btnGetDevices.Location = new System.Drawing.Point(33, 148);
            this.btnGetDevices.Name = "btnGetDevices";
            this.btnGetDevices.Size = new System.Drawing.Size(185, 23);
            this.btnGetDevices.TabIndex = 9;
            this.btnGetDevices.Text = "Get Devices";
            this.btnGetDevices.UseVisualStyleBackColor = true;
            this.btnGetDevices.Click += new System.EventHandler(this.btnGetDevices_Click);
            // 
            // btnMonitorDevice
            // 
            this.btnMonitorDevice.Location = new System.Drawing.Point(33, 232);
            this.btnMonitorDevice.Name = "btnMonitorDevice";
            this.btnMonitorDevice.Size = new System.Drawing.Size(185, 23);
            this.btnMonitorDevice.TabIndex = 10;
            this.btnMonitorDevice.Text = "Monitor Device";
            this.btnMonitorDevice.UseVisualStyleBackColor = true;
            this.btnMonitorDevice.Click += new System.EventHandler(this.btnMonitorDevice_Click);
            // 
            // cboServerNames
            // 
            this.cboServerNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServerNames.FormattingEnabled = true;
            this.cboServerNames.Location = new System.Drawing.Point(97, 40);
            this.cboServerNames.Name = "cboServerNames";
            this.cboServerNames.Size = new System.Drawing.Size(121, 21);
            this.cboServerNames.TabIndex = 11;
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.Location = new System.Drawing.Point(97, 177);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(121, 20);
            this.txtDeviceID.TabIndex = 12;
            // 
            // btnQueryDevice
            // 
            this.btnQueryDevice.Location = new System.Drawing.Point(33, 203);
            this.btnQueryDevice.Name = "btnQueryDevice";
            this.btnQueryDevice.Size = new System.Drawing.Size(185, 23);
            this.btnQueryDevice.TabIndex = 13;
            this.btnQueryDevice.Text = "Query Device";
            this.btnQueryDevice.UseVisualStyleBackColor = true;
            this.btnQueryDevice.Click += new System.EventHandler(this.btnQueryDevice_Click);
            // 
            // lblDeviceID
            // 
            this.lblDeviceID.AutoSize = true;
            this.lblDeviceID.Location = new System.Drawing.Point(36, 180);
            this.lblDeviceID.Name = "lblDeviceID";
            this.lblDeviceID.Size = new System.Drawing.Size(58, 13);
            this.lblDeviceID.TabIndex = 14;
            this.lblDeviceID.Text = "Device ID:";
            // 
            // lblNumberToDial
            // 
            this.lblNumberToDial.AutoSize = true;
            this.lblNumberToDial.Location = new System.Drawing.Point(36, 264);
            this.lblNumberToDial.Name = "lblNumberToDial";
            this.lblNumberToDial.Size = new System.Drawing.Size(80, 13);
            this.lblNumberToDial.TabIndex = 16;
            this.lblNumberToDial.Text = "Number to Dial:";
            // 
            // txtNumberToDial
            // 
            this.txtNumberToDial.Location = new System.Drawing.Point(122, 261);
            this.txtNumberToDial.Name = "txtNumberToDial";
            this.txtNumberToDial.Size = new System.Drawing.Size(96, 20);
            this.txtNumberToDial.TabIndex = 15;
            // 
            // btnDial
            // 
            this.btnDial.Location = new System.Drawing.Point(33, 287);
            this.btnDial.Name = "btnDial";
            this.btnDial.Size = new System.Drawing.Size(185, 23);
            this.btnDial.TabIndex = 17;
            this.btnDial.Text = "Dial";
            this.btnDial.UseVisualStyleBackColor = true;
            this.btnDial.Click += new System.EventHandler(this.btnDial_Click);
            // 
            // btnHangUp
            // 
            this.btnHangUp.Location = new System.Drawing.Point(33, 316);
            this.btnHangUp.Name = "btnHangUp";
            this.btnHangUp.Size = new System.Drawing.Size(185, 23);
            this.btnHangUp.TabIndex = 18;
            this.btnHangUp.Text = "Hang Up";
            this.btnHangUp.UseVisualStyleBackColor = true;
            this.btnHangUp.Click += new System.EventHandler(this.btnHangUp_Click);
            // 
            // FrmTSAPIClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 481);
            this.Controls.Add(this.btnHangUp);
            this.Controls.Add(this.btnDial);
            this.Controls.Add(this.lblNumberToDial);
            this.Controls.Add(this.txtNumberToDial);
            this.Controls.Add(this.lblDeviceID);
            this.Controls.Add(this.btnQueryDevice);
            this.Controls.Add(this.txtDeviceID);
            this.Controls.Add(this.cboServerNames);
            this.Controls.Add(this.btnMonitorDevice);
            this.Controls.Add(this.btnGetDevices);
            this.Controls.Add(this.btnGetServerNames);
            this.Controls.Add(this.rtbOutput);
            this.Controls.Add(this.lblLoginID);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtLoginID);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblServerID);
            this.Controls.Add(this.btnConnect);
            this.Name = "FrmTSAPIClient";
            this.Text = "TSAPI Client Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblServerID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLoginID;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblLoginID;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.Button btnGetServerNames;
        private System.Windows.Forms.Button btnGetDevices;
        private System.Windows.Forms.Button btnMonitorDevice;
        private System.Windows.Forms.ComboBox cboServerNames;
        private System.Windows.Forms.TextBox txtDeviceID;
        private System.Windows.Forms.Button btnQueryDevice;
        private System.Windows.Forms.Label lblDeviceID;
        private System.Windows.Forms.Label lblNumberToDial;
        private System.Windows.Forms.TextBox txtNumberToDial;
        private System.Windows.Forms.Button btnDial;
        private System.Windows.Forms.Button btnHangUp;
    }
}

