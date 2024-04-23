using HMS_Software_V._01.Common_UseForms;
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
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private int userID;
        public DoctorCheck_Dashboard(int userID = 6)
        {
            InitializeComponent();
            this.userID = userID;
            getTableDate(); // Get Dashboard Data
        }

        private void getTableDate()
        {
            try
            {
                connect.Open();

                string query = "SELECT D_NameWithInitials, D_Position, Doctor_ID FROM Doctor WHERE Doctor_ID = @UserID";

                SqlCommand sqlCommand = new SqlCommand(query, connect);
                sqlCommand.Parameters.AddWithValue("@UserID", userID);
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

        private void DCD_confrim_btn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DCD_enterPatientID_tbx.Text))
            {
                try
                {
                    connect.Open();

                    string query = "SELECT P_RegistrationID FROM Patient WHERE P_RegistrationID = @PatientID";

                    SqlCommand sqlCommand = new SqlCommand(query, connect);
                    sqlCommand.Parameters.AddWithValue("@PatientID", DCD_enterPatientID_tbx.Text);

                    SqlDataReader reader = sqlCommand.ExecuteReader();


                    while (reader.Read())
                    {
                        string patientID_str = reader.GetString(0);
                        /*int patientID = int.Parse(patientID_str);*/

                        string inputIDstr = DCD_enterPatientID_tbx.Text;
                        /*int inputID = int.Parse(inputIDstr);*/

                        string doctorPosition = DCD_doctor_position_lbl.Text;
                        string doctorName = DCD_doctorName_lbl.Text;

                        if (patientID_str == inputIDstr)
                        {
                            // go to a from
                            DoctorCheck_PatientCheck doctorCheck_PatientCheck = new DoctorCheck_PatientCheck(patientID_str, userID, doctorPosition, doctorName);
                            doctorCheck_PatientCheck.Show();
                            this.Hide();
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Invalide Patient Number", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
