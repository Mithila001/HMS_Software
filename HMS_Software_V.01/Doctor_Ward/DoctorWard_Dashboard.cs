using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Doctor_Ward
{
    public partial class DoctorWard_Dashboard : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private MyTableData_Automation automation;

        public DoctorWard_Dashboard()
        {
            InitializeComponent();
            automation = new MyTableData_Automation();
            /*automation.MyGetAdmittedPatientRecord();*/
        }
    }
}
