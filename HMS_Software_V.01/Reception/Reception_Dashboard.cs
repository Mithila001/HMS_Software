﻿using HMS_Software_V._01.Reception.UserControls;
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

namespace HMS_Software_V._01.Reception
{
    public partial class Reception_Dashboard : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);
        public Reception_Dashboard()
        {
            InitializeComponent();
            MyLoadUserData();
        }

        private void MyLoadUserData()
        {
            try
            {
                // ----------------------------- Left FlowLayoutPanel ------------------------------------
                connect.Open();
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                

                string query = "SELECT cc.ClinicName AS ClinicName, d.D_NameWithInitials AS DoctorName, ce.CE_HallNumber AS HallNumber, ce.CE_StartTime AS StartTime, ce.CE_EndTime AS EndTime, ce.CE_Date as ClinicDate," +
               "CONVERT(varchar(15), ce.CE_StartTime, 100) + ' - ' + CONVERT(varchar(15), ce.CE_EndTime, 100) AS TimeRange " +
               "FROM ClinicEvents ce " +
               "JOIN Doctor d ON ce.Doctor_ID = d.Doctor_ID " +
               "JOIN ClinicCategories cc ON ce.Clinic_ID = cc.Clinic_ID;";

                using (SqlCommand command = new SqlCommand(query, connect))
                using (SqlDataReader reader1 = command.ExecuteReader())

                while (reader1.Read())
                {
                    DateTime clinicDate = Convert.ToDateTime(reader1["ClinicDate"]);
                    if (clinicDate.Date != DateTime.Today)
                    {
                        // If ClinicDate is not today, skip this record and move to the next one
                        continue;
                    }

                    Recep_D_TodayClinics recep_D_TodayClinics = new Recep_D_TodayClinics();

                    recep_D_TodayClinics.RDTC_ClincType.Text = reader1["ClinicName"].ToString();
                    recep_D_TodayClinics.RDTC_DoctorName.Text = "Dr. " + reader1["DoctorName"].ToString();
                    recep_D_TodayClinics.RDTC_hallNumber.Text = reader1["HallNumber"].ToString();
                    recep_D_TodayClinics.RDTC_time.Text = reader1["TimeRange"].ToString();


                    TimeSpan endTime = (TimeSpan)reader1["EndTime"];
                    
                    if (currentTime > endTime)
                    {
                        recep_D_TodayClinics.RDTC_availability.Text = "  Ended  ";
                        recep_D_TodayClinics.RDTC_availability.BackColor = Color.FromArgb(250, 192, 162);
                    }
                    else
                    {
                        recep_D_TodayClinics.RDTC_availability.Text = "Available";
                    }

                    // Adjust the width of the user control to match the width of the parent container
                    flowLayoutPanel_RD_left.SizeChanged += (sender, e) => 
                    {
                        
                        recep_D_TodayClinics.Width = flowLayoutPanel_RD_left.ClientSize.Width - recep_D_TodayClinics.Margin.Horizontal;
                    };

                    flowLayoutPanel_RD_left.Controls.Add(recep_D_TodayClinics);
                }
                /*reader1.Close();*/

                // ----------------------------- Left FlowLayoutPanel ------------------------------------

                string query2 = "SELECT P_NameWithIinitials, P_Age, P_RegistrationID, P_RegisteredDate, P_RegisteredTime, P_Status, " +
                                    "CONVERT(varchar(15), P_RegisteredTime, 100) AS TimeRange " +
                                    "FROM Patient " +
                                    "WHERE P_RegisteredDate >= DATEADD(day, -3, CAST(GETDATE() AS DATE)) " +
                                    "ORDER BY P_RegisteredDate DESC, P_RegisteredTime DESC";


                using (SqlCommand command2 = new SqlCommand(query2, connect))
                using (SqlDataReader reader2 = command2.ExecuteReader())

                while (reader2.Read())
                {
                    Recep_ImergencyPatients recep_ImergencyPatients = new Recep_ImergencyPatients();
                    recep_ImergencyPatients.RIP_patientNameAge_lbl.Text = reader2["P_NameWithIinitials"].ToString() + " (" + reader2["P_Age"].ToString() + ")";
                    recep_ImergencyPatients.RIP_patienRegistD_lbl.Text = reader2["P_RegistrationID"].ToString();
                    recep_ImergencyPatients.RIP_patienAdmitDate_lbl.Text = ((DateTime)reader2["P_RegisteredDate"]).ToString("MM/dd/yyyy");
                    recep_ImergencyPatients.RIP_patienAdmitTime_lbl.Text = reader2["TimeRange"].ToString();
                    /*recep_ImergencyPatients.RIP_patienStatus_lbl.Text = reader2["P_Status"].ToString();*/

                    if (reader2["P_Status"].ToString() == "OPD")
                    {
                            recep_ImergencyPatients.RIP_patienStatus_lbl.BackColor = Color.FromArgb(25, 217, 255);
                            recep_ImergencyPatients.RIP_patienStatus_lbl.Text = "OPD";
                    }
                    else if(reader2["P_Status"].ToString() == "Ward")
                    {
                            recep_ImergencyPatients.RIP_patienStatus_lbl.BackColor = Color.FromArgb(237, 238, 168);
                            recep_ImergencyPatients.RIP_patienStatus_lbl.Text = "Ward";
                    }
                    else if(reader2["P_Status"].ToString() == "Ward ETU")
                    {
                        recep_ImergencyPatients.RIP_patienStatus_lbl.BackColor = Color.FromArgb(250, 192, 162);
                        recep_ImergencyPatients.RIP_patienStatus_lbl.Text = "ETU";
                    }
                    else if (reader2["P_Status"].ToString() == "Ward PCU")
                    {
                        recep_ImergencyPatients.RIP_patienStatus_lbl.BackColor = Color.FromArgb(250, 192, 162);
                        recep_ImergencyPatients.RIP_patienStatus_lbl.Text = "PCU";
                    }

                        // Adjust the width of the user control to match the width of the parent container
                        flowLayoutPanel_RD_mid.SizeChanged += (sender, e) =>
                    {
                        recep_ImergencyPatients.Width = flowLayoutPanel_RD_mid.ClientSize.Width - recep_ImergencyPatients.Margin.Horizontal;
                    };

                    flowLayoutPanel_RD_mid.Controls.Add(recep_ImergencyPatients);

                }

                // ----------------------------- Right FlowLayoutPanel ------------------------------------


                string query3 = "SELECT PD_P_RegistrationID, PD_P_NameWithIinitials, PD_Ward_No " +
                                    "FROM PatientDischarge";
                using (SqlCommand command3 = new SqlCommand(query3, connect))
                using (SqlDataReader reader3 = command3.ExecuteReader())

                    while (reader3.Read())
                    {
                        Recep_PatientDischarge recep_PatientDischarge = new Recep_PatientDischarge();

                        recep_PatientDischarge.RPD_patienName_lbl.Text = reader3["PD_P_NameWithIinitials"].ToString();
                        recep_PatientDischarge.RPD_registrationNo_lbl.Text = reader3["PD_P_RegistrationID"].ToString();
                        recep_PatientDischarge.RPD_warNo_lbl.Text = reader3["PD_Ward_No"].ToString();

                        // Adjust the width of the user control to match the width of the parent container
                        flowLayoutPanel_RD_right.SizeChanged += (sender, e) =>
                        {
                            recep_PatientDischarge.Width = flowLayoutPanel_RD_right.ClientSize.Width - recep_PatientDischarge.Margin.Horizontal;
                        };

                        flowLayoutPanel_RD_right.Controls.Add(recep_PatientDischarge);

                    }
                /*reader2.Close();*/


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }
            finally
            {
                connect.Close();
            }
        }

        private void R_register_btn_Click(object sender, EventArgs e)
        {
            Reception_PatientRegistration reception_PatientRegistration = new Reception_PatientRegistration();
            reception_PatientRegistration.Show();
            this.Hide();
        }

        private void R_search_btn_Click(object sender, EventArgs e)
        {
            Reception_PatientSearch reception_PatientSearch = new Reception_PatientSearch();
            reception_PatientSearch.Show();
            this.Hide();
        }
    }
}