﻿using System;
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
        private SerialPort mySerialPort;
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
                MessageBox.Show("Error starting RFID monitor: " + ex.Message);
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
                    rfidMonitorBLL.UpdateVisitorStatus(rfidUID, "Exited");

                    ShowTimedMessage("A Visitor has Exited!.", 2000);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating visitor status: " + ex.Message);
                }
            }
        }
        private void InitializeSerialPort()
        {
            //mySerialPort = new SerialPort("COM5");
            mySerialPort = new SerialPort("COM7");  // Adjust the COM port as needed
            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
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
    }
}
