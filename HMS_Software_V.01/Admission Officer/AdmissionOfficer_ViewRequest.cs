﻿using HMS_Software_V._01.Admition_Officer;
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

namespace HMS_Software_V._01.Admission_Officer
{
    public partial class AdmissionOfficer_ViewRequest : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);


        private int AdmissionRequestID;
        public AdmissionOfficer_ViewRequest(int admissionRequestID)
        {
            InitializeComponent();
            this.AdmissionRequestID = admissionRequestID;
            Console.WriteLine("AdmissionRequestID Got reciverd to AdmissionOfficer_ViewRequest form:" + AdmissionRequestID);
            MyLoadData();
            MyDisplayData();
        }

        //Retrived Details from Admit Request Table
        private string P_RegistrationID;
        private string P_NameWithInitials;
        private string P_Age;
        private string P_Gender;
        private int Doctor_ID;
        private string Unit_Type;

        //Retriving Details From Doctor Table
        private string D_NameWithInitials;
        private string D_Position;
        private string D_Specialty;

        //Getting Data from tables
        private void MyLoadData()
        {
            try
            {
                connect.Open();

                //Getting Admit Request Details from the Database
                string query1 = "SELECT P_RegistrationID, P_NameWithInitials, P_Age, P_Gender, Doctor_ID, Unit_Type FROM" +
                    " Patient_Admit WHERE PatientAdmit_ID = @admitID";

                using (SqlCommand cmd = new SqlCommand(query1, connect))
                {
                    cmd.Parameters.AddWithValue("@admitID", AdmissionRequestID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                P_RegistrationID = reader["P_RegistrationID"].ToString();
                                P_NameWithInitials = reader["P_NameWithInitials"].ToString();
                                P_Age = reader["P_Age"].ToString();
                                P_Gender = reader["P_Gender"].ToString();
                                Doctor_ID = Convert.ToInt32(reader["Doctor_ID"]);
                                Unit_Type = reader["Unit_Type"].ToString();

                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Failed to get Admit Request Details.");
                            }
                        }
                    }
                }

                //Getting Doctor Details from the Database
                string query2 = "SELECT D_NameWithInitials, D_Position, D_Specialty FROM" +
                    " Doctor WHERE Doctor_ID = @dcotorID";
                using (SqlCommand cmd2 = new SqlCommand(query2, connect))
                {
                    cmd2.Parameters.AddWithValue("@dcotorID", Doctor_ID);

                    using (SqlDataReader reader = cmd2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                D_NameWithInitials = reader["D_NameWithInitials"].ToString();
                                D_Position = reader["D_Position"].ToString();
                                D_Specialty = reader["D_Specialty"].ToString();

                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Failed to get Doctor Details.");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }

        // Displaying recived Data
        private void MyDisplayData()
        {
            AOVR_doc_Name.Text = D_NameWithInitials;
            AOVR_doc_ID.Text = Doctor_ID.ToString();
            AOVR_doc_Position.Text = D_Position;
            AOVR_doc_Specialty.Text = D_Specialty;

            AOVR_patient_Age.Text = P_Age;
            AOVR_patient_Gender.Text = P_Gender;
            AOVR_patient_ID.Text = P_RegistrationID;
            AOVR_patient_Name.Text = P_NameWithInitials;

            AOVR_unitType.Text = Unit_Type;

            // Adding date and time
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("d MMMM yyyy");

            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("hh:mm tt");

            AOVR_date.Text = formattedDate;
            AOVR_time.Text = timeString;
        }


        //Switch Control managing
        private void AOVR_switch_Ward_CheckedChanged(object sender, EventArgs e)
        {
            if (AOVR_switch_Ward.Checked)
            {
                AOVR_switch_ETU.Checked = false;
                AOVR_switch_PCU.Checked = false;
            }

        }

        private void AOVR_switch_ETU_CheckedChanged(object sender, EventArgs e)
        {
            
            if (AOVR_switch_ETU.Checked)
            {
                AOVR_switch_Ward.Checked = false;
                AOVR_switch_PCU.Checked = false;
            }
        }

        private void AOVR_switch_PTU_CheckedChanged(object sender, EventArgs e)
        {
            if (AOVR_switch_PCU.Checked)
            {
                AOVR_switch_ETU.Checked = false;
                AOVR_switch_Ward.Checked = false;
            }

        }



        private bool isAdmitted = false;
        private string patientStatus ="";
        private void AOVR_admit_btn_Click(object sender, EventArgs e)
        {
            

            if (AOVR_switch_ETU.Checked)
            {
               
                isAdmitted = true;
                patientStatus = "ETU";
            }
            else if(AOVR_switch_Ward.Checked && !string.IsNullOrEmpty(AOVR_ward_tbx.Text))
            {
               
                isAdmitted = true;
                patientStatus = "Ward ("+ AOVR_ward_tbx.Text + ") ETU";
            }
            else if (AOVR_switch_PCU.Checked)
            {
              
                isAdmitted = true;
                patientStatus = "PCU";
            }
            else
            {
                Console.WriteLine("Failed to get in.");
                MessageBox.Show("Select one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Console.WriteLine("isAdmitted:"+isAdmitted);
            Console.WriteLine("patientStatus:" + patientStatus);
            if (isAdmitted && !string.IsNullOrEmpty(patientStatus))
            {
                Console.WriteLine("Data adding process started");
                bool table1 = false;
                bool table2 = false;
                try
                {
                    connect.Open();

                    string updateQuery1 = "UPDATE Patient SET P_Status = @patientStatus WHERE P_RegistrationID = @patienRID";
                    using (SqlCommand updateCommand1 = new SqlCommand(updateQuery1, connect))
                    {
                        updateCommand1.Parameters.AddWithValue("@patientStatus", patientStatus);
                        updateCommand1.Parameters.AddWithValue("@patienRID", P_RegistrationID);

                        int rowsAffected = updateCommand1.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Patient status updated successfully.");
                            table1 = true;


                        }
                        else
                        {
                            Console.WriteLine("Failed to update Patient status.");
                            MessageBox.Show("Failed to update Patient status", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    string updateQuery2 = "UPDATE Patient_Admit SET Is_Admitted = @isAdmitted WHERE PatientAdmit_ID = @admissiontRequestID";
                    using (SqlCommand updateCommand2 = new SqlCommand(updateQuery2, connect))
                    {
                        updateCommand2.Parameters.AddWithValue("@isAdmitted", 1);
                        updateCommand2.Parameters.AddWithValue("@admissiontRequestID", AdmissionRequestID);

                        int rowsAffected = updateCommand2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Patient_Admit status updated successfully.");
                            table2 = true;

                        }
                        else
                        {
                            Console.WriteLine("Failed to update Patient_Admit status.");
                            MessageBox.Show("Failed to update Patient_Admitt status", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if(table1 && table2)
                    {
                        MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        AdmissionOfficer_Dashboard admissionOfficer_Dashboard = new AdmissionOfficer_Dashboard();
                        admissionOfficer_Dashboard.Show();

                        this.Hide();
                        
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void AdmissionOfficer_ViewRequest_FormClosed(object sender, FormClosedEventArgs e)
        {
            AdmissionOfficer_Dashboard admissionOfficer_Dashboard = new AdmissionOfficer_Dashboard();
            admissionOfficer_Dashboard.Show();
            
        }
    }
}
