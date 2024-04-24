using HMS_Software_V._01.Common_UseForms;
using HMS_Software_V._01.Doctor_OPD;
using HMS_Software_V._01.Reception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Admin
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
            Application.Run(new DoctorCheck_Dashboard());
        }
    }
}
