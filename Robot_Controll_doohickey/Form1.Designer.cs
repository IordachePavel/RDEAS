using System.Drawing;
using System.Windows.Forms;

namespace Robot_Control_doohickey
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.SendMSG = new System.Windows.Forms.Button();
            this.SendMSGTest = new System.Windows.Forms.TabControl();
            this.SetupLogicControl = new System.Windows.Forms.TabPage();
            this.AddrLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SetIP = new System.Windows.Forms.Button();
            this.Demo_Transmit = new System.Windows.Forms.TabPage();
            this.ReceiveBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MessageBox = new System.Windows.Forms.TextBox();
            this.ComSettings = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PortList = new System.Windows.Forms.ComboBox();
            this.BaudBox = new System.Windows.Forms.TextBox();
            this.BeginSerial = new System.Windows.Forms.Button();
            this.ReadoutDoohickey = new System.Windows.Forms.TabControl();
            this.Config = new System.Windows.Forms.TabPage();
            this.UpdateConfigData = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Graph = new System.Windows.Forms.TabPage();
            this.GraphDataReader = new System.Windows.Forms.TextBox();
            this.MeasurementList = new System.Windows.Forms.ComboBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.DataEx = new System.Windows.Forms.TabPage();
            this.NetworkMSGBox = new System.Windows.Forms.TextBox();
            this.Cntrl = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ButtonReverseB = new System.Windows.Forms.Button();
            this.ButtonForwardsB = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ButtonReverseA = new System.Windows.Forms.Button();
            this.ButtonForwardsA = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TurnLeft = new System.Windows.Forms.Button();
            this.TurnRight = new System.Windows.Forms.Button();
            this.DriveBackwards = new System.Windows.Forms.Button();
            this.DriveForwards = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.TransmitterTimer = new System.Windows.Forms.Timer(this.components);
            this.SerialMSGBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SendMSGTest.SuspendLayout();
            this.SetupLogicControl.SuspendLayout();
            this.Demo_Transmit.SuspendLayout();
            this.ComSettings.SuspendLayout();
            this.ReadoutDoohickey.SuspendLayout();
            this.Config.SuspendLayout();
            this.Graph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.DataEx.SuspendLayout();
            this.Cntrl.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // SendMSG
            // 
            this.SendMSG.Location = new System.Drawing.Point(5, 5);
            this.SendMSG.Name = "SendMSG";
            this.SendMSG.Size = new System.Drawing.Size(64, 20);
            this.SendMSG.TabIndex = 0;
            this.SendMSG.Text = "TX!";
            this.SendMSG.UseVisualStyleBackColor = true;
            this.SendMSG.Click += new System.EventHandler(this.SendMSG_Click_1);
            // 
            // SendMSGTest
            // 
            this.SendMSGTest.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.SendMSGTest.Controls.Add(this.SetupLogicControl);
            this.SendMSGTest.Controls.Add(this.Demo_Transmit);
            this.SendMSGTest.Controls.Add(this.ComSettings);
            this.SendMSGTest.Location = new System.Drawing.Point(22, 313);
            this.SendMSGTest.Name = "SendMSGTest";
            this.SendMSGTest.SelectedIndex = 0;
            this.SendMSGTest.Size = new System.Drawing.Size(212, 187);
            this.SendMSGTest.TabIndex = 1;
            // 
            // SetupLogicControl
            // 
            this.SetupLogicControl.Controls.Add(this.AddrLabel);
            this.SetupLogicControl.Controls.Add(this.label3);
            this.SetupLogicControl.Controls.Add(this.label2);
            this.SetupLogicControl.Controls.Add(this.PortBox);
            this.SetupLogicControl.Controls.Add(this.IPBox);
            this.SetupLogicControl.Controls.Add(this.label1);
            this.SetupLogicControl.Controls.Add(this.SetIP);
            this.SetupLogicControl.Location = new System.Drawing.Point(4, 4);
            this.SetupLogicControl.Name = "SetupLogicControl";
            this.SetupLogicControl.Padding = new System.Windows.Forms.Padding(3);
            this.SetupLogicControl.Size = new System.Drawing.Size(204, 161);
            this.SetupLogicControl.TabIndex = 1;
            this.SetupLogicControl.Text = "Setup";
            this.SetupLogicControl.UseVisualStyleBackColor = true;
            // 
            // AddrLabel
            // 
            this.AddrLabel.AutoSize = true;
            this.AddrLabel.Location = new System.Drawing.Point(9, 137);
            this.AddrLabel.Name = "AddrLabel";
            this.AddrLabel.Size = new System.Drawing.Size(61, 13);
            this.AddrLabel.TabIndex = 6;
            this.AddrLabel.Text = "Not set yet.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Port (e.g 8888)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP Address (e.g. 192.168.0.191)";
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(5, 100);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(86, 20);
            this.PortBox.TabIndex = 3;
            // 
            // IPBox
            // 
            this.IPBox.Location = new System.Drawing.Point(5, 48);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(177, 20);
            this.IPBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Set IP Addr";
            // 
            // SetIP
            // 
            this.SetIP.Location = new System.Drawing.Point(5, 5);
            this.SetIP.Name = "SetIP";
            this.SetIP.Size = new System.Drawing.Size(116, 20);
            this.SetIP.TabIndex = 0;
            this.SetIP.Text = "Set Address";
            this.SetIP.UseVisualStyleBackColor = true;
            this.SetIP.Click += new System.EventHandler(this.SetIP_Click_1);
            // 
            // Demo_Transmit
            // 
            this.Demo_Transmit.Controls.Add(this.ReceiveBox);
            this.Demo_Transmit.Controls.Add(this.label5);
            this.Demo_Transmit.Controls.Add(this.MessageBox);
            this.Demo_Transmit.Controls.Add(this.SendMSG);
            this.Demo_Transmit.Location = new System.Drawing.Point(4, 4);
            this.Demo_Transmit.Name = "Demo_Transmit";
            this.Demo_Transmit.Padding = new System.Windows.Forms.Padding(3);
            this.Demo_Transmit.Size = new System.Drawing.Size(204, 161);
            this.Demo_Transmit.TabIndex = 0;
            this.Demo_Transmit.Text = "RX_TX";
            this.Demo_Transmit.UseVisualStyleBackColor = true;
            // 
            // ReceiveBox
            // 
            this.ReceiveBox.Location = new System.Drawing.Point(5, 77);
            this.ReceiveBox.Multiline = true;
            this.ReceiveBox.Name = "ReceiveBox";
            this.ReceiveBox.ReadOnly = true;
            this.ReceiveBox.Size = new System.Drawing.Size(190, 79);
            this.ReceiveBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Received:";
            // 
            // MessageBox
            // 
            this.MessageBox.Location = new System.Drawing.Point(5, 30);
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.Size = new System.Drawing.Size(190, 20);
            this.MessageBox.TabIndex = 1;
            // 
            // ComSettings
            // 
            this.ComSettings.Controls.Add(this.label7);
            this.ComSettings.Controls.Add(this.label6);
            this.ComSettings.Controls.Add(this.label4);
            this.ComSettings.Controls.Add(this.PortList);
            this.ComSettings.Controls.Add(this.BaudBox);
            this.ComSettings.Controls.Add(this.BeginSerial);
            this.ComSettings.Location = new System.Drawing.Point(4, 4);
            this.ComSettings.Name = "ComSettings";
            this.ComSettings.Size = new System.Drawing.Size(204, 161);
            this.ComSettings.TabIndex = 2;
            this.ComSettings.Text = "JoystickControl";
            this.ComSettings.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 26);
            this.label7.TabIndex = 5;
            this.label7.Text = "Control the target device using the \r\nserial remote";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "COM port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Baud Rate";
            // 
            // PortList
            // 
            this.PortList.FormattingEnabled = true;
            this.PortList.Location = new System.Drawing.Point(3, 58);
            this.PortList.Name = "PortList";
            this.PortList.Size = new System.Drawing.Size(100, 21);
            this.PortList.TabIndex = 2;
            // 
            // BaudBox
            // 
            this.BaudBox.Location = new System.Drawing.Point(3, 32);
            this.BaudBox.Name = "BaudBox";
            this.BaudBox.Size = new System.Drawing.Size(100, 20);
            this.BaudBox.TabIndex = 1;
            // 
            // BeginSerial
            // 
            this.BeginSerial.Location = new System.Drawing.Point(3, 3);
            this.BeginSerial.Name = "BeginSerial";
            this.BeginSerial.Size = new System.Drawing.Size(75, 23);
            this.BeginSerial.TabIndex = 0;
            this.BeginSerial.Text = "Begin";
            this.BeginSerial.UseVisualStyleBackColor = true;
            this.BeginSerial.Click += new System.EventHandler(this.BeginSerial_Click);
            // 
            // ReadoutDoohickey
            // 
            this.ReadoutDoohickey.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.ReadoutDoohickey.Controls.Add(this.Config);
            this.ReadoutDoohickey.Controls.Add(this.Graph);
            this.ReadoutDoohickey.Controls.Add(this.DataEx);
            this.ReadoutDoohickey.Controls.Add(this.Cntrl);
            this.ReadoutDoohickey.Location = new System.Drawing.Point(22, 12);
            this.ReadoutDoohickey.Multiline = true;
            this.ReadoutDoohickey.Name = "ReadoutDoohickey";
            this.ReadoutDoohickey.SelectedIndex = 0;
            this.ReadoutDoohickey.Size = new System.Drawing.Size(640, 262);
            this.ReadoutDoohickey.TabIndex = 5;
            // 
            // Config
            // 
            this.Config.Controls.Add(this.UpdateConfigData);
            this.Config.Controls.Add(this.textBox2);
            this.Config.Location = new System.Drawing.Point(42, 4);
            this.Config.Name = "Config";
            this.Config.Padding = new System.Windows.Forms.Padding(3);
            this.Config.Size = new System.Drawing.Size(594, 254);
            this.Config.TabIndex = 0;
            this.Config.Text = "Sys. config";
            this.Config.UseVisualStyleBackColor = true;
            // 
            // UpdateConfigData
            // 
            this.UpdateConfigData.Location = new System.Drawing.Point(3, 219);
            this.UpdateConfigData.Name = "UpdateConfigData";
            this.UpdateConfigData.Size = new System.Drawing.Size(585, 23);
            this.UpdateConfigData.TabIndex = 1;
            this.UpdateConfigData.Text = "Update device configuration data";
            this.UpdateConfigData.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 6);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(582, 207);
            this.textBox2.TabIndex = 0;
            // 
            // Graph
            // 
            this.Graph.Controls.Add(this.GraphDataReader);
            this.Graph.Controls.Add(this.MeasurementList);
            this.Graph.Controls.Add(this.chart1);
            this.Graph.Location = new System.Drawing.Point(42, 4);
            this.Graph.Name = "Graph";
            this.Graph.Padding = new System.Windows.Forms.Padding(3);
            this.Graph.Size = new System.Drawing.Size(594, 254);
            this.Graph.TabIndex = 1;
            this.Graph.Text = "Graphing";
            this.Graph.UseVisualStyleBackColor = true;
            // 
            // GraphDataReader
            // 
            this.GraphDataReader.Location = new System.Drawing.Point(145, 119);
            this.GraphDataReader.Multiline = true;
            this.GraphDataReader.Name = "GraphDataReader";
            this.GraphDataReader.ReadOnly = true;
            this.GraphDataReader.Size = new System.Drawing.Size(443, 129);
            this.GraphDataReader.TabIndex = 2;
            // 
            // MeasurementList
            // 
            this.MeasurementList.FormattingEnabled = true;
            this.MeasurementList.Location = new System.Drawing.Point(6, 119);
            this.MeasurementList.Name = "MeasurementList";
            this.MeasurementList.Size = new System.Drawing.Size(121, 21);
            this.MeasurementList.TabIndex = 1;
            // 
            // chart1
            // 
            chartArea3.BackSecondaryColor = System.Drawing.Color.Black;
            chartArea3.BorderColor = System.Drawing.Color.White;
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.Location = new System.Drawing.Point(6, 3);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(582, 110);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // DataEx
            // 
            this.DataEx.Controls.Add(this.label9);
            this.DataEx.Controls.Add(this.label8);
            this.DataEx.Controls.Add(this.SerialMSGBox);
            this.DataEx.Controls.Add(this.NetworkMSGBox);
            this.DataEx.Location = new System.Drawing.Point(42, 4);
            this.DataEx.Name = "DataEx";
            this.DataEx.Size = new System.Drawing.Size(594, 254);
            this.DataEx.TabIndex = 2;
            this.DataEx.Text = "Raw Data";
            this.DataEx.UseVisualStyleBackColor = true;
            // 
            // NetworkMSGBox
            // 
            this.NetworkMSGBox.AcceptsReturn = true;
            this.NetworkMSGBox.AcceptsTab = true;
            this.NetworkMSGBox.Location = new System.Drawing.Point(3, 71);
            this.NetworkMSGBox.Multiline = true;
            this.NetworkMSGBox.Name = "NetworkMSGBox";
            this.NetworkMSGBox.ReadOnly = true;
            this.NetworkMSGBox.Size = new System.Drawing.Size(284, 180);
            this.NetworkMSGBox.TabIndex = 3;
            // 
            // Cntrl
            // 
            this.Cntrl.Controls.Add(this.groupBox3);
            this.Cntrl.Controls.Add(this.groupBox2);
            this.Cntrl.Controls.Add(this.groupBox1);
            this.Cntrl.Controls.Add(this.pictureBox4);
            this.Cntrl.Location = new System.Drawing.Point(42, 4);
            this.Cntrl.Name = "Cntrl";
            this.Cntrl.Size = new System.Drawing.Size(594, 254);
            this.Cntrl.TabIndex = 3;
            this.Cntrl.Text = "Manual control";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ButtonReverseB);
            this.groupBox3.Controls.Add(this.ButtonForwardsB);
            this.groupBox3.Location = new System.Drawing.Point(187, 155);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(172, 84);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Motor B Rotation";
            // 
            // ButtonReverseB
            // 
            this.ButtonReverseB.Location = new System.Drawing.Point(45, 48);
            this.ButtonReverseB.Name = "ButtonReverseB";
            this.ButtonReverseB.Size = new System.Drawing.Size(75, 23);
            this.ButtonReverseB.TabIndex = 1;
            this.ButtonReverseB.Text = "Backwards";
            this.ButtonReverseB.UseVisualStyleBackColor = true;
            // 
            // ButtonForwardsB
            // 
            this.ButtonForwardsB.Location = new System.Drawing.Point(45, 19);
            this.ButtonForwardsB.Name = "ButtonForwardsB";
            this.ButtonForwardsB.Size = new System.Drawing.Size(75, 23);
            this.ButtonForwardsB.TabIndex = 0;
            this.ButtonForwardsB.Text = "Forwards";
            this.ButtonForwardsB.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ButtonReverseA);
            this.groupBox2.Controls.Add(this.ButtonForwardsA);
            this.groupBox2.Location = new System.Drawing.Point(9, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(172, 84);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Motor A Rotation";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // ButtonReverseA
            // 
            this.ButtonReverseA.Location = new System.Drawing.Point(45, 48);
            this.ButtonReverseA.Name = "ButtonReverseA";
            this.ButtonReverseA.Size = new System.Drawing.Size(75, 23);
            this.ButtonReverseA.TabIndex = 1;
            this.ButtonReverseA.Text = "Backwards";
            this.ButtonReverseA.UseVisualStyleBackColor = true;
            this.ButtonReverseA.Click += new System.EventHandler(this.button4_Click);
            // 
            // ButtonForwardsA
            // 
            this.ButtonForwardsA.Location = new System.Drawing.Point(45, 19);
            this.ButtonForwardsA.Name = "ButtonForwardsA";
            this.ButtonForwardsA.Size = new System.Drawing.Size(75, 23);
            this.ButtonForwardsA.TabIndex = 0;
            this.ButtonForwardsA.Text = "Forwards";
            this.ButtonForwardsA.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TurnLeft);
            this.groupBox1.Controls.Add(this.TurnRight);
            this.groupBox1.Controls.Add(this.DriveBackwards);
            this.groupBox1.Controls.Add(this.DriveForwards);
            this.groupBox1.Location = new System.Drawing.Point(9, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 133);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drive control";
            // 
            // TurnLeft
            // 
            this.TurnLeft.Location = new System.Drawing.Point(84, 44);
            this.TurnLeft.Name = "TurnLeft";
            this.TurnLeft.Size = new System.Drawing.Size(50, 44);
            this.TurnLeft.TabIndex = 3;
            this.TurnLeft.Text = "Left";
            this.TurnLeft.UseVisualStyleBackColor = true;
            // 
            // TurnRight
            // 
            this.TurnRight.Location = new System.Drawing.Point(196, 44);
            this.TurnRight.Name = "TurnRight";
            this.TurnRight.Size = new System.Drawing.Size(50, 44);
            this.TurnRight.TabIndex = 2;
            this.TurnRight.Text = "Right";
            this.TurnRight.UseVisualStyleBackColor = true;
            // 
            // DriveBackwards
            // 
            this.DriveBackwards.Location = new System.Drawing.Point(140, 69);
            this.DriveBackwards.Name = "DriveBackwards";
            this.DriveBackwards.Size = new System.Drawing.Size(50, 44);
            this.DriveBackwards.TabIndex = 1;
            this.DriveBackwards.Text = "Backwards";
            this.DriveBackwards.UseVisualStyleBackColor = true;
            // 
            // DriveForwards
            // 
            this.DriveForwards.Location = new System.Drawing.Point(140, 19);
            this.DriveForwards.Name = "DriveForwards";
            this.DriveForwards.Size = new System.Drawing.Size(50, 44);
            this.DriveForwards.TabIndex = 0;
            this.DriveForwards.Text = "Forwards";
            this.DriveForwards.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::Robot_Control_doohickey.Properties.Resources.SensorTurretDetail;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(375, 29);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(203, 188);
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox3.Location = new System.Drawing.Point(12, -1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(654, 287);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox1.Location = new System.Drawing.Point(12, 301);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(233, 211);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Robot_Control_doohickey.Properties.Resources.skydiving_kok__World_s_funniest_image_;
            this.pictureBox2.Location = new System.Drawing.Point(-1, -2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(684, 528);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // TransmitterTimer
            // 
            this.TransmitterTimer.Enabled = true;
            this.TransmitterTimer.Interval = 150;
            // 
            // SerialMSGBox
            // 
            this.SerialMSGBox.Location = new System.Drawing.Point(307, 71);
            this.SerialMSGBox.Multiline = true;
            this.SerialMSGBox.Name = "SerialMSGBox";
            this.SerialMSGBox.ReadOnly = true;
            this.SerialMSGBox.Size = new System.Drawing.Size(284, 180);
            this.SerialMSGBox.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(-1, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(242, 20);
            this.label8.TabIndex = 5;
            this.label8.Text = "Received network messages:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(303, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(222, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "Received serial messages:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 524);
            this.Controls.Add(this.ReadoutDoohickey);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.SendMSGTest);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.SendMSGTest.ResumeLayout(false);
            this.SetupLogicControl.ResumeLayout(false);
            this.SetupLogicControl.PerformLayout();
            this.Demo_Transmit.ResumeLayout(false);
            this.Demo_Transmit.PerformLayout();
            this.ComSettings.ResumeLayout(false);
            this.ComSettings.PerformLayout();
            this.ReadoutDoohickey.ResumeLayout(false);
            this.Config.ResumeLayout(false);
            this.Config.PerformLayout();
            this.Graph.ResumeLayout(false);
            this.Graph.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.DataEx.ResumeLayout(false);
            this.DataEx.PerformLayout();
            this.Cntrl.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button SendMSG;
        private TabControl SendMSGTest;
        private TabPage Demo_Transmit;
        private TabPage SetupLogicControl;
        private TextBox MessageBox;
        private Label label3;
        private Label label2;
        private TextBox PortBox;
        private TextBox IPBox;
        private Label label1;
        private Button SetIP;
        private Label AddrLabel;
        private TextBox ReceiveBox;
        private Label label5;
        private TabPage ComSettings;
        private ComboBox PortList;
        private TextBox BaudBox;
        private Button BeginSerial;
        private Label label6;
        private Label label4;
        private Label label7;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private TabControl ReadoutDoohickey;
        private TabPage Config;
        private TabPage Graph;
        private TabPage DataEx;
        private TabPage Cntrl;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.IO.Ports.SerialPort serialPort1;
        private ComboBox MeasurementList;
        private Button UpdateConfigData;
        private TextBox textBox2;
        private TextBox GraphDataReader;
        private TextBox NetworkMSGBox;
        private PictureBox pictureBox4;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button ButtonReverseA;
        private Button ButtonForwardsA;
        private Button DriveForwards;
        private Button ButtonReverseB;
        private Button ButtonForwardsB;
        private Button TurnLeft;
        private Button TurnRight;
        private Button DriveBackwards;
        private Timer TransmitterTimer;
        private Label label9;
        private Label label8;
        private TextBox SerialMSGBox;
    }
}


