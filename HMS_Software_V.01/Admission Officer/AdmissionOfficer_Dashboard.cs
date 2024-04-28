using HMS_Software_V._01.Admission_Officer.UserControls;
using HMS_Software_V._01.Doctor_OPD.UserControls;
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

namespace HMS_Software_V._01.Admition_Officer
{
    public partial class AdmissionOfficer_Dashboard : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);


        public AdmissionOfficer_Dashboard()
        {
            InitializeComponent();

            LoadUserData();
        }



   

        private void LoadUserData()
        {
            try
            {
                connect.Open();
                DateTime today = DateTime.Today;

                string query = "SELECT P_RegistrationID, P_NameWithInitials, P_Age, P_Gender, P_ReferralNote, Doctor_ID, " +
                "Is_Urgent, Is_Admitted, Unit_Type, PatientAdmit_ID " +
                "FROM Patient_Admit";


                SqlCommand command = new SqlCommand(query, connect);
                using (SqlDataReader reader1 = command.ExecuteReader())
                {
                    // Loop through the records retrieved from the database
                    while (reader1.Read())
                    {
                        string p_RegistrationID = reader1["P_RegistrationID"].ToString();
                        string p_NameWithInitials = reader1["P_NameWithInitials"].ToString();
                        string p_age = reader1["P_Age"].ToString();
                        string p_Gender = reader1["P_Gender"].ToString();
                        string p_ReferralNote = reader1["P_ReferralNote"].ToString();
                        string p_DoctorID = reader1["Doctor_ID"].ToString();
                        bool p_IsUrgent = (bool)reader1["Is_Urgent"];
                        bool p_IsAdmitted = (bool)reader1["Is_Admitted"];
                        string p_UnitType = reader1["Unit_Type"].ToString();
                        int p_admitID = (int)reader1["PatientAdmit_ID"];

                        /*int getclinicID = 0;

                        if (int.TryParse(GclinicID, out getclinicID))  // Convert to int
                        {

                            Console.WriteLine("Investigation ID: " + getclinicID);
                        }*/

                        /*DoctorCheck_clincType doctorOPD_ClincType = new DoctorCheck_clincType();

                        doctorOPD_ClincType.DOPDA_ClincType_lbl.Text = reader1["CT_Name"].ToString();*/

                        AdmitRequest admitRequest = new AdmitRequest();

                        admitRequest.AR_patienName.Text = p_NameWithInitials;
                        admitRequest.AR_patienRID.Text = p_RegistrationID;
                        admitRequest.AR_patientGender.Text = p_Gender;
                        admitRequest.AR_patientAge.Text = p_age;
                        admitRequest.AR_unitType.Text = p_UnitType;

                        admitRequest.AdmitRequestID = p_admitID;

                        if (p_IsUrgent)
                        {
                            admitRequest.AR_patientPanel_colorChanger.BackColor = Color.FromArgb(255, 128, 128);
                        }

                        admitRequests_FlowLP.SizeChanged += (sender, e) =>
                        {
                            // Adjust the width of the user control to match the width of the parent container
                            admitRequest.Width = admitRequests_FlowLP.ClientSize.Width - admitRequest.Margin.Horizontal;
                        };

                        if (!p_IsAdmitted)
                        {
                            admitRequests_FlowLP.Controls.Add(admitRequest);
                        }



                    }
                    reader1.Close();

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error1:" + ex);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
