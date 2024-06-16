namespace cannotbelieveitbrokeagain
{
    partial class Sensor_Graphs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sensor_Graphs));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.BeginMeasure = new System.Windows.Forms.Button();
            this.GraphReload = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ReceivedLED = new System.Windows.Forms.PictureBox();
            this.RequestLED = new System.Windows.Forms.PictureBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.TimeBox = new System.Windows.Forms.ComboBox();
            this.Measurement_receive = new System.Windows.Forms.Label();
            this.Temp_Read = new System.Windows.Forms.Label();
            this.Atm_Read = new System.Windows.Forms.Label();
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
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(315, 125);
            // 
            // BeginMeasure
            // 
            this.BeginMeasure.Location = new System.Drawing.Point(3, 134);
            this.BeginMeasure.Name = "BeginMeasure";
            this.BeginMeasure.Size = new System.Drawing.Size(118, 20);
            this.BeginMeasure.TabIndex = 2;
            this.BeginMeasure.Text = "Start Measuring";
            this.BeginMeasure.Click += new System.EventHandler(this.BeginMeasure_Click);
            // 
            // GraphReload
            // 
            this.GraphReload.Location = new System.Drawing.Point(3, 160);
            this.GraphReload.Name = "GraphReload";
            this.GraphReload.Size = new System.Drawing.Size(118, 20);
            this.GraphReload.TabIndex = 3;
            this.GraphReload.Text = "Reset Graph";
            this.GraphReload.Click += new System.EventHandler(this.GraphReload_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox3.Location = new System.Drawing.Point(127, 134);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(190, 46);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(135, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 26);
            this.label1.Text = "Time between measurements (ms)\r\n\r\n";
            // 
            // ReceivedLED
            // 
            this.ReceivedLED.BackColor = System.Drawing.Color.Silver;
            this.ReceivedLED.Location = new System.Drawing.Point(287, 180);
            this.ReceivedLED.Name = "ReceivedLED";
            this.ReceivedLED.Size = new System.Drawing.Size(30, 8);
            // 
            // RequestLED
            // 
            this.RequestLED.BackColor = System.Drawing.Color.Silver;
            this.RequestLED.Location = new System.Drawing.Point(251, 180);
            this.RequestLED.Name = "RequestLED";
            this.RequestLED.Size = new System.Drawing.Size(30, 8);
            // 
            // TimeBox
            // 
            this.TimeBox.Items.Add("500");
            this.TimeBox.Items.Add("650");
            this.TimeBox.Items.Add("1000");
            this.TimeBox.Items.Add("2000");
            this.TimeBox.Items.Add("3000");
            this.TimeBox.Items.Add("5000");
            this.TimeBox.Location = new System.Drawing.Point(251, 146);
            this.TimeBox.Name = "TimeBox";
            this.TimeBox.Size = new System.Drawing.Size(55, 22);
            this.TimeBox.TabIndex = 9;
            this.TimeBox.SelectedIndexChanged += new System.EventHandler(this.TimeBox_SelectedIndexChanged);
            // 
            // Measurement_receive
            // 
            this.Measurement_receive.BackColor = System.Drawing.Color.Black;
            this.Measurement_receive.ForeColor = System.Drawing.Color.Yellow;
            this.Measurement_receive.Location = new System.Drawing.Point(3, 108);
            this.Measurement_receive.Name = "Measurement_receive";
            this.Measurement_receive.Size = new System.Drawing.Size(100, 20);
            // 
            // Temp_Read
            // 
            this.Temp_Read.BackColor = System.Drawing.Color.Black;
            this.Temp_Read.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Temp_Read.Location = new System.Drawing.Point(251, 108);
            this.Temp_Read.Name = "Temp_Read";
            this.Temp_Read.Size = new System.Drawing.Size(33, 20);
            // 
            // Atm_Read
            // 
            this.Atm_Read.BackColor = System.Drawing.Color.Black;
            this.Atm_Read.ForeColor = System.Drawing.Color.Red;
            this.Atm_Read.Location = new System.Drawing.Point(287, 108);
            this.Atm_Read.Name = "Atm_Read";
            this.Atm_Read.Size = new System.Drawing.Size(31, 20);
            // 
            // Sensor_Graphs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.Controls.Add(this.Atm_Read);
            this.Controls.Add(this.Temp_Read);
            this.Controls.Add(this.Measurement_receive);
            this.Controls.Add(this.TimeBox);
            this.Controls.Add(this.RequestLED);
            this.Controls.Add(this.ReceivedLED);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.GraphReload);
            this.Controls.Add(this.BeginMeasure);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Sensor_Graphs";
            this.Text = "Sensor Graphs";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button BeginMeasure;
        private System.Windows.Forms.Button GraphReload;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox ReceivedLED;
        private System.Windows.Forms.PictureBox RequestLED;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox TimeBox;
        private System.Windows.Forms.Label Measurement_receive;
        private System.Windows.Forms.Label Temp_Read;
        private System.Windows.Forms.Label Atm_Read;
    }
}