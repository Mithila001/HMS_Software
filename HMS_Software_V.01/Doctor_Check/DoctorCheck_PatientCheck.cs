﻿using HMS_Software_V._01.Common_UseForms;
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

           

            MyGetPatientDetails();
            MyStartPatientMedicalEvent();


        }
        // =========================== Data Transporter =============================
        public class MyDataStoringClass
        {
            public int DoctorID { get; set; }
            public string DoctorName { get; set; }
            public string DoctorPosition { get; set; }
            public string PatientRID { get; set; }
            public string PatientName { get; set; }
            public string PatientAge { get; set; }
            public string PatientGender { get; set; }
            public string PatientMedicalEventID { get; set; }

        }

        private string PatientMedicalEventID;


        private void MyStartPatientMedicalEvent() // Create PatientMedical_Event record

        {

            // Adding date and time
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("d MMMM yyyy");

            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("hh:mm tt");

            DOPDPC_date.Text = formattedDate;
            DOPDPC_time.Text = timeString;

            try
            {
                connect.Open();

                string query = "INSERT INTO PatientMedical_Event (PatientRegistration_ID, Doctor_ID, PMRE_Location, PMRE_Date, PMRE_Time)"
                    + "VALUES (@patietnRegistrationId, @doctorId, @location, @date, @time)";

                using (SqlCommand command = new SqlCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@patietnRegistrationId", patientRID);
                    command.Parameters.AddWithValue("@doctorId", userID);
                    command.Parameters.AddWithValue("@location", "OPD");
                    command.Parameters.AddWithValue("@date", currentDate);
                    command.Parameters.AddWithValue("@time", timeString);
                    // Need to add PatientExaminatioNote


                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        string getIdQuery = "SELECT PatientMedicalEvent_ID FROM PatientMedical_Event WHERE" +
                            " PMRE_Date = @date AND PMRE_Time = @time AND Doctor_ID = @doctorId";

                        using (SqlCommand getIdCommand = new SqlCommand(getIdQuery, connect))
                        {
                            getIdCommand.Parameters.AddWithValue("@date", currentDate);
                            getIdCommand.Parameters.AddWithValue("@time", timeString);
                            getIdCommand.Parameters.AddWithValue("@doctorId", userID);

                            // Executing the query
                            object result = getIdCommand.ExecuteScalar();
                            if (result != null)
                            {
                                PatientMedicalEventID = result.ToString();
                                Console.WriteLine($"PatientMedical_Event Record with ID {PatientMedicalEventID} found successfully.");
                            }
                            else
                            {
                                Console.WriteLine("PatientMedical_Event Record not found for the given criteria.");
                            }
                        }
                    
                        Console.WriteLine($"PatientMedical_Event Record with ID {PatientMedicalEventID} inserted successfully.");
                     

                    }
                    else
                    {
                        Console.WriteLine("Failed to insert PatientMedical_Event record.");
                        
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine("Error:  "+ex);
            }
            finally
            {
                connect.Close();
            }


        }

        private void MyGetPatientDetails()
        {
            DateTime currentDate = DateTime.Today;
            DateTime currentTime = DateTime.Now;

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

                    MyDataStoringClass transport = new MyDataStoringClass();
                    transport.PatientName = reader2.GetString(0);
                    transport.PatientAge = reader2.GetString(1);
                    transport.PatientGender = reader2.GetString(2);


                }
                else
                {
                    // Handle case when no matching record is found
                    MessageBox.Show("Patien Registration ID not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine("DCPC;; " + ex);
            }
            finally
            {
                connect.Close();
            }

        }

        private void DOPDPC_addLabRequest_Click(object sender, EventArgs e)
        {
            MyDataStoringClass dataTranspoter = new MyDataStoringClass();

            dataTranspoter.DoctorID = userID;
            dataTranspoter.DoctorName = doctorName;
            dataTranspoter.DoctorPosition = doctorPosition;
            dataTranspoter.PatientRID = patientRID;

            //Ensure that patient details are retrieved before populating these properties
            dataTranspoter.PatientName = DOPDPC_patietName_lbl.Text;
            dataTranspoter.PatientAge = DOPDPC_patietage_lbl.Text;
            dataTranspoter.PatientGender = DOPDPC_patietGender_lbl.Text;
            dataTranspoter.PatientMedicalEventID = PatientMedicalEventID;


            Console.WriteLine($"DoctorID: {dataTranspoter.DoctorID}");
            Console.WriteLine($"DoctorName: {dataTranspoter.DoctorName}");
            Console.WriteLine($"DoctorPosition: {dataTranspoter.DoctorPosition}");
            Console.WriteLine($"PatientRID: {dataTranspoter.PatientRID}");
            Console.WriteLine($"PatientName: {dataTranspoter.PatientName}");
            Console.WriteLine($"PatientAge: {dataTranspoter.PatientAge}");
            Console.WriteLine($"PatientGender: {dataTranspoter.PatientGender}");
            Console.WriteLine($"PatientMedicalEventID: {dataTranspoter.PatientMedicalEventID}");



            Common_MakeLabRequest common_MakeLabRequest = new Common_MakeLabRequest(dataTranspoter);
            common_MakeLabRequest.Show();
            this.Hide();
        }
    }
}
