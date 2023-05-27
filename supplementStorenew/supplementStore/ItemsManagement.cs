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

namespace supplementStore
{
    public partial class ItemsManagement : Form
    {
        DataBaseAccess access = new DataBaseAccess();
        List<Items> itm = new List<Items>(); //كل الاصناف
        public ItemsManagement()
        {
            InitializeComponent();
        }


        void loadItems()
        {
            dataGridView1.Rows.Clear();
            itm.Clear();
            try
            {


                access.Open();

                using (SqlCommand cmd = new SqlCommand("select * from Items", access.conn))
                {

                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {

                            itm.Add(new Items { id = Convert.ToInt32(read["id"].ToString()), name = read["name"].ToString(), description = read["description"].ToString(), buyPrice = Convert.ToDouble(read["buyPrice"].ToString()), sellPrice = double.Parse(read["sellPrice"].ToString()), quntity = Convert.ToDouble(read["quntity"].ToString()), });


                        }
                    }

                    for (int i = 0; i < itm.Count; i++)
                    {

                        dataGridView1.Rows.Add(new string[] { (i + 1).ToString(), itm[i].name, itm[i].quntity.ToString(), itm[i].buyPrice.ToString(), itm[i].sellPrice.ToString(), itm[i].description.ToString()});

                    }
                    access.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error while getting Items " + ex.ToString());
            }

        }


     



        private void buttonNEw_Click(object sender, EventArgs e)
        {
            NewItem frm = new NewItem();
            frm.Show();
        }

        private void ItemsManagement_Load(object sender, EventArgs e)
        {
            loadItems();
        }
        void search()
        {
            dataGridView1.Rows.Clear();
            itm.Clear();
            try
            {
                 string query = "select * from Items i   where i.name like N'%" + textBoxSearch.Text + "%' ";

                access.Open();

                using (SqlCommand cmd = new SqlCommand(query, access.conn))
                {

                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {


                            itm.Add(new Items { id = Convert.ToInt32(read["id"].ToString()), name = read["name"].ToString(), description = read["description"].ToString(), buyPrice = Convert.ToDouble(read["buyPrice"].ToString()), sellPrice = double.Parse(read["sellPrice"].ToString()), quntity = Convert.ToDouble(read["quntity"].ToString()), });



                        }
                    }
                    access.Close();
                }

                for (int i = 0; i < itm.Count; i++)
                {

                    dataGridView1.Rows.Add(new string[] { (i + 1).ToString(), itm[i].name, itm[i].quntity.ToString(), itm[i].buyPrice.ToString(), itm[i].sellPrice.ToString(), itm[i].description.ToString() });


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error while getting Search Items " + ex.ToString());
            }
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                NewItem frm = new NewItem(itm[dataGridView1.SelectedRows[0].Index]);
                frm.isEdit = true;
                frm.Show();
            }
            else
            {
                MessageBox.Show("choose the item please ");
            }
        }
        void deleteFrmItem(int id)
        {
            try
            {

                string query = "delete from Items where  id ='" + id + "'";

                access.Open();
                SqlCommand cmd = new SqlCommand(query, access.conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("The item is deleted ");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while Deleting " + ex.ToString());
            }
            access.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            int id = itm[dataGridView1.CurrentRow.Index].id;

            if (dataGridView1.SelectedRows.Count == 1)
            {


                DialogResult res = MessageBox.Show("Are you sure to delete it", "warning !", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {

                    deleteFrmItem(id);
                    loadItems();
                }
                else
                {

                }


            }
            else
            {
                MessageBox.Show("Select the item you want to delete");
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ItemsManagement_Activated(object sender, EventArgs e)
        {
            loadItems();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
