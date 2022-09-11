using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music
{
    public partial class Login : Form
    {
        //int i = 0;
        public Login()
        {
            InitializeComponent();
        }

        private void lblReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {   

            if (txtUsername.Text == "Username" || txtPassword.Text == "")
            {
                MessageBox.Show("Missing Information");
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
            else if (txtUsername.Text == "Admin" && txtPassword.Text == "Pass")
            {
                timer1.Start();
                //Home obj = new Home();
                //obj.Show();
                //this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Username or/and Password");
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //i++;
            progressBar1.Increment(10);

            if (txtUsername.Text == "Admin" && txtPassword.Text == "Pass")
            {
                timer1.Stop();
                Home obj = new Home();
                obj.Show();
                this.Hide();
            }
        }
    }
}
