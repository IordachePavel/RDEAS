using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;


namespace PdaGamingIfItWasAwesome
{
    public partial class Form1 : Form
    {
        bool FirstTimeGaming = true;
        //Set home values:
        int OrigRX=0;
        int OrigRY=0;
        int OrigLX=0;
        int OrigLY=0;


        bool EnableTimerTrigger = false;// I apologize for the solution, but I do not know anymore if it is me, or the outdated software that can't do one thing right, I have used the same thing before in another program and it worked flawlessly, repeated it here and nothing works again. I am at the end of my own power I do not know what to do anymore, I am in a genuine state of despair. "Oh, why didn't you use ChatGPT?", my brother in Christ, not even it can help me solve this problem. Help from who?? I'm all by myself here. Final solution is to keep the timer running from start and enable via if statement the calling of a subroutine. Its gonna take up resources, but at this point, I do not know.

        public int TestTimer = 0;
        // Toggle keyboard
        bool isKeyboardEnabled = true;

        // Define the global SerialPort instance
        private SerialPort PortThatIsSerial;

        // How does the program handle any received serial messages?
        private int Serial_Reciever_mode=1;
        //1 Normal: Display any received data to serial port.
        //2 Gaming: Run the "joystick visualizer".


        public Form1()
        {
            InitializeComponent();
            // Initialize the global SerialPort instance
            PortThatIsSerial = new SerialPort();
            PortThatIsSerial.DataReceived += SerialReceived;
            ActivateSerialPort(9600);
            tabControl1.TabPages[1].Enabled = false;
            tabControl1.TabPages[2].Enabled = false;
            tabControl1.TabPages[3].Enabled = false;
            timer1.Interval = 1000; // Set interval to 1 second
            timer1.Tick += new EventHandler( Event_check_gaming);
            
            
        }




        public void MCU_Serial_BaudRateAdjust(int baudrate)
        {
            SendSerialMessage("BDA" + Convert.ToString(baudrate));
            Thread.Sleep(250);
            DeactivateSerialPort();
            ActivateSerialPort(baudrate);
        }

        public void ActivateSerialPort(int ValueBaud)
        {
            try
            {
                // Begin serial port
                PortThatIsSerial.PortName = "COM1";
                PortThatIsSerial.BaudRate = ValueBaud;
                PortThatIsSerial.Parity = Parity.None;
                PortThatIsSerial.DataBits = 8;
                PortThatIsSerial.StopBits = StopBits.One;
                // Open the serial port
                PortThatIsSerial.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open the serial port!\nThe program will now close.\nError:" + ex.Message, "Whoops!");
                PortThatIsSerial.Close();
                Application.Exit();
            }
        }

        public void DeactivateSerialPort()
        {
            try
            {
                PortThatIsSerial.Close();
            }
            catch(Exception ex){ //How would you even get here??
               MessageBox.Show("Catastrophic failure!\nCannot close the serial port!\nThe program will now close.\nError:" + ex.Message, "HOW?!?!");
               Application.Exit();
            }
        }

        public void SendSerialMessage(string MSG)
        {
            try
            {
                // Make sure that the serial port is open before writing data
                if (PortThatIsSerial.IsOpen)
                {
                    // Write the data to the serial port
                    PortThatIsSerial.Write(MSG);

                    // PortThatIsSerial.WriteLine(data);
                }
                else
                {
                    MessageBox.Show("Somehow, the serial port is not open.\n But I'll reopen it for you!","HOW?!?!");
                    MessageBox.Show("You really should've not gotten this message, something went wrong.\nIf everything works, carry on, else, restart the program.", "HOW?!?!");
                    //Start it harder, turn it on and off again.
                    Exception ExceptionCaught=null;
                    int BaudSelection = 9600;
                    if (BaudBox.SelectedIndex != -1)
                    {
                        BaudSelection =Convert.ToInt32(BaudBox.Items[BaudBox.SelectedIndex]);
                    }
                    try
                    {
                        PortThatIsSerial.Close();
                    }
                    catch (Exception ex) //It was already closed.
                    {
                        ExceptionCaught = ex;
                        ActivateSerialPort(BaudSelection);
                    }
                    if (ExceptionCaught==null)
                        ActivateSerialPort(BaudSelection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error transmitting data!\nTry again, if it persists, restart the program. Error: "+ ex.Message);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void separateValues(string MSG, out int RX, out int RY, out int LX, out int LY)
        {
            RX = RY = LX = LY = 0; 
            string[] parts = MSG.Split(' '); 
            foreach (string part in parts)
            {
                string[] keyValue = part.Split('=');

                if (keyValue.Length == 2) 
                {
                    int value = 0;
                    try
                    {
                        value = Convert.ToInt32(keyValue[1]); 
                    }
                    catch (Exception ex)
                    {
                        if (FirstTimeGaming)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        //Don't do anything. Nothing happened. its alright. the program must continue.
                    }

                    if (keyValue[0] == "RX")
                    {
                        RX = value;
                    }
                    else if (keyValue[0] == "RY")
                    {
                        RY = value;
                    }
                    else if (keyValue[0] == "LX")
                    {
                        LX = value;
                    }
                    else if (keyValue[0] == "LY")
                    {
                        LY = value;
                    }
                }
            }
           // MessageBox.Show("Hello I am subroutine and I have successfully ran!!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputPanel1.Enabled = isKeyboardEnabled;
            isKeyboardEnabled = !isKeyboardEnabled;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int BaudSelection = BaudBox.SelectedIndex;
            if (BaudSelection != -1)
            {
                MCU_Serial_BaudRateAdjust(Convert.ToInt32(BaudBox.Items[BaudSelection] as string));
            }
            else
            {
                MessageBox.Show("No baud rate selected!\n Serial port is currently set on 9600.", "No selection!");
            }

        }

        public void Event_check_gaming(object sender, EventArgs e) // I HATE TIMERS!! I HATE TIMERS!!! I HATE TIMERS!!! I HATE TIMERS!! I HATE TIMERS!!! I HATE TIMERS!! I HATE TIMERS!!!
        {
            label6.Text = "Tick " + Convert.ToString(TestTimer); // Check if this is still working after the fix.
            if (EnableTimerTrigger)
            {
                TestTimer++;
                if (Serial_Reciever_mode == 2)
                {
                    Serial_Reciever_mode = 1;
                }
                else
                {
                    EnableTimerTrigger = false;
                    debugLabel7("Timer has Stopped!");

                    //You are currently not gaming.

                    if (label1.InvokeRequired)
                    {
                        label1.BeginInvoke((Action)(() => label1.Text = "You are currently not gaming."));
                    }
                    else
                    {
                        label1.Text = "You are currently not gaming.";
                    }

                }
            }
        }

        public void GamingJoystickPosition(int LX, int LY, int RX, int RY)
        {
            if (FirstTimeGaming)
            {
                OrigLX = LX;
                OrigLY = LY;
                OrigRX = RX;
                OrigRY = RY;
                FirstTimeGaming = false;
            }

            // ADC max value is 1023.
            // ADC min value is 0.

           
        }



        public void SerialReceived(object sender, SerialDataReceivedEventArgs e)// What to do when a message is received?
        {
            string Received = PortThatIsSerial.ReadExisting();
            if (Received.Contains("LX") && Received.Contains("LY") && Received.Contains("RX") && Received.Contains("RY"))
            {// its gaming mode measurements, switch to gaming mode;
                EnableTimerTrigger= true;
                //debugLabel7("Timer has activated!");
                Serial_Reciever_mode = 2;
                
            }

            if (Serial_Reciever_mode == 2)
            {
                int A, B, C, D;
                separateValues(Received, out A, out B, out C, out D);
                GamingJoystickPosition(A, B, C, D);
                //debugLabel7(Convert.ToString(A) + " " + Convert.ToString(B) + " " + Convert.ToString(C) + " " + Convert.ToString(D));
                if (label1.InvokeRequired)
                {
                    label1.BeginInvoke((Action)(() => label1.Text = "You are gaming."));
                }
                else
                {
                    label1.Text = "You are gaming.";
                }
            }
            

            if (label5.InvokeRequired)
            {
                label5.BeginInvoke((Action)(() => label5.Text = Received));
            }
            else
            {
                label5.Text = Received;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SendSerialMessage(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MCU_Serial_BaudRateAdjust(9600);
            tabControl1.TabPages[1].Enabled = true;
            tabControl1.TabPages[2].Enabled = true;
            tabControl1.TabPages[3].Enabled = true;
            button1.Enabled = false;
            label3.Text = "All is OK. You're done with this page.";
        }

        private void debugLabel7(string MSG)
        {
            if (label7.InvokeRequired)
            {
                label7.BeginInvoke((Action)(() => label7.Text = MSG));
            }
            else
            {
                label7.Text = MSG;
            }
        }

        private void LY1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}