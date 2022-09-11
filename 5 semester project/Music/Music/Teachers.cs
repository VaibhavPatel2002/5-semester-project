using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Music
{
    public partial class Teachers : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public Teachers()
        {
            con = new SqlConnection("Data Source=VAIBHAVPATEL;Initial Catalog=MusicSchool;Integrated Security=True");
            
            InitializeComponent();
            DisplayTeachers();
        }
        private void Teachers_Load(object sender, EventArgs e)
        {
            
        }
        private void picthome_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }
        public void DisplayTeachers()
        {
            cmd = new SqlCommand("sp_dataofTeacher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            TeacherDGV.DataSource = dt;
            TeacherDGV.Refresh();
        }
        private void btnTSave_Click(object sender, EventArgs e)
        {
            if (txtTName.Text == "" || cmbTQualification.SelectedIndex == -1 || cmbTGender.SelectedIndex == -1 || txtTAddress.Text == "" || txtTPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("sp_InsertDataOfTeacher", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nm", txtTName.Text);
                    cmd.Parameters.AddWithValue("@dt", TDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@phone", txtTPhone.Text);
                    cmd.Parameters.AddWithValue("@qul", cmbTQualification.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@gen", cmbTGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@add", txtTAddress.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Added Successfully");
                    con.Close();
                    DisplayTeachers();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int key = 0;
        private void TeacherDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTName.Text = TeacherDGV.SelectedRows[0].Cells[1].Value.ToString();
            TDOB.Value = Convert.ToDateTime(TeacherDGV.SelectedRows[0].Cells[2].Value.ToString());
            txtTPhone.Text = TeacherDGV.SelectedRows[0].Cells[3].Value.ToString();
            cmbTQualification.SelectedItem = TeacherDGV.SelectedRows[0].Cells[4].Value.ToString();
            cmbTGender.SelectedItem = TeacherDGV.SelectedRows[0].Cells[5].Value.ToString();
            txtTAddress.Text = TeacherDGV.SelectedRows[0].Cells[6].Value.ToString();
            
            if (txtTName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt16(TeacherDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
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

        private void btnTEdit_Click(object sender, EventArgs e)
        {
            
            if (txtTName.Text == "" || cmbTQualification.SelectedIndex == -1 || cmbTGender.SelectedIndex == -1 || txtTAddress.Text == "" || txtTPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("sp_EditDataOfTeachers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nm", txtTName.Text);
                    cmd.Parameters.AddWithValue("@dt", TDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@phone", txtTPhone.Text);
                    cmd.Parameters.AddWithValue("@qul", cmbTQualification.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@gen", cmbTGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@add", txtTAddress.Text);
                    cmd.Parameters.AddWithValue("@no", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Updated Successfully");
                    con.Close();
                    DisplayTeachers();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btnTDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Teacher To Be Deleted ");
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("sp_DeleteDataofTeachers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@no", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Deleted Successfully");
                    DisplayTeachers();
                    con.Close();
                   
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void txtTName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTPhone_KeyPress(object sender, KeyPressEventArgs e)
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

        private void lbldash_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }


    }
}
