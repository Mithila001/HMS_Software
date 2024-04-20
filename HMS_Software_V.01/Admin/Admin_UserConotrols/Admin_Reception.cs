using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Admin.Admin_UserConotrols
{
    public partial class Admin_Reception : UserControl
    {
        public Admin_Reception()
        {
            InitializeComponent();
        }

        private void RegisterReception_btn_Click(object sender, EventArgs e)
        {
            Admin_ReceptionRegistration admin_ReceptionRegistration = new Admin_ReceptionRegistration();
            admin_ReceptionRegistration.Show();

            Form parentForm = this.FindForm();
            parentForm.Hide();
        }
    }
}
