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
    public partial class Fees : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public Fees()
        {
            con = new SqlConnection("Data Source=VAIBHAVPATEL;Initial Catalog=MusicSchool;Integrated Security=True");
            InitializeComponent();
            DisplayFees();
            GetStudents();
            GetCourse();
        }
        private void GetStudents()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select StNum from StudentTb1",con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("StNum",typeof(int));
            dt.Load(Rdr);
            cmbFStudent.ValueMember = "StNum";
            cmbFStudent.DataSource = dt;
            con.Close();
        }
        private void FetchStName()
        {
            con.Open();
            string Query = "select * from StudentTb1 where StNum='" + cmbFStudent.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(Query, con);
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtStudentName.Text = dr["StName"].ToString();
            }
            con.Close();
        }

        private void GetCourse()
        {
            con.Open();
            cmd = new SqlCommand("select CNum from CourseTb1", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("CNum", typeof(int));
            dt.Load(Rdr);
            cmbFCourses.ValueMember = "CNum";
            cmbFCourses.DataSource = dt;
            con.Close();
        }
        private void FetchCName()
        {
            con.Open();
            string Query = "select * from CourseTb1 where CNum='" + cmbFCourses.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(Query, con);
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtFCourses.Text = dr["CName"].ToString();
            }
            con.Close();
        }
        

        
        public void DisplayFees()
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("sp_getdataofFees", con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                FeesDGV.DataSource = dt;
                FeesDGV.Refresh();
                con.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error" + e);
            }
        }

        private void cmbFStudent_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FetchStName();
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void cmbFCourses_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FetchCName();
        }
        private void Reset()
        {
            txtFAmount.Text = "";
            txtStudentName.Text = "";
            txtFCourses.Text = "";
        }
        private void btnFpay_Click(object sender, EventArgs e)
        {

            if (txtStudentName.Text == "" || txtFCourses.Text == "" || txtFAmount.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    sda = new SqlDataAdapter("select Count(*) from FeesTb1 where FStudId = '" + cmbFStudent.SelectedValue.ToString() + "'And FCourseId = '" + cmbFCourses.SelectedValue.ToString() + "'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show("Fees Already Payed");
                        Reset();
                    }
                    else
                    {
                        cmd = new SqlCommand("sp_insertintoFees",con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FSid", cmbFStudent.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@FSnm", txtStudentName.Text);
                        cmd.Parameters.AddWithValue("@FCid", cmbFCourses.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@FCnm", txtFCourses.Text);
                        cmd.Parameters.AddWithValue("@Fdate", FpaymentDate.Value.Date);
                        cmd.Parameters.AddWithValue("@Famt", txtFAmount.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfull Payment");
                    }
                    con.Close();
                    DisplayFees();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
       

        private void btnFReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void pictlogout_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void txtFAmount_KeyPress(object sender, KeyPressEventArgs e)
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

