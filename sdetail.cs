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
    public partial class sdetail : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\garment.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        DataTable dt = new DataTable();
        public sdetail()
        {
            InitializeComponent();
        }

        private void sdetail_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1010, 600);
            Clear();
            sclear();
            getBrandName();

            dt.Clear();
            dt.Columns.Add("sid");
            dt.Columns.Add("brand");
            dt.Columns.Add("pname");
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
            sidTextBox.Text = "";
            comboBox1.Text = "";
            quantityTextBox.Text = "";
            priceTextBox.Text = "";
            amountTextBox.Text = "0";
            gstTextBox.Text = "0";
            comboBox2.Text = "";
        }

        public void getBrandName()
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
                foreach (DataRow dr in dt2.Rows)
                {
                    comboBox2.Items.Add(dr["name"].ToString());
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
                String remaining;
                remaining = textBox2.Text;
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
                    textBox2.Text = dr["quantity"].ToString();
                }
                if (textBox2.Text == "0")
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
                    dr["brand"] = comboBox1.Text;
                    dr["pname"] = comboBox2.Text;
                    dr["quantity"] = quantityTextBox.Text;
                    dr["price"] = priceTextBox.Text;
                    dr["amount"] = amountTextBox.Text;
                    dr["gst"] = gstTextBox.Text;

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
                MessageBox.Show("No record to delete");
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
                    cmd.CommandText = "update sales set date='" + dateDateTimePicker.Value.ToString("yyyy/MM/dd") + "',cname='" + cnameTextBox.Text + "',contact='" + contactTextBox.Text + "',total='" + totalTextBox.Text + "',gst='" + gstTextBox.Text + "',discount='" + discountTextBox.Text + "',grandtotal='" + grandtotalTextBox.Text + "' where sid='" + textBox1.Text + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select * from sdetail where sid='" + textBox1.Text + "'";
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
                    cmd4.CommandText = "delete from sdetail where sid='" + textBox1.Text + "'";
                    cmd4.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd5 = con.CreateCommand();
                    cmd5.CommandType = CommandType.Text;
                    cmd5.CommandText = "select * from sales where sid='" + textBox1.Text + "'";
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
                        cmd2.CommandText = "insert into sdetail values('" + dr["sid"] + "','" + dr["brand"] + "','" + dr["pname"] + "','" + dr["quantity"] + "','" + dr["price"] + "','" + dr["amount"] + "','" + dr["gst"] + "')";
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        qty = Convert.ToInt32(dr["quantity"].ToString());
                        pname = dr["pname"].ToString();

                        con.Open();
                        SqlCommand cmd3 = con.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "update product set quantity=quantity-'" + qty + "' where name='" + pname.ToString() + "'";
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                dt.Clear();
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " select * from sales where sid='" + textBox1.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    sidTextBox.Text = dr["sid"].ToString();
                    dateDateTimePicker.Text = dr["date"].ToString();
                    cnameTextBox.Text = dr["cname"].ToString();
                    contactTextBox.Text = dr["contact"].ToString();
                    totalTextBox.Text = dr["total"].ToString();
                    gstTextBox.Text = dr["gst"].ToString();
                    discountTextBox.Text = dr["discount"].ToString();
                    grandtotalTextBox.Text = dr["grandtotal"].ToString();
                }

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from sdetail where sid='" + sidTextBox.Text + "'";
                cmd.ExecuteNonQuery();

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
                    cmd.CommandText = "update sales set date='" + dateDateTimePicker.Value.ToString("yyyy/MM/dd") + "',cname='" + cnameTextBox.Text + "',contact='" + contactTextBox.Text + "',total='" + totalTextBox.Text + "',gst='" + gstTextBox.Text + "',discount='" + discountTextBox.Text + "',grandtotal='" + grandtotalTextBox.Text + "' where sid='" + textBox1.Text + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select * from sdetail where sid='" + textBox1.Text + "'";
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
                    cmd4.CommandText = "delete from sdetail where sid='" + textBox1.Text + "'";
                    cmd4.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd5 = con.CreateCommand();
                    cmd5.CommandType = CommandType.Text;
                    cmd5.CommandText = "select * from sales where sid='" + textBox1.Text + "'";
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
                        cmd2.CommandText = "insert into sdetail values('" + dr["sid"] + "','" + dr["brand"] + "','" + dr["pname"] + "','" + dr["quantity"] + "','" + dr["price"] + "','" + dr["amount"] + "','" + dr["gst"] + "')";
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        qty = Convert.ToInt32(dr["quantity"].ToString());
                        pname = dr["pname"].ToString();

                        con.Open();
                        SqlCommand cmd3 = con.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "update product set quantity=quantity-'" + qty + "' where name='" + pname.ToString() + "'";
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

            billreport br = new billreport();
            br.getValue(Convert.ToInt32(textBox1.Text.ToString()));
            br.Show();
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
                MessageBox.Show("Enter bill no only");
            }
        }
    }
}
