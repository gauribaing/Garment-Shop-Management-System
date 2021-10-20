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
    public partial class purchasee : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\garment.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        DataTable dt = new DataTable();
        public purchasee()
        {
            InitializeComponent();
        }

        private void purchasee_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1010, 600);
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
            pidTextBox.Text = "";
            comboBox1.Text = "";
            contactTextBox.Text = "";
            totalTextBox.Text = "0";
            gstTextBox.Text = "0";
            discountTextBox.Text = "0";
            grandtotalTextBox.Text = "0";
        }

        public void sclear()
        {
            brandTextBox.Text = "";
            quantityTextBox.Text = "";
            priceTextBox.Text = "";
            amountTextBox.Text = "0";
            gstTextBox.Text = "0";
            comboBox2.Text = "";
        }

        public void getId()
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

        public void getSupplierName()
        {
            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = " select * from sup where name='" + comboBox1.Text + "'";
            cmd1.ExecuteNonQuery();
            con.Close();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
            {
                brandTextBox.Text = dr["brand"].ToString();
                contactTextBox.Text = dr["contact"].ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from product where brand='" + brandTextBox.Text + "' and name='" + comboBox2.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    priceTextBox.Text = dr["price"].ToString();
                    gstTextBox.Text = dr["gst"].ToString();
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
                DataRow dr = dt.NewRow();
                dr["pid"] = pidTextBox.Text;
                dr["sname"] = comboBox1.Text;
                dr["quantity"] = quantityTextBox.Text;
                dr["price"] = priceTextBox.Text;
                dr["amount"] = amountTextBox.Text;
                dr["gst"] = gstTextBox.Text;
                dr["brand"] = brandTextBox.Text;


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
                gstTextBox1.Text = totalgst.ToString();

                try
                {
                    grandtotalTextBox.Text = Convert.ToString(Convert.ToInt32(totalTextBox.Text) + Convert.ToDouble(gstTextBox1.Text));
                }
                catch (Exception)
                {
                    throw;
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
                gstTextBox1.Text = totalgst.ToString();

                try
                {
                    grandtotalTextBox.Text = Convert.ToString(Convert.ToInt32(totalTextBox.Text) + Convert.ToDouble(gstTextBox1.Text));
                }
                catch (Exception)
                {
                    throw;
                }
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
                if (comboBox1.Text == "" || contactTextBox.Text == "" || grandtotalTextBox.Text == "")
                {
                    MessageBox.Show("Enter all fields");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update purchase set date='" + dateDateTimePicker.Value.ToString("yyyy/MM/dd") + "',sname='" + comboBox1.Text + "',contact='" + contactTextBox.Text + "',total='" + totalTextBox.Text + "',gst='" + gstTextBox.Text + "',discount='" + discountTextBox.Text + "',grandtotal='" + grandtotalTextBox.Text + "' where pid='" + pidTextBox.Text + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select * from pdetail where pid='" + pidTextBox.Text + "'";
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
                    cmd4.CommandText = "delete from pdetail where pid='" + pidTextBox.Text + "'";
                    cmd4.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd5 = con.CreateCommand();
                    cmd5.CommandType = CommandType.Text;
                    cmd5.CommandText = "select * from purchase where pid='" + pidTextBox.Text + "'";
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


    }
}
