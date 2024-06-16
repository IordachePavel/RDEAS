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
namespace cannotbelieveitbrokeagain
{
    public partial class AreaScanner : Form
    {   
        // 3D map of what's ahead
        Bitmap Graph = new Bitmap(320, 188);
        // Define global serial port
        private SerialPort PortThatIsSerial;
        public static string BaudRate;
        public static string PortName;
        public static string CallName;
        public int movementTimer= 0;
        bool active = false;
        bool firstTimeFailure = false;
        public double YawStepCounter = 0;
        public double PitchStepCounter = 0;
        public double DistanceMemory = 0;
        public double SensorTemp = 0;
        public double TargetTemp = 0;
        public static double[] tempMem = new double[7];
        public static double[] distMem = new double[7];
        //Task memory. Avoid overlapping due to outdated serial protocol, and some very unknown delay on the robot's MCU.
        public char[] TaskMem = new char[65];
        public int[] TaskMemParameter = new int[65];
        public int TaskMemQueue = 1;
        public char TaskInterm;
        public int TaskParamInterm;
        bool TaskReady = true;

        public string StepMem;
        
        public AreaScanner()
        {
            InitializeComponent();
            PortThatIsSerial = new SerialPort();
            PortThatIsSerial.DataReceived += SerialReceived;

            //Zero out the task memory on start
            for (int i = 1; i < 65; i++)
            {
                TaskMem[i] = '\0';
                TaskMemParameter[i] = 0;
            }

            //Task manager clock
            timer1.Tick += new EventHandler(TaskQueueHandler);
            
            timer1.Interval = 500;
            timer1.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // Event handler for receiving data
        private void SerialReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Read the data from the serial port
            string Received = PortThatIsSerial.ReadExisting();
            if (Received.Contains("RCV"))
            {
                PositionCounter.Invoke((Action)(() => PositionCounter.Text = TaskMemQueue.ToString()));
                TaskReady = true;
                DisplayUI();
            }
            if (Received.Contains("TMP"))
            {
                string Intermediary = new string(Received.Where(char.IsDigit).ToArray());
                TargetTemp = Convert.ToDouble(Intermediary)/10.0;
                TaskReady = true;
            }
            if (Received.StartsWith("DST"))
            {
                string Intermediary = new string(Received.Where(char.IsDigit).ToArray());
                DistanceMemory = Convert.ToDouble(Intermediary)/100.0;
                TaskReady = true;
            }
            Status_Read.Invoke((Action)(() => Status_Read.Text = TargetTemp.ToString() + " " + DistanceMemory.ToString()));
        }
        private void DisplayUI()
        {
            Bitmap Graphical = new Bitmap(174, 137);//174, 137
            using (Pen pen = new Pen(Color.White))
            using (Graphics gfx = Graphics.FromImage(Graphical))
            {
                pen.Color = Color.White;
                pen.Width = 1;
                gfx.DrawLine(pen, 87, 0, 87, 137);
                gfx.DrawLine(pen, 43, 68,43+ Convert.ToInt32(20.0 * Math.Cos(Math.PI / 180.0 * ((246.0 - (double)PitchStepCounter) / 123.0 * 90.0))),68+  Convert.ToInt32(20.0 * Math.Sin(Math.PI / 180.0 * ((246.0 - (double)PitchStepCounter) / 123.0 * 90.0))));
                gfx.DrawLine(pen, 129, 68, 129 + Convert.ToInt32(20.0 * Math.Cos(Math.PI / 180.0 * ((380.0 - (double)YawStepCounter) / 190.0 * 90.0))), 68 + Convert.ToInt32(10.0 * Math.Sin(Math.PI / 180.0 * ((380.0 - (double)YawStepCounter) / 190.0 * 90.0))));
            }
            Ekran.Invoke((Action)(() => Ekran.Image = (Bitmap)Graphical.Clone()));
            double value = (246.0 - PitchStepCounter) / 123.0 * 90.0;
            DegDispl.Invoke((Action)(() => DegDispl.Text = value.ToString()));

        }

        private void ButtonBeginSerial_Click_1(object sender, EventArgs e)
        {
            if (!active)
            {
                if (!(string.IsNullOrEmpty(PortName) && string.IsNullOrEmpty(BaudRate)))
                {
                    // Configure the serial port
                    PortThatIsSerial.PortName = PortName;
                    PortThatIsSerial.BaudRate = Convert.ToInt32(BaudRate as string);
                    PortThatIsSerial.Parity = Parity.None;
                    PortThatIsSerial.DataBits = 8;
                    PortThatIsSerial.StopBits = StopBits.One;

                    // Open the serial port
                    PortThatIsSerial.Open();
                    // do something to show that the device is ready
                    MessageBox.Show("READY.");
                    PortThatIsSerial.Write("READY.");
                    ButtonBeginSerial.Text = "Stop";
                    active = true;
                }
                else
                {
                    MessageBox.Show("Baud/Port not selected dumbass!!!");
                }
            }
            else
            {
                PortThatIsSerial.Close();
                ButtonBeginSerial.Text = "Start";
                active = false;
            }
        }

        private void Config_Click_1(object sender, EventArgs e)
        {
            using (ScanSettings msg = new ScanSettings())
            {
                msg.ShowDialog();
            }
        }

        private void AreaScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                Status_Read.Text = "Turn Left";
                Drive_Left();
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                Status_Read.Text = "Turn Right";
                Drive_Right();
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                Status_Read.Text = "Backwards";
                Drive_Backwards();
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                Status_Read.Text = "Forwards";
                Drive_Forwards();
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

        }
        private void Drive_Forwards()
        {
            if (active)
                TaskHandlerAddTask('F', movementTimer);
            else
            {
                Status_Read.Text = "Port not ready.";
            }
        }
        private void Drive_Left()
        {
            if (active)
                TaskHandlerAddTask('L', movementTimer);
            else
            {
                Status_Read.Text = "Port not ready.";
            }
        }
        private void Drive_Right()
        {
            if (active)
                TaskHandlerAddTask('R', movementTimer);
            else
            {
                Status_Read.Text = "Port not ready.";
            }
        }
        private void Drive_Backwards()
        {
            if (active)
                TaskHandlerAddTask('B', movementTimer);
            else
            {
                Status_Read.Text = "Port not ready.";
            }
        }
        private void TurnHeadRight()
        {
            if (active)
            {
                if (movementTimer < 381)
                {
                    if (TaskHandlerAddTask('Y', movementTimer))
                        YawStepCounter = YawStepCounter - movementTimer;
                }
                else
                {
                    MessageBox.Show("Timer exceeds 180 degrees of movement. This can cause damage to the head due to wires tangling.");
                }
            }
            else
            {
                Status_Read.Text = "Port not ready.";
            }
        }
        private void TurnHeadLeft()
        {
            if (active)
            {
                if (movementTimer < 381)
                {
                    if (TaskHandlerAddTask('X', movementTimer))
                        YawStepCounter = YawStepCounter + movementTimer;
                }
                else
                {
                    MessageBox.Show("Timer exceeds 180 degrees of movement. This can cause damage to the head due to wires tangling.");
                }
            }
            else
            {
                Status_Read.Text = "Port not ready.";
            }
        }
        private void HeadLookUp()
        {
            if (active)
            {
                if (movementTimer < 247)
                {
                    if (TaskHandlerAddTask('D', movementTimer))
                        PitchStepCounter = PitchStepCounter - movementTimer;
                }
                else
                {
                    MessageBox.Show("Timer exceeds 180 degrees of movement. This can cause damage to the head due to wires tangling.");
                }
            }
            else
            {
                Status_Read.Text = "Port not ready.";
            }
        }
        private void HeadLookDown()
        {
            if (active)
            {
                if (movementTimer < 246)
                {
                    if(TaskHandlerAddTask('U',movementTimer))
                    PitchStepCounter = PitchStepCounter + movementTimer;
                }
                else
                {
                    MessageBox.Show("Timer exceeds 180 degrees of movement. This can cause damage to the head due to wires tangling.");
                }
            }
            else
            {
                Status_Read.Text = "Port not ready.";
            }
        }
        private void StopMovement()
        {
            if (active)
            {
                PortThatIsSerial.Write("NAV N");
                for (int i = 1; i < 65; i++)
                {
                    TaskMem[i] = '\0';
                    TaskMemParameter[i] = 0;
                }
                TaskMemQueue = 1;
                TaskReady = true;
            }
            else
            {
                Status_Read.Text = "Port not ready.";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AheadScan();
        }

        private void Forwards_Click(object sender, EventArgs e)
        {
            Status_Read.Text = "Forwards";
            Drive_Forwards();
            // Up
        }

        private void Turn_Left_Click(object sender, EventArgs e)
        {
            Status_Read.Text = "Turn Left";
            Drive_Left();
            // Left
        }

        private void Turn_Right_Click(object sender, EventArgs e)
        {
            Status_Read.Text = "Turn Right";
            Drive_Right();
            // Right
        }

        private void Backwards_Click(object sender, EventArgs e)
        {
            Status_Read.Text = "Backwards";
            Drive_Backwards();
            // Down
        }

        private void Head_LookDown_Click(object sender, EventArgs e)
        {
            Status_Read.Text = "Head Move down";
            HeadLookDown();
        }

        private void Head_LookUp_Click(object sender, EventArgs e)
        {
            Status_Read.Text = "Head Move up";
            HeadLookUp();
        }

        private void Head_TurnLeft_Click(object sender, EventArgs e)
        {
            Status_Read.Text = "Head Turn left";
            TurnHeadLeft();
        }

        private void Head_TurnRight_Click(object sender, EventArgs e)
        {
            Status_Read.Text = "Head Turn right";
            TurnHeadRight();

        }

        private void StopAll_Click(object sender, EventArgs e)
        {
            Status_Read.Text = "Full stop";
            StopMovement();
        }

        private void MovementTimer_TextChanged(object sender, EventArgs e)
        {   try{
            movementTimer = Convert.ToInt32(MovementTimer.Text);
            }
        catch (Exception)
        {
            movementTimer = 0;//Clear old value
            if(!firstTimeFailure)
            {
                MessageBox.Show("Invalid parameter format!\nNUMBERS. DIGITS. Have you ever seen a clock\ntell time with words, letters and spaces???\nFurther incorrect parameter entries will be ignored.");
                firstTimeFailure = true;
            }
        }
        }
        public bool TaskHandlerAddTask(char Operation, int Parameter)
        {
            bool AllOK = true;
            if (TaskMemQueue <= 64)
            {
            TaskMem[TaskMemQueue] = Operation;
            TaskMemParameter[TaskMemQueue] = Parameter;
            TaskMemQueue++;
                        }
            else
            {
                MessageBox.Show("Command queue is busy!");
                AllOK = false;
            }
            return AllOK;
        }
        private void TaskQueueHandler(object sender, EventArgs e)
        {
            if (active && TaskReady && TaskMemQueue >1)
            {
                    Thread.Sleep(1000);
                    if (TaskMem[1] == 'A')
                    {
                        tempMem[TaskMemParameter[1]] = TargetTemp;
                    }
                    else if (TaskMem[1] == 'M')
                    {
                        distMem[TaskMemParameter[1]] = DistanceMemory;
                    }
                    else
                    PortThatIsSerial.Write("NAV " + TaskMem[1].ToString() + " " + TaskMemParameter[1].ToString());
                    TaskReady = false;
                    for (int i = 1; i < TaskMemQueue; i++)
                    {
                        TaskMem[i-1] = TaskMem[i];
                        TaskMemParameter[i-1] = TaskMemParameter[i];
                    }
                    TaskMemQueue--;
            }
            if (TaskMemQueue <= 1)
            {
                for (int i = 1; i < 65; i++)
                {
                    TaskMem[i] = '\0';
                    TaskMemParameter[i] = 0;
                }
                TaskMemQueue = 1;
            }
        }
        private void AheadScan()
        {
            TaskHandlerAddTask('X',63);
            Thread.Sleep(200);
            TaskHandlerAddTask('Y', 63);
            Thread.Sleep(200);
            TaskHandlerAddTask('D', 63);
            Thread.Sleep(200);
            TaskHandlerAddTask('U', 63);
            Thread.Sleep(200);

        }

        private void MSRM_Click(object sender, EventArgs e)
        {
            TaskHandlerAddTask('P', 0);
        }

        private void TMP_Click(object sender, EventArgs e)
        {
            TaskHandlerAddTask('T', 0);
        }

        private void Show_Queue_Click(object sender, EventArgs e)
        {
            StepMem = "";
            for (int i = 1; i < TaskMemQueue; i++)
            {
                StepMem = StepMem + TaskMem[i].ToString();
            }
            Steps_Queue.Invoke((Action)(() => Steps_Queue.Text = StepMem));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FullScan3D msg = new FullScan3D())
            {
                msg.ShowDialog();
            }
        }
    }
}