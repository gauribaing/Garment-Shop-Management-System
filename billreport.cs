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
    public partial class billreport : Form
    {
        int a;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\garment.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
     
        public billreport()
        {
            InitializeComponent();
        }

        private void billreport_Load(object sender, EventArgs e)
        {
            garmentDataSet ds=new garmentDataSet();
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from sales where sid="+ a +"";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds.sales);
            con.Close();

            con.Open();
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from sdetail where sid=" + a + "";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(ds.sdetail);
            da2.Fill(dt2);
            con.Close();

            CrystalReport3 cr3 = new CrystalReport3();
            cr3.SetDataSource(ds);
            crystalReportViewer1.ReportSource = cr3;
            crystalReportViewer1.Visible = true;
        }

        public void getValue(int i)
        {
            a = i;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        

        
    }
}
