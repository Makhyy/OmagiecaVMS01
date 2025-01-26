using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmagiecaVMS01
{
    public partial class Notify : Form
    {
        public Notify()
        {
            InitializeComponent();
        }

        public Notify(string message)
        {
            InitializeComponent();
            lblMessage.Text = message;
            // Set the message text and dynamically adjust the label
            AdjustLabelToFitText(message, 27); // Adjust font size as required

            // Create a timer to close the form automatically
            Timer closeTimer = new Timer();
            closeTimer.Interval = 8000;  // 8000 milliseconds = 8 seconds
            closeTimer.Tick += (sender, args) =>
            {
                this.Close();  // Closes the Notify form
                closeTimer.Stop();
                closeTimer.Dispose();
            };
            closeTimer.Start();

        }

        private void AdjustLabelToFitText(string text, int fontSize)
        {
            // Force the text into two lines if necessary
            string[] words = text.Split(' ');
            if (words.Length > 4) // Adjust to break long texts into two lines
            {
                int midPoint = words.Length / 2;
                text = string.Join(" ", words.Take(midPoint)) + "\n" + string.Join(" ", words.Skip(midPoint));
            }

            // Set font size and make text bold
            lblMessage.Font = new Font(lblMessage.Font.FontFamily, fontSize, FontStyle.Bold);

            // Assign formatted text and enable multiline wrapping
            lblMessage.Text = text;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            lblMessage.AutoSize = false; // Prevent automatic resizing
            lblMessage.MaximumSize = new Size(this.ClientSize.Width - 40, 0); // 40px padding
            lblMessage.Size = new Size(this.ClientSize.Width - 40, lblMessage.PreferredHeight);

            // Center the label within the form
            lblMessage.Location = new Point(
                (this.ClientSize.Width - lblMessage.Width) / 2,
                (this.ClientSize.Height - lblMessage.Height) / 2
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Notify_Load(object sender, EventArgs e)
        {

        }
    }
}
