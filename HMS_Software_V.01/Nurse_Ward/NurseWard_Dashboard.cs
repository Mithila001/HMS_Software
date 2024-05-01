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
        private MyTableData_Automation automation; //Automation


        private int NurseID;
        /*private string UnitName;*/
        private int WardNumber;
        public NurseWard_Dashboard(int userID =5, int WardNumber = 5)
        {
            InitializeComponent();
            this.NurseID = userID;
            this.WardNumber = WardNumber;

            LoadData();

            automation = new MyTableData_Automation();
            automation.MyGetAdmittedPatientRecord();
        }

        private string DoctorName;

        private string PatientRID;
        private string PatientName;
        private string PatientAge;
        private string PatientGender;
        private string P_Condition;
        private int P_MedicalEventID;
        private int VisitRound;
        private string NurseTreatmentStatus;
        private int VisitedNurseID;
        private string WardName;


        string NureseName;
        string N_TreatmentStatus;


        private void LoadData()
        {
            try
            {
                DateTime today = DateTime.Today;

                NWD_Date.Text = today.ToString("yyyy-MM-dd");
                NWD_wardNumber.Text = WardNumber.ToString();

                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Load Nurse Details ------------------------------------------------------------------------
                    string query = "SELECT N_NameWithInitials" +
                    " FROM Nurse WHERE Nurce_ID = @Nurce_ID";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        Console.WriteLine("Nurce_ID: " + NurseID);
                        command.Parameters.AddWithValue("@Nurce_ID", NurseID);
                        
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                NureseName = reader["N_NameWithInitials"].ToString();
                                NWD_N_Name.Text = NureseName;

                            }
                            else
                            {
                                MessageBox.Show("No matching N_NameWithInitials record found.");
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error1:" + ex);
                        }
                    }


                    // Load Ward Details ------------------------------------------------------------------------
                    string query3 = "SELECT WardName" +
                    " FROM WardTypes WHERE WardNumber = @WardNumber";
                    using (SqlCommand command = new SqlCommand(query3, connect))
                    {
                        command.Parameters.AddWithValue("@WardNumber", WardNumber);
                        Console.WriteLine("WardNumber from dashboard: " + WardNumber);
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                WardName = reader["WardName"].ToString();
                                NWD_WardName.Text = WardName;
                            }
                            else
                            {
                                MessageBox.Show("No matching WardName record found.");
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:2 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Erro2:" + ex);
                        }
                    }



                    //Load Today Admitted Patient Events  ------------------------------------------------------------------------
                    string query1 = "SELECT P_RID, P_NameWithInitials, P_Age, P_Gender, Visite_Round, P_Condition, P_MedicalEventID, N_TreatmentStatus, Visited_Nurse_ID, P_Ward, N_TreatmentStatus FROM" +
                       " Admitted_Patients_VisitEvent" +
                       " WHERE Visite_Date = @visite_Date";


                    using (SqlCommand cmd = new SqlCommand(query1, connect))
                    {
                        cmd.Parameters.AddWithValue("@visite_Date", today);


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
                                    P_MedicalEventID = reader.IsDBNull(reader.GetOrdinal("P_MedicalEventID")) ? 0 : (int)reader["P_MedicalEventID"];
                                    NurseTreatmentStatus = reader["N_TreatmentStatus"].ToString();  
                                    VisitRound = (int)reader["Visite_Round"];
                                    N_TreatmentStatus = reader["N_TreatmentStatus"].ToString();

                                    VisitedNurseID = reader.IsDBNull(reader.GetOrdinal("Visited_Nurse_ID")) ? 0 : (int)reader["Visited_Nurse_ID"];



                                    /*VisitedNurseID = reader.IsDBNull(reader.GetOrdinal("Visited_Nurse_ID")) ?
                                                       default(int) :
                                                       reader.GetInt32(reader.GetOrdinal("Visited_Nurse_ID"));*/

                                    

                                    Console.WriteLine("PatientRID: " + PatientRID);
                                    /*Console.WriteLine("PatientName: " + PatientName);
                                    Console.WriteLine("PatientAge: " + PatientAge);
                                    Console.WriteLine("PatientGender: " + PatientGender);
                                    Console.WriteLine("P_Condition: " + P_Condition);
                                    Console.WriteLine("P_MedicalEventID: " + P_MedicalEventID);
                                    Console.WriteLine("NurseTreatmentStatus: " + NurseTreatmentStatus);
                                    Console.WriteLine("VisitRound: " + VisitRound);
                                    Console.WriteLine("VisitedNurseID: " + VisitedNurseID);
                                    */


                                }
                                catch (FormatException)
                                {
                                    MessageBox.Show("Patient RID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                                N_ShowAllPatients n_ShowAllPatients = new N_ShowAllPatients();

                                // Generate UserControls to only records with Patient Medical Evenets. Why? Becouse There are nothing to Treate if the doctor didint gave instructions
                                // So that meand Patient Medical event is neccessary in ordert to display user control in this form.
                                // 
                                if (P_MedicalEventID != 0)
                                {
                                    Console.WriteLine("---- N_TreatmentStatus is Null or Empty,  MEID: ----: " + P_MedicalEventID);

                                    // Adding Values to labels
                                    n_ShowAllPatients.NSAP_name.Text = PatientName;
                                    n_ShowAllPatients.NSAP_age.Text = PatientAge;
                                    n_ShowAllPatients.NSAP_Condition.Text = P_Condition;
                                    n_ShowAllPatients.NSAP_gender.Text = PatientGender;
                                    n_ShowAllPatients.NSAP_id.Text = PatientRID;

                                    if (string.IsNullOrEmpty(NurseTreatmentStatus))
                                    {
                                        n_ShowAllPatients.panel1.BackColor = Color.FromArgb(255, 205, 101);
                                    }
                                    else if (NurseTreatmentStatus == "Pending")
                                    {
                                        n_ShowAllPatients.panel1.BackColor = Color.FromArgb(86, 114, 255);

                                        // Show Nurse ID if Nurese Visited at least one time
                                        n_ShowAllPatients.SWP_NurseName.Text = VisitedNurseID.ToString();
                                    }
                                    else
                                    {
                                        // Show Nurse ID if Nurese Visited even after patient treatment is completed.
                                        n_ShowAllPatients.SWP_NurseName.Text = VisitedNurseID.ToString();
                                        n_ShowAllPatients.panel1.BackColor = Color.FromArgb(83, 217, 72);
                                    }

                                    //Storing Valuse to user control
                                    n_ShowAllPatients.NSAPUC_P_Name = PatientName;
                                    n_ShowAllPatients.NSAPUC_P_RID = PatientRID;
                                    n_ShowAllPatients.NSAPUC_P_Age = PatientAge;
                                    n_ShowAllPatients.NSAPUC_P_Gender = PatientGender;
                                    n_ShowAllPatients.NSAPUC_P_Condition = P_Condition;
                                    n_ShowAllPatients.NSAPUC_NureseID = NurseID;
                                    n_ShowAllPatients.NSAPUC_P_Ward = WardName;
                                    n_ShowAllPatients.NSAPUC_P_MedicalEventID = P_MedicalEventID;



                                    NWCP_ShowPatients_FlowLP.Controls.Add(n_ShowAllPatients);



                                    NWCP_ShowPatients_FlowLP.SizeChanged += (sender, e) =>
                                    {
                                        // Adjust the width of the user control to match the width of the parent container
                                        n_ShowAllPatients.Width = NWCP_ShowPatients_FlowLP.ClientSize.Width - n_ShowAllPatients.Margin.Horizontal;
                                    };

                                }
                                else
                                {
                                    Console.WriteLine("Record doesnt hava a PatiemtMedical Event ID");
                                }

                            }
                            reader.Close();
                        }
                    }

                }

            }


            catch (Exception ex)
            {
                MessageBox.Show("Error3 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error3:" + ex);

            }
        }
    }
}
