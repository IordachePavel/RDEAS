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
    public partial class TTL_Demo : Form
    {
        // Define the global SerialPort instance
        private SerialPort PortThatIsSerial;

        public TTL_Demo()
        {
            InitializeComponent();

            // Initialize the global SerialPort instance
            PortThatIsSerial = new SerialPort();
            PortThatIsSerial.DataReceived += SerialReceived;

            // Populate the ComboBox with available serial ports
            string[] SerialPorts = SerialPort.GetPortNames();
            foreach (string portName in SerialPorts)
            {
                PortBox.Items.Add(portName);
            }
        }

        private void OpenPort_Click(object sender, EventArgs e)
        {
            // Get the selected baud rate and port
            int BaudSelection = BaudBox.SelectedIndex;
            int PortSelection = PortBox.SelectedIndex;

            if (BaudSelection != -1 && PortSelection != -1)
            {
                // Configure the serial port
                PortThatIsSerial.PortName = PortBox.Items[PortSelection] as string;
                PortThatIsSerial.BaudRate = Convert.ToInt32(BaudBox.Items[BaudSelection] as string);
                PortThatIsSerial.Parity = Parity.None;
                PortThatIsSerial.DataBits = 8;
                PortThatIsSerial.StopBits = StopBits.One;

                // Open the serial port
                PortThatIsSerial.Open();
                TextDisplay.Text = "Serial port is ready!";
            }
            else
            {
                TextDisplay.Text = "Baud/Port not selected dumbass!!!";
            }
        }

        private void SerialClose_Click(object sender, EventArgs e)
        {
            // Close the serial port
            PortThatIsSerial.Close();
            TextDisplay.Text = "Serial port is closed.";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            TransmitData(textBox1.Text);
        }
        //Transmit data to serial port
        private void TransmitData(string data)
        {
            try
            {
                // Make sure that the serial port is open before writing data
                if (PortThatIsSerial.IsOpen)
                {
                    // Write the data to the serial port
                    PortThatIsSerial.Write(data);

                    // PortThatIsSerial.WriteLine(data);
                }
                else
                {
                    TextDisplay.Text = "Serial port is not open.";
                }
            }
            catch (Exception ex)
            {
                TextDisplay.Text = "Error transmitting data: " + ex.Message;
            }
        }
        // Event handler for receiving data
        private void SerialReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Read the data from the serial port
            string Received = PortThatIsSerial.ReadExisting();

            if (cannotbelieveitbrokeagain.Program.DistinguishFromCommand(Received))// Don't display echoed commands
            {
                // Update TextDisplay TextBox with the received data, from a different thread
                if (TextDisplay.InvokeRequired)
                {
                    TextDisplay.Invoke((Action)(() => TextDisplay.Text = Received));
                    string Intermediary = new string(Received.Where(char.IsDigit).ToArray());
                    Decoder.Invoke((Action)(() => Decoder.Text = Intermediary));

                }
            }   
        }
        // Open atmospherics menu and request measurements
        private void AtmosAsk_Click(object sender, EventArgs e)
        {
            try
            {
                // Make sure that the serial port is open before writing data
                if (PortThatIsSerial.IsOpen)
                {
                    // Write the data to the serial port
                    PortThatIsSerial.Write("MCU 3");

                    // PortThatIsSerial.WriteLine(data);
                }
                else
                {
                    TextDisplay.Text = "Serial port is not open.";
                }
            }
            catch (Exception ex)
            {
                TextDisplay.Text = "Error transmitting data: " + ex.Message;
            }
        }
        // Send command to display main menu
        private void Home_Click(object sender, EventArgs e)
        {
            try
            {
                // Make sure that the serial port is open before writing data
                if (PortThatIsSerial.IsOpen)
                {
                    // Write the data to the serial port
                    PortThatIsSerial.Write("MCU 2");

                    // PortThatIsSerial.WriteLine(data);
                }
                else
                {
                    TextDisplay.Text = "Serial port is not open.";
                }
            }
            catch (Exception ex)
            {
                TextDisplay.Text = "Error transmitting data: " + ex.Message;
            }
        }
    }
}
