using HMS_Software_V._01.Common_UseForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Doctor_OPD
{
    public partial class DoctorCheck_PatientCheck : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private int userID;
        private string patientRID;
        private string doctorPosition;
        private string doctorName;
        public DoctorCheck_PatientCheck(string patientID_str, int userID, string doctorPosition, string doctorName)
        {
            InitializeComponent();
            this.patientRID = patientID_str;
            this.userID = userID;
            this.doctorPosition = doctorPosition;
            this.doctorName = doctorName;

            DOPDPC_doctorName.Text = doctorName;
            DOPDPC_docPosition.Text = doctorPosition;
            DOPDPC_docID.Text = userID.ToString();

            MyStartPatientMedicalEvent();


        }

        //Variable Store area

        private string G_doctorName;
        private string G_doctorPosition;
        private string G_patientName;
        private string G_patientAge;
        private string G_patientGender;
       


        private void MyStartPatientMedicalEvent()
            // This is also where we get patient deatils and going to send them acros the user control
        {
            // Get current date and time
            DateTime currentDate = DateTime.Today;
            DateTime currentTime = DateTime.Now;

            // Assign date and time to labels
            DOPDPC_date.Text = currentDate.ToShortDateString();
            DOPDPC_time.Text = currentTime.ToShortTimeString();

            try
            {
                connect.Open();

                string query2 = "SELECT P_NameWithIinitials, P_Age, P_Gender FROM Patient WHERE P_RegistrationID = @patientRID";
                SqlCommand sqlCommand2 = new SqlCommand(query2, connect);
                sqlCommand2.Parameters.AddWithValue("@patientRID", patientRID);
                SqlDataReader reader2 = sqlCommand2.ExecuteReader();
                if (reader2.Read())
                {
                    DOPDPC_patietName_lbl.Text = reader2.GetString(0);
                    DOPDPC_patietage_lbl.Text = reader2.GetString(1);
                    DOPDPC_patietGender_lbl.Text = reader2.GetString(2);


                }
                else
                {
                    // Handle case when no matching record is found
                    MessageBox.Show("Patien Registration ID not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }




               /* string query = "INSERT INTO PatientMedical_Event (PatientRegistration_ID, Doctor_ID, PMRE_Location, PatientExaminatioNote )"
                    +"VALUES (@patietnRegistrationId, @doctorId, @location, @examinationNote)";

                using (SqlCommand command = new SqlCommand(query,connect))
                {
                    command.Parameters.AddWithValue("@patietnRegistrationId", patientRID);
                    command.Parameters.AddWithValue("@doctorId", userID);
                    command.Parameters.AddWithValue("@PMRE_Location", "OPD");
                    // Need to add PatientExaminatioNote

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }*/


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine("DCPC;; "+ex);
            }
            finally
            {
                connect.Close();
            }


        }

        private void DOPDPC_addLabRequest_Click(object sender, EventArgs e)
        {
            Common_MakeLabRequest common_MakeLabRequest = new Common_MakeLabRequest();
            common_MakeLabRequest.Show();
            this.Hide();
        }
    }
}
