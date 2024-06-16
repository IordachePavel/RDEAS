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
    public partial class FullScan3D : Form
    {

        public FullScan3D()
        {
            InitializeComponent();
            LB1.Text = AreaScanner.tempMem[1].ToString();
            LB2.Text = AreaScanner.tempMem[2].ToString();
            LB3.Text = AreaScanner.tempMem[3].ToString();
            LB4.Text = AreaScanner.tempMem[4].ToString();
            LB5.Text = AreaScanner.tempMem[5].ToString();
            LB6.Text = AreaScanner.tempMem[6].ToString();

            LD1.Text = AreaScanner.distMem[1].ToString();
            LD2.Text = AreaScanner.distMem[2].ToString();
            LD3.Text = AreaScanner.distMem[3].ToString();
            LD4.Text = AreaScanner.distMem[4].ToString();
            LD5.Text = AreaScanner.distMem[5].ToString();
            LD6.Text = AreaScanner.distMem[6].ToString();
        }
    }
}