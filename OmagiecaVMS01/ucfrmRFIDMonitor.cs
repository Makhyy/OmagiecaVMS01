
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.IO.Ports;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using BLL;

namespace OmagiecaVMS01
{
    public partial class ucfrmRFIDMonitor : UserControl
    {
        private SerialPort mySerialPort;
        private RFIDMonitorBLL rfidMonitorBLL;
        public ucfrmRFIDMonitor()
        {
            InitializeComponent();
            InitializeSerialPort();
            rfidMonitorBLL = new RFIDMonitorBLL();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (!mySerialPort.IsOpen)
                {
                    mySerialPort.Open();
                    ShowTimedMessage("RFID Reader is Ready!", 2000);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Access denied. Port might be in use or locked by another process.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorTimedMessage("Error starting RFID monitor: " + ex.Message, 2000);
            }

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            CloseSerialPort();
        }
        private void CloseSerialPort()
        {
            try
            {
                if (mySerialPort.IsOpen)
                {
                    mySerialPort.Close();
                    MessageBox.Show("RFID Reader stopped.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error stopping RFID monitor: " + ex.Message);
            }
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string rfidUID = sp.ReadLine().Trim();  // Read the RFID UID from the serial port
            UpdateVisitorStatus(rfidUID);  // Delegate to update visitor status
        }
        private void UpdateVisitorStatus(string rfidUID)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { UpdateVisitorStatus(rfidUID); });
            }
            else
            {
                try
                {
                    rfidMonitorBLL.UpdateVisitorStatus(rfidUID, "Entered");

                    ShowTimedMessage("A Visitor has Entered!.", 2000);
                }
                catch (Exception ex)
                {

                    ShowErrorTimedMessage("Error updating visitor status:" + ex.Message, 2000);
                }
            }
        }
       
        private void InitializeSerialPort()
        {
            mySerialPort = new SerialPort("COM5");
            //mySerialPort = new SerialPort("COM7");  // Adjust the COM port as needed
            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);


        }

        private void ucfrmRFIDMonitor_Load(object sender, EventArgs e)
        {

        }
        private void ShowTimedMessage(string message, int duration)
        {
            using (TimedMessageBoxForm timedMessage = new TimedMessageBoxForm(message, duration))
            {
                timedMessage.StartPosition = FormStartPosition.CenterParent;
                timedMessage.ShowDialog();
            }
        }
        private void ShowErrorTimedMessage(string message, int duration)
        {
            using (TimedMessageErrorBoxForm timedMessage = new TimedMessageErrorBoxForm(message, duration))
            {
                timedMessage.StartPosition = FormStartPosition.CenterParent;
                timedMessage.ShowDialog();
            }
        }
    }
}
    
