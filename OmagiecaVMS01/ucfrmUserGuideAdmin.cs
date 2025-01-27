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
    public partial class ucfrmUserGuideAdmin : UserControl
    {
        public ucfrmUserGuideAdmin()
        {
            InitializeComponent();

          

        }

        private void LoadEnhancedUserGuideAdmin()
        {
            string rtfContent = @"{\rtf1\ansi
{\colortbl;\red0\green77\blue187;\red0\green0\blue0;\red100\green100\blue100;}
\qc{\b\fs36\cf1 OMAGIECA Visitor Management System With Arduino and RFID Integration User Guide\cf0\fs24\par\par}
\qc{\b\fs28 For ADMINISTRATORS\fs24\b0\par\par}
\ql\pard\li0\par
{\b\fs28 1. Logging In\par\fs20\b0}
\pard\li720\bullet\tab Step 1: Open the application on your desktop.\par
\bullet\tab Step 2: Enter your username and password in the login fields.\par
\bullet\tab Step 3: Click the ""Login"" button to access the dashboard.\par
\pard\li0\par

{\b\fs28 2. Dashboard Overview\par\fs20\b0}
\pard\li720\bullet\tab View real-time statistics including Total Visitors today, Entered visitors, Exited Visitors and Remaining Visitors.\par
\pard\li0\par

{\b\fs28 3. Reports Management\par\fs20\b0}
\pard\li720\bullet\tab View Visitors Daily, Weekly, Monthly and Custom Total Visitors and Total Revenue Reports\par
\pard\li0\par

{\b\fs28 4. RFID Tags Management\par\fs20\b0}
\pard\li720\tab To Register,Update and Delete  RFID Tags \par
\pard\li720\bullet\tab Navigate to the RFID Management section.\par
\bullet\tab Fill in the required fields such as RFIDTag UID, RFIDTag Number, and RFIDTag Status.\par
\bullet\tab Click Save to Register the tags \par
\bullet\tab Click Update to Update the tags \par
\bullet\tab Click Delete to Delete the tags \par

\pard\li0\par
{\b\fs28 3. Managing User Accounts\par\fs20\b0}
{\b\i Creating Accounts:\i0\par}
\pard\li720\bullet\tab Navigate to the ""User Management"" section.\par
\bullet\tab Click on ""Add New User"".\par
\bullet\tab Fill in the required fields such as First Name, Last Name, Username, Password, Security Question, Securit Answer, Role  
 \tab(Admin/Receptionist), and Status.\par
\bullet\tab Submit the form to create a new user.\par
\pard\li0
{\b\i Updating Accounts:\i0\par}
\pard\li720\bullet\tab In the ""User Management"" section, select an existing user.\par
\bullet\tab Click ""Edit"" to modify details.\par
\bullet\tab After changes, Click ""Save"".\par
\pard\li0
{\b\i Deleting Accounts:\i0\par}
\pard\li720\bullet\tab Select the user to remove and Click ""Delete"".\par
\pard\li0\par

{\b\fs28 4. Managing Visitors\par\fs20\b0}
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


{\b\fs28 5. Payment Management\par\fs20\b0}
{\b\i Processing Payments:\i0\par}
\pard\li720\bullet\tab In ""Payment Management"", click ""Add Payment"".\par
\bullet\tab Enter payment details linked to a visit and click save to submit payment.\par
\pard\li0\par

{\b\fs28 6. Generating Reports\par\fs20\b0}
\pard\li720\bullet\tab Generate daily, weekly, or monthly reports through the ""Reports"" section by selecting the desired timeframe and type of report.\par
\pard\li0\par

{\b\fs28 7. To View user information and Edit \par\fs20\b0}
\pard\li720\bullet\tab Click the Profile Icon to Edit the User Account\par
\pard\li0\par

{\b\fs28 8. Forgot Password \par\fs20\b0}
\pard\li720\bullet\tab Click the Forgot password in Login form \par
\pard\li720\bullet\tab Verify Username and answer the Security Question\par
\pard\li720\bullet\tab Enter New password\par}";

            richTextBox1.Rtf = rtfContent;
        }


        private void ucfrmUserGuideAdmin_Load(object sender, EventArgs e)
        {
            LoadEnhancedUserGuideAdmin();
        }
      



    }
}
