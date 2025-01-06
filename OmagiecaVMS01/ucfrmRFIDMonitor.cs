
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmagiecaVMS01
{
    public partial class ucfrmRFIDMonitor : UserControl
    {
        private SerialPort mySerialPort;
        public ucfrmRFIDMonitor()
        {
            InitializeComponent();
            InitializeSerialPort();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!mySerialPort.IsOpen)
            {
                mySerialPort.Open();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (mySerialPort.IsOpen)
            {
                mySerialPort.Close();
            }
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine().Trim();  // Read data and trim any whitespace
            this.Invoke(new MethodInvoker(delegate () {
                // Safe way to call a Windows Forms control if the SerialPort is on a different thread
                textBoxRFID.Text = indata;
            }));
        }

        private void InitializeSerialPort()
        {
           mySerialPort = new SerialPort("COM5");  // Adjust the COM port as needed
            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

           try
           {
                //mySerialPort.Open();
           }
            catch (Exception ex)
            {
               MessageBox.Show("Error opening serial port: " + ex.Message);
            }
        }

        private void ucfrmRFIDMonitor_Load(object sender, EventArgs e)
        {

        }
    }
}
