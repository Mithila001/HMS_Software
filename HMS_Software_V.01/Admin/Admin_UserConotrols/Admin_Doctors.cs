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
    public partial class Admin_Doctors : UserControl
    {
        public Admin_Doctors()
        {
            InitializeComponent();
        }

        private void searchDoctor_btn_Click(object sender, EventArgs e)
        {
            Admin_DoctorSearch admin_DoctorSearch = new Admin_DoctorSearch();  
            admin_DoctorSearch.Show();

            Form parentForm = this.FindForm();
            parentForm.Hide();


        }

        private void registerDoctor_btn_Click(object sender, EventArgs e)
        {
            Admin_DoctorRegister admin_DoctorRegister   = new Admin_DoctorRegister();
            admin_DoctorRegister.Show();

            Form parentForm = this.FindForm();  
            parentForm.Hide();
            
        }
    }
}
