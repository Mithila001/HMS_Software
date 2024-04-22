﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Reception.UserControls
{
    public partial class R_A_SelectClinicType : UserControl
    {
        public R_A_SelectClinicType()
        {
            InitializeComponent();
            AttachClickEventHandlers(this);
        }

        private void AttachClickEventHandlers(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Attach event handler for the current control
                control.Click += Control_Click;
               /* control.MouseEnter += panel1_MouseEnter;
                control.MouseLeave += panel1_MouseLeave;*/

                // If the current control has child controls, attach event handlers to them
                if (control.HasChildren)
                {
                    AttachClickEventHandlers(control);
                }
            }
        }
        public event EventHandler<int> ClinicClicked;
        private void Control_Click(object sender, EventArgs e)
        {
            int clinicID = (int)this.Tag;

            /* Console.WriteLine("Clinic ID::::::::::::: " + clinicID);


             Console.WriteLine(" ((---2---)) Clicked control's Tag property (clinicID): " + clinicID);*/


            ClinicClicked?.Invoke(this, clinicID);


            /*Console.WriteLine(" ((---2---)) ClinicClicked event invoked with clinicID: " + clinicID);*/

            // Trigger the UserControl click event
            OnClick(e);
        }
    }
}
