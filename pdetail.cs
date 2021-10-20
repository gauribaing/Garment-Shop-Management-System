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
    public partial class pdetail : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\garment.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        DataTable dt = new DataTable();
        public pdetail()
        {
            InitializeComponent();
        }

        private void pdetail_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1010, 600);
            Clear();
            sclear();
            getSupplierName();
            getBrand();

            dt.Clear();
            dt.Columns.Add("pid");
            dt.Columns.Add("pname");
            dt.Columns.Add("brand");
            dt.Columns.Add("quantity");
            dt.Columns.Add("price");
            dt.Columns.Add("amount");
            dt.Columns.Add("gst");
        }

        public void Clear()
        {
            pidTextBox.Text = "";
            cnameTextBox.Text = "";
            contactTextBox.Text = "";
            totalTextBox.Text = "0";
            gstTextBox.Text = "0";
            discountTextBox.Text = "0";
            grandtotalTextBox.Text = "0";
        }

        public void sclear()
        {
            pidTextBox.Text = "";
            textBox1.Text = "";
            quantityTextBox.Text = "";
            priceTextBox.Text = "";
            amountTextBox.Text = "0";
            gstTextBox1.Text = "0";
            comboBox2.Text = "";
        }

        public void getSupplierName()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select * from sup where name='" + cnameTextBox.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    textBox2.Text = dr["brand"].ToString();
                    contactTextBox.Text = dr["contact"].ToString();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public void getBrand()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select * from sup where name='" + cnameTextBox.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    textBox2.Text = dr["brand"].ToString();

                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        

        //product
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from product where name='" + comboBox2.Text + "'";
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

        //add
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "" || comboBox2.Text == "" || priceTextBox.Text == "" || quantityTextBox.Text == "" || amountTextBox.Text == "")
                {
                    MessageBox.Show("Enter all fields");
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["pid"] = pidTextBox.Text;
                    dr["brand"] = textBox2.Text;
                    dr["pname"] = comboBox2.Text;
                    dr["quantity"] = quantityTextBox.Text;
                    dr["price"] = priceTextBox.Text;
                    dr["amount"] = amountTextBox.Text;
                    dr["gst"] = gstTextBox1.Text;

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

        //delete
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

        //save
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
                    cmd.CommandText = "update purchase set date='" + dateDateTimePicker.Value.ToString("yyyy/MM/dd") + "',sname='" + cnameTextBox.Text + "',contact='" + contactTextBox.Text + "',total='" + totalTextBox.Text + "',gst='" + gstTextBox.Text + "',discount='" + discountTextBox.Text + "',grandtotal='" + grandtotalTextBox.Text + "' where pid='" + textBox1.Text + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select * from pdetail where pid='" + textBox1.Text + "'";
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    DataTable dt3 = new DataTable();
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                    da2.Fill(dt3);
                    foreach (DataRow dr2 in dt3.Rows)
                    {
                        int qty = 0;
                        String pname = "";
                        qty = Convert.ToInt32(dr2["quantity"].ToString());
                        pname = dr2["pname"].ToString();

                        con.Open();
                        SqlCommand cmd3 = con.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "update product set quantity=quantity+'" + qty + "' where name='" + pname.ToString() + "'";
                        cmd3.ExecuteNonQuery();
                        con.Close();
                    }

                    con.Open();
                    SqlCommand cmd4 = con.CreateCommand();
                    cmd4.CommandType = CommandType.Text;
                    cmd4.CommandText = "delete from pdetail where pid='" + textBox1.Text + "'";
                    cmd4.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd5 = con.CreateCommand();
                    cmd5.CommandType = CommandType.Text;
                    cmd5.CommandText = "select * from purchase where pid='" + textBox1.Text + "'";
                    cmd5.ExecuteNonQuery();
                    con.Close();

                    DataTable dt2 = new DataTable();
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd5);
                    da3.Fill(dt2);

                    foreach (DataRow dr in dt.Rows)
                    {
                        int qty = 0;
                        String pname = "";
                        con.Open();
                        SqlCommand cmd2 = con.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "insert into pdetail values('" + dr["pid"] + "','" + dr["brand"] + "','" + dr["pname"] + "','" + dr["quantity"] + "','" + dr["price"] + "','" + dr["amount"] + "','" + dr["gst"] + "')";
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        qty = Convert.ToInt32(dr["quantity"].ToString());
                        pname = dr["pname"].ToString();

                        con.Open();
                        SqlCommand cmd3 = con.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "update product set quantity=quantity+'" + qty + "' where name='" + pname.ToString() + "'";
                        cmd3.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("Saved successfully");
                    dt.Clear();
                    Clear();
                    sclear();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        //search
        private void button5_Click(object sender, EventArgs e)
        {
           try
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select * from purchase where pid='" + textBox1.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    pidTextBox.Text = dr["pid"].ToString();
                    dateDateTimePicker.Text = dr["date"].ToString();
                    cnameTextBox.Text = dr["sname"].ToString();
                    contactTextBox.Text = dr["contact"].ToString();
                    totalTextBox.Text = dr["total"].ToString();
                    gstTextBox.Text = dr["gst"].ToString();
                    discountTextBox.Text = dr["discount"].ToString();
                    grandtotalTextBox.Text = dr["grandtotal"].ToString();

                }
                con.Open();
                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = " select * from pdetail where pid='" + pidTextBox.Text + "'";
                cmd2.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                getBrand();
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

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
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
                    MessageBox.Show("Enter correct contact number");
                }
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
                grandtotalTextBox.Text = Convert.ToString(Convert.ToDouble(totalTextBox.Text) - (Convert.ToDouble(totalTextBox.Text) * Convert.ToDouble(discountTextBox.Text) / 100));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Enter Bill no only");
            }
        }
    }
}
