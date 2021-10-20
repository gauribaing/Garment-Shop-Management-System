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
    public partial class employee : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\garment.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public employee()
        {
            InitializeComponent();
        }

        private void employee_Load(object sender, EventArgs e)
        {
            this.Size = new Size(900, 600);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from employee";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            getId();
        }

        public void Display()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from employee";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        public void getId()
        {
            int count = 0;
            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = " select top 1 id  from employee order by id desc";
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

        public void clear()
        {
            nameTextBox.Text = "";
            addressTextBox.Text = "";
            emailTextBox.Text = "";
            contactTextBox.Text = "";
            salaryTextBox.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(nameTextBox.Text == "" || addressTextBox.Text=="" || emailTextBox.Text=="" || contactTextBox.Text=="" || salaryTextBox.Text=="")
            {
                MessageBox.Show("Enter all fields");
            }
            else
            {
                int count = 0;
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select top 1 id  from employee order by id desc";
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
                cmd.CommandText = "insert into employee values('" + count.ToString() + "','" + nameTextBox.Text + "','" + addressTextBox.Text + "','" + emailTextBox.Text + "','" + contactTextBox.Text + "','" + salaryTextBox.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Add Successfully");
                Display();
                getId();
                clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd3 = con.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "update employee set name='" + nameTextBox.Text + "', address='" + addressTextBox.Text + "', email='" + emailTextBox.Text + "',mobile='" + contactTextBox.Text + "', salary='" + salaryTextBox.Text + "' where ID='" + idTextBox.Text + "'";
            cmd3.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("update Successful");
            Display();
            getId();
            clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd3 = con.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "delete employee where ID='" + idTextBox.Text + "'";
            cmd3.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Delete Successful");
            Display();
            getId();
            clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int count = 0;
            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = " select top 1 id  from employee order by id desc";
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

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from employee where name='" + textBox1.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            idTextBox.Text = selectedRow.Cells[0].Value.ToString();
            nameTextBox.Text = selectedRow.Cells[1].Value.ToString();
            addressTextBox.Text = selectedRow.Cells[2].Value.ToString();
            emailTextBox.Text = selectedRow.Cells[3].Value.ToString();
            contactTextBox.Text = selectedRow.Cells[4].Value.ToString();
            salaryTextBox.Text = selectedRow.Cells[5].Value.ToString();
        }

        private void contactTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void salaryTextBox_KeyPress(object sender, KeyPressEventArgs e)
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
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar!=' '))
            {
                e.Handled = true;
                MessageBox.Show("Enter Letters Only");
            }
        }

        private void contactTextBox_Leave(object sender, EventArgs e)
        {
            if(contactTextBox.Text.Length!=10)
            {
                MessageBox.Show("Enter correct contact number");
            }
        }
    }
}
