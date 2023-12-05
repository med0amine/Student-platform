/*
 _name_: main Fom of the Student Paltform
 _autor_: Moahamed Amine Ben Ammar
 _date_: 12/05/2021
 _modified_: 06/012/2023
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Student_platform
{
    public partial class CourseForm : Form
    {
        CourseClass course = new CourseClass();

        public CourseForm()
        {
            InitializeComponent();
        }

        private void showData()
        {
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (textBox_coursename.Text == "" || textBox_coursename.Text == "")
            {
                MessageBox.Show("Missing Course data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                string coursename = textBox_coursename.Text;
                int hour = Convert.ToInt32(textBox_hour.Text);
                string description = textBox_descreption.Text;


                if (course.insetCourse(coursename, hour, description))
                {
                    showData();
                    button_clear.PerformClick();
                    MessageBox.Show("New course inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Course not insert", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_coursename.Clear();
            textBox_hour.Clear();
            textBox_descreption.Clear();
        }

        private void CourseForm_Load(object sender, EventArgs e)
        {
            showData();
        }
    }
}
