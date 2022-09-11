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
    public partial class Courses : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public Courses()
        {
            con = new SqlConnection("Data Source=VAIBHAVPATEL;Initial Catalog=MusicSchool;Integrated Security=True");
            InitializeComponent();
            GetTeachers();
            DisplayCourse();
        }

        private void picthome_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void GetTeachers()
        {
            con.Open();
            cmd = new SqlCommand("sp_getTeacher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("TNum",typeof(int));
            dt.Load(Rdr);
            cmdCTeacher.ValueMember = "TNum";
            cmdCTeacher.DataSource = dt;
            con.Close();
        }
        private void FetchTName()
        {
            con.Open();
            cmd = new SqlCommand("sp_FecthTeacherName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cid", cmdCTeacher.SelectedValue.ToString());
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtCTeacherName.Text = dr["TName"].ToString();
            }
            con.Close();
        }

        private void btnCSave_Click(object sender, EventArgs e)
        {
            if (txtCName.Text == "" || cmdCTeacher.SelectedIndex == -1 || txtCTeacherName.Text == "" || txtCPrice.Text == "" || txtCDuration.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("sp_InsertDatainCourse", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cnm", txtCName.Text);
                    cmd.Parameters.AddWithValue("@cid", cmdCTeacher.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@ctnm", txtCTeacherName.Text);
                    cmd.Parameters.AddWithValue("@cprc", txtCPrice.Text);
                    cmd.Parameters.AddWithValue("@cdur", txtCDuration.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Course Added Successfully");
                    con.Close();
                    DisplayCourse();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        public void DisplayCourse()
        {
            cmd = new SqlCommand("sp_dataofCourse", con);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            CoursesDGV.DataSource = dt;
            CoursesDGV.Refresh();
        }

       
        private void lblHome_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void cmdCTeacher_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FetchTName();
        }
        int key = 0;
        private void CoursesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCName.Text = CoursesDGV.SelectedRows[0].Cells[1].Value.ToString();
            cmdCTeacher.SelectedValue = CoursesDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtCTeacherName.Text = CoursesDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtCPrice.Text = CoursesDGV.SelectedRows[0].Cells[4].Value.ToString();
            txtCDuration.Text = CoursesDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (txtCName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt16(CoursesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnCDelete_Click_1(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Course To Be Deleted ");
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("sp_DeleteDataofCourse", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cno", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Course Deleted Successfully");
                    con.Close();
                    DisplayCourse();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btnCEdit_Click_1(object sender, EventArgs e)
        {
            
            if (txtCName.Text == "" || cmdCTeacher.SelectedIndex == -1 || txtCTeacherName.Text == "" || txtCPrice.Text == "" || txtCDuration.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("sp_EditDataOfCourse", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cnm", txtCName.Text);
                    cmd.Parameters.AddWithValue("@cid", cmdCTeacher.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@ctnm", txtCTeacherName.Text);
                    cmd.Parameters.AddWithValue("@cprc", txtCPrice.Text);
                    cmd.Parameters.AddWithValue("@cdur", txtCDuration.Text);
                    cmd.Parameters.AddWithValue("@cno", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Course updated Successfully");
                    con.Close();
                    DisplayCourse();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictlogout_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void txtCName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 64 && e.KeyChar < 91 || e.KeyChar > 96 && e.KeyChar < 123 || e.KeyChar == 8 || e.KeyChar == 32 || e.KeyChar == 9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 47 && e.KeyChar < 58 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numeric value");
            }
        }

        private void txtCDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 47 && e.KeyChar < 58 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numeric value");
            }
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

        private void lbldash_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void gunaCirclePictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

