using HMS_Software_V._01.Common_UseForms.UserControls;
using HMS_Software_V._01.Nurse_Ward.UserControls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Nurse_Ward
{
    public partial class NurseWard_TreatePatient : Form
    {

        public Form DashboardFormReference { get; set; } //Getting Dashboard From refferece from the user control


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


            LoadBasicData();
            LoadData();
        }


        string LabRequestIDs;
        string PrescriptionRequestIDs;
        int PatientMonitorRequestID;
        
        private void LoadBasicData()
        {
            NWTP_P_Name.Text = PatientName;
            NWTP_P_Age.Text = PatientAge;
            NWTP_P_Gender.Text = PatientGender;
            NWTP_P_Cause.Text = PatientCondition;
            NWTP_P_RID.Text = PatientRID;

            DateTime time1 = DateTime.Now;
            DateTime today1 = DateTime.Today;

            NWTP_date.Text = today1.ToString("yyyy-MM-dd");
            NWTP_time.Text = time1.ToString("hh:mm:ss tt");

            NWTP_N_ID.Text = NurseID.ToString();

        }


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
                                Console.WriteLine("Admitted Patient Medical Events Recived. ");
                                try
                                {
                                    LabRequestIDs = reader["LabRequest_ID"].ToString();
                                    PrescriptionRequestIDs = reader["PrescriptionRequest_ID"].ToString();

                                    // Becouse if ID is empt, there going to be an error
                                    PatientMonitorRequestID = !reader.IsDBNull(reader.GetOrdinal("PatinetMonitortRequest_ID")) ? Convert.ToInt32(reader["PatinetMonitortRequest_ID"]) : 0;

                                    Console.WriteLine("++ LabRequestIDs assigned: " + LabRequestIDs);
                                    Console.WriteLine("++ PrescriptionRequestIDs assigned: " + PrescriptionRequestIDs);
                                    Console.WriteLine("++ PatientMonitorRequestID assigned: " + PatientMonitorRequestID);

                                }
                                catch (FormatException)
                                {
                                    MessageBox.Show("PatientMedicalEvent_ID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                                NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents = new NWTP_PatientMedicalEvents();

                                //Since there are calling a single Patient Medical Event Record, there will be no Loops.
                                

                                
                                /*List<int> PrescriptionRequestID_List = MyConvertStringRequestIDtoInt(PrescriptionRequestIDs);*/

                                //--------------------------------------------------------------------------------------------------------------------------------
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

                                    Console.WriteLine("Received the list. Lets Generate UserControl for that list:");

                                    /*foreach (int id in LabRequestID_Lists)
                                    {
                                        Console.WriteLine(id);
                                    }*/

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
            Console.WriteLine($"Input string: {value}");

            // Split the input string by comma delimiter
            string[] parts = value.Split(',');

            List<int> intList = new List<int>();

            foreach (string part in parts)
            {
                // Skip empty or whitespace-only parts
                if (string.IsNullOrWhiteSpace(part))
                {
                    Console.WriteLine($"Skipping empty or whitespace-only part.");
                    continue;
                }

                // Attempt to parse the part as an integer
                if (int.TryParse(part.Trim(), out int num))
                {
                    Console.WriteLine($"Processing part: {part}. Parsed integer: {num}");
                    intList.Add(num);
                }
                else
                {
                    Console.WriteLine($"Skipping non-numeric value: '{part}'");
                }
            }

            Console.WriteLine("Conversion completed.");
            return intList;
        }

        
        static bool MyIfTaskIsCompleted(string input) //To Check tasks are completed
        {
            Console.WriteLine("Input string: " + input);

            string[] parts = input.Split(',');

            // Output all the parts
            Console.WriteLine("Parts:");
            foreach (string part in parts)
            {
                Console.WriteLine("  " + part.Trim());
            }

            // Check if the last part is "off"
            string lastPart = parts[parts.Length - 1].Trim(); // Trim to remove any leading/trailing whitespace

            if (lastPart.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                // "yes" is present at the end
                Console.WriteLine("Last task is completed: Yes");
                return true;
            }
            else
            {
                // "off" is not present at the end
                Console.WriteLine("Last task is completed: No");
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
                        Console.WriteLine("MyGenerateLabTaks --> LabID: " + id);

                        using (SqlCommand cmd = new SqlCommand(query1, connect))
                        {
                            cmd.Parameters.AddWithValue("@labRequest_ID", id);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    if (reader.Read()) // Check if there is data available
                                    {
                                        Console.WriteLine("MyGenerateLabTaks --> Lab Record Found for LabID: " + id);

                                        string investigationName = reader["LR_InvestigationName"].ToString();
                                        string specimentName = reader["LR_SpecimenName"].ToString();
                                        string labelNumber = reader["LR_LabelNumber"].ToString();

                                        Console.WriteLine($"Investigation Name: {investigationName}");
                                        Console.WriteLine($"Specimen Name: {specimentName}");
                                        Console.WriteLine($"Label Number: {labelNumber}");


                                        // Create a string to store investigationName, specimentName, and labelNumber
                                        string DisplayLabInfo = "Investigation: " + investigationName + "\n" +
                                            "Specimen: " + specimentName + "\n" + labelNumber;

                                        Console.WriteLine("Create a string to store investigationName, specimentName, and labelNumber: " + DisplayLabInfo);

                                        // Add values to the user control
                                        nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text = "Lab Request";
                                        nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Text = DisplayLabInfo;

                                        //---------------------------------------------------------------------------------------------------------------------------------------

                                        // Change lable Coordinate
                                        int changeY = 8;

                                        // Get the current location of the label
                                        Point currentLocation =  nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Location;
                                        int newY = currentLocation.Y - changeY;

                                        // Set the new location with both X and Y coordinates
                                        nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Location = new Point(currentLocation.X, newY);

                                        //---------------------------------------------------------------------------------------------------------------------------------------

                                        //Check if the task is alrady completed.
                                        bool IsComplteted = MyIfTaskIsCompleted(LabRequestIDs);
                                        Console.WriteLine("Is last task completed? " + IsComplteted);

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


                                        P_MedicalEvents_FlowLP.SizeChanged += (sender, e) =>
                                        {
                                            // Adjust the width of the user control to match the width of the parent container
                                            nWTP_PatientMedicalEvents.Width = P_MedicalEvents_FlowLP.ClientSize.Width - nWTP_PatientMedicalEvents.Margin.Horizontal;
                                        };
                                    }
                                    else
                                    {
                                        Console.WriteLine("MyGenerateLabTaks --> No Lab Record Found for LabID: " + id);
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

        private void NurseWard_TreatePatient_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DashboardFormReference != null && !DashboardFormReference.IsDisposed)
            {
                DashboardFormReference.Show();
            }
            else
            {
                // Handle the situation where the reference form is closed or disposed
                Console.WriteLine("Dashboard form is closed or disposed.");
            }
        }


        private List<bool> Lab_boolList = new List<bool>();
        private List<bool> General_boolList = new List<bool>();
        private bool IsMedicationRequestComplete;

        private void NWTP_Save_btn_Click(object sender, EventArgs e)
        {
            foreach (Control control in P_MedicalEvents_FlowLP.Controls)
            {
                if (control is NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents)
                {

                    /*string NWTP_RequestDetails = nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Text;*/
                    string NWTP_RequestType = nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text;
                    bool NWTP_IsCompleted = nWTP_PatientMedicalEvents.NWTPUC_checkBox.Checked;

                    General_boolList.Add(NWTP_IsCompleted); // To deside what should we display in Nures dashboard Patient status --> Pending,Completed or None



                    if (NWTP_RequestType == "Lab Request")
                    {
                        Lab_boolList.Add(NWTP_IsCompleted);


                    }
                    else if(NWTP_RequestType == "Medication")
                    {
                        if(NWTP_IsCompleted)
                        {
                            IsMedicationRequestComplete = true;
                        }

                    }

                }

            }
            MyUpdateDataTables();
        }


        string LabRequestCompleteStatus;
        private void MyUpdateDataTables()
        {

            // If the list is empty, return false
            if (Lab_boolList.Count == 0)
            {
                Console.WriteLine("Error !! --> Lab result Bool is empty!");   
            }
                

            // Use LINQ's All method to check if all values are true
            bool result1 = Lab_boolList.All(value => value);
            // Use LINQ's Any method to check if any value is true
            bool result2 = Lab_boolList.Any(value => value);


            if (result1)
            {
                //All the Lab Request are Completed
                LabRequestCompleteStatus = "Completed";

            }
            else if(result2)
            {
                //All are not Completed
                LabRequestCompleteStatus = "Pending";
            }
            else
            {
                //Non is completed
                LabRequestCompleteStatus = "";
            }



            try
            {
                using(SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();


                    string query = "INSERT INTO Admitted_Patients_VisitEvent (N_TreatmentStatus)" +
                    " VALUES (@N_TreatmentStatus)";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        Console.WriteLine("Inserting to Admitted_Patients_VisitEvent table, LabRequestCompleteStatus : " + LabRequestCompleteStatus);
                      
                        cmd.Parameters.AddWithValue("@N_TreatmentStatus", LabRequestCompleteStatus);

                        cmd.ExecuteNonQuery();
                    }

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:3 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error3:" + ex);
            }

        }
    }
}
