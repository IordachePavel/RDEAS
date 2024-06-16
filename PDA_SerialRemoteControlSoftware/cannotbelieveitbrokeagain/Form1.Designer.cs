namespace cannotbelieveitbrokeagain
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.Atmos_Graph_Button = new System.Windows.Forms.Button();
            this.SerialMonitor = new System.Windows.Forms.Button();
            this.Ext_dev_cntrl = new System.Windows.Forms.Button();
            this.Self_test_AVR = new System.Windows.Forms.Button();
            this.DB9_Ext_test = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.Help_menu = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // Atmos_Graph_Button
            // 
            this.Atmos_Graph_Button.Location = new System.Drawing.Point(15, 23);
            this.Atmos_Graph_Button.Name = "Atmos_Graph_Button";
            this.Atmos_Graph_Button.Size = new System.Drawing.Size(187, 20);
            this.Atmos_Graph_Button.TabIndex = 0;
            this.Atmos_Graph_Button.Text = "On-Board Sensor Monitor";
            this.Atmos_Graph_Button.Click += new System.EventHandler(this.Atmos_Graph_Button_Click);
            // 
            // SerialMonitor
            // 
            this.SerialMonitor.Location = new System.Drawing.Point(15, 49);
            this.SerialMonitor.Name = "SerialMonitor";
            this.SerialMonitor.Size = new System.Drawing.Size(187, 20);
            this.SerialMonitor.TabIndex = 1;
            this.SerialMonitor.Text = "Manual UART Serial Terminal";
            this.SerialMonitor.Click += new System.EventHandler(this.Serial_Monitor_Click);
            // 
            // Ext_dev_cntrl
            // 
            this.Ext_dev_cntrl.Location = new System.Drawing.Point(15, 75);
            this.Ext_dev_cntrl.Name = "Ext_dev_cntrl";
            this.Ext_dev_cntrl.Size = new System.Drawing.Size(187, 20);
            this.Ext_dev_cntrl.TabIndex = 2;
            this.Ext_dev_cntrl.Text = "External Device";
            this.Ext_dev_cntrl.Click += new System.EventHandler(this.Ext_dev_cntrl_Click);
            // 
            // Self_test_AVR
            // 
            this.Self_test_AVR.Location = new System.Drawing.Point(175, 103);
            this.Self_test_AVR.Name = "Self_test_AVR";
            this.Self_test_AVR.Size = new System.Drawing.Size(115, 20);
            this.Self_test_AVR.TabIndex = 3;
            this.Self_test_AVR.Text = "UART BUS";
            this.Self_test_AVR.Click += new System.EventHandler(this.Self_test_AVR_Click);
            // 
            // DB9_Ext_test
            // 
            this.DB9_Ext_test.Location = new System.Drawing.Point(175, 129);
            this.DB9_Ext_test.Name = "DB9_Ext_test";
            this.DB9_Ext_test.Size = new System.Drawing.Size(115, 20);
            this.DB9_Ext_test.TabIndex = 4;
            this.DB9_Ext_test.Text = "Ext UART BUS";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 20);
            this.label1.Text = "Choose a interfacing method:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Silver;
            this.pictureBox1.Location = new System.Drawing.Point(3, 101);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(298, 76);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(3, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 54);
            this.label2.Text = "On the other hand, you could perform the following self-test operations:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(212, 96);
            // 
            // Help_menu
            // 
            this.Help_menu.Location = new System.Drawing.Point(221, 3);
            this.Help_menu.Name = "Help_menu";
            this.Help_menu.Size = new System.Drawing.Size(96, 20);
            this.Help_menu.TabIndex = 8;
            this.Help_menu.Text = "Help";
            this.Help_menu.Click += new System.EventHandler(this.Help_menu_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(221, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 20);
            this.button1.TabIndex = 14;
            this.button1.Text = "Stopwatch";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(320, 188);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Help_menu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Atmos_Graph_Button);
            this.Controls.Add(this.SerialMonitor);
            this.Controls.Add(this.Ext_dev_cntrl);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.DB9_Ext_test);
            this.Controls.Add(this.Self_test_AVR);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "ENVAAS-E75 Remote Assesment";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Atmos_Graph_Button;
        private System.Windows.Forms.Button SerialMonitor;
        private System.Windows.Forms.Button Ext_dev_cntrl;
        private System.Windows.Forms.Button Self_test_AVR;
        private System.Windows.Forms.Button DB9_Ext_test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button Help_menu;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

