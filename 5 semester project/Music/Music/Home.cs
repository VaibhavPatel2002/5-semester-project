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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void lbllearners_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            this.Hide();
            obj.Show();
        }

        private void lblcourses_Click(object sender, EventArgs e)
        {
            Courses obj = new Courses();
            this.Hide();
            obj.Show();
        }

        private void lblteachers_Click(object sender, EventArgs e)
        {
            Teachers obj = new Teachers();
            this.Hide();
            obj.Show();
        }

        private void lblfees_Click(object sender, EventArgs e)
        {
            Fees obj = new Fees();
            this.Hide();
            obj.Show();
        }

        private void lbldash_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            this.Hide();
            obj.Show();
        }

        private void gunaCirclePictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictlogout_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void picthome_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void pitclearners_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            obj.Show();
            this.Hide();
        }

        private void pictcourses_Click(object sender, EventArgs e)
        {
            Courses obj = new Courses();
            obj.Show();
            this.Hide();
        }

        private void pictteachers_Click(object sender, EventArgs e)
        {
            Teachers obj = new Teachers();
            obj.Show();
            this.Hide();
        }

        private void pictfees_Click(object sender, EventArgs e)
        {
            Fees obj = new Fees();
            obj.Show();
            this.Hide();
        }

        private void pictdash_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }
    }
}
