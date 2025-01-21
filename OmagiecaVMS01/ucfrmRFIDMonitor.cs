using System;
using System.ComponentModel;
using System.IO.Ports;
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
                    ShowTimedMessage("The Entrance RFID Reader is Ready!", 1500);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Access denied. Port might be in use or locked by another process.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorTimedMessage("The Entrance RFID Reader is Disconnected ", 1500);
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
                    ShowErrorTimedMessage("The Entrance RFID Reader has Stopped ", 1500);
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
            string rfidUID = sp.ReadLine().Trim(); // Read the RFID UID from the serial port
            UpdateVisitorStatus(rfidUID); // Delegate to update visitor status
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
                    string currentStatus = rfidMonitorBLL.GetCurrentVisitorStatus(rfidUID);

                    if (currentStatus != "Registered")
                    {
                        SendCommandToArduino("RED_ON");  // Turn on the red LED if not registered
                        ShowErrorTimedMessage("Visitor status: Not Registered", 1500);
                    }
                    else
                    {
                        rfidMonitorBLL.UpdateVisitorStatus(rfidUID, "Entered");
                        ShowTimedMessage("Visitor has Entered!.", 1500);
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorTimedMessage("Error updating visitor status: " + ex.Message, 1500);
                    SendCommandToArduino("RED_ON");  // Ensure red light in case of any exception
                }
            }
        }

        private void InitializeSerialPort()
        {
            mySerialPort = new SerialPort("COM5");
            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        private void ucfrmRFIDMonitor_Load(object sender, EventArgs e)
        {
            // Load event logic here if needed
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
            if (mySerialPort.IsOpen)
            {
                mySerialPort.WriteLine(command + "\n"); // Ensure the command is terminated with a newline for Arduino to process it correctly
            }
            else
            {
                MessageBox.Show("Unable to send command to Arduino. Serial port is not open.", "Communication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
