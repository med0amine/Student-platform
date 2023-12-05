/*
 _name_: main Fom of the Student Paltform
 _autor_: Moahamed Amine Ben Ammar
 _date_: 12/05/2021
 _modified_: 06/012/2023
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Student_platform
{
    internal class CourseClass
    {
        DBconnect connect = new DBconnect();

        public bool insetCourse(string coursename, int hour, string description)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `course`(`Coursename`, `Coursehour`, `Description`) VALUES (@cn,@ch,@desc)", connect.GetConnection);
            
            //@cn,@ch,@desc
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = coursename;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value = hour;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = description;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }

        public bool updateCourse(int id, string coursename, int hour, string description)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `course` SET`Coursename`=@cn,`Coursehour`=@ch,`Description`=@desc WHERE  `Courseid`=@id", connect.GetConnection);
            //@id,@cn,@ch,@desc
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = coursename;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value = hour;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = description;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }

        public bool deleteCourse (int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `course` WHERE `Courseid`=@id", connect.GetConnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }

        public DataTable getCourse(MySqlCommand command)
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
