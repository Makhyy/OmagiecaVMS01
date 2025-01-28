using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmagiecaVMS01
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true) // Loop to allow re-login
            {
                using (frmLogin loginForm = new frmLogin())
                {
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        break; // Exit if login was not successful or was closed
                    }

                    Form mainForm = null;
                    if (loginForm.UserRole == "Admin")
                    {
                        mainForm = new frmAdmin();
                    }
                    else if (loginForm.UserRole == "Receptionist")
                    {
                        mainForm = new frmReceptionist();
                    }

                    if (mainForm != null)
                    {
                        // Show the main form and wait for it to be closed
                        mainForm.ShowDialog();

                        // If the user logged out, restart the login form by continuing the loop
                        // If the form was closed for any other reason, exit the application
                        if (mainForm.DialogResult != DialogResult.OK)
                        {
                            break;
                        }
                    }
                }
            }
            Application.Exit(); // Ensure the application exits completely when the loop ends
        }
    }
}

/* while (true) // Loop to allow re-login
             {
                 using (frmLogin loginForm = new frmLogin())
                 {
                     if (loginForm.ShowDialog() != DialogResult.OK)
                     {
                         break; // Exit if login was not successful or was closed
                     }

                     Form mainForm = null;
                     if (loginForm.UserRole == "Admin")
                     {
                         mainForm = new frmAdmin();
                     }
                     else if (loginForm.UserRole == "Receptionist")
                     {
                         mainForm = new frmReceptionist();
                     }

                     if (mainForm != null)
                     {
                         if (mainForm.ShowDialog() != DialogResult.OK)
                         {
                             break; // Exit the application if the main form is closed without logging out
                         }
                     }
                 }
             }
             Application.Exit(); // Ensure the application exits completely when the loop ends
         }
     }
   }
*/

/*
            Application.Run(new frmAdmin());
}
}
}
*/
