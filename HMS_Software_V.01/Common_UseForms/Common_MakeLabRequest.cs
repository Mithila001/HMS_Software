using HMS_Software_V._01.Common_UseForms.UserControls;
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
using static HMS_Software_V._01.Doctor_OPD.DoctorCheck_PatientCheck;

namespace HMS_Software_V._01.Common_UseForms
{
    public partial class Common_MakeLabRequest : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private MyDataStoringClass dataImporter; 
        public Common_MakeLabRequest(MyDataStoringClass dataImporter)
        {
            this.dataImporter = dataImporter; // Put it befor MyLoadBasicDetails()  --ChatGPT
            InitializeComponent();
            MyLoadBasicDetails();
        }

        /*private void MyloadResult()
        {
            try
            {
                connect.Open();

                string query = "SELECT LabInvestigationType_ID, LIT_Name FROM Lab_InvestigationType";

                SqlCommand sqlCommand = connect.CreateCommand();
                sqlCommand.CommandText = query;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                labInvestigationSearch_dataGrV.DataSource = dt;

                Console.WriteLine("Lab Investigation Types:");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"ID: {row["LabInvestigationType_ID"]}, Name: {row["LIT_Name"]}");
                }



            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine(ex);

            }
            finally
            {
                connect.Close();
            }
        }*/

        private void MyLoadBasicDetails() //Load basic UI info
        {
            CMLR_doctorName.Text = dataImporter.DoctorName;
            CMLR_DocPosition.Text = dataImporter.DoctorPosition;
            CMLR_DocID.Text = dataImporter.DoctorID.ToString();
            CMLR_Pati_Name.Text = dataImporter.PatientName;
            CMLR_Pati_Age.Text = dataImporter.PatientAge;
            CMLR_Pati_Gender.Text = dataImporter.PatientGender;

            // Adding date and time
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("d MMMM yyyy");

            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("hh:mm tt");

            CMLR_date.Text = formattedDate;
            CMLR_time.Text = timeString;
        }


        // ============================================= Lab Request Type Search =======================================
        private int investigationID = 0;
        private void labInvestigationSearch_dataGrV_CellClick(object sender, DataGridViewCellEventArgs e) //Search bar click result
        {
            
            DataGridViewRow row = this.labInvestigationSearch_dataGrV.Rows[e.RowIndex];
            string investigationIDstring  = row.Cells["Investigation_ID"].Value.ToString();
            LabInvestigations_tbx.Text = row.Cells["InvestigationName"].Value.ToString();
            labInvestigationSearch_dataGrV.Height = 0;

            int investigationID = 0; //Convert investigationId to int

            if (int.TryParse(investigationIDstring, out investigationID))
            {
                // Parsing successful, investigationID now holds the integer value
                Console.WriteLine("Investigation ID: " + investigationID);
            }
            else
            {
                // Parsing failed, handle the error (e.g., display an error message)
                Console.WriteLine("Failed to parse Investigation ID  InvestigationName");
            }



        }

        private void labInvestigationSearch_tbx_TextChanged_1(object sender, EventArgs e) //Search bar Show result
        {
            try
            {
                connect.Open();

                if (LabInvestigations_tbx.TextLength >= 1)
                {
                    string query = "SELECT LabInvestigationType_ID, LIT_Name FROM Lab_InvestigationType ";
                    query += "WHERE LabInvestigationType_ID LIKE @LabInvestigationType_ID OR LIT_Name LIKE @LIT_Name";

                    SqlCommand sqlCommand = connect.CreateCommand();
                    sqlCommand.CommandText = query;

                    sqlCommand.CommandText = query;
                    sqlCommand.Parameters.AddWithValue("@LabInvestigationType_ID", LabInvestigations_tbx.Text + "%");
                    sqlCommand.Parameters.AddWithValue("@LIT_Name", LabInvestigations_tbx.Text + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = sqlCommand;

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        labInvestigationSearch_dataGrV.DataSource = dt;
                        labInvestigationSearch_dataGrV.Height = labInvestigationSearch_dataGrV.Rows.Count * 30;

                    }
                    else
                    {
                        labInvestigationSearch_dataGrV.Height = 0;
                    }

                }
                else if (LabInvestigations_tbx.TextLength <= 0)
                {
                    labInvestigationSearch_dataGrV.Height = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine(ex);

            }
            finally
            {
                connect.Close();
            }

        }


        // ============================================= Specimen Search =======================================
        private int specimenID = 0;
        private void specimenSearch_dataGrV_CellClick(object sender, DataGridViewCellEventArgs e) //Search bar click result
        {
            DataGridViewRow row = this.specimenSearch_dataGrV.Rows[e.RowIndex];
            string speciementIDstring = row.Cells["labSpecimentTypeID"].Value.ToString();
            Speciment_tbx.Text = row.Cells["specimenName"].Value.ToString();
            specimenSearch_dataGrV.Height = 0;

            

            if (int.TryParse(speciementIDstring, out specimenID))
            {
                // Parsing successful, investigationID now holds the integer value
                Console.WriteLine("Investigation ID: " + specimenID);
            }
            else
            {
                // Parsing failed, handle the error (e.g., display an error message)
                Console.WriteLine("Failed to parse Investigation ID  InvestigationName");
            }

        }

        private void Speciment_tbx_TextChanged(object sender, EventArgs e) //Search bar Show result
        {
            try
            {
                connect.Open();

                if (Speciment_tbx.TextLength >= 1)
                {
                    string query = "SELECT Lab_SpeciementType_ID, LST_Name FROM Lab_SpeciementType ";
                    query += "WHERE Lab_SpeciementType_ID LIKE @LabSpeciementTypeID OR LST_Name LIKE @LSTName";

                    SqlCommand sqlCommand = connect.CreateCommand();
                    sqlCommand.CommandText = query;

                    sqlCommand.CommandText = query;
                    sqlCommand.Parameters.AddWithValue("@LabSpeciementTypeID", Speciment_tbx.Text + "%");
                    sqlCommand.Parameters.AddWithValue("@LSTName", Speciment_tbx.Text + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = sqlCommand;

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        specimenSearch_dataGrV.DataSource = dt;
                        specimenSearch_dataGrV.Height = specimenSearch_dataGrV.Rows.Count * 30;

                    }
                    else
                    {
                        specimenSearch_dataGrV.Height = 0;
                    }

                }
                else if (Speciment_tbx.TextLength <= 0)
                {
                    specimenSearch_dataGrV.Height = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine(ex);

            }
            finally
            {
                connect.Close();
            }

        }


        // ============================================= Button Clicked =======================================
        private void CMLR_add_btn_Click(object sender, EventArgs e)
        {
            int getSpecimenID = specimenID;
            int getinvestigationID = investigationID;
            string labInvestigations = LabInvestigations_tbx.Text;
            string specimenName = Speciment_tbx.Text;

            if (!string.IsNullOrEmpty(labInvestigations) && !string.IsNullOrEmpty(specimenName))
            {
                MyLoadUserData(getSpecimenID, getinvestigationID, labInvestigations, specimenName);
                

                Speciment_tbx.Text = "";
                LabInvestigations_tbx.Text = "";
            }
            else
            {
                // LabInvestigations_tbx.Text does not exist or is empty
                MessageBox.Show("Fill blanks");
            }

        }


        // ============================================= Button Clikced FlowLayoutPanel=======================================
        private void MyLoadUserData(int getSpecimenID, int getinvestigationID, string labInvestigations, string specimenName)
        {
            // Generate a number for the LabRequest
            DateTime now = DateTime.Now;
            string generatedSpceimentNumber = specimenName.Substring(0, Math.Min(specimenName.Length, 3)).ToUpper() + " " + now.Month + " " + now.Day + " " + now.ToString("HHmm");

            AddLabRequest addLabRequest = new AddLabRequest();
            addLabRequest.investigationType_lbl.Text = labInvestigations.ToString();
            addLabRequest.specimenName_lbl.Text = specimenName.ToString();
            addLabRequest.requestNumber_lbl.Text = generatedSpceimentNumber.ToString();

            //Store and send data to the UserControl
            addLabRequest.LabInvestigations = getinvestigationID;
            addLabRequest.SpecimenName = getSpecimenID;

            flowLayoutPanel_CMLR_selected.SizeChanged += (sender, e) =>
            {
                addLabRequest.Width = flowLayoutPanel_CMLR_selected.ClientSize.Width - addLabRequest.Margin.Horizontal;
            };
            addLabRequest.Width = flowLayoutPanel_CMLR_selected.ClientSize.Width - addLabRequest.Margin.Horizontal; //Intentional

            flowLayoutPanel_CMLR_selected.Controls.Add(addLabRequest);

            
        }


        // ============================================= Button Clikced Save Data =======================================
        private void CMLR_save_btn_Click(object sender, EventArgs e)
        {
            foreach (Control control in flowLayoutPanel_CMLR_selected.Controls)
            {
                if (control is AddLabRequest addLabRequest)
                {
                    // Get data from the AddLabRequest user control
                    int labInvestigations = addLabRequest.LabInvestigations;
                    int specimenName = addLabRequest.SpecimenName;

                    string investigationName = addLabRequest.investigationType_lbl.Text;
                    string scpecimenName = addLabRequest.specimenName_lbl.Text;
                    string generatedNumber = addLabRequest.requestNumber_lbl.Text;


                    // Insert data into the database
                    MyInsertDataIntoDatabase(investigationName, scpecimenName, generatedNumber);
                }
            }

        }


        // ============================================= Button Clikced -> Save Data and Send to databse =======================================
        private void MyInsertDataIntoDatabase(string investigationName, string scpecimenName, string generatedNumber )
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
