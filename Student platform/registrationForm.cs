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
    public partial class registrationForm : Form
    {
        StudentClass student = new StudentClass();
        public registrationForm()
        {
            InitializeComponent();
        }

        // create a ferification
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

        private void registrationForm_Load(object sender, EventArgs e)
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

        private void button_add_Click_1(object sender, EventArgs e)
        {
            // add new student
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
                    // get photo from picture box
                    MemoryStream ms = new MemoryStream();
                    pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                    byte[] photo = ms.ToArray();
                    if (student.insertStudent(firstname, lastname, birthdate, phone, gender, address, photo))
                    {
                        MessageBox.Show("New student added", "Add student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error - Student did not add", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty field", "Add student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_clear_Click_1(object sender, EventArgs e)
        {
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

        private void button_photoupload_Click_1(object sender, EventArgs e)
        {
            // browse photo from your computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select photo(*.jpg;*.png;*.gif) |*.jpg;*.png;*;gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }
    }
}
