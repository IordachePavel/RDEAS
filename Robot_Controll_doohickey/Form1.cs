using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


namespace Robot_Control_doohickey
{
    public partial class Form1 : Form
    {
        private StringBuilder networkBuffer = new StringBuilder();
        private StringBuilder serialBuffer = new StringBuilder();

        bool SerialPortActive = false;

        static TcpClient client;
        static NetworkStream stream;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        private SerialPort PortThatIsSerial;

        bool EnableTimerSendCommand=false;
        bool EnableTimerSendCommandRotation = false;
        char DriveDirection = 'S';
        bool AlreadyStopped = true;

        public Form1()
        {
            InitializeComponent();
            SendMSGTest.TabPages[1].Enabled = false;
            SendMSGTest.TabPages[2].Enabled = false;
            ReadoutDoohickey.Enabled = false;

            DriveForwards.MouseDown += DriveForwards_MouseDown;
            DriveBackwards.MouseDown += DriveBackwards_MouseDown;
            TurnLeft.MouseDown += TurnLeft_MouseDown;
            TurnRight.MouseDown += TurnRight_MouseDown;

            DriveForwards.MouseUp += DriveForwards_MouseUp;
            DriveBackwards.MouseUp += DriveBackwards_MouseUp;
            TurnLeft.MouseUp += TurnLeft_MouseUp;
            TurnRight.MouseUp += TurnRight_MouseUp;

            ButtonForwardsA.MouseDown += ButtonForwardsA_MouseDown;
            ButtonReverseA.MouseDown += ButtonReverseA_MouseDown;
            ButtonForwardsB.MouseDown += ButtonForwardsB_MouseDown;
            ButtonReverseB.MouseDown += ButtonReverseB_MouseDown;

            ButtonForwardsA.MouseUp += ButtonForwardsA_MouseUp;
            ButtonReverseA.MouseUp += ButtonReverseA_MouseUp;
            ButtonForwardsB.MouseUp += ButtonForwardsB_MouseUp;
            ButtonReverseB.MouseUp += ButtonReverseB_MouseUp;

            TransmitterTimer.Tick += new EventHandler(TimerForwardMovementComandTransmitterDelayNonClogRequest);

            // Initialize the global SerialPort instance
            PortThatIsSerial = new SerialPort();
            PortThatIsSerial.DataReceived += SerialReceived;

            // Populate the ComboBox with available serial ports
            string[] SerialPorts = SerialPort.GetPortNames();
            foreach (string portName in SerialPorts)
            {
                PortList.Items.Add(portName);
            }
        }


        private void SerialReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Read the data from the serial port
            string received = PortThatIsSerial.ReadExisting();

            // Accumulate the received data
            serialBuffer.Append(received);

            // Assuming your messages end with a newline character, process complete messages
            while (serialBuffer.ToString().Contains("\n"))
            {
                // Find the position of the first newline character
                int newlineIndex = serialBuffer.ToString().IndexOf('\n');

                // Extract the complete message (up to and including the newline character)
                string completeMessage = serialBuffer.ToString().Substring(0, newlineIndex + 1);

                // Remove the processed message from the buffer
                serialBuffer.Remove(0, newlineIndex + 1);

                // Invoke UI update
                SerialMSGBox.Invoke(new Action(() => SerialMSGBox.AppendText(completeMessage)));

                // Respond to the message if necessary
                if (completeMessage.Trim() == "MSR")
                {
                    SendMessage("SNSR");
                    SendMessage("SNST");
                    PortThatIsSerial.Write("RES");
                }
                else
                {
                    SendMessage(completeMessage.Trim());
                }
            }
        }

        private void TimerForwardMovementComandTransmitterDelayNonClogRequest(object sender, EventArgs e)
        {
            if (EnableTimerSendCommand)
            {
                SendMessage("MOV"+Convert.ToString(DriveDirection));
                AlreadyStopped = false;
            }
            else if(EnableTimerSendCommandRotation) {
                SendMessage("ROT" + Convert.ToString(DriveDirection));
                AlreadyStopped = false;
            }
            else if (!AlreadyStopped)
            {
                SendMessage("MOV"+Convert.ToString('S'));
                DriveDirection = 'S';
                AlreadyStopped = true;
            }
        }

        private void DriveForwards_MouseUp(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommand = false;
        }

        private void DriveForwards_MouseDown(object sender, MouseEventArgs e)
        {
                EnableTimerSendCommand = true;
                DriveDirection = 'F';
        }

        private void DriveBackwards_MouseUp(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommand = false;
        }
        private void DriveBackwards_MouseDown(object sender, MouseEventArgs e)
        {
                EnableTimerSendCommand = true;
                DriveDirection = 'B';
        }
        private void TurnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommand = false;
        }
        private void TurnLeft_MouseDown(object sender, MouseEventArgs e)
        {
                EnableTimerSendCommand = true;
                DriveDirection = 'L';
        }
        private void TurnRight_MouseUp(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommand = false;
        }
        private void TurnRight_MouseDown(object sender, MouseEventArgs e)
        {
                EnableTimerSendCommand = true;
                DriveDirection = 'R';
        }

        //RotButtonBlockStart

        private void ButtonForwardsA_MouseUp(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommandRotation = false;
        }

        private void ButtonForwardsA_MouseDown(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommandRotation = true;
            DriveDirection = 'A';
        }

        private void ButtonReverseA_MouseUp(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommandRotation = false;
        }
        private void ButtonReverseA_MouseDown(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommandRotation = true;
            DriveDirection = 'B';
        }
        private void ButtonForwardsB_MouseUp(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommandRotation = false;
        }
        private void ButtonForwardsB_MouseDown(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommandRotation = true;
            DriveDirection = 'C';
        }
        private void ButtonReverseB_MouseUp(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommandRotation = false;
        }
        private void ButtonReverseB_MouseDown(object sender, MouseEventArgs e)
        {
            EnableTimerSendCommandRotation = true;
            DriveDirection = 'D';
        }


        private void SetupConnection(string ipAddress, int port)
        {
            try
            {
                client = new TcpClient(ipAddress, port);
                stream = client.GetStream();

                MessageReceived += OnMessageReceived;
                BeginReceive();
                AddrLabel.Text = IPBox.Text + ":" + PortBox.Text;
                SendMSGTest.TabPages[1].Enabled = true;
                SendMSGTest.TabPages[2].Enabled = true;
                ReadoutDoohickey.Enabled = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error setting up connection: " + ex.Message);
            }
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            // Handle received message here
            string receivedMessage = e.Message;

            // Update UI components
            ReceiveBox.Invoke(new Action(() => ReceiveBox.AppendText(receivedMessage + "\n")));
            NetworkMSGBox.Invoke(new Action(() => NetworkMSGBox.AppendText(receivedMessage + "\n")));
        }

        private void BeginReceive()
        {
            try
            {
                byte[] buffer = new byte[1024];
                stream.BeginRead(buffer, 0, buffer.Length, asyncResult =>
                {
                    int bytesRead = stream.EndRead(asyncResult);
                    if (bytesRead > 0)
                    {
                        // Convert the received bytes to a string
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        // Accumulate the received data
                        networkBuffer.Append(message);

                        // Assuming your messages end with a newline character, process complete messages
                        while (networkBuffer.ToString().Contains("\n"))
                        {
                            // Find the position of the first newline character
                            int newlineIndex = networkBuffer.ToString().IndexOf('\n');

                            // Extract the complete message (up to and including the newline character)
                            string completeMessage = networkBuffer.ToString().Substring(0, newlineIndex + 1);

                            // Remove the processed message from the buffer
                            networkBuffer.Remove(0, newlineIndex + 1);

                            // Raise the MessageReceived event with the complete message
                            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(completeMessage.Trim()));
                        }

                        // Continue listening for more messages
                        BeginReceive();
                    }
                    else
                    {
                        // Handle disconnection
                        System.Windows.Forms.MessageBox.Show("Client disconnected", "Disconnected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }, null);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void SendMessage(string message)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending message: " + ex.Message);
            }
        }
        public class MessageReceivedEventArgs : EventArgs
        {
            public string Message { get; }

            public MessageReceivedEventArgs(string message)
            {
                Message = message;
            }
        }

        private void SendMSG_Click_1(object sender, EventArgs e)
        {
            SendMessage(MessageBox.Text);
        }

        private void SetIP_Click_1(object sender, EventArgs e)
        {
            SetupConnection(IPBox.Text, Convert.ToInt32(PortBox.Text));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void BeginSerial_Click(object sender, EventArgs e)
        {
            if (!SerialPortActive)
            {               
                // Get the selected baud rate and port
                int BaudSelection = Convert.ToInt32(BaudBox.Text);
                int PortSelection = PortList.SelectedIndex;

                if (BaudSelection != 0 && PortSelection != -1)
                {
                    try
                    {
                        // Configure the serial port
                        PortThatIsSerial.PortName = PortList.Items[PortSelection] as string;
                        PortThatIsSerial.BaudRate = BaudSelection;
                        PortThatIsSerial.Parity = Parity.None;
                        PortThatIsSerial.DataBits = 8;
                        PortThatIsSerial.StopBits = StopBits.One;

                        // Open the serial port
                        PortThatIsSerial.Open();
                        BeginSerial.Text = "Close";
                        SerialPortActive = true;
                    }
                    catch(Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message, "Invalid port configuration!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    System.Windows.Forms.MessageBox.Show("Serial port is ready!", "Yeah!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Baud / Port not selected dumbass!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                PortThatIsSerial.Close();
                SerialPortActive = false;
                BeginSerial.Text = "Begin";
            }
        }

    }
}
