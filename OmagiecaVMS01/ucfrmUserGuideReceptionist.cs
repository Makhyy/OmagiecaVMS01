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
\qc{\b\fs36\cf1 OMAGIECA Visitor Management System With Arduino and RFID Integration User Guide\cf0\fs24\par\par}

\qc{\b\fs28 For RECEPTIONISTS\fs24\b0\par\par}
\ql\pard\li0\par
{\b\fs28 1. Logging In\par\fs20\b0}
\pard\li720\bullet\tab Step 1: Open the application on your desktop.\par
\bullet\tab Step 2: Enter your username and password in the login fields.\par
\bullet\tab Step 3: Click the ""Login"" button to access the dashboard.\par
\pard\li0\par

{\b\fs28 2. Dashboard Overview\par\fs20\b0}
\pard\li720\bullet\tab View daily visitor statistics, including total visitors and remaining checked-in visitors.\par
\bullet\tab Access quick links to register new visitors and manage RFID tags.\par
\pard\li0\par

{\b\fs28 3. Managing Visitors\par\fs20\b0}
{\b\i Registering New Visitors:\i0\par}
\pard\li720\bullet\tab Go to ""Visitor Management"".\par
\bullet\tab Complete the form with visitor details and assign an RFID tag.\par
\bullet\tab Click ""Register"" to save the visitor's information.\par
\bullet\tab Click the ""All records"" to view the list of all visitors.\par
\pard\li0
{\b\i Editing Visitor Information:\i0\par}
\pard\li720\bullet\tab Find the visitor in ""Visitor Management"".\par
\bullet\tab Click ""Update"" to update information.\par
\bullet\tab Click ""Delete"" to delete the visitor information.\par
\pard\li0\par

{\b\fs28 4. Visitor Status\par\fs20\b0}
\pard\li720\bullet\tab View visitor status, including checked-in and checked-out visitors.\par
\pard\li0\par

{\b\fs28 5. To View user information and Edit \par\fs20\b0}
\pard\li720\bullet\tab Click the Profile Icon to Edit the User Account\par
\pard\li0\par

{\b\fs28 6. Forgot password \par\fs20\b0}
\pard\li720\bullet\tab Click the Forgot password in Login form \par
\pard\li720\bullet\tab Verify Uername and answer the Security Question\par
\pard\li720\bullet\tab Enter New password\par}";

            richTextBox2.Rtf = rtfContent;
        }

        private void ucfrmUserGuideReceptionist_Load(object sender, EventArgs e)
        {
            LoadEnhancedUserGuideReceptionist();
        }
    }
}
