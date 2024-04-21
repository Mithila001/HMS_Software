using HMS_Software_V._01.Reception.UserControls;
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
                connect.Open();
                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                string query = "SELECT cc.ClinicName AS ClinicName, d.D_NameWithInitials AS DoctorName, ce.CE_HallNumber AS HallNumber, ce.CE_StartTime AS StartTime, ce.CE_EndTime AS EndTime, ce.CE_Date as ClinicDate," +
               "CONVERT(varchar(15), ce.CE_StartTime, 100) + ' - ' + CONVERT(varchar(15), ce.CE_EndTime, 100) AS TimeRange " +
               "FROM ClinicEvents ce " +
               "JOIN Doctor d ON ce.Doctor_ID = d.Doctor_ID " +
               "JOIN ClinicCategories cc ON ce.Clinic_ID = cc.Clinic_ID;";

                SqlCommand command = new SqlCommand(query, connect);
                SqlDataReader reader1 = command.ExecuteReader();

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
                        recep_D_TodayClinics.RDTC_availability.Text = "Ended";
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
                reader1.Close();

            }
            catch(Exception ex)
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
    }
}
