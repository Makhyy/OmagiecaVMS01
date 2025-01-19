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
    public partial class ucfrmUserGuideReceptionist : UserControl
    {
        public ucfrmUserGuideReceptionist()
        {
            InitializeComponent();
            this.Load += ucfrmUserGuideReceptionist_Load;

        }
        private void LoadEnhancedUserGuideReceptionist()
        {
            string rtfContent = @"{\rtf1\ansi
{\colortbl;\red0\green77\blue187;\red0\green0\blue0;\red100\green100\blue100;}
\qc{\b\fs36\cf1 Omagieca Visitor Management System User Guide\cf0\fs24\par\par}

\qc{\b\fs28 For Receptionists\fs24\b0\par\par}
\ql
{\b\fs28 1. Logging In\par\fs20\b0}
\pard\li720\bullet\tab Step 1: Open the application on your desktop.\par
\bullet\tab Step 2: Enter your username and password in the login fields.\par
\bullet\tab Step 3: Click the ""Login"" button to access the dashboard.\par
\pard\li0\par
{\b\fs28 2. Dashboard Overview\par\fs20\b0}
\pard\li720\bullet\tab View daily visitor statistics, including total visitors and remaining checked-in visitors.\par
\bullet\tab Access quick links to register new visitors and manage RFID tags.\par
\pard\li0\par
{\b\fs28 3. Visitor Registration\par\fs20\b0}
{\b\i Registering Visitors:\i0\par}
\pard\li720\bullet\tab Click on ""Visitor Management"" or use the ""Quick Register"" button on the dashboard.\par
\bullet\tab Enter visitor details such as name, age, gender, and city/municipality.\par
\bullet\tab Assign an RFID tag to the visitor.\par
\bullet\tab Submit the registration to save the visitor's information.\par
\pard\li0
{\b\i Editing Visitor Information:\i0\par}
\pard\li720\bullet\tab Locate a visitor in ""Visitor Management"".\par
\bullet\tab Click ""Edit"" to update the details.\par
\bullet\tab Save changes to apply them.\par
\pard\li0\par

}";
            richTextBox2.Rtf = rtfContent;
        }

        private void ucfrmUserGuideReceptionist_Load(object sender, EventArgs e)
        {
            LoadEnhancedUserGuideReceptionist();
        }
    }
}
