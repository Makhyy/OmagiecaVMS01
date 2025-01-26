using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmagiecaVMS01
{
    public partial class ucfrmADashboard : UserControl
    {
        private VisitorBLL visitorBLL;
        public ucfrmADashboard()
        {
            InitializeComponent();

            UpdateVisitorCount();
            LoadVisitorEnteredCount();
            LoadVisitorExitedCount();
            LoadRemainingVisitorCount();
            ucfrmRFIDMonitor rfidMonitor = new ucfrmRFIDMonitor();
            rfidMonitor.Dock = DockStyle.Fill;
            pnlRFIDMonitor.Controls.Add(rfidMonitor);

            ucfrmRFIDMonitorExit rfidMonitorExit = new ucfrmRFIDMonitorExit();
            rfidMonitorExit.Dock = DockStyle.Fill;
            pnlRFIDMonitorExit.Controls.Add(rfidMonitorExit);


            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("MMMM dd, yyyy");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("hh:mm:ss tt");

            
        }
        private void UpdateVisitorCount()
        {
            try
            {
                VisitorBLL visitorManager = new VisitorBLL();
                DataTable filteredData = visitorManager.GetVisitorsForToday();  // This now fetches today's visitors
                dgvVisitorsReport.DataSource = filteredData;
                labelTotalRecords.Text = $"{filteredData.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucfrmADashboard_Load(object sender, EventArgs e)
        {
            dgvVisitorsReport.Visible = false;


            // Start the closing timer when the form loads
            ClosingTimer.Interval = 60000; // Check every minute
            ClosingTimer.Tick += ClosingTimer_Tick;
            ClosingTimer.Start();
        }

        private void labelTotalRecords_Click(object sender, EventArgs e)
        {

        }
        public void LoadVisitorEnteredCount()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                if (visitorBLL == null)
                {
                    MessageBox.Show("Visitor BLL is not initialized!");
                    return;
                }
                
                int count = visitorBLL.GetDailyEnteredVisitorCount();
                lblDailyVisitorsEntered.Text = count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void lblDailyVisitorsEntered_Click(object sender, EventArgs e)
        {

        }
        public void LoadVisitorExitedCount()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                if (visitorBLL == null)
                {
                    MessageBox.Show("Visitor BLL is not initialized!");
                    return;
                }

                int count = visitorBLL.GetDailyExitedVisitorCount();
                lblDailyVisitorsExited.Text = count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void lblDailyVisitorsExited_Click(object sender, EventArgs e)
        {

        }
        private void LoadRemainingVisitorCount()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                if (visitorBLL == null)
                {
                    MessageBox.Show("Visitor BLL is not initialized!");
                    return;
                }

                int count = visitorBLL.GetRemainingVisitorCount();
                lblDailyRemainingVisitors.Text = count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void lblDailyRemainingVisitors_Click(object sender, EventArgs e)
        {

        }
        private void ClosingTimer_Tick(object sender, EventArgs e)
        {
            DateTime closingTime = DateTime.Today.AddHours(14).AddMinutes(23); // Adjusted to the desired closing time
            TimeSpan timeLeft = closingTime - DateTime.Now;

            // Debug output to check the time left
            Debug.WriteLine($"Time left until closing: {timeLeft}");

            // Trigger 10 minutes before closing
            if (timeLeft <= TimeSpan.FromMinutes(5) && timeLeft >= TimeSpan.Zero)
            {
                ClosingTimer.Stop(); // Stop timer to prevent further alerts

                if (visitorBLL == null)
                {
                    visitorBLL = new VisitorBLL(); // Ensure the BLL is initialized
                }

                int remainingVisitors = visitorBLL.GetRemainingVisitorCount();
                string message = $"Attention: The facility will close in 10 minutes. There are {remainingVisitors} visitors still inside.";

                // Play the notification sound just before showing the notification
                PlayNotificationSound();

                // Display the notification form
                Notify notificationForm = new Notify(message);
                notificationForm.Show();
               

            }
        }


        // Method to play the notification sound
        private void PlayNotificationSound()
        {
            try
            {
                // Path to the .wav file
                string soundFilePath = @"C:\Users\lenovo\Documents\capstone system\OmagiecaVMS01\OmagiecaVMS01\Sound\alarm-ringtone-download-for-mobile.wav"; // Replace with the actual path
                using (SoundPlayer player = new SoundPlayer(soundFilePath))
                {
                    player.Play(); // Use PlaySync() if you want the sound to play fully before continuing
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
