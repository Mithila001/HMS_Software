using HMS_Software_V._01.Admin.Admin_UserConotrols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Admin
{
    public partial class Admin_Dashboard : Form
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void admin_doctors_btn_Click(object sender, EventArgs e)
        {
            admin_Doctors1.Visible = true;
            admin_home1.Visible = false;
        }

        private void admin_home_btn_Click(object sender, EventArgs e)
        {
            admin_Doctors1.Visible = false;
            admin_home1.Visible = true;
        }
    }
}
