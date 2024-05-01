using HMS_Software_V._01.Common_UseForms;
using HMS_Software_V._01.Doctor_Check;
using HMS_Software_V._01.Reception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Doctor_OPD
{
    public partial class DoctorCheck_Dashboard : Form
    {
        public Form DoctorDashboardFromReferece { get; set; }

        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private int DoctorID;
        private string unitTypeName;
        private int WardNumber;
        public DoctorCheck_Dashboard(int userID , string unit, int WardNumber)
        {
            InitializeComponent();

            this.DoctorID = userID;
            this.unitTypeName = unit;
            this.WardNumber = WardNumber;

            MyLoadbasicUIDeatils();
            getTableDate(); // Get Dashboard Data



            
        }


        //Load Basic Details for the dashboard
        private void MyLoadbasicUIDeatils()
        {
            if(unitTypeName == "OPD")
            {
                DisplayUnittype_doctors.Text = "Available OPD Doctors";
                DisplayUnittype_patient.Text = "Total OPD Patients";
                DisplayUnittype_title.Text = "Outpatient Department";

            }
            else if(unitTypeName == "Ward")
            {
                DisplayUnittype_doctors.Text = "Available Ward Doctors";
                DisplayUnittype_patient.Text = "Total Ward Patients";
                DisplayUnittype_title.Text = "Inpatient Department";

            }
            else if(unitTypeName == "Clinic")
            {
                DisplayUnittype_doctors.Text = "Available Clinci Doctors";
                DisplayUnittype_patient.Text = "Total Clinci Patients";
                DisplayUnittype_title.Text = "Clinic Department";

            }
            else
            {
                MessageBox.Show("Unit Type is not valid:"+ unitTypeName, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        //Get Doctor Details from the databse
        private void getTableDate() 
        {
            try
            {
                connect.Open();

                string query = "SELECT D_NameWithInitials, D_Position, Doctor_ID FROM Doctor WHERE Doctor_ID = @UserID";

                SqlCommand sqlCommand = new SqlCommand(query, connect);
                sqlCommand.Parameters.AddWithValue("@UserID", DoctorID);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                

                if (reader.Read())
                {
                    DCD_doctorName_lbl.Text = reader.GetString(0);
                    DCD_doctor_position_lbl.Text = reader.GetString(1);
                }
                else
                {
                    // Handle case when no matching record is found
                    MessageBox.Show("Doctor not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine(ex);
            }
            finally
            {
                connect.Close();
            }


        }


        //Patient Ckeck Button Clickced
        private void DCD_confrim_btn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DCD_enterPatientID_tbx.Text))
            {
                try
                {
                    connect.Open();

                    string query = "SELECT P_RegistrationID FROM Patient WHERE P_RegistrationID = @PatientID";

                    using (SqlCommand sqlCommand = new SqlCommand(query, connect))
                    {
                        sqlCommand.Parameters.AddWithValue("@PatientID", "P" + DCD_enterPatientID_tbx.Text);

                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string patientID_str = reader.GetString(0);

                                string inputIDstr = "P" + DCD_enterPatientID_tbx.Text;

                                string doctorPosition = DCD_doctor_position_lbl.Text;
                                string doctorName = DCD_doctorName_lbl.Text;

                                if (patientID_str == inputIDstr)
                                {
                                    // Go to a form
                                    DoctorCheck_PatientCheck doctorCheck_PatientCheck = new DoctorCheck_PatientCheck(patientID_str, DoctorID, doctorPosition, doctorName, unitTypeName, WardNumber);
                                    doctorCheck_PatientCheck.DoctorPatientCheckFromReferece = this;
                                    doctorCheck_PatientCheck.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Patient Number", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid Patient Number", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Console.WriteLine(ex);
                }
                finally
                {
                    connect.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Add a Patietn number", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
