using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Nurse_Ward.UserControls
{
    public partial class N_ShowAllPatients : UserControl
    {
        public N_ShowAllPatients()
        {
            InitializeComponent();
        }

        public string NSAPUC_P_Name { get; set; }
        public string NSAPUC_P_RID { get; set; }
        public string NSAPUC_P_Age { get; set; }
        public string NSAPUC_P_Gender { get; set; }
        public string NSAPUC_P_Condition { get; set; }
        public string NSAPUC_P_MedicalEventID { get; set; }
        public int NSAPUC_NureseID { get; set; }
        public string NSAPUC_P_Ward { get; set; }
       
    }

}
