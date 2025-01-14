﻿using System;
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
    public partial class TimedMessageErrorBoxForm : Form
    {
        private Timer closeTimer;
        public TimedMessageErrorBoxForm(string messages, int durations)
        {
            InitializeComponent();

            InitializeLabel();
            this.Resize += new EventHandler(Form_Resize); // Add resize event handler
            label1.TextChanged += new EventHandler(label1_TextChanged); // Add text changed event handler
            // Assuming there is a Label control called label1 on this form
            label1.Text = messages;  // Display the message

            // Configure the timer
            closeTimer = new Timer();
            closeTimer.Interval = durations;  // Set the timer interval
            closeTimer.Tick += TimerTick;  // Subscribe to the Tick event
            closeTimer.Start();  // Start the timer
        }
        private void Form_Resize(object sender, EventArgs e)
        {
            // Code to execute when the form is resized
            CenterLabel(); // Assuming you have a method to center the label
        }
        private void TimerTick(object sender, EventArgs e)
        {
            this.Close();  // Close the form when the Timer's Tick event is fired.
        }
        private void InitializeLabel()
        {
            // Assuming 'label1' is your label
            label1.AutoSize = false; // Disable auto-size
            label1.TextAlign = ContentAlignment.MiddleCenter; // Center the text horizontally and vertically
            label1.Dock = DockStyle.Fill; // Optional: Fill the parent container
                                          // If not using Dock, you can manually set the size and position:
                                          // label1.Width = this.Width; // Set the width of the label to the width of the form
                                          // label1.Height = 100; // Example height
                                          // label1.Location = new Point(0, (this.Height - label1.Height) / 2); // Center vertically
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
        private void CenterLabel()
        {
            label1.Width = this.Width; // Adjust width to form width
            label1.Height = 100; // Set your desired height
            label1.Location = new Point(0, (this.Height - label1.Height) / 2); // Recenter vertically
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            CenterLabel();
        }

        private void TimedMessageErrorBoxForm_TextChanged(object sender, EventArgs e)
        {

        }
    }
}