using HMS_Software_V._01.Admission_Officer;
using HMS_Software_V._01.Admission_Officer.UserControls;
using HMS_Software_V._01.Doctor_OPD.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Admition_Officer
{
    public partial class AdmissionOfficer_Dashboard : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private int AdmissionOfficerID;
        public AdmissionOfficer_Dashboard(int userID = 9)
        {
            InitializeComponent();
            this.AdmissionOfficerID = userID;

            LoadBasicData();

            LoadAdmitRequestData();

        }

        private string AO_Name;
        private string AO_Position;
        private string AO_Specialty;
        private string AO_RegistrationID;


        private void LoadBasicData()
        {
            try
            {
                connect.Open();

                // Load Admission Officer (Doctor) Name
                string query = "SELECT D_NameWithInitials, D_Position, D_Specialty, D_RegistrationID" +
                    " FROM Doctor WHERE Doctor_ID = @aoID";
                using (SqlCommand command = new SqlCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@aoID", AdmissionOfficerID);
                    Console.WriteLine("AdmissionOfficerID from dashboard: " + AdmissionOfficerID);

                    try
                    {
                     
                        SqlDataReader reader = command.ExecuteReader();

                        // Check if any rows were returned
                        if (reader.Read())
                        {
                            AO_Name = reader["D_NameWithInitials"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No matching record found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Error1:" + ex);
                    }
                }

                AOD_name.Text = AO_Name;

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error1:" + ex);
            }
            finally
            {
                connect.Close();
            }
        }
   

        private void LoadAdmitRequestData()
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
                        Console.WriteLine("Found UserData");
                        string p_RegistrationID = reader1["P_RegistrationID"].ToString();
                        string p_NameWithInitials = reader1["P_NameWithInitials"].ToString();
                        string p_age = reader1["P_Age"].ToString();
                        string p_Gender = reader1["P_Gender"].ToString();
                        string p_ReferralNote = reader1["P_ReferralNote"].ToString();
                        int p_DoctorID = Convert.ToInt32(reader1["Doctor_ID"]);
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

                        admitRequest.AO_Name = AO_Name;
                        admitRequest.AO_Position = AO_Position;
                        admitRequest.AO_RegistrationID = AO_RegistrationID;
                        admitRequest.AO_Specialty = AO_Specialty;
                        admitRequest.AO_ID = AdmissionOfficerID;



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

        string patientRID;
        string patientName;
        string patientAge;
        string patientGender;

        private void AOD_directAdmit_btn_Click(object sender, EventArgs e)
        {
           

            if (!string.IsNullOrEmpty(AOVR_ward_tbx.Text))
            {
                try
                {
                    connect.Open();
                    //Getting Admit Request Details from the Database
                    string query1 = "SELECT P_RegistrationID, P_NameWithIinitials, P_Age, P_Gender FROM" +
                        " Patient WHERE P_RegistrationID = @patientRID";

                    using (SqlCommand cmd = new SqlCommand(query1, connect))
                    {
                        cmd.Parameters.AddWithValue("@patientRID", AOVR_ward_tbx.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    patientRID = reader["P_RegistrationID"].ToString();
                                    patientName = reader["P_NameWithInitials"].ToString();
                                    patientAge = reader["P_Age"].ToString();
                                    patientGender = reader["P_Gender"].ToString();

                                }
                                catch (FormatException)
                                {
                                    MessageBox.Show("Patient RID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(patientRID))
                    {
                        AdmissionOfficer_DirectAdmit admissionOfficer_DirectAdmit = new AdmissionOfficer_DirectAdmit(patientName, patientAge, patientGender, AO_Name, patientRID, AdmissionOfficerID);
                        admissionOfficer_DirectAdmit.Show();
                        this.Hide();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Error:" + ex);

                }
                finally
                {
                    connect.Close();
                }
            }
            else
            {
                MessageBox.Show("Add Patient RID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
