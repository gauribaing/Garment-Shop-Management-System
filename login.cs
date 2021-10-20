using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Garment_Shop_Management
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user, pass;
            user = textBox1.Text;
            pass = textBox2.Text;
            if (user == "admin" && pass == "1234") 
            {
                MessageBox.Show("Login Successful");
                this.Hide();
                MDIParent2 h = new MDIParent2();
                h.Show();
            }
            if (user != "admin" || pass != "1234")
            {
                MessageBox.Show("Invalid username or Password");
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
