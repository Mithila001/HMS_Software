using HMS_Software_V._01.Nurse_Ward.UserControls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Nurse_Ward
{
    public partial class NurseWard_TreatePatient : Form
    {
        string PatientName;
        string PatientRID;
        string PatientAge;
        string PatientGender;
        string PatientCondition;
        int PatientMEID;
        int NurseID;
        string PatientWard;

        public NurseWard_TreatePatient(string NSAPUC_P_Name, string NSAPUC_P_RID, string NSAPUC_P_Age, string NSAPUC_P_Gender,
                string NSAPUC_P_Condition, int NSAPUC_P_MedicalEventID, int NSAPUC_NureseID, string NSAPUC_P_Ward)
        {
            InitializeComponent();

            this.PatientName = NSAPUC_P_Name;
            this.PatientRID = NSAPUC_P_RID;
            this.PatientAge = NSAPUC_P_Age;
            this.PatientGender = NSAPUC_P_Gender;
            this.PatientCondition = NSAPUC_P_Condition;
            this.PatientMEID = NSAPUC_P_MedicalEventID;
            this.NurseID = NSAPUC_NureseID;
            this.PatientWard = NSAPUC_P_Ward;



            LoadData();
        }


        string LabRequestIDs;
        string PrescriptionRequestIDs;
        int PatientMonitorRequestID;
        

        private void LoadData()
        {
            try
            {

                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    //Load Today Admitted Patient Medical Events  ------------------------------------------------------------------------
                    string query1 = "SELECT LabRequest_ID, PrescriptionRequest_ID, PatinetMonitortRequest_ID FROM" +
                       " PatientMedical_Event" +
                       " WHERE PatientMedicalEvent_ID = @patientMEID";


                    DateTime today = DateTime.Today;

                    using (SqlCommand cmd = new SqlCommand(query1, connect))
                    {
                        cmd.Parameters.AddWithValue("@patientMEID", PatientMEID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("Admitted_Patients_VisitEvent Table Reads");
                                try
                                {
                                    LabRequestIDs = reader["LabRequest_ID"].ToString();
                                    PrescriptionRequestIDs = reader["PrescriptionRequest_ID"].ToString();

                                    // Becouse if ID is empt, there going to be an error
                                    PatientMonitorRequestID = !reader.IsDBNull(reader.GetOrdinal("PatinetMonitortRequest_ID")) ? Convert.ToInt32(reader["PatinetMonitortRequest_ID"]) : 0;

                                    Console.WriteLine("LabRequestIDs assigned: " + LabRequestIDs);
                                    Console.WriteLine("PrescriptionRequestIDs assigned: " + PrescriptionRequestIDs);
                                    Console.WriteLine("PatientMonitorRequestID assigned: " + PatientMonitorRequestID);

                                }
                                catch (FormatException)
                                {
                                    MessageBox.Show("PatientMedicalEvent_ID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                                NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents = new NWTP_PatientMedicalEvents();

                                //Since there are calling a single Patient Medical Event Record, there will be no Loops.
                                

                                
                                /*List<int> PrescriptionRequestID_List = MyConvertStringRequestIDtoInt(PrescriptionRequestIDs);*/

                                //Check If there are Prescription request table is available
                                if (!string.IsNullOrEmpty(PrescriptionRequestIDs))
                                {
                                    Console.WriteLine("Prescription Request IDs is available");

                                    nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text = "Medication";
                                    nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Text = PrescriptionRequestIDs + " for Now";
                                    
                                    //Check if the task is alrady completed.
                                    bool IsComplteted = MyIfTaskIsCompleted(PrescriptionRequestIDs);
                                    if (IsComplteted)
                                    {
                                        nWTP_PatientMedicalEvents.NWTPUC_checkBox.Checked = true;
                                        P_MedicalEvents_FlowLP.Controls.Add(nWTP_PatientMedicalEvents); // Generate A Single User Control
                                    }
                                    else
                                    {
                                        nWTP_PatientMedicalEvents.NWTPUC_checkBox.Checked = false;
                                        P_MedicalEvents_FlowLP.Controls.Add(nWTP_PatientMedicalEvents); // Generate A Single User Control
                                    }
                                }

                                // Check if the Lab Request available
                                if (!string.IsNullOrEmpty(LabRequestIDs))
                                {
                                    Console.WriteLine("LabRequestIDs is available");

                                    // Assigne string IDs to int list
                                    List<int> LabRequestID_Lists = MyConvertStringRequestIDtoInt(LabRequestIDs);
                                    // Generate UserControl for that list
                                    MyGenerateLabTaks(LabRequestID_Lists);
                                }


                                P_MedicalEvents_FlowLP.SizeChanged += (sender, e) =>
                                {
                                    // Adjust the width of the user control to match the width of the parent container
                                    nWTP_PatientMedicalEvents.Width = P_MedicalEvents_FlowLP.ClientSize.Width - nWTP_PatientMedicalEvents.Margin.Horizontal;
                                };

                            }
                            reader.Close();
                        }
                    }

                }

            }


            catch (Exception ex)
            {
                MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error1:" + ex);

            }
        }

        // Method to convert a string to a list of integers
        private static List<int> MyConvertStringRequestIDtoInt(string value) // Dividi Table Cells that contain mutiple ID in string fomat
        {
            // Split the input string by comma delimiter
            string[] parts = value.Split(',');

            List<int> intList = new List<int>();

            // Loop through each part and parse it to an integer
            foreach (string part in parts)
            {
               
                if (int.TryParse(part.Trim(), out int num))
                {
                   
                    intList.Add(num);
                }
                else
                {
                    // Handle invalid input 
                    throw new ArgumentException($"Invalid input: '{part}' is not a valid integer.");
                }
            }

        
            return intList;
        }

        
        static bool MyIfTaskIsCompleted(string input) //To Check tasks are completed
        {
            string[] parts = input.Split(',');

            // Check if the last part is "off"
            string lastPart = parts[parts.Length - 1].Trim(); // Trim to remove any leading/trailing whitespace

            if (lastPart.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                // "yes" is present at the end
                return true;
            }
            else
            {
                // "off" is not present at the end
                return false;
            }
        }


        // Generate Lab Task User Controls for each Lab Request
        private void MyGenerateLabTaks(List<int> LabRequestID_Lists)
        {
            NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents = new NWTP_PatientMedicalEvents();

            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    //Load the Lab Request Table  ------------------------------------------------------------------------
                    string query1 = "SELECT LR_InvestigationName, LR_SpecimenName, LR_LabelNumber FROM" +
                       " Lab_Request" +
                       " WHERE LabRequest_ID = @labRequest_ID";

                    foreach (int id in LabRequestID_Lists) // For each Lab Request IDs, generate a user control
                    {
                        /*Console.WriteLine(id);*/

                        using (SqlCommand cmd = new SqlCommand(query1, connect))
                        {
                            cmd.Parameters.AddWithValue("@labRequest_ID", id);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    string investigationName = reader["LR_InvestigationName"].ToString();
                                    string specimentName = reader["LR_SpecimenName"].ToString();
                                    string labelNumber = reader["LR_LabelNumber"].ToString();


                                    // Create a string to store investigationName, specimentName, and labelNumber
                                    string DisplayLabInfo = "Investigation: " + investigationName + "\n"+
                                        "Specimen: "+ specimentName + "\n" + labelNumber;


                                    // Add values to the user control
                                    nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text = "Lab Request";
                                    nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Text = DisplayLabInfo;


                                    //Check if the task is alrady completed.
                                    bool IsComplteted = MyIfTaskIsCompleted(LabRequestIDs);
                                    if (IsComplteted)
                                    {
                                        nWTP_PatientMedicalEvents.NWTPUC_checkBox.Checked = true;
                                        P_MedicalEvents_FlowLP.Controls.Add(nWTP_PatientMedicalEvents); // Generate A Single User Control
                                    }
                                    else
                                    {
                                        nWTP_PatientMedicalEvents.NWTPUC_checkBox.Checked = false;
                                        P_MedicalEvents_FlowLP.Controls.Add(nWTP_PatientMedicalEvents); // Generate A Single User Control
                                    }


                                }
                                catch (FormatException)
                                {
                                    MessageBox.Show("PatientMedicalEvent_ID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }

                        }


                    }

                   



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:2 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error2:" + ex);
            }
        }
    }
}
