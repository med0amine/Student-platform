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

namespace Student_platform
{
    public partial class refrechForm : Form
    {
        public refrechForm()
        {
            InitializeComponent();
        }

        int startpoint = 0;

        private void refrechForm_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void timer_Tick_1(object sender, EventArgs e)
        {
            startpoint += 1;
            if (startpoint > 40)
            {
                LoginForm login = new LoginForm();
                timer.Stop();
                this.Hide();
                login.Show();
            }
        }
    }
}
