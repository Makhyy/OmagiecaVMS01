using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using System.IO.Ports;
using System.IO;


namespace OmagiecaVMS01
{
    public partial class ucfrmRFIDMonitorExit : UserControl
    {
        private SerialPort mySerialPortExit;
        private RFIDMonitorBLL rfidMonitorBLL;
        public ucfrmRFIDMonitorExit()
        {
            InitializeComponent();
            InitializeSerialPort();
            rfidMonitorBLL = new RFIDMonitorBLL();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (!mySerialPortExit.IsOpen)
                {
                    mySerialPortExit.Open();
                    ShowTimedMessage("The Exit RFID Reader is Ready!", 1500);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Access denied. Port might be in use or locked by another process.\n" + ex.Message);
            }
            catch (Exception ex)
            {

                ShowErrorTimedMessage("The Exit RFID Reader is Disconnected ", 1500);
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
                if (mySerialPortExit.IsOpen)
                {
                    mySerialPortExit.Close();
                    ShowErrorTimedMessage("The Exit RFID Reader has Stopped ", 1500);
                }
            }
            catch (Exception ex)
            {
                ShowErrorTimedMessage("Error stopping RFID monitor: " + ex.Message, 1500);
            }
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string rfidUID = sp.ReadLine().Trim();  // Read the RFID UID from the serial port
            UpdateVisitorStatusExit(rfidUID);  // Delegate to update visitor status
        }
        private void UpdateVisitorStatusExit(string rfidUID)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { UpdateVisitorStatusExit(rfidUID); });
            }
            else
            {
                try
                {
                    // Assuming GetCurrentVisitorStatus is a method that fetches the current status
                    string currentStatus = rfidMonitorBLL.GetCurrentVisitorStatus(rfidUID);

                    // Check the current status before updating
                    if (currentStatus != "Exited")
                    {
                        rfidMonitorBLL.UpdateVisitorStatusExit(rfidUID, "Exited");
                        ShowTimedMessage("Visitor has Exited!.", 1500);
                    }
                    else
                    {
                        // Handle the case where status is already 'Entered', e.g., log or notify
                        ShowTimedMessage("RFID Tag has already been scanned!", 2000);
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorTimedMessage("Error updating visitor status: " + ex.Message, 1500);
                }
            }
        }
        private void InitializeSerialPort()
        {
            //mySerialPort = new SerialPort("COM5");
            mySerialPortExit = new SerialPort("COM7");  // Adjust the COM port as needed
            mySerialPortExit.BaudRate = 9600;
            mySerialPortExit.Parity = Parity.None;
            mySerialPortExit.StopBits = StopBits.One;
            mySerialPortExit.DataBits = 8;
            mySerialPortExit.Handshake = Handshake.None;
            mySerialPortExit.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            /*
           try
           {
                mySerialPort.Open();
                MessageBox.Show("Port opened successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Access denied. Port might be in use or locked by another process.\n" + ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show("IO Exception, check port settings and device connection.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("General error when trying to open port.\n" + ex.Message);
            }*/

        }
        private void ShowTimedMessage(string message, int duration)
        {
            using (TimedMessageBoxForm timedMessage = new TimedMessageBoxForm(message, duration))
            {
                timedMessage.StartPosition = FormStartPosition.CenterParent;
                timedMessage.ShowDialog();
            }
        }
        private void ShowErrorTimedMessage(string messages, int durations)
        {
            using (TimedMessageErrorBoxForm timedMessage = new TimedMessageErrorBoxForm(messages, durations))
            {
                timedMessage.StartPosition = FormStartPosition.CenterParent;
                timedMessage.ShowDialog();
            }
        }
    }
}
