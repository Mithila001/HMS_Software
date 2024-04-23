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
    public partial class UserLogin : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);
        public UserLogin()
        {
            InitializeComponent();
            comboB_selcePosition.SelectedIndex = 0;
        }

        private void userLogin_btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(useName_tbx.Text) && !string.IsNullOrEmpty(userPassword_tbx.Text))
            {
                try
                {
                    connect.Open();

                    string query = "SELECT UserID, UserPosition, UserName, UserPassword FROM UserLogin";

                    SqlCommand sqlCommand = new SqlCommand(query, connect);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    bool loginSuccessful = false; // Flag to track successful login

                    while (reader.Read())
                    {
                        // Retrieve values from the current record
                        int userID = reader.GetInt32(0);
                        string userPosition = reader.GetString(1);
                        string userName = reader.GetString(2);
                        string userPassword = reader.GetString(3);


                        if (userName == useName_tbx.Text && userPassword == userPassword_tbx.Text && comboB_selcePosition.Text == userPosition)
                        {
                            loginSuccessful = true; // Set flag to true if login is successful
                            break; // Exit the loop since login is successful
                        }

                    }
                    // Check if login was successful
                    if (loginSuccessful)
                    {
                        MessageBox.Show("Login Successful", "Infromation Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else
            {
                MessageBox.Show("Error", "Error Messsage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

            
        }
    }
}
