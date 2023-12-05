/*
 _name_: main Fom of the Student Paltform
 _autor_: Moahamed Amine Ben Ammar
 _date_: 12/05/2021
 _modified_: 06/012/2023
 */

using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Student_platform
{
    public partial class mainForm : Form
    {
        StudentClass student = new StudentClass();
        CourseClass course = new CourseClass();

        public mainForm()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            studentCount();
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
            comboBox_course.DisplayMember = "Coursename";
            comboBox_course.ValueMember = "Coursename";
        }

        private void studentCount()
        {
            label_total.Text = "Total students : " + student.totalStudent();
        }

        private void customizeDesign()
        {
            panel_student.Visible = false;
            panel_course.Visible = false;
            panel_score.Visible = false;
        }

        private void hidesubmenu()
        {
            if (panel_student.Visible == true)
                panel_student.Visible = false;
            if (panel_course.Visible == true)
                panel_course.Visible = false;
            if (panel_score.Visible == true)
                panel_score.Visible = false;
        }

        private void showsubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hidesubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e) //student button
        {
            showsubmenu(panel_student);
        }

        #region student
        private void button3_Click(object sender, EventArgs e) //student regestration button
        {
            openChilForm(new registrationForm());
            hidesubmenu();
        }

        private void button_student_managestudent_Click(object sender, EventArgs e)
        {
            openChilForm(new manageStudentForm());
            hidesubmenu();
        }

        private void button_sutdent_print_Click(object sender, EventArgs e)
        {
            openChilForm(new PrintStudentForm());
            hidesubmenu();
        }
        #endregion student

        private void button_course_Click(object sender, EventArgs e)
        {
            showsubmenu(panel_course);
        }
        #region course

        private void button_course_addnewcource_Click(object sender, EventArgs e)
        {
            openChilForm(new CourseForm());
            hidesubmenu();
        }

        private void button_course_managecourse_Click(object sender, EventArgs e)
        {
            openChilForm(new manageCourseForm());
            hidesubmenu();
        }

        private void button_course_print_Click(object sender, EventArgs e)
        {
            openChilForm(new PrintCourseForm());
            hidesubmenu();
        }
        #endregion course

        private void button_score_Click(object sender, EventArgs e)
        {
            showsubmenu(panel_score);

        }
        #region score
        private void button_score_addscore_Click(object sender, EventArgs e)
        {
            openChilForm(new ScoreForm());
            hidesubmenu();
        }

        private void button_score_managescore_Click(object sender, EventArgs e)
        {
            openChilForm(new ManageScoreForm());
            hidesubmenu();
        }

        private void button_score_print_Click(object sender, EventArgs e)
        {
            openChilForm(new PrintScoreForm());
            hidesubmenu();
        }
        #endregion score

        private Form activeForm = null;
        private void openChilForm(Form ChildForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.FormBorderStyle = FormBorderStyle.None;
            ChildForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(ChildForm);
            panel_main.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            panel_main.Controls.Add(panel_cover);
            studentCount();
            LoginForm login = new LoginForm();
            this.Hide();
            login.Show();
        }

        private void button_dashboard_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            panel_main.Controls.Add(panel_cover);
            studentCount();
        }

        private void label_total_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            panel_main.Controls.Add(panel_cover);
            studentCount();
        }
    }
}