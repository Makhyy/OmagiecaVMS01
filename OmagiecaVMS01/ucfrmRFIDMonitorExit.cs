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
            catch (Exception)
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
            UpdateVisitorExitStatus(rfidUID);  // Delegate to update visitor status
        }
        private void UpdateVisitorExitStatus(string rfidUID)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { UpdateVisitorExitStatus(rfidUID); });
            }
            else
            {
                try
                {
                    string currentStatus = rfidMonitorBLL.GetVisitStatusByRfidTag(rfidUID);

                    if (currentStatus == "Entered")
                    {
                        rfidMonitorBLL.UpdateVisitStatusExit(rfidUID, "Exited");
                        SendCommandToArduino("GREEN_ON");  // Turn on the green LED to indicate a successful exit
                        ShowTimedMessage("Visitor has Exited!", 1500);
                    }
                    else
                    {
                        SendCommandToArduino("RED_ON");  // Turn on the red LED if the status is not 'Entered'
                        ShowErrorTimedMessage("Exit Denied. Visitor status: " + currentStatus, 1500);
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorTimedMessage("Visitor has not Entered or Registered! " , 1500);
                    SendCommandToArduino("RED_ON");  // Ensure red light in case of any exception
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
                SendCommandToArduino("RED_ON");  // Send command to turn on the red LED
            }
            // Optional: turn off the red LED after a certain delay
            Task.Delay(duration).ContinueWith(_ => SendCommandToArduino("RED_OFF"));
        }
        private void SendCommandToArduino(string command)
        {
            if (mySerialPortExit.IsOpen)
            {
                mySerialPortExit.WriteLine(command + "\n"); // Ensure the command is terminated with a newline for Arduino to process it correctly
            }
            else
            {
                MessageBox.Show("Unable to send command to Arduino. Serial port is not open.", "Communication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
