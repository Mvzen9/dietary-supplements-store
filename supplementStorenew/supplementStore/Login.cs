using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace supplementStore
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

       

        List<USERS> user = new List<USERS>();
        DataBaseAccess access = new DataBaseAccess();
        void searchItem()
        {



            try
            {


                access.Open();

                using (SqlCommand cmd = new SqlCommand("select * from USERS u  where u.USER_NAME=@name and u.PASSWORD=@pass ", access.conn))
                {
                    cmd.Parameters.AddWithValue("@name", textBoxUserName.Text);
                    cmd.Parameters.AddWithValue("@pass", textBoxPassword.Text);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {

                                this.Hide();
                                MainForm frm = new MainForm();
                                frm.Show();
                            }
                        }
                        else
                        {
                          
                        }

                        
                    
                        // reader closed and disposed up here





                        access.Close();


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Search about id " + ex.ToString());
            }


        }

      

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text)||string.IsNullOrWhiteSpace(textBoxUserName.Text))
            {
                MessageBox.Show("ادخل الاسم و الباسورد");
                return;
            }else
            {
                searchItem();
            }
           
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

 

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonLogin_Click_1(object sender, EventArgs e)
        {
            searchItem();
        }
    }
}
