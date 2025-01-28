using System;
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

            // Show Splash Screen
            using (SplashScreen splash = new SplashScreen())
            {
                splash.ShowDialog(); // Block until splash screen closes
            }

            // Login Loop: Allows re-login if user logs out
            while (true)
            {
                using (frmLogin loginForm = new frmLogin())
                {
                    // Show the login form
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        break; // Exit the application if login fails or is closed
                    }

                    // Determine which form to show based on user role
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
                        // Show the main form and wait until it's closed
                        if (mainForm.ShowDialog() != DialogResult.OK)
                        {
                            break; // Exit if the main form was closed without logging out
                        }
                    }
                }
            }

            // Ensure the application exits completely
            Application.Exit();
        }
    }
}
