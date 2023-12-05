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
    public partial class ScoreForm : Form
    {
        CourseClass course = new CourseClass();
        StudentClass student = new StudentClass();
        ScoreClass score = new ScoreClass();
        
        public ScoreForm()
        {
            InitializeComponent();
        }

        private void showScore()
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.Studentid,student.Studentfirstname,student.Studentlastname,score.Coursename,score.Score,score.Description FROM student INNER JOIN score ON score.Studentid=student.Studentid"));
        }

        private void ScoreForm_Load(object sender, EventArgs e)
        {
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
            comboBox_course.DisplayMember = "Coursename";
            comboBox_course.ValueMember = "Coursename";
            // to show data on score datagridview

            //To Display the student list on Datagridview
            DataGridView_score.DataSource = student.getList(new MySqlCommand("SELECT `Studentid`,`Studentfirstname`,`Studentlastname` FROM `student`"));
        }

        private void button_showstudent_Click(object sender, EventArgs e)
        {
            DataGridView_score.DataSource = student.getList(new MySqlCommand("SELECT `Studentid`,`Studentfirstname`,`Studentlastname` FROM `student`"));
        }

        private void button_showscore_Click(object sender, EventArgs e)
        {
            showScore();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_studentid.Clear();
            textBox_score.Clear();
            comboBox_course.SelectedIndex = 0;
            textBox_descreption.Clear();
        }

        private void DataGridView_score_Click(object sender, DataGridViewCellEventArgs e)
        {
            textBox_studentid.Text = DataGridView_score.CurrentRow.Cells[0].Value.ToString();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (textBox_studentid.Text == "" || textBox_score.Text == "")
            {
                MessageBox.Show("No score data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_studentid.Text);
                string cn = comboBox_course.Text;
                double sc = Convert.ToInt32(textBox_score.Text);
                string desc = textBox_descreption.Text;
                if (!score.checkScore(id, cn))
                {

                    if (score.insertScore(id, cn, sc, desc))
                    {
                        showScore();
                        button_clear.PerformClick();
                        MessageBox.Show("New score added", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Score not added", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The score for this course are alerady exists", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
