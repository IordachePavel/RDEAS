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
    public partial class ScanSettings : Form
    {
        private SerialPort PortThatIsSerial;
        public ScanSettings()
        {
            InitializeComponent();
            PortThatIsSerial = new SerialPort();
            string[] SerialPorts = SerialPort.GetPortNames();
            foreach (string portName in SerialPorts)
            {
                PortBox.Items.Add(portName);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AreaScanner.BaudRate = BaudBox.Items[BaudBox.SelectedIndex] as string;
        }

        private void PortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AreaScanner.PortName =PortBox.Items[PortBox.SelectedIndex] as string;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AreaScanner.CallName = NameBox.Text;
        }
    }
}