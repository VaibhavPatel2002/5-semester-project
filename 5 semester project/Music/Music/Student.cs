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
    public partial class Student : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public Student()
        {
            con = new SqlConnection("Data Source=VAIBHAVPATEL;Initial Catalog=MusicSchool;Integrated Security=True");
            InitializeComponent();
            GetCourses();
            DisplayStudents();
        }
        

        private void GetCourses()
        {
            con.Open();
            cmd = new SqlCommand("sp_GetCourseinStudentTB", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CNum", typeof(int));
            dt.Load(Rdr);
            cmbLCourse.ValueMember = "CNum";
            cmbLCourse.DataSource = dt;
            con.Close();
        }
        private void FetchCName()
        {
            con.Open();
            cmd = new SqlCommand("sp_FecthCourseName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cno",cmbLCourse.SelectedValue.ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtLCourse.Text = dr["CName"].ToString();
            }
            con.Close();
        }
        private void btnLSave_Click(object sender, EventArgs e)
        {
            if (txtLName.Text == "" || txtLAddress.Text == "" || cmbLGender.SelectedIndex == -1 || txtLRemark.Text == "" || txtLCourse.Text == "" || txtLPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("sp_InsertDatainStudent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Snm", txtLName.Text);
                    cmd.Parameters.AddWithValue("@SDob", LDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SAdd", txtLAddress.Text);
                    cmd.Parameters.AddWithValue("@Sphone", txtLPhone.Text);
                    cmd.Parameters.AddWithValue("@Scur", cmbLCourse.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SCnm", txtLCourse.Text);
                    cmd.Parameters.AddWithValue("@SGen", cmbLGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SRmrk", txtLRemark.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Added Successfully");
                    con.Close();
                    DisplayStudents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        public void DisplayStudents()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("sp_DataofStudent", con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                StudentDGV.DataSource = dt;
                StudentDGV.Refresh();
                con.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error" + e);
            }
        }
        private void cmdLCourse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FetchCName();
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }
        int key = 0;
        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLName.Text = StudentDGV.SelectedRows[0].Cells[1].Value.ToString();
            LDOB.Value = Convert.ToDateTime(StudentDGV.SelectedRows[0].Cells[2].Value.ToString());
            txtLAddress.Text = StudentDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtLPhone.Text = StudentDGV.SelectedRows[0].Cells[4].Value.ToString();
            cmbLCourse.Text = StudentDGV.SelectedRows[0].Cells[5].Value.ToString();
            txtLCourse.Text = StudentDGV.SelectedRows[0].Cells[6].Value.ToString();
            cmbLGender.Text = StudentDGV.SelectedRows[0].Cells[7].Value.ToString();
            txtLRemark.Text = StudentDGV.SelectedRows[0].Cells[8].Value.ToString();
           
            if (txtLName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt16(StudentDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnLDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Student To Be Delete ");
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("sp_DeleteDataofStudent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Stno", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Deleted Successfully");
                    con.Close();
                    DisplayStudents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btnLEdit_Click(object sender, EventArgs e)
        {
           
            if (txtLName.Text == "" || txtLAddress.Text == "" || cmbLGender.SelectedIndex == -1 || txtLRemark.Text == "" || txtLCourse.Text == "" || txtLPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("sp_EditDataOfStudent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Snm", txtLName.Text);
                    cmd.Parameters.AddWithValue("@SDob", LDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SAdd", txtLAddress.Text);
                    cmd.Parameters.AddWithValue("@Sphone", txtLPhone.Text);
                    cmd.Parameters.AddWithValue("@Scur", cmbLCourse.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SCnm", txtLCourse.Text);
                    cmd.Parameters.AddWithValue("@SGen", cmbLGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SRmrk", txtLRemark.Text);
                    cmd.Parameters.AddWithValue("@Stno", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Updated Successfully");
                    con.Close();
                    DisplayStudents();
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
        }

        private void txtLName_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtLPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 47 && e.KeyChar <  58 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numeric value");
            }
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

