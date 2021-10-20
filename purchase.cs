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
    public partial class purchase : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\garment.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        DataTable dt = new DataTable();
        public purchase()
        {
            InitializeComponent();
        }

        private void purchase_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1020, 600);
            Clear();
            sclear();
            getId();
            
            getSupplierName();

            dt.Clear();
            dt.Columns.Add("pid");
            dt.Columns.Add("sname");
            dt.Columns.Add("brand");
            dt.Columns.Add("pname");
            dt.Columns.Add("quantity");
            dt.Columns.Add("price");
            dt.Columns.Add("amount");
            dt.Columns.Add("gst");
        }

        public void Clear()
        {
            try
            {
                pidTextBox.Text = "";
                comboBox4.Text = "";
                contactTextBox.Text = "";
                totalTextBox.Text = "0";
                gstTextBox.Text = "0";
                discountTextBox.Text = "0";
                grandtotalTextBox.Text = "0";
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public void sclear()
        {
            try
            {
                textBox1.Text = "";
                quantityTextBox.Text = "";
                priceTextBox.Text = "";
                amountTextBox.Text = "0";
                gstTextBox.Text = "0";
                comboBox2.Text = "";
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
                cmd1.CommandText = " select top 1 pid  from purchase order by pid desc";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    count = Convert.ToInt32(dr["pid"].ToString());
                }
                count = count + 1;
                pidTextBox.Text = count.ToString();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public void getSupplierName()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select * from supplier";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    comboBox4.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from supplier where name='" + comboBox4.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    contactTextBox.Text = dr["contact"].ToString();
                    textBox1.Text = dr["company"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from product where brand='" + textBox1.Text + "' and name='" + comboBox2.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    priceTextBox.Text = dr["price"].ToString();
                    gstTextBox1.Text = dr["gst"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || comboBox2.Text == "" || priceTextBox.Text == "" || quantityTextBox.Text == "" || amountTextBox.Text == "")
                {
                    MessageBox.Show("Enter all fields");
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["pid"] = pidTextBox.Text;
                    dr["sname"] = comboBox4.Text;
                    dr["quantity"] = quantityTextBox.Text;
                    dr["price"] = priceTextBox.Text;
                    dr["amount"] = amountTextBox.Text;
                    dr["gst"] = gstTextBox1.Text;
                    dr["brand"] = textBox1.Text;
                    dr["pname"] = comboBox2.Text;

                    dt.Rows.Add(dr);
                    dataGridView1.DataSource = dt;
                    sclear();

                    int total = 0;
                    double totalgst = 0;
                    foreach (DataRow dr2 in dt.Rows)
                    {
                        total += Convert.ToInt32(dr2["amount"]);
                        totalgst += Convert.ToDouble(dr2["gst"]);
                    }
                    totalTextBox.Text = total.ToString();
                    gstTextBox.Text = totalgst.ToString();

                    try
                    {
                        grandtotalTextBox.Text = Convert.ToString(Convert.ToInt32(totalTextBox.Text) + Convert.ToDouble(gstTextBox.Text));
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
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
                dt.Rows.RemoveAt(Convert.ToInt32(dataGridView1.CurrentCell.RowIndex.ToString()));

                int total = 0;
                double totalgst = 0;
                foreach (DataRow dr2 in dt.Rows)
                {
                    total += Convert.ToInt32(dr2["amount"]);
                    totalgst += Convert.ToDouble(dr2["gst"]);
                }
                totalTextBox.Text = total.ToString();
                gstTextBox.Text = totalgst.ToString();

                try
                {
                    grandtotalTextBox.Text = Convert.ToString(Convert.ToInt32(totalTextBox.Text) + Convert.ToDouble(gstTextBox.Text));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("no item to delete");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox4.Text == "" || contactTextBox.Text == "" || grandtotalTextBox.Text == "")
                {
                    MessageBox.Show("Enter all fields");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into purchase values('" + pidTextBox.Text + "','" + dateDateTimePicker.Value.ToString("dd/MM/yyy") + "','" + comboBox4.Text + "','" + contactTextBox.Text + "','" + totalTextBox.Text + "','" + gstTextBox.Text + "','" + discountTextBox.Text + "','" + grandtotalTextBox.Text + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        int qty = 0;
                        String pname = null;

                        con.Open();
                        SqlCommand cmd1 = con.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "insert into pdetail values('" + dr["pid"] + "','" + dr["brand"] + "','" + dr["pname"] + "','" + dr["quantity"] + "','" + dr["price"] + "','" + dr["amount"] + "','" + dr["gst"] + "')"; 
                        cmd1.ExecuteNonQuery();
                        con.Close();


                        qty = Convert.ToInt32(dr["quantity"].ToString());
                        pname = dr["pname"].ToString();

                        con.Open();
                        SqlCommand cmd2 = con.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "update product set quantity=  quantity+'" + qty + "'where name='" + pname.ToString() + "'";
                        cmd2.ExecuteNonQuery();
                        con.Close();


                    }
                    MessageBox.Show("Data Saved");
                    dt.Clear();
                    Clear();
                    sclear();
                    getId();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var myForm = new pdetail();
                myForm.Show();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void contactTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void contactTextBox_Leave(object sender, EventArgs e)
        {
            if (contactTextBox.Text.Length != 10)
            {
                MessageBox.Show("Enter correct contact number");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ' '))
            {
                e.Handled = true;
                MessageBox.Show("Enter Letters Only");
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ' '))
            {
                e.Handled = true;
                MessageBox.Show("Enter Letters Only");
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

        private void quantityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void amountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void gstTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void totalTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void discountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void grandtotalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void quantityTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                amountTextBox.Text = Convert.ToString(Convert.ToInt32(priceTextBox.Text) * Convert.ToInt32(quantityTextBox.Text));
        
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void discountTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                grandtotalTextBox.Text = Convert.ToString(Convert.ToDouble(gstTextBox.Text) + (Convert.ToDouble(totalTextBox.Text) - (Convert.ToDouble(totalTextBox.Text)*Convert.ToDouble(discountTextBox.Text)/100)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void amountTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                gstTextBox1.Text = Convert.ToString(Convert.ToInt32(amountTextBox.Text) * Convert.ToInt32(gstTextBox1.Text) / 100);
            }
            catch (Exception)
            {

                throw;
            }
        }

       

        
        
    }
}
