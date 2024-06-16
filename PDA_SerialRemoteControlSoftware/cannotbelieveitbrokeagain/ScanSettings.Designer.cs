namespace cannotbelieveitbrokeagain
{
    partial class ScanSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanSettings));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.BaudBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PortBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.pictureBox2.Size = new System.Drawing.Size(314, 161);
            // 
            // BaudBox
            // 
            this.BaudBox.Items.Add("4800");
            this.BaudBox.Items.Add("9600");
            this.BaudBox.Items.Add("19200");
            this.BaudBox.Items.Add("38400");
            this.BaudBox.Items.Add("57600");
            this.BaudBox.Items.Add("112500");
            this.BaudBox.Location = new System.Drawing.Point(16, 14);
            this.BaudBox.Name = "BaudBox";
            this.BaudBox.Size = new System.Drawing.Size(100, 22);
            this.BaudBox.TabIndex = 3;
            this.BaudBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(122, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.Text = "Baud rate ";
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(16, 42);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(100, 22);
            this.PortBox.TabIndex = 5;
            this.PortBox.SelectedIndexChanged += new System.EventHandler(this.PortBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(122, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.Text = "Com Port";
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(16, 70);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(100, 21);
            this.NameBox.TabIndex = 8;
            this.NameBox.Text = "NAME";
            this.NameBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(122, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 20);
            this.label3.Text = "Name to call external device";
            // 
            // ScanSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BaudBox);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "ScanSettings";
            this.Text = "Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox BaudBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PortBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label label3;
    }
}