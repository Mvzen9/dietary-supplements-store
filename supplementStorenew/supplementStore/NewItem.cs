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
    public partial class NewItem : Form
    {
        DataBaseAccess access = new DataBaseAccess();
        Items itm = new Items();
        public bool isEdit=false;
        public NewItem()
        {
            InitializeComponent();
        }
        public NewItem(Items Mitem)
        {
            InitializeComponent();
            itm = Mitem;
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
           Close();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Hide();
            NewItem frm = new NewItem();
            frm.Show();
        }



        void addNew()
        {
            try
            {
                access.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Items ([name] ,[buyPrice] ,[sellPrice],[description],[quntity]) VALUES(@name ,@buyPrice ,@sellPrice,@description,@quntity)", access.conn);

                cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                cmd.Parameters.AddWithValue("@buyPrice", Convert.ToDouble(textBoxBuy.Text));
                cmd.Parameters.AddWithValue("@sellPrice", Convert.ToDouble(textBoxSell.Text));
                cmd.Parameters.AddWithValue("@description", textBoxDecribe.Text);
                cmd.Parameters.AddWithValue("@quntity",Convert.ToDouble(textBoxQnty.Text));
                cmd.ExecuteNonQuery();

                access.Close();
                buttonSave.Enabled = false;
                MessageBox.Show("Added successfully");




            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Inserting " + ex.ToString());
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            decimal outputvalue = 0;
            bool isNumberBuy = false;
            bool isNumberSell = false;

            isNumberBuy = decimal.TryParse(textBoxBuy.Text, out outputvalue);
            isNumberSell = decimal.TryParse(textBoxSell.Text, out outputvalue);

            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("write the name of the item");
                return;
            }
            
          else  if (!isNumberSell)
            {
                MessageBox.Show("invaild input");

                return;
            }
            else if (!isNumberBuy)
            {
                MessageBox.Show("invaild input ");

                return;
            }

            else if(Convert.ToDouble(textBoxSell.Text)<Convert.ToDouble(textBoxBuy.Text))
            {
                MessageBox.Show("The selling price is lower than the purchase price");
                return;

            }
            else if (string.IsNullOrEmpty(textBoxQnty.Text))
            {
                MessageBox.Show("Enter quantity ");
                return;
            }
            else
            {
                if (isEdit)
                {
                    try
                    {
                        access.Open();
                        SqlCommand cmd = new SqlCommand("update  Items  set name=@name ,buyPrice=@buyPrice ,sellPrice=@sellPrice,description=@description,quntity=@quntity where id =@id", access.conn);

                        cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                        cmd.Parameters.AddWithValue("@buyPrice", Convert.ToDouble(textBoxBuy.Text));
                        cmd.Parameters.AddWithValue("@sellPrice", Convert.ToDouble(textBoxSell.Text));
                        cmd.Parameters.AddWithValue("@description", textBoxDecribe.Text);
                        cmd.Parameters.AddWithValue("@quntity", Convert.ToDouble(textBoxQnty.Text));
                        cmd.Parameters.AddWithValue("@id",itm.id);
                        cmd.ExecuteNonQuery();

                        access.Close();
                       
                        MessageBox.Show("edited successfully");




                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error in Inserting " + ex.ToString());
                    }

                }
                else
                {
                    addNew();
                }
              


            }
          
        }

         void editData()
        {

        }
        private void NewItem_Load(object sender, EventArgs e)
        {
            if (isEdit)
            {
                textBoxName.Text = itm.name;
                textBoxQnty.Text = itm.quntity.ToString();
                textBoxDecribe.Text = itm.description.ToString();
                textBoxSell.Text = itm.sellPrice.ToString();
                textBoxBuy.Text = itm.buyPrice.ToString();
            }
            else
            {

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
