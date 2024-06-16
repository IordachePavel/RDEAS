using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace cannotbelieveitbrokeagain
{
    public partial class Sensor_Graphs : Form
    {
        private SerialPort PortThatIsSerial;

        public Sensor_Graphs()
        {
            InitializeComponent();


            PortThatIsSerial = new SerialPort();
            PortThatIsSerial.DataReceived += SerialReceived;
            timer1.Tick += new EventHandler(Timer);
            timer1.Interval = 999999999;
            BeginSerial();
        }
        Int64 DataMem;
        private void BeginSerial()
        {

            // Configure the serial port
            PortThatIsSerial.PortName = "COM1";
            PortThatIsSerial.BaudRate = 9600;
            PortThatIsSerial.Parity = Parity.None;
            PortThatIsSerial.DataBits = 8;
            PortThatIsSerial.StopBits = StopBits.One;
        }


        private bool startMeasurements = true;
        /// Graph table size
        /// Graph table size
        private Int64[] MeasurementMem = new Int64[158];
        private Int64[] MeasurementMemAtm = new Int64[158];
        private Int32 POS=0;
        private bool firstTimeFailure = false;

        private void decode(Int64 DATA, out int temp, out int pressure)
        {
            Boolean TempNeg = false;
            if (DATA < 0)
            {
                DATA = DATA * -1;
                TempNeg = true;
            }

            int TempLength = Convert.ToInt32(DATA % 10);
            int PressureLength = Convert.ToInt32(DATA / 10 % 10);
            temp = 0;
            pressure = 0;
            DATA = DATA / 100;
            int pwr = 1;
            while (TempLength > 0)
            {
                temp = temp + Convert.ToInt32((DATA % 10) * pwr);
                pwr = pwr * 10;
                TempLength--;
                DATA = DATA / 10;
            }
            if (TempNeg == true)
            {
                temp = temp * -1;
            }
            pwr = 1;
            while (PressureLength > 0)
            {
                pressure = pressure + Convert.ToInt32(DATA % 10 * pwr);
                pwr = pwr * 10;
                PressureLength--;
                DATA = DATA / 10;

            }

        }
        // Graphs the data stored
                private void graph_stored_data()
        {
            Bitmap Graph = new Bitmap(315,125);
            using (Pen pen = new Pen(Color.White))
                using (Graphics gfx = Graphics.FromImage(Graph))
                {
                    pen.Color = Color.Blue;
                    pen.Width = 1;
                    int lastPosMem = 0;
                    for (int i = 0;(i <= 152)&&(i<=POS); i++)
                    {
                        //Draw temperature graph
                        pen.Color = Color.Blue;
                        gfx.DrawLine(pen, lastPosMem, Convert.ToInt32(62 - (MeasurementMem[i])), i*2, Convert.ToInt32(62 - (MeasurementMem[i])));
                        if (Temp_Read.InvokeRequired)
                        {
                            Temp_Read.Invoke((Action)(()=>Temp_Read.Text=Convert.ToString(MeasurementMem[POS-1])));
                        }
                        //Draw atmospheric pressure graph (kPa)
                        pen.Color = Color.Red;
                        gfx.DrawLine(pen, lastPosMem, Convert.ToInt32(62 - MeasurementMemAtm[i]/2), i*2, Convert.ToInt32(62 - MeasurementMemAtm[i]/2));
                        if (Atm_Read.InvokeRequired)
                        {
                            Atm_Read.Invoke((Action)(()=>Atm_Read.Text=Convert.ToString(MeasurementMemAtm[POS-1])));
                        }
                        lastPosMem=i*2;
                    }
                    // 0 line
                    pen.Color = Color.Yellow;
                    gfx.DrawLine(pen, 0, 62, 315, 62);
                }
            if (pictureBox2.InvokeRequired)
            {

                pictureBox2.Invoke((Action)(() => pictureBox2.Image = (Bitmap)Graph.Clone()));
            }
                    Graph.Dispose();
        }

        /// Toggle button for measurements
        private void BeginMeasure_Click(object sender, EventArgs e)
        {
            startMeasurements = !startMeasurements;
            // Toggle button logic, if 1 = disabled if 0 = enabled.
            if (startMeasurements)
            {
                BeginMeasure.Text = "Start Measuring";
                // Close the serial port
                PortThatIsSerial.Close();
                timer1.Enabled = false;
            }
            else
            {
                BeginMeasure.Text = "Stop Measuring";
                // Open the serial port
                try
                {
                    PortThatIsSerial.Open();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                timer1.Enabled = true;
            }
        }

        private void Timer(object sender, EventArgs e)
        {
            PortThatIsSerial.Write("MCU 3");
            // Blinks an imagebox
            RequestLED.BackColor = System.Drawing.Color.Green;
            // Delay for the LED blink to be visible
            for (int i = 999999; i!=0; i--) ;
            RequestLED.BackColor = System.Drawing.Color.Gray;

        }
        //obj.Invoke((Action)(()=>obj.thethingy=value))
        private void SerialReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Read the data from the serial port
            string Received = PortThatIsSerial.ReadExisting();
            if (Measurement_receive.InvokeRequired)
            {
                Measurement_receive.Invoke((Action)(() => Measurement_receive.Text = Received));
            }
            if (ReceivedLED.InvokeRequired)
            {
                try
                {
                    Int64 Data_Received = Convert.ToInt64(Received);
                    int Temp = 0;
                    int Pres = 0;
                    // Decode received measurements
                    decode(Data_Received, out Temp, out Pres);
                    MeasurementMem[POS] = Temp / 100 % 100;
                    MeasurementMemAtm[POS] = Pres / 100;
                    //Increment position on graph. Reset if 158
                    POS++;
                    if (POS == 158)
                    {
                        POS = 0;
                    }
                    //Blinks an imagebox
                    ReceivedLED.Invoke((Action)(() => ReceivedLED.BackColor = System.Drawing.Color.Red));
                    // Delay for the LED blink to be visible
                    for (int i = 999999; i != 0; i--) ;
                    ReceivedLED.Invoke((Action)(() => ReceivedLED.BackColor = System.Drawing.Color.Gray));
                    graph_stored_data();
                }
                catch (Exception)
                {
                    if (firstTimeFailure == false)
                    {
                        MessageBox.Show("MCU screwed up lmao \nIncrease timing interval to avoid measurement loss \n(The measurements are getting garbled)");
                        firstTimeFailure = true;
                    }
                }
            }
            
        }
        // Update delay on time change
        private void TimeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Selection=TimeBox.SelectedIndex;
            timer1.Interval = Convert.ToInt16(TimeBox.Items[Selection] as string);
        }
        // Reload graph by setting position to 0
        private void GraphReload_Click(object sender, EventArgs e)
        {
            POS = 0;
        }
    }
}