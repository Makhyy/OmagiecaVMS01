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
    public partial class SplashScreen : Form
    {
        private int progressValue = 0;
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value += 5; // Increment progress
            }
            else
            {
                timer1.Stop();
                this.Close(); // Close the splash screen
            }
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;  // Reset the progress bar
            timer1.Interval = 100;  // Set the timer interval (100 ms)
            timer1.Start();         // Start the timer
        }
    }
    
}
