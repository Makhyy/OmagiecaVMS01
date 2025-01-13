using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmagiecaVMS01
{
    public partial class TimedMessageBoxForm : Form
    {
        private Timer closeTimer;
        public TimedMessageBoxForm(string message, int duration)
        {

            InitializeComponent();
            // Assuming there is a Label control called label1 on this form
            label1.Text = message;  // Display the message

            // Configure the timer
            closeTimer = new Timer();
            closeTimer.Interval = duration;  // Set the timer interval
            closeTimer.Tick += TimerTick;  // Subscribe to the Tick event
            closeTimer.Start();  // Start the timer
        }
        private void TimerTick(object sender, EventArgs e)
        {
            this.Close();  // Close the form when the Timer's Tick event is fired.
        }
        private void TimerTick_Tick(object sender, EventArgs e)
        {
            
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            closeTimer?.Stop();
            closeTimer?.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
