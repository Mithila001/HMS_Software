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

namespace HMS_Software_V._01.Doctor_Ward
{
    public partial class DoctorWard_ProgressNote : Form
    {
        string PatienName;
        string PatientRID;
        string P_Condition;
        int PatientVisitCount;
        string DoctorName;
        string DoctorRID;
        string WardName;
        string DoctorTitle;
        int DoctorID;
        public DoctorWard_ProgressNote(
                string SWP_PatientName,
                string SWP_PatientRID,
                string SWP_PatientCondition,
                int SWP_PatietnVisitCount,
                string SWP_D_Name,
                string SWP_D_Title,
                string SWP_D_RID,
                string SWP_WardName,
                int SWP_D_ID)
        {

            InitializeComponent();

            this.PatienName = SWP_PatientName;
            this.PatientRID = SWP_PatientRID;
            this.P_Condition = SWP_PatientCondition;
            this.PatientVisitCount = SWP_PatietnVisitCount;
            this.DoctorName = SWP_D_Name;
            this.DoctorRID = SWP_D_RID;
            this.WardName = SWP_WardName;
            this.DoctorTitle = SWP_D_Title;
            this.DoctorID = SWP_D_ID;


            MyLoadBasicDetails();
            MyCreateMedicalEvetn();
        }
        private void MyLoadBasicDetails()
        {
            string todayDateString = DateTime.Today.ToString("yyyy-MM-dd");

            string formattedTime = DateTime.Now.ToString("h:mm tt");


            DWPN_Date.Text = todayDateString;
            DWPN_Time.Text = formattedTime;

            DWPN_D_Name.Text = DoctorName;
            DWPN_D_ID.Text = DoctorRID;
            DWPN_D_Title.Text = DoctorTitle;

            DWPN_WardName.Text = WardName;

            DWPN_P_Name.Text = PatienName;
            DWPN_P_Condition.Text = P_Condition;
            DWPN_P_RID.Text = PatientRID;

            PatientVisitCount = PatientVisitCount + 1; // Add 1 to the Count value
            DWPN_P_VisitCount.Text = PatientVisitCount.ToString(); 


        }

        private int WardID;

        private void MyCreateMedicalEvetn()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Find the Ward Number -------------------------------------------------------------------------------------------------
                    string query1 = "SELECT WardNumber FROM WardTypes WHERE WardName = @wardName";

                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        command.Parameters.AddWithValue("@wardName", WardName);
                        Console.WriteLine("Ward Name " + WardName);
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                WardID = Convert.ToInt32(reader["WardNumber"]);

                            }
                            else
                            {
                                MessageBox.Show("No matching Ward record found.");
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:11 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error11:" + ex);
                        }
                    }


                    // Creating a Patient Medical Event -------------------------------------------------------------------------------------------------

                    string query = "INSERT INTO PatientMedical_Event (PatientRegistration_ID, Doctor_ID, PMRE_Location, PMRE_Date, PMRE_Time, PatinetProgrestNote_ID, PatietnMedicalCondition)"
                    + "VALUES (@patietnRegistrationId, @doctorId, @location, @date, @time, @patinetProgrestNote_ID, @patietnMedicalCondition)";

                    DateTime today = DateTime.Today;
                    DateTime currentTime = DateTime.Now;


                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@patietnRegistrationId", PatientRID);
                        command.Parameters.AddWithValue("@doctorId", DoctorID);
                        command.Parameters.AddWithValue("@location", "Ward"); // Warnig: this need to change
                        command.Parameters.AddWithValue("@date", today);
                        command.Parameters.AddWithValue("@time", currentTime);
                        command.Parameters.AddWithValue("@patinetProgrestNote_ID", DWPN_P_ProgressNote_RichTbx.Text);
                        command.Parameters.AddWithValue("@patietnMedicalCondition", DWPN_P_AddCondition_tbx.Text);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Query executed successfully
                            Console.WriteLine("Insert into PatientMedical_Event executed successfully.");
                        }
                        else
                        {
                            // No rows affected (possible validation)
                            Console.WriteLine("Failed to insert into PatientMedical_Event.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:22 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error22:" + ex);
            }
        }


        private string PatientMID;

        private void DWPN_P_Confirm_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DWPN_P_AddCondition_tbx.Text))
            {
                MessageBox.Show("Add Patietn Condition", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(DWPN_P_ProgressNote_RichTbx.Text))
            {
                MessageBox.Show("Add Patietn ProgressNote", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // If every validation is passed
                bool Admitted_Patients = false;
                bool Admitted_Patients_VisitEvent = false;

                try
                {
                    using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                    {
                        connect.Open();


                        // Updating Admitted_Patients Table --------------------------------------------------------------------------------------------
                        
                        string query = "UPDATE Admitted_Patients" +
                            " SET P_Condition = @p_Condition, "
                            + "P_Visite_TotalRounds = @p_Visite_TotalRounds "
                            + "WHERE P_RID = @p_RID;";

                        using (SqlCommand command = new SqlCommand(query, connect))
                        {
                            command.Parameters.AddWithValue("@p_Condition", P_Condition);
                            command.Parameters.AddWithValue("@p_Visite_TotalRounds", PatientVisitCount+1);
                            command.Parameters.AddWithValue("@p_RID", PatientRID);


                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                // Query executed successfully 
                                Console.WriteLine("UPDATE Admitted_Patients successfully.");
                                Admitted_Patients = true;
                            }
                            else
                            {
                                // No rows affected (possible validation)
                                Console.WriteLine("Failed to UPDATE Admitted_Patients.");
                            }
                        }



                        // Getting Patient Medical Event ID  --------------------------------------------------------------------------------------------
                        
                        string query3 = "SELECT PatientMedicalEvent_ID FROM" + " PatientMedical_Event" + " WHERE PatientRegistration_ID = @PatientRegistration_ID";

                        using (SqlCommand command = new SqlCommand(query3, connect))
                        {
                            command.Parameters.AddWithValue("@PatientRegistration_ID", PatientRID);
                            /*Console.WriteLine("DoctorID from dashboard: " + DoctorID);*/
                            try
                            {
                                SqlDataReader reader = command.ExecuteReader();

                                // Check if any rows were returned
                                if (reader.Read())
                                {
                                    PatientMID = reader["PatientMedicalEvent_ID"].ToString();

                                }
                                else
                                {
                                    MessageBox.Show("No matching PatientMedicalEvent_ID record found.");
                                }
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error:4 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Console.WriteLine("Error4:" + ex);
                            }
                        }



                        // Updating Admitted_Patients_VisitEvent Table  --------------------------------------------------------------------------------------------+
                        
                        string query2 = "UPDATE Admitted_Patients_VisitEvent" +
                            " SET Visited_Doctor_ID = @visited_Doctor_ID, "
                            + "Visite_Time = @visite_Time, "
                            + "P_MedicalEventID = @p_MedicalEventID, "
                            + "Is_VisitedByDoctor = @is_VisitedByDoctor, "
                            + "P_Condition = @p_Condition, "
                            + "Visite_Round = @visite_Round "
                            + "WHERE P_RID = @p_RID;";
                        DateTime currentTime = DateTime.Now;

                        using (SqlCommand command = new SqlCommand(query2, connect))
                        {
                            command.Parameters.AddWithValue("@visited_Doctor_ID", DoctorID);
                            command.Parameters.AddWithValue("@visite_Time", currentTime);
                            command.Parameters.AddWithValue("@p_MedicalEventID", PatientMID);
                            command.Parameters.AddWithValue("@is_VisitedByDoctor", 1);
                            command.Parameters.AddWithValue("@p_Condition", P_Condition);
                            command.Parameters.AddWithValue("@visite_Round", PatientVisitCount + 1);
                            command.Parameters.AddWithValue("@p_RID", PatientRID);


                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                // Query executed successfully
                                Console.WriteLine("UPDATE Admitted_Patients successfully.");
                                Admitted_Patients_VisitEvent = true;
                            }
                            else
                            {
                                // No rows affected (possible validation)
                                Console.WriteLine("Failed to UPDATE Admitted_Patients.");
                            }
                        }

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error:3 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Error3:" + ex);
                }

                if((Admitted_Patients) && (Admitted_Patients_VisitEvent))
                {
                    MessageBox.Show("Success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            
        }
    }
}
