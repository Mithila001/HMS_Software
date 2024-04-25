using HMS_Software_V._01.Common_UseForms.UserControls;
using HMS_Software_V._01.Doctor_OPD.UserControls;
using HMS_Software_V._01.Reception;
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

namespace HMS_Software_V._01.Doctor_OPD
{
    public partial class DoctorCheck_AddClinic : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        /*private int _clinicId;*/
        public DoctorCheck_AddClinic()
        {
            InitializeComponent();
            LoadUserData();

            this.SizeChanged += MyRe_Appointments_SizeChanged; //To fix clinicTypeAvailableDates Usercontrol scaling issues from small window to Full screen

            /*DoctorOPD_clincType doctorOPD_ClincType = new DoctorOPD_clincType();
            doctorOPD_ClincType.ClinicClicked += (sender, clinicId) =>
            {
                _clinicId = clinicId;

            };*/

        }
        private void MyRe_Appointments_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control control in flowLP_DOPDA_left.Controls)
            {
                if (control is DoctorCheck_clincType doctorOPD_ClincType)
                {
                    doctorOPD_ClincType.Width = flowLP_DOPDA_left.ClientSize.Width - doctorOPD_ClincType.Margin.Horizontal;
                }
            }
        }

        //================================= Show Clinic Types UserControls =================================
        private void LoadUserData()
        {
            try
            {
                connect.Open();
                DateTime today = DateTime.Today;
                
                string query = "SELECT CT_Name, CT_WardNo, ClincType_ID "
                    + "FROM ClincType";

                SqlCommand command = new SqlCommand(query, connect);
                SqlDataReader reader1 = command.ExecuteReader();

                // Loop through the records retrieved from the database
                while (reader1.Read())
                {
                    string getclincName = reader1["CT_Name"].ToString();
                    string GclinicID = reader1["ClincType_ID"].ToString();

                    int getclinicID = 0;

                    if (int.TryParse(GclinicID, out getclinicID))  // Convert to int
                    {
                        
                        Console.WriteLine("Investigation ID: " + getclinicID);
                    }

                    DoctorCheck_clincType doctorOPD_ClincType = new DoctorCheck_clincType();

                    doctorOPD_ClincType.DOPDA_ClincType_lbl.Text = reader1["CT_Name"].ToString();
                    


                    // Set the ID as the Tag property
                   /* doctorOPD_ClincType.Tag = reader1["ClincType_ID"];*/

                    doctorOPD_ClincType.DOPDA_add_btn.Click += (sender, clinicID) =>
                    {
                        LoadUserData2(sender, getclinicID, getclincName);  // Call LoadUserData2() method when ClinicClicked event is raised

                        /* flowLP_DOPDA_left.Controls.Remove(doctorOPD_ClincType);*/
                        //Auto remove when add button clicked. Curently off becouse cant risk messing up the flow if we try to add that usercontrol again from LoadUserData2(); method
                    };


                    flowLP_DOPDA_left.SizeChanged += (sender, e) =>
                    {
                        // Adjust the width of the user control to match the width of the parent container
                        doctorOPD_ClincType.Width = flowLP_DOPDA_left.ClientSize.Width - doctorOPD_ClincType.Margin.Horizontal;
                    };

                    flowLP_DOPDA_left.Controls.Add(doctorOPD_ClincType);

                }
                reader1.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("11111111111111111111111111111  " + ex);
            }
            finally
            {
                connect.Close();
            }
        }

        private void LoadUserData2(object sendersenderObj, int clinicID, string clincName)
        {
            int ClinicID = clinicID;


            DoctorCheck_showAddedClinics doctorOPD_ShowAddedClinics = new DoctorCheck_showAddedClinics();
            doctorOPD_ShowAddedClinics.DOPDA_ClincType_lbl.Text = clincName;


            doctorOPD_ShowAddedClinics.DOPDA_delete_btn.Click += (sender, clinicID2 ) => //no idea about this  clinicID2
            {
                flowLP_DOPDA_Right.Controls.Remove(doctorOPD_ShowAddedClinics);

            };

            flowLP_DOPDA_Right.SizeChanged += (sender, e) =>
            {
                doctorOPD_ShowAddedClinics.Width = flowLP_DOPDA_Right.ClientSize.Width - doctorOPD_ShowAddedClinics.Margin.Horizontal;
            };
            doctorOPD_ShowAddedClinics.Width = flowLP_DOPDA_Right.ClientSize.Width - doctorOPD_ShowAddedClinics.Margin.Horizontal; //Intentional

            flowLP_DOPDA_Right.Controls.Add(doctorOPD_ShowAddedClinics);

        }


        // ============================================= Button Clikced Save Data =======================================


        // ************************** need to change values 
        private void DOPDA_save_btn_Click(object sender, EventArgs e)
        {
            foreach (Control control in flowLP_DOPDA_Right.Controls)
            {
                if (control is AddLabRequest addLabRequest)
                {
                    // Get data from the AddLabRequest user control
                    int labInvestigations = addLabRequest.LabInvestigationsID;
                    int specimenName = addLabRequest.SpecimenNameID;

                    string investigationName = addLabRequest.investigationType_lbl.Text;
                    string scpecimenName = addLabRequest.specimenName_lbl.Text;
                    string generatedNumber = addLabRequest.requestNumber_lbl.Text;


                    // Insert data into the database
                    InsertDataIntoDatabase(investigationName, scpecimenName, generatedNumber);
                }
            }
        }

        // ============================================= Button Clikced Save Data and Send to databse =======================================
        private void InsertDataIntoDatabase(string investigationName, string scpecimenName, string generatedNumber)
        {
            try
            {

                connect.Open();
                string query = "INSERT INTO Lab_Request (LR_Specimen, LR_InvestigatonType, LR_SpceimenNumber)" +
                    " VALUES (@specimen, @investigationType, @specimentNumber)";
                SqlCommand cmd = new SqlCommand(query, connect);

                cmd.Parameters.AddWithValue("@specimen", scpecimenName);
                cmd.Parameters.AddWithValue("@investigationType", investigationName);
                cmd.Parameters.AddWithValue("@specimentNumber", generatedNumber);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


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
    }
}
