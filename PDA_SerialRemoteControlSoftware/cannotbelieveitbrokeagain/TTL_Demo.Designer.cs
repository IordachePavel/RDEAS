namespace cannotbelieveitbrokeagain
{
    partial class TTL_Demo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TTL_Demo));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.BaudBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextDisplay = new System.Windows.Forms.Label();
            this.PortBox = new System.Windows.Forms.ComboBox();
            this.OpenPort = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.SerialClose = new System.Windows.Forms.Button();
            this.AtmosAsk = new System.Windows.Forms.Button();
            this.Home = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Decoder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BaudBox
            // 
            this.BaudBox.Items.Add("4800");
            this.BaudBox.Items.Add("9600");
            this.BaudBox.Items.Add("19200");
            this.BaudBox.Items.Add("38400");
            this.BaudBox.Items.Add("57600");
            this.BaudBox.Items.Add("112500");
            this.BaudBox.Location = new System.Drawing.Point(12, 32);
            this.BaudBox.Name = "BaudBox";
            this.BaudBox.Size = new System.Drawing.Size(100, 22);
            this.BaudBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Baud Rate";
            // 
            // TextDisplay
            // 
            this.TextDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TextDisplay.Location = new System.Drawing.Point(12, 83);
            this.TextDisplay.Name = "TextDisplay";
            this.TextDisplay.Size = new System.Drawing.Size(193, 23);
            this.TextDisplay.Text = "Received text will be shown here";
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(173, 32);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(141, 22);
            this.PortBox.TabIndex = 3;
            // 
            // OpenPort
            // 
            this.OpenPort.Location = new System.Drawing.Point(218, 9);
            this.OpenPort.Name = "OpenPort";
            this.OpenPort.Size = new System.Drawing.Size(49, 20);
            this.OpenPort.TabIndex = 6;
            this.OpenPort.Text = "Open";
            this.OpenPort.Click += new System.EventHandler(this.OpenPort_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(173, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 20);
            this.label3.Text = "Port";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(185, 60);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 20);
            this.button2.TabIndex = 8;
            this.button2.Text = "Transmit";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(167, 21);
            this.textBox1.TabIndex = 9;
            // 
            // SerialClose
            // 
            this.SerialClose.Location = new System.Drawing.Point(273, 9);
            this.SerialClose.Name = "SerialClose";
            this.SerialClose.Size = new System.Drawing.Size(41, 20);
            this.SerialClose.TabIndex = 13;
            this.SerialClose.Text = "Close";
            this.SerialClose.Click += new System.EventHandler(this.SerialClose_Click);
            // 
            // AtmosAsk
            // 
            this.AtmosAsk.Location = new System.Drawing.Point(218, 86);
            this.AtmosAsk.Name = "AtmosAsk";
            this.AtmosAsk.Size = new System.Drawing.Size(72, 20);
            this.AtmosAsk.TabIndex = 17;
            this.AtmosAsk.Text = "Atmos";
            this.AtmosAsk.Click += new System.EventHandler(this.AtmosAsk_Click);
            // 
            // Home
            // 
            this.Home.Location = new System.Drawing.Point(218, 112);
            this.Home.Name = "Home";
            this.Home.Size = new System.Drawing.Size(72, 20);
            this.Home.TabIndex = 18;
            this.Home.Text = "Home";
            this.Home.Click += new System.EventHandler(this.Home_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 188);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(314, 159);
            // 
            // Decoder
            // 
            this.Decoder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Decoder.Location = new System.Drawing.Point(12, 106);
            this.Decoder.Name = "Decoder";
            this.Decoder.Size = new System.Drawing.Size(193, 26);
            this.Decoder.Text = "Decoder";
            // 
            // TTL_Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.Controls.Add(this.Decoder);
            this.Controls.Add(this.Home);
            this.Controls.Add(this.AtmosAsk);
            this.Controls.Add(this.SerialClose);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.OpenPort);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.TextDisplay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BaudBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "TTL_Demo";
            this.Text = "Serial Monitor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox BaudBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TextDisplay;
        private System.Windows.Forms.ComboBox PortBox;
        private System.Windows.Forms.Button OpenPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button SerialClose;
        private System.Windows.Forms.Button AtmosAsk;
        private System.Windows.Forms.Button Home;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label Decoder;
    }
}