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
    public partial class supplier : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\garment.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
      
        public supplier()
        {
            InitializeComponent();
        }

        private void supplier_Load(object sender, EventArgs e)
        {
            this.Size = new Size(900, 600);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from supplier";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            getId();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (nameTextBox.Text == "" || companyTextBox.Text == "" || addressTextBox.Text == "" || emailTextBox.Text == "" || mobileTextBox.Text == "")
                {
                    MessageBox.Show("Enter all fields");
                }
                else
                {
                    int count = 0;
                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = " select top 1 id  from supplier order by id desc";
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
                    cmd.CommandText = "insert into supplier values('" + count.ToString() + "','" + nameTextBox.Text + "','" + companyTextBox.Text + "','" + addressTextBox.Text + "','" + emailTextBox.Text + "','" + mobileTextBox.Text + "')";
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
                cmd.CommandText = "select * from supplier";
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "update supplier set name='" + nameTextBox.Text + "',company='" + companyTextBox.Text + "', address='" + addressTextBox.Text + "', email='" + emailTextBox.Text + "',contact='" + mobileTextBox.Text + "' where ID='" + idTextBox.Text + "'";
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "delete supplier where ID='" + idTextBox.Text + "'";
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select top 1 id  from supplier order by id desc";
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
                cmd1.CommandText = " select top 1 id  from supplier order by id desc";
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
                companyTextBox.Text = "";
                addressTextBox.Text = "";
                emailTextBox.Text = "";
                mobileTextBox.Text = "";
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
                companyTextBox.Text = selectedRow.Cells[2].Value.ToString();
                addressTextBox.Text = selectedRow.Cells[3].Value.ToString();
                emailTextBox.Text = selectedRow.Cells[4].Value.ToString();
                mobileTextBox.Text = selectedRow.Cells[5].Value.ToString();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from supplier where company='" + textBox1.Text + "'";
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

        private void mobileTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void emailTextBox_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (emailTextBox.Text.Length > 0 && emailTextBox.Text.Trim().Length != 0)
            {
                if (!rEmail.IsMatch(emailTextBox.Text.Trim()))
                {
                    MessageBox.Show("check email id");
                    emailTextBox.SelectAll();
                    e.Cancel = true;
                }
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

        private void companyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ' '))
            {
                e.Handled = true;
                MessageBox.Show("Enter Letters Only");
            }
        }

        private void mobileTextBox_Leave(object sender, EventArgs e)
        {
            if (mobileTextBox.Text.Length != 10)
            {
                MessageBox.Show("Enter correct contact number");
            }
        }
    }
}
