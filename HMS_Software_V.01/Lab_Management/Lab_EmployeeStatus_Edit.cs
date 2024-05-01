﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Lab_Management
{
    public partial class Lab_EmployeeStatus_Edit : Form
    {

        bool labCollapse;


        public Lab_EmployeeStatus_Edit()
        {
            InitializeComponent();
            Properties.Settings.Default.Count = 0;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            label1.Text = $"Mr.Saman(MLT) : Active.";
            Properties.Settings.Default.Count += 1;
            button1.Enabled = false;
            button2.Enabled = true;
            Properties.Settings.Default.result = label1.Text;
            Properties.Settings.Default.Save();
        }

        public void button2_Click_1(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {
                label1.Text = $"Mr.Saman(MLT) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button2.Enabled = false;
                button1.Enabled = true;

            }
            else
            {
                label1.Text = $"Mr.Saman(MLT) : Not in Work.";
                button2.Enabled = false;
            }
            Properties.Settings.Default.result = label1.Text;
            Properties.Settings.Default.Save();

        }

        public void button7_Click(object sender, EventArgs e)
        {
            if (button8.Enabled == false)
            {
                label5.Text = $"Mr.Saman(LT) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button7.Enabled = false;
                button8.Enabled = true;

            }
            else
            {
                label5.Text = $"Mr.Saman(LT) : Not in Work.";
                button7.Enabled = false;
            }
            Properties.Settings.Default.result4 = label5.Text;
            Properties.Settings.Default.Save();
        }

        public void button9_Click(object sender, EventArgs e)
        {
            if (button10.Enabled == false)
            {
                label6.Text = $"Mr.Saman(Phlebotomist) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button9.Enabled = false;
                button10.Enabled = true;

            }
            else
            {
                label6.Text = $"Mr.Saman(Phlebotomist) : Not in Work.";
                button9.Enabled = false;
            }
            Properties.Settings.Default.result5 = label6.Text;
            Properties.Settings.Default.Save();
        }

        public void button11_Click(object sender, EventArgs e)
        {
            if (button12.Enabled == false)
            {
                label7.Text = $"Mr.Saman(Phlebotomist) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button11.Enabled = false;
                button12.Enabled = true;

            }
            else
            {
                label7.Text = $"Mr.Saman(Phlebotomist) : Not in Work.";
                button11.Enabled = false;
            }
            Properties.Settings.Default.result6 = label7.Text;
            Properties.Settings.Default.Save();
        }

        public void button4_Click(object sender, EventArgs e)
        {

            label2.Text = "Mr.Saman(MLT) : Active.";
            Properties.Settings.Default.Count += 1;
            button4.Enabled = false;
            button3.Enabled = true;
            Properties.Settings.Default.result2 = label2.Text;
            Properties.Settings.Default.Save();
        }

        public void button3_Click(object sender, EventArgs e)
        {
            if (button4.Enabled == false)
            {
                label2.Text = $"Mr.Saman(MLT) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button3.Enabled = false;
                button4.Enabled = true;

            }
            else
            {
                label2.Text = $"Mr.Saman(MLT) : Not in Work.";
                button3.Enabled = false;
            }
            Properties.Settings.Default.result2 = label2.Text;
            Properties.Settings.Default.Save();
        }

        public void button6_Click(object sender, EventArgs e)
        {

            label3.Text = "Mr.Saman(LT) : Active.";
            Properties.Settings.Default.Count += 1;
            button6.Enabled = false;
            button5.Enabled = true;
            Properties.Settings.Default.result3 = label3.Text;
            Properties.Settings.Default.Save();
        }

        public void button5_Click(object sender, EventArgs e)
        {
            if (button6.Enabled == false)
            {
                label3.Text = $"Mr.Saman(LT) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button5.Enabled = false;
                button6.Enabled ^= true;

            }
            else
            {
                label3.Text = $"Mr.Saman(LT) : Not in Work.";
                button5.Enabled = false;
            }
            Properties.Settings.Default.result3 = label3.Text;
            Properties.Settings.Default.Save();
        }

        public void button8_Click(object sender, EventArgs e)
        {

            label5.Text = "Mr.Saman(LT) : Active.";
            Properties.Settings.Default.Count += 1;
            button8.Enabled = false;
            button7.Enabled = true;
            Properties.Settings.Default.result4 = label5.Text;
            Properties.Settings.Default.Save();
        }

        public void button10_Click(object sender, EventArgs e)
        {

            label6.Text = $"Mr.Saman(Phlebotomist) : Active.";
            Properties.Settings.Default.Count += 1;
            button10.Enabled = false;
            button9.Enabled = true;
            Properties.Settings.Default.result5 = label6.Text;
            Properties.Settings.Default.Save();
        }

        public void button12_Click(object sender, EventArgs e)
        {

            label7.Text = $"Mr.Saman(Phlebotomist) : Active.";
            Properties.Settings.Default.Count += 1;
            button12.Enabled = false;
            button11.Enabled = true;
            Properties.Settings.Default.result6 = label7.Text;
            Properties.Settings.Default.Save();
        }
        private void transfertoform()
        {
            Lab_Dashboard form1 = new Lab_Dashboard();
            form1.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }
        private void openform1()
        {
            Lab_Dashboard form1 = new Lab_Dashboard();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void labtimer_Tick(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            labtimer.Start();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form1 in Application.OpenForms)
            {
                if (Form1.Name == "Form1")
                {
                    Isopen = true;
                    Form1.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Dashboard form1 = new Lab_Dashboard();
                form1.Show();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form8 in Application.OpenForms)
            {
                if (Form8.Name == "Form8")
                {
                    Isopen = true;
                    Form8.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_UpdateTestDetails form8 = new Lab_UpdateTestDetails();
                form8.Show();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form5 in Application.OpenForms)
            {
                if (Form5.Name == "Form5")
                {
                    Isopen = true;
                    Form5.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus form5 = new Lab_EmployeeStatus();
                form5.Show();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form6 in Application.OpenForms)
            {
                if (Form6.Name == "Form6")
                {
                    Isopen = true;
                    Form6.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_TestToDo form6 = new Lab_TestToDo();
                form6.Show();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form3 in Application.OpenForms)
            {
                if (Form3.Name == "Form3")
                {
                    Isopen = true;
                    Form3.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Inventory form3 = new Lab_Inventory();
                form3.Show();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form9 in Application.OpenForms)
            {
                if (Form9.Name == "Form9")
                {
                    Isopen = true;
                    Form9.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_CompleteTestDetails form9 = new Lab_CompleteTestDetails();
                form9.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = $"Mr.Saman(MLT) : Active.";
            Properties.Settings.Default.Count += 1;
            button1.Enabled = false;
            button2.Enabled = true;
            Properties.Settings.Default.result = label1.Text;
            Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {
                label1.Text = $"Mr.Saman(MLT) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button2.Enabled = false;
                button1.Enabled = true;

            }
            else
            {
                label1.Text = $"Mr.Saman(MLT) : Not in Work.";
                button2.Enabled = false;
            }
            Properties.Settings.Default.result = label1.Text;
            Properties.Settings.Default.Save();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            label2.Text = "Mr.Saman(MLT) : Active.";
            Properties.Settings.Default.Count += 1;
            button4.Enabled = false;
            button3.Enabled = true;
            Properties.Settings.Default.result2 = label2.Text;
            Properties.Settings.Default.Save();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (button4.Enabled == false)
            {
                label2.Text = $"Mr.Saman(MLT) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button3.Enabled = false;
                button4.Enabled = true;

            }
            else
            {
                label2.Text = $"Mr.Saman(MLT) : Not in Work.";
                button3.Enabled = false;
            }
            Properties.Settings.Default.result2 = label2.Text;
            Properties.Settings.Default.Save();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            label3.Text = "Mr.Saman(LT) : Active.";
            Properties.Settings.Default.Count += 1;
            button6.Enabled = false;
            button5.Enabled = true;
            Properties.Settings.Default.result3 = label3.Text;
            Properties.Settings.Default.Save();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (button6.Enabled == false)
            {
                label3.Text = $"Mr.Saman(LT) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button5.Enabled = false;
                button6.Enabled ^= true;

            }
            else
            {
                label3.Text = $"Mr.Saman(LT) : Not in Work.";
                button5.Enabled = false;
            }
            Properties.Settings.Default.result3 = label3.Text;
            Properties.Settings.Default.Save();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            label5.Text = "Mr.Saman(LT) : Active.";
            Properties.Settings.Default.Count += 1;
            button8.Enabled = false;
            button7.Enabled = true;
            Properties.Settings.Default.result4 = label5.Text;
            Properties.Settings.Default.Save();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (button8.Enabled == false)
            {
                label5.Text = $"Mr.Saman(LT) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button7.Enabled = false;
                button8.Enabled = true;

            }
            else
            {
                label5.Text = $"Mr.Saman(LT) : Not in Work.";
                button7.Enabled = false;
            }
            Properties.Settings.Default.result4 = label5.Text;
            Properties.Settings.Default.Save();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            label6.Text = $"Mr.Saman(Phlebotomist) : Active.";
            Properties.Settings.Default.Count += 1;
            button10.Enabled = false;
            button9.Enabled = true;
            Properties.Settings.Default.result5 = label6.Text;
            Properties.Settings.Default.Save();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (button10.Enabled == false)
            {
                label6.Text = $"Mr.Saman(Phlebotomist) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button9.Enabled = false;
                button10.Enabled = true;

            }
            else
            {
                label6.Text = $"Mr.Saman(Phlebotomist) : Not in Work.";
                button9.Enabled = false;
            }
            Properties.Settings.Default.result5 = label6.Text;
            Properties.Settings.Default.Save();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            label7.Text = $"Mr.Saman(Phlebotomist) : Active.";
            Properties.Settings.Default.Count += 1;
            button12.Enabled = false;
            button11.Enabled = true;
            Properties.Settings.Default.result6 = label7.Text;
            Properties.Settings.Default.Save();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (button12.Enabled == false)
            {
                label7.Text = $"Mr.Saman(Phlebotomist) : Not in Work.";
                Properties.Settings.Default.Count -= 1;
                button11.Enabled = false;
                button12.Enabled = true;

            }
            else
            {
                label7.Text = $"Mr.Saman(Phlebotomist) : Not in Work.";
                button11.Enabled = false;
            }
            Properties.Settings.Default.result6 = label7.Text;
            Properties.Settings.Default.Save();
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            labtimer.Start();
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form1 in Application.OpenForms)
            {
                if (Form1.Name == "Form1")
                {
                    Isopen = true;
                    Form1.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Dashboard form1 = new Lab_Dashboard();
                form1.Show();
            }
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form8 in Application.OpenForms)
            {
                if (Form8.Name == "Form8")
                {
                    Isopen = true;
                    Form8.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_UpdateTestDetails form8 = new Lab_UpdateTestDetails();
                form8.Show();
            }
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form5 in Application.OpenForms)
            {
                if (Form5.Name == "Form5")
                {
                    Isopen = true;
                    Form5.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus form5 = new Lab_EmployeeStatus();
                form5.Show();
            }
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form6 in Application.OpenForms)
            {
                if (Form6.Name == "Form6")
                {
                    Isopen = true;
                    Form6.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_TestToDo form6 = new Lab_TestToDo();
                form6.Show();
            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form9 in Application.OpenForms)
            {
                if (Form9.Name == "Form9")
                {
                    Isopen = true;
                    Form9.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_CompleteTestDetails form9 = new Lab_CompleteTestDetails();
                form9.Show();
            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form3 in Application.OpenForms)
            {
                if (Form3.Name == "Form3")
                {
                    Isopen = true;
                    Form3.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Inventory form3 = new Lab_Inventory();
                form3.Show();
            }
        }

        private void labtimer_Tick_1(object sender, EventArgs e)
        {
            if (labCollapse)
            {
                panel8.Height += 10;
                if (panel8.Height == panel8.MaximumSize.Height)
                {
                    labCollapse = false;
                    labtimer.Stop();
                }
            }
            else
            {
                panel8.Height -= 10;
                if (panel8.Height == panel8.MinimumSize.Height)
                {
                    labCollapse = true;
                    labtimer.Stop();
                }
            }
        }
    }
}
