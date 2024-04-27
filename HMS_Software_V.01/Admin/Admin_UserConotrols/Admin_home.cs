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
    public partial class Admin_home : UserControl
    {
        public string AdminName;
        public Admin_home()
        {
            InitializeComponent();
        }

        public void MySendDataToUserControl(string adminName, string date, string time)
        {
            A_H_adminName.Text = adminName;
            A_H_date.Text = date;
            A_H_time.Text = time;
            Console.WriteLine("Admin Name recived to userControl:"+adminName);
        }
    }
}
