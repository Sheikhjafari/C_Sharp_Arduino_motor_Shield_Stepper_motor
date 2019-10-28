using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace Stepper_motor
{
    public partial class Form1 : Form
    {
        bool isConnected = false;
        String[] ports;
        SerialPort port;
        int S1_steps=0, S2_steps = 0;
        int steps_per_round = 2048;
        int octant = 2048 / 8;



        public Form1()
        {
            InitializeComponent();
             disableControls();
            getAvailableComPorts();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                Console.WriteLine(port);
                if (ports[0] != null)
                {
                    comboBox1.SelectedItem = ports[0];
                }
            }
        }

        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                connectToSerialport();
            }
            else
            {
                disconnectFromSerialport();
            }
        }

        private void connectToSerialport()
        {

            try
            {
                string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
                port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
                port.Open();
                isConnected = true;
                btn_connect.Text = "Disconnect";
                enableControls();
            }
            catch
            {

            }
        }

        private void disconnectFromSerialport()
        {
            isConnected = false;
            port.Write("#STOP\n");
            port.Close();
            btn_connect.Text = "Connect";
            disableControls();
        }

        private void enableControls()
        {
            groupBox1.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;

        }

        private void disableControls()
        {
            groupBox1.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        void Stepper_Motor_one_move(int des)
        {
            int diff;
            if (S1_steps < des)
            {
                diff = des - S1_steps;
                port.Write("(S1F," + diff.ToString() + ")\n");
            }
            else if (S1_steps > des)
            {
                diff = S1_steps- des;
                port.Write("(S1B," + diff.ToString() + ")\n");

            }
            else
            {
                //No things
            }
            S1_steps = des;

        }

        void Stepper_Motor_two_move(int des)
        {
            int diff;
            if (S2_steps < des)
            {
                diff = des - S2_steps;
                port.Write("(S2F," + diff.ToString() + ")\n");
            }
            else if (S2_steps > des)
            {
                diff = S2_steps - des;
                port.Write("(S2B," + diff.ToString() + ")\n");

            }
            else
            {
                //No things
            }
            S2_steps = des;

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)Stepper_Motor_one_move(0);
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true) Stepper_Motor_one_move(octant);//45 
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true) Stepper_Motor_one_move(octant*2);//90 
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true) Stepper_Motor_one_move(octant*3);//135
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true) Stepper_Motor_one_move(octant*4);//180
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true) Stepper_Motor_one_move(octant*5);//225
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true) Stepper_Motor_one_move(octant*6);//270
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked == true) Stepper_Motor_one_move(octant*7);//315
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int steps;
            
            if (textBox1.Text !="")
            {
                steps = Convert.ToInt32(textBox1.Text);
                port.Write("(S1F," + steps + ")\n");
                S1_steps = (steps+S1_steps) % steps_per_round;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int steps;

            if (textBox1.Text != "")
            {
                steps = Convert.ToInt32(textBox1.Text);
                port.Write("(S1B," + steps + ")\n");
                S1_steps = (Math.Abs(steps - S1_steps)) % steps_per_round;

            }

        }

        //================================================================================
        //Stepper tow
        //=================================================================================
        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton16.Checked == true) Stepper_Motor_two_move(0);
        }
        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton15.Checked == true) Stepper_Motor_two_move(octant);//45 
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton14.Checked == true) Stepper_Motor_two_move(octant*2);//90 
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton13.Checked == true) Stepper_Motor_two_move(octant*3);//135
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked == true) Stepper_Motor_two_move(octant*4);//180
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked == true) Stepper_Motor_two_move(octant*5);//225
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked == true) Stepper_Motor_two_move(octant*6);//270
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked == true) Stepper_Motor_two_move(octant*7);//315
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int steps;

            if (textBox2.Text != "")
            {
                steps = Convert.ToInt32(textBox2.Text);
                port.Write("(S2B," + steps + ")\n");
                S2_steps = (Math.Abs(steps - S2_steps)) % 2000;

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            S2_steps = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            S1_steps = 0;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ShowAbout()
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog(this);
        }
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int steps;

            if (textBox2.Text != "")
            {
                steps = Convert.ToInt32(textBox2.Text);
                port.Write("(S2F," + steps + ")\n");
                S2_steps = (steps + S2_steps) % 2000;

            }
        }

    }
}
