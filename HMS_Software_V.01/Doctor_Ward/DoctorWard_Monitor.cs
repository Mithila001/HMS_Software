﻿using System;
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

namespace HMS_Software_V._01.Doctor_Ward
{
    public partial class DoctorWard_Monitor : Form
    {
        public Form DoctorPatientCheckWardFromReferece { get; set; }

        string DoctorName;
        string WardName;
        string PatientMID_string;
        int PatientMID;
        int DoctorID;
        string P_RID;

        public DoctorWard_Monitor(string DoctorName, string WardName, string P_RID, string patientMID,int DoctorID)
        {
            InitializeComponent();
            this.DoctorName = DoctorName;
            this.WardName = WardName;
            this.PatientMID_string = patientMID;

            PatientMID = int.Parse(this.PatientMID_string);

            this.DoctorID = DoctorID;
            this.P_RID = P_RID;

            // Adding date and time
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("d MMMM yyyy");

            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("hh:mm tt");

            DWM_Date.Text = formattedDate;
            DWM_Time.Text = timeString;

            DWM_D_Name.Text = DoctorName;
            DWM_WardName.Text = WardName;


            Console.WriteLine("DoctorName: " + DoctorName);
            Console.WriteLine("WardName: " + WardName);
            Console.WriteLine("PatientMID_string: " + PatientMID_string);
            Console.WriteLine("PatientMID: " + PatientMID);
            Console.WriteLine("DoctorID: " + DoctorID);
            Console.WriteLine("P_RID: " + P_RID);








        }


        private int MonitorRequest_ID;
        private void MyAssigneData()
        {
           
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();


                    //Assigne Data to the table
                    string query = "INSERT INTO Monitor_Request (MR_DoctorID, MR_Info, MR_P_MEID)"
                    + " VALUES (@MR_DoctorID, @MR_Info, @MR_P_MEID)";

                    Console.WriteLine("Creating a Monitor_Request record");

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@MR_DoctorID", DoctorID);
                        command.Parameters.AddWithValue("@MR_Info", DWM_richTextBox1.Text);
                        command.Parameters.AddWithValue("@MR_P_MEID", PatientMID);


                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Query executed successfully
                            Console.WriteLine("INSERT INTO PatientMID successfully.");
                        }
                        else
                        {
                            // No rows affected (possible validation)
                            Console.WriteLine("Failed to INSERT INTO Monitor_Request.");
                        }
                    }


                    //Retriving Monitor_Request from the table

                    string query1 = "SELECT MonitorRequest_ID FROM Monitor_Request WHERE MR_P_MEID = @MR_P_MEID";

                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        command.Parameters.AddWithValue("@MR_P_MEID", PatientMID);
                        
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                MonitorRequest_ID = Convert.ToInt32(reader["MonitorRequest_ID"]);

                            }
                            else
                            {
                                MessageBox.Show("No matching MR_P_MEID record found.");
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:11 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error11:" + ex);
                        }
                    }

                    //Updateing Patient Medical Event

                    string query3 = "UPDATE PatientMedical_Event" +
                            " SET PatinetMonitortRequest_ID = @PatinetMonitortRequest_ID "
                            + "WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID;";

                    using (SqlCommand command = new SqlCommand(query3, connect))
                    {
                        command.Parameters.AddWithValue("@PatientMedicalEvent_ID", PatientMID);
                        command.Parameters.AddWithValue("@PatinetMonitortRequest_ID", MonitorRequest_ID);



                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Query executed successfully 
                            Console.WriteLine("UPDATE Admitted_Patients successfully.");
                            MessageBox.Show("Success! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Close();

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
                MessageBox.Show("Error:22 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error22:" + ex);
            }
            

        }


        string MonitorDetails;
        private void DWM_Save_Click(object sender, EventArgs e)
        {
            MonitorDetails = DWM_richTextBox1.Text;
            Console.WriteLine("MonitorDetails: "+ MonitorDetails);
            if (string.IsNullOrEmpty(MonitorDetails))
            {
                MessageBox.Show("Add Monitor Details ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MyAssigneData();
            }

        }
    }
}
