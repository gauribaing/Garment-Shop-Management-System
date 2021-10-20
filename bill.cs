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
    public partial class bill : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\garment.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        DataTable dt = new DataTable();
        public bill()
        {
            InitializeComponent();
        }

        private void bill_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1010, 600);
            Clear();
            sclear();
            getId();
            getBrandName();

            dt.Clear();
            dt.Columns.Add("sid");
            dt.Columns.Add("pname");
            dt.Columns.Add("brand");
            dt.Columns.Add("quantity");
            dt.Columns.Add("price");
            dt.Columns.Add("amount");
            dt.Columns.Add("gst");
        }

        public void Clear()
        {
            sidTextBox.Text = "";
            cnameTextBox.Text = "";
            contactTextBox.Text = "";
            totalTextBox.Text = "0";
            gstTextBox.Text = "0";
            discountTextBox.Text = "0";
            grandtotalTextBox.Text = "0";
        }

        public void sclear()
        {
            comboBox1.Text = "";
            quantityTextBox.Text = "";
            priceTextBox.Text = "";
            amountTextBox.Text = "0";
            gstTextBox1.Text = "0";
            comboBox2.Text = "";
        }

        public void getId()
        {
            try
            {
                int count = 0;
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select top 1 sid  from sales order by sid desc";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    count = Convert.ToInt32(dr["sid"].ToString());
                }
                count = count + 1;
                sidTextBox.Text = count.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void getBrandName()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select * from brand";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    comboBox1.Items.Add(dr["brand"].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from product where brand='" + comboBox1.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
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
                String remaining;
                remaining = textBox1.Text;
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from product where brand='" + comboBox1.Text + "' and name='" + comboBox2.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    priceTextBox.Text = dr["price"].ToString();
                    gstTextBox1.Text = dr["gst"].ToString();
                    textBox1.Text = dr["quantity"].ToString();
                }
                if (textBox1.Text == "0")
                {
                    MessageBox.Show("Out of Stock");
                    quantityTextBox.Enabled = false;
                }
                else
                {
                    quantityTextBox.Enabled = true;
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
                if (comboBox1.Text == "" || comboBox2.Text == "" || priceTextBox.Text == "" || quantityTextBox.Text == "" || amountTextBox.Text == "")
                {
                    MessageBox.Show("Enter all fields");
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["sid"] = sidTextBox.Text;
                    dr["pname"] = comboBox2.Text;
                    dr["quantity"] = quantityTextBox.Text;
                    dr["price"] = priceTextBox.Text;
                    dr["amount"] = amountTextBox.Text;
                    dr["gst"] = gstTextBox1.Text;
                    dr["brand"] = comboBox1.Text;

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
                MessageBox.Show("No data to delete");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (cnameTextBox.Text == "" || contactTextBox.Text == "" || grandtotalTextBox.Text == "")
                {
                    MessageBox.Show("Enter all fields");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into sales values('" + sidTextBox.Text + "','" + dateDateTimePicker.Value.ToString("yyyy/MM/dd") + "','" + cnameTextBox.Text + "','" + contactTextBox.Text + "','" + totalTextBox.Text + "','" + gstTextBox.Text + "','" + discountTextBox.Text + "','" + grandtotalTextBox.Text + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        int qty = 0;
                        String pname = "";
                        con.Open();
                        SqlCommand cmd3 = con.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "insert into sdetail values('" + dr["sid"] + "','" + dr["brand"] + "','" + dr["pname"] + "','" + dr["quantity"] + "','" + dr["price"] + "','" + dr["amount"] + "','" + dr["gst"] + "')";
                        cmd3.ExecuteNonQuery();
                        con.Close();
                        qty = Convert.ToInt32(dr["quantity"].ToString());
                        pname = dr["pname"].ToString();

                        con.Open();
                        SqlCommand cmd4 = con.CreateCommand();
                        cmd4.CommandType = CommandType.Text;
                        cmd4.CommandText = "update product set quantity=quantity-'" + qty + "' where name='" + pname.ToString() + "'";
                        cmd4.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("Saved successfully");
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (cnameTextBox.Text == "" || contactTextBox.Text == "" || grandtotalTextBox.Text == "")
                {
                    MessageBox.Show("Enter all fields");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into sales values('" + sidTextBox.Text + "','" + dateDateTimePicker.Value.ToString("yyyy/MM/dd") + "','" + cnameTextBox.Text + "','" + contactTextBox.Text + "','" + totalTextBox.Text + "','" + gstTextBox.Text + "','" + discountTextBox.Text + "','" + grandtotalTextBox.Text + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        int qty = 0;
                        String pname = "";
                        con.Open();
                        SqlCommand cmd3 = con.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "insert into sdetail values('" + dr["sid"] + "','" + dr["brand"] + "','" + dr["pname"] + "','" + dr["quantity"] + "','" + dr["price"] + "','" + dr["amount"] + "','" + dr["gst"] + "')";
                        cmd3.ExecuteNonQuery();
                        con.Close();
                        qty = Convert.ToInt32(dr["quantity"].ToString());
                        pname = dr["pname"].ToString();

                        con.Open();
                        SqlCommand cmd4 = con.CreateCommand();
                        cmd4.CommandType = CommandType.Text;
                        cmd4.CommandText = "update product set quantity=quantity-'" + qty + "' where name='" + pname.ToString() + "'";
                        cmd4.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("Saved successfully");

                    billreport br = new billreport();
                    br.getValue(Convert.ToInt32(sidTextBox.Text.ToString()));
                    br.Show();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                var myForm = new sdetail();
                myForm.Show();
            }
            catch (Exception)
            {   
                throw;
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

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void quantityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Numbers Only");
            }
        }

        private void cnameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ' '))
            {
                e.Handled = true;
                MessageBox.Show("Enter Letters Only");
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
            try
            {
                if (contactTextBox.Text.Length != 10)
                {
                    MessageBox.Show("Enter the correct number.");
                }
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
                gstTextBox1.Text=Convert.ToString(Convert.ToInt32(amountTextBox.Text)*Convert.ToInt32(gstTextBox1.Text)/100);
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
                grandtotalTextBox.Text = Convert.ToString(Convert.ToDouble(gstTextBox.Text) + (Convert.ToDouble(totalTextBox.Text) - (Convert.ToDouble(totalTextBox.Text) * Convert.ToDouble(discountTextBox.Text) / 100)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        

       
    }
}
