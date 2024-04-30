using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Doctor_Ward.UserControls
{
    public partial class ShowWaitionInPatients : UserControl
    {
        public ShowWaitionInPatients()
        {
            InitializeComponent();
            MyAttachClickEventHandlers(this);
        }

        public string SWP_PatientName { get; set; }
        public string SWP_PatientRID { get; set; }
        public string SWP_PatientCondition { get; set; }
        public int SWP_PatietnVisitCount { get; set; }

        public string SWP_D_Name { get; set; }
        public string SWP_D_Title { get; set; }
        public string SWP_D_ID { get; set; }

        public string SWP_WardName { get; set; }


        private void MyAttachClickEventHandlers(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Attach event handler for the current control
                control.Click += AdmitRequest_Click;
                control.MouseEnter += panel1_MouseEnter;
                control.MouseLeave += panel1_MouseLeave;


                // If the current control has child controls, attach event handlers to them
                if (control.HasChildren)
                {
                    MyAttachClickEventHandlers(control);
                }
            }
        }
    }
}
