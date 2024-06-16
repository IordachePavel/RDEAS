using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace cannotbelieveitbrokeagain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Serial_Monitor_Click(object sender, EventArgs e)
        {
            using (TTL_Demo msg = new TTL_Demo())
            {
                msg.ShowDialog();
            }
        }

        private void Help_menu_Click(object sender, EventArgs e)
        {
            using (Help_Page msg = new Help_Page())
            {
                msg.ShowDialog();
            }
        }

        private void Atmos_Graph_Button_Click(object sender, EventArgs e)
        {
            using (Sensor_Graphs msg = new Sensor_Graphs())
            {
                msg.ShowDialog();
            }
        }

        private void Self_test_AVR_Click(object sender, EventArgs e)
        {

        }

        private void Ext_dev_cntrl_Click(object sender, EventArgs e)
        {
            using (AreaScanner msg = new AreaScanner())
            {
                msg.ShowDialog();
            }
        }
    }
}