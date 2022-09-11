using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music
{
    public partial class Dashboard : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public Dashboard()
        {
            con = new SqlConnection("Data Source=VAIBHAVPATEL;Initial Catalog=MusicSchool;Integrated Security=True");
            InitializeComponent();
            CountStudent();
            SumAmount();
            CountTeachers();
            CountCourses();
        }

        private void picthome_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }
        
        private void CountStudent()
        {
            con.Open();
            sda = new SqlDataAdapter("Select Count(*) from StudentTb1",con);
            dt = new DataTable();
            sda.Fill(dt);
            lblDNumber.Text = dt.Rows[0][0].ToString()+" Student";
            con.Close();
        }
        private void CountCourses()
        {
            con.Open();
            sda = new SqlDataAdapter("Select Count(*) from CourseTb1", con);
            dt = new DataTable();
            sda.Fill(dt);
            lblDNumberofCourses.Text = dt.Rows[0][0].ToString() + " Courses";
            con.Close();
        }
        private void CountTeachers()
        {
            con.Open();
            sda = new SqlDataAdapter("Select Count(*) from TeachersTb1", con);
            dt = new DataTable();
            sda.Fill(dt);
            lblDInstructornumber.Text = dt.Rows[0][0].ToString() + " Instructors";
            con.Close();
        }
        private void SumAmount()
        {
            con.Open();
            sda = new SqlDataAdapter("Select Sum(FAmount) from FeesTb1", con);
            dt = new DataTable();
            sda.Fill(dt);
            lblDRupees.Text = dt.Rows[0][0].ToString() + " Rupees";
            con.Close();
        }
        private void lbldash_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void pictlogout_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void pitclearners_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            obj.Show();
            this.Hide();
        }

        private void pictteachers_Click(object sender, EventArgs e)
        {
            Teachers obj = new Teachers();
            obj.Show();
            this.Hide();
        }

        private void pictcourses_Click(object sender, EventArgs e)
        {
            Courses obj = new Courses();
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

        private void lbllearners_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            obj.Show();
            this.Hide();
        }

        private void lblcourses_Click(object sender, EventArgs e)
        {
            Courses obj = new Courses();
            obj.Show();
            this.Hide();
        }

        private void lblteachers_Click(object sender, EventArgs e)
        {
            Teachers obj = new Teachers();
            obj.Show();
            this.Hide();
        }

        private void lblfees_Click(object sender, EventArgs e)
        {
            Fees obj = new Fees();
            obj.Show();
            this.Hide();
        }

        private void gunaCirclePictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
