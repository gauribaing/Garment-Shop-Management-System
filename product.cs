using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Garment_Shop_Management
{
    public partial class product : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\garment.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
      
        public product()
        {
            InitializeComponent();
        }

        private void supplier_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1000, 600);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from product";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            getId();
        }

        //add
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (nameTextBox.Text == "" || brandTextBox.Text == "" || priceTextBox.Text == "" || gstTextBox.Text == "" || quantityTextBox.Text == "")
                {
                    MessageBox.Show("Enter all fields");
                }
                else
                {
                    int count = 0;
                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = " select top 1 id  from product order by id desc";
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    DataTable dt2 = new DataTable();
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                    da2.Fill(dt2);
                    foreach (DataRow dr in dt2.Rows)
                    {
                        count = Convert.ToInt32(dr["id"].ToString());
                    }
                    count = count + 1;
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into product values('" + count.ToString() + "','" + nameTextBox.Text + "','" + brandTextBox.Text + "','" + priceTextBox.Text + "','" + gstTextBox.Text + "','" + quantityTextBox.Text + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Add Successfully");
                    Display();
                    getId();
                    clear();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Display()
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from product";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        //update
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "update product set name='" + nameTextBox.Text + "', brand='" + brandTextBox.Text + "', price='" + priceTextBox.Text + "',gst='" + gstTextBox.Text + "',quantity='" + quantityTextBox.Text + "' where ID='" + idTextBox.Text + "'";
                cmd3.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("update Successful");
                Display();
                getId();
                clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //delete
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "delete product where ID='" + idTextBox.Text + "'";
                cmd3.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Delete Successful");
                Display();
                getId();
                clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //save
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select top 1 id  from product order by id desc";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    count = Convert.ToInt32(dr["id"].ToString());
                }
                count = count + 1;
                idTextBox.Text = count.ToString();
                Display();
                clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void getId()
        {
            try
            {
                int count = 0;
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select top 1 id  from product order by id desc";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    count = Convert.ToInt32(dr["id"].ToString());
                }
                count = count + 1;
                idTextBox.Text = count.ToString();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public void clear()
        {
            try
            {
                nameTextBox.Text = "";
                brandTextBox.Text = "";
                priceTextBox.Text = "";
                gstTextBox.Text = "";
                quantityTextBox.Text = "";
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                idTextBox.Text = selectedRow.Cells[0].Value.ToString();
                nameTextBox.Text = selectedRow.Cells[1].Value.ToString();
                brandTextBox.Text = selectedRow.Cells[2].Value.ToString();
                priceTextBox.Text = selectedRow.Cells[3].Value.ToString();
                gstTextBox.Text = selectedRow.Cells[4].Value.ToString();
                quantityTextBox.Text = selectedRow.Cells[5].Value.ToString();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        //search
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from product where brand='" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void priceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void gstTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void quantityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ' '))
            {
                e.Handled = true;
                MessageBox.Show("Enter Letters Only");
            }
        }

        private void brandTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ' '))
            {
                e.Handled = true;
                MessageBox.Show("Enter Letters Only");
            }
        }

        
    }
}
