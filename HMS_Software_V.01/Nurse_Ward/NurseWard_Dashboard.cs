using HMS_Software_V._01.Doctor_Ward.UserControls;
using HMS_Software_V._01.Nurse_Ward.UserControls;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace HMS_Software_V._01.Nurse_Ward
{
    public partial class NurseWard_Dashboard : Form
    {
        private int NurseID;
        public NurseWard_Dashboard()
        {
            InitializeComponent();

            LoadData();
        }

        private string DoctorName;

        private string PatientRID;
        private string PatientName;
        private string PatientAge;
        private string PatientGender;
        private string P_Condition;
        private string P_MedicalEventID;
        private int VisitRound;
        private string NurseTreatmentStatus;
        private int VisitedNurseID;
        private string WardName;
        


        private void LoadData()
        {
            try
            {

                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Load Nurse Details ------------------------------------------------------------------------
                    /*string query = "SELECT D_NameWithInitials, D_Position, D_Specialty, D_RegistrationID" +
                    " FROM Doctor WHERE Doctor_ID = @doctorID";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@doctorID", DoctorID);
                        *//*Console.WriteLine("DoctorID from dashboard: " + DoctorID);*//*
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                DoctorName = reader["D_NameWithInitials"].ToString();
                                DoctorPosition = reader["D_Position"].ToString();
                                DoctorSpecialty = reader["D_Specialty"].ToString();
                                Doctor_RID = reader["D_RegistrationID"].ToString();

                                DWD_name.Text = DoctorName;
                                DWD_Position.Text = DoctorPosition;

                            }
                            else
                            {
                                MessageBox.Show("No matching record found.");
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error1:" + ex);
                        }
                    }*/



                    //Load Today Admitted Patient Events  ------------------------------------------------------------------------
                    string query1 = "SELECT P_RID, P_NameWithInitials, P_Age, P_Gender, Visite_Round, P_Condition, P_MedicalEventID, N_TreatmentStatus, Visited_Nurse_ID, P_Ward FROM" +
                       " Admitted_Patients_VisitEvent"+
                       " WHERE Visite_Date = @visite_Date";

                    using (SqlCommand cmd = new SqlCommand(query1, connect))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("Admitted_Patients_VisitEvent Table Reads");
                                try
                                {
                                    PatientRID = reader["P_RID"].ToString();
                                    PatientName = reader["P_NameWithInitials"].ToString();
                                    PatientAge = reader["P_Age"].ToString();
                                    PatientGender = reader["P_Gender"].ToString();
                                    P_Condition = reader["P_Condition"].ToString();
                                    P_MedicalEventID = reader["P_MedicalEventID"].ToString();
                                    NurseTreatmentStatus = reader["N_TreatmentStatus"].ToString();
                                    VisitRound = (int)reader["Visite_Round"];
                                    VisitedNurseID = (int)reader["Visited_Nurse_ID"];
                                    WardName = reader["P_Ward"].ToString();



                                }
                                catch (FormatException)
                                {
                                    MessageBox.Show("Patient RID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                N_ShowAllPatients n_ShowAllPatients = new N_ShowAllPatients();

                                // Adding Values to labels
                                n_ShowAllPatients.NSAP_name.Text = PatientName;
                                n_ShowAllPatients.NSAP_age.Text = PatientAge;
                                n_ShowAllPatients.NSAP_Condition.Text = P_Condition;
                                n_ShowAllPatients.NSAP_gender.Text = PatientGender;
                                n_ShowAllPatients.NSAP_id.Text = PatientRID;

                                if (string.IsNullOrEmpty(NurseTreatmentStatus))
                                {
                                    panel1.BackColor = Color.FromArgb(255, 205, 101);
                                }
                                else if(NurseTreatmentStatus == "Pending")
                                {
                                    panel1.BackColor = Color.FromArgb(86, 114, 255);

                                    // Show Nurse ID if Nurese Visited at least one time
                                    n_ShowAllPatients.SWP_NurseName.Text = VisitedNurseID.ToString();
                                }
                                else
                                {
                                    // Show Nurse ID if Nurese Visited even after patient treatment is completed.
                                    n_ShowAllPatients.SWP_NurseName.Text = VisitedNurseID.ToString();
                                    panel1.BackColor = Color.FromArgb(83, 217, 72);
                                }

                                //Storing Valuse to user control
                                n_ShowAllPatients.NSAPUC_P_Name = PatientName;
                                n_ShowAllPatients.NSAPUC_P_RID = PatientRID;
                                n_ShowAllPatients.NSAPUC_P_Age = PatientAge;
                                n_ShowAllPatients.NSAPUC_P_Gender = PatientGender;
                                n_ShowAllPatients.NSAPUC_P_Condition = P_Condition;
                                n_ShowAllPatients.NSAPUC_NureseID = NurseID;
                                n_ShowAllPatients.NSAPUC_P_Ward = WardName;






                                if (!IsVisitedByDoctor)
                                {
                                    DWD_DisplayInPatient_FlowLP.Controls.Add(showWaitionInPatients);


                                }

                                DWD_DisplayInPatient_FlowLP.SizeChanged += (sender, e) =>
                                {
                                    // Adjust the width of the user control to match the width of the parent container
                                    showWaitionInPatients.Width = DWD_DisplayInPatient_FlowLP.ClientSize.Width - showWaitionInPatients.Margin.Horizontal;
                                };

                            }
                            reader.Close();
                        }
                        }

                    }

                }
            

            catch (Exception ex)
            {

            }
        }
    }
}
