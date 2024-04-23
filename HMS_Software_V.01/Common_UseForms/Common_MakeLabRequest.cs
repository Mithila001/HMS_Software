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

namespace HMS_Software_V._01.Common_UseForms
{
    public partial class Common_MakeLabRequest : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);
        public Common_MakeLabRequest()
        {
            InitializeComponent();
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

        private void Common_MakeLabRequest_Load(object sender, EventArgs e)
        {
            /*MyloadResult();*/
        }


        // ============================================= Lab Request Type Search =======================================

        private int investigationID = 0;
        private void labInvestigationSearch_dataGrV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.labInvestigationSearch_dataGrV.Rows[e.RowIndex];
            string investigationIDstring  = row.Cells["Investigation_ID"].Value.ToString();
            LabInvestigations_tbx.Text = row.Cells["InvestigationName"].Value.ToString();
            labInvestigationSearch_dataGrV.Height = 0;

            int investigationID = 0; 

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

        private void labInvestigationSearch_tbx_TextChanged_1(object sender, EventArgs e)
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
        private void specimenSearch_dataGrV_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void Speciment_tbx_TextChanged(object sender, EventArgs e)
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

        private void CMLR_add_btn_Click(object sender, EventArgs e)
        {
            int getSpecimenID = specimenID;
            int getinvestigationID = investigationID;

            if (!string.IsNullOrEmpty(LabInvestigations_tbx.Text) && !string.IsNullOrEmpty(Speciment_tbx.Text))
            {
                // LabInvestigations_tbx.Text exists and is not empty
                // Your code here
            }
            else
            {
                // LabInvestigations_tbx.Text does not exist or is empty
                MessageBox.Show("Fill blanks");
            }

        }
    }
}
