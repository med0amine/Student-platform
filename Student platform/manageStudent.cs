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
using System.IO;
using MySql.Data.MySqlClient;

namespace Student_platform
{
    public partial class manageStudentForm : Form
    {
        StudentClass student = new StudentClass();
        public manageStudentForm()
        {
            InitializeComponent();
        }

        bool verify()
        {
            if ((textBox_firstname.Text == "") || (textBox_lastname.Text == "") ||
                (textBox_phone.Text == "") || (textBox_address.Text == "") ||
                (pictureBox_student.Image == null))
            {
                return false;
            }
            else
                return true;
        }

        private void manageStudentForm_Load(object sender, EventArgs e)
        {
            showTable();
        }

        public void showTable()
        {
            DataGridView_student.DataSource = student.getStudentlist(new MySqlCommand("SELECT * FROM `student`"));
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            textBox_id.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
            textBox_firstname.Text = DataGridView_student.CurrentRow.Cells[1].Value.ToString();
            textBox_lastname.Text = DataGridView_student.CurrentRow.Cells[2].Value.ToString();

            textBox_birthdate.Value = (DateTime)DataGridView_student.CurrentRow.Cells[3].Value;

            textBox_phone.Text = DataGridView_student.CurrentRow.Cells[4].Value.ToString();
            if (DataGridView_student.CurrentRow.Cells[5].Value.ToString() == "Male")
                radioButton_male.Checked = true;
            else if (DataGridView_student.CurrentRow.Cells[5].Value.ToString() == "Female")
                radioButton_female.Checked = true;
            else if (DataGridView_student.CurrentRow.Cells[5].Value.ToString() == "Diverse")
                radioButton_diverse.Checked = true;

            textBox_address.Text =  DataGridView_student.CurrentRow.Cells[6].Value.ToString();
            byte[] photo = (byte[])DataGridView_student.CurrentRow.Cells[7].Value;
            MemoryStream ms = new MemoryStream(photo);
            pictureBox_student.Image = Image.FromStream(ms);
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_firstname.Clear();
            textBox_lastname.Clear();
            textBox_birthdate.Value = DateTime.Now;
            textBox_phone.Clear();
            textBox_address.Clear();
            radioButton_male.Checked = false;
            radioButton_female.Checked = false;
            radioButton_diverse.Checked = false;
            pictureBox_student.Image = null;
        }

        private void button_photoupload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select photo(*.jpg;*.png;*.gif) |*.jpg;*.png;*;gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.searchStudent( textBox_search.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            // update student
            int id = Convert.ToInt32(textBox_id.Text);
            string firstname = textBox_firstname.Text;
            string lastname = textBox_lastname.Text;
            DateTime birthdate = textBox_birthdate.Value;
            string phone = textBox_phone.Text;
            string gender = radioButton_diverse.Checked ? "Diverse" : radioButton_male.Checked ? "Male" : "Female";
            string address = textBox_address.Text;

            // check student age between 17 and 100

            int born_year = textBox_birthdate.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 17 || (this_year - born_year) > 100)
            {
                MessageBox.Show("The student age must be between 17 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                    byte[] photo = ms.ToArray();
                    if (student.updateStudent(id, firstname, lastname, birthdate, phone, gender, address, photo))
                    {
                        MessageBox.Show("Student data updated", "Update student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error - Student did not add", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty field", "Update student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox_id.Text);

            if (MessageBox.Show("Are you sure you want to remove this student", "Remove Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (student.deleteStudent(id))
                {
                    showTable();
                    MessageBox.Show("Student Removed", "Remove student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button_clear.PerformClick();
                }
            }
        }
    }
}


