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
    public partial class manageCourseForm : Form
    {
        CourseClass course = new CourseClass();

        public manageCourseForm()
        {
            InitializeComponent();
        }

        private void showData()
        {
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course` WHERE CONCAT(`Coursename`)LIKE '%" + textBox_search.Text + "%'"));
            textBox_search.Clear();
        }

        private void DataGridView_course_Click(object sender, DataGridViewCellEventArgs e)
        {
            textBox_courseid.Text = DataGridView_course.CurrentRow.Cells[0].Value.ToString();
            textBox_coursename.Text = DataGridView_course.CurrentRow.Cells[1].Value.ToString();
            textBox_hour.Text = DataGridView_course.CurrentRow.Cells[2].Value.ToString();
            textBox_descreption.Text = DataGridView_course.CurrentRow.Cells[3].Value.ToString();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_courseid.Clear();
            textBox_coursename.Clear();
            textBox_hour.Clear();
            textBox_descreption.Clear();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (textBox_coursename.Text == "" || textBox_hour.Text == "" || textBox_courseid.Text.Equals(""))
            {
                MessageBox.Show("Need Course data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_courseid.Text);
                string cName = textBox_coursename.Text;
                int chr = Convert.ToInt32(textBox_hour.Text);
                string desc = textBox_descreption.Text;


                if (course.updateCourse(id, cName, chr, desc))
                {
                    showData();
                    button_clear.PerformClick();
                    MessageBox.Show("course updated successfuly", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Error-Course does not Edited", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_courseid.Text.Equals(""))
            {
                MessageBox.Show("Need Course Id", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    int id = Convert.ToInt32(textBox_courseid.Text);
                    if (course.deleteCourse(id))
                    {
                        showData();
                        button_clear.PerformClick();
                        MessageBox.Show("course Deleted", "Removed Course", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                catch (Exception ex)

                {
                    MessageBox.Show(ex.Message, "Removed Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void manageCourseForm_Load(object sender, EventArgs e)
        {
            showData();
        }
    }
}
