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
    public partial class ManageScoreForm : Form
    {
        CourseClass course = new CourseClass();
        ScoreClass score = new ScoreClass();

        public ManageScoreForm()
        {
            InitializeComponent();
        }

        public void showScore()
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.Studentid,student.Studentfirstname,student.Studentlastname,score.Coursename,score.Score,score.Description FROM student INNER JOIN score ON score.Studentid=student.Studentid"));
        }

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
            comboBox_course.DisplayMember = "Coursename";
            comboBox_course.ValueMember = "Coursename";
            showScore();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.Studentid, student.Studentfirstname, student.Studentlastname, score.Coursename, score.Score, score.Description FROM student INNER JOIN score ON score.Studentid = student.Studentid WHERE CONCAT(student.Studentfirstname, student.Studentlastname, score.Coursename)LIKE '%" + textBox_search.Text + "%'"));
        }

        private void DataGridView_score_Click(object sender, DataGridViewCellEventArgs e)
        {
            textBox_studentid.Text = DataGridView_score.CurrentRow.Cells[0].Value.ToString();
            comboBox_course.Text = DataGridView_score.CurrentRow.Cells[3].Value.ToString();
            textBox_score.Text = DataGridView_score.CurrentRow.Cells[4].Value.ToString();
            textBox_descreption.Text = DataGridView_score.CurrentRow.Cells[5].Value.ToString();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_studentid.Clear();
            textBox_score.Clear();
            textBox_descreption.Clear();
            textBox_search.Clear();
        }

        private void button_update_Click(object sender, EventArgs e)
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

                if (score.updateScore(id, cn, sc, desc))
                {
                    showScore();
                    button_clear.PerformClick();
                    MessageBox.Show("Score Edited Complete", "Update Score", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Score not edit", "Update Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_studentid.Text == "")
            {
                MessageBox.Show("Field Error- no student id", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_studentid.Text);
                if (MessageBox.Show("Are you sure you want to remove this score", "Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (score.deleteScore(id))
                    {
                        showScore();
                        MessageBox.Show("Score Removed", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clear.PerformClick();
                    }
                }

            }
        }
    }
}
