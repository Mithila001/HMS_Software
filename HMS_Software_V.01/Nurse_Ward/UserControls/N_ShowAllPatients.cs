using HMS_Software_V._01.Doctor_Ward;
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
            MyAttachClickEventHandlers(this);
        }

        public string NSAPUC_P_Name { get; set; }
        public string NSAPUC_P_RID { get; set; }
        public string NSAPUC_P_Age { get; set; }
        public string NSAPUC_P_Gender { get; set; }
        public string NSAPUC_P_Condition { get; set; }
        public int NSAPUC_P_MedicalEventID { get; set; }
        public int NSAPUC_NureseID { get; set; }
        public string NSAPUC_P_Ward { get; set; }




        private void MyAttachClickEventHandlers(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Attach event handler for the current control
                control.Click += N_ShowAllPatients_Click;
                control.MouseEnter += panel2_MouseEnter;
                control.MouseLeave += panel2_MouseLeave;


                // If the current control has child controls, attach event handlers to them
                if (control.HasChildren)
                {
                    MyAttachClickEventHandlers(control);
                }
            }
        }

        private void N_ShowAllPatients_Click(object sender, EventArgs e)
        {
            NurseWard_TreatePatient nurseWard_TreatePatient = new NurseWard_TreatePatient(NSAPUC_P_Name, NSAPUC_P_RID, NSAPUC_P_Age, NSAPUC_P_Gender,
                NSAPUC_P_Condition, NSAPUC_P_MedicalEventID, NSAPUC_NureseID, NSAPUC_P_Ward);
            nurseWard_TreatePatient.Show();

            //Closing UserControl's Parent Form
            NurseWard_Dashboard parentForm = this.ParentForm as NurseWard_Dashboard;
            if (parentForm != null)
            {
                parentForm.Hide();
            }
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(227, 227, 255);
            panel3.BackColor = Color.FromArgb(227, 227, 255);
            panel4.BackColor = Color.FromArgb(227, 227, 255);
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
        }
    }

}
