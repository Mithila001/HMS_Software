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
    public partial class Admin_Nurses : UserControl
    {
        public Admin_Nurses()
        {
            InitializeComponent();
        }

        //Reciving Data from the form
        public void MySendDataToUserControl(string adminName, string date, string time)
        {
            Ad_Nurse_Date.Text = date;
            Ad_Nurse_Time.Text = time;
        }

        private void RegisterNurse_btn_Click(object sender, EventArgs e)
        {
            Admin_NurseRegister admin_NurseRegister = new Admin_NurseRegister();
            admin_NurseRegister.Show();

            Form parentForm = this.FindForm();
            parentForm.Hide();
        }

        private void SearchNurse_btn_Click(object sender, EventArgs e)
        {
            Admin_NurseSearch admin_NurseSearch = new Admin_NurseSearch();  
            admin_NurseSearch.Show();

            Form parentForm = this.FindForm();
            parentForm.Hide();
        }
    }
}
