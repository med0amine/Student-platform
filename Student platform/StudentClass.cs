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
using MySql.Data.MySqlClient;
using System.Data;

namespace Student_platform
{
    class StudentClass
    {
        DBconnect connect = new DBconnect();

        public bool insertStudent (string firstname, string lastname, DateTime birthdate, string phone, string gender, string address, byte[] photo)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`( `Studentfirstname`, `Studentlastname`, `Birthdate`, `Phone`, `Gender`, `Address`, `Photo`) VALUES (@fn,@ln,@bd,@ph,@gd,@adr,@img)", connect.GetConnection);

            // @fn, @ln, @bd, @ph, @gd, @adr, @img
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = firstname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lastname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = birthdate;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = photo;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.openConnect();
                return false;
            }

        }

        public bool updateStudent(int id, string firstname, string lastname, DateTime birthdate, string phone, string gender, string address, byte[] photo)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `student` SET `Studentfirstname`=@fn,`Studentlastname`=@ln,`Birthdate`=@bd,`Phone`=@ph,`Gender`=@gd,`Address`=@adr,`Photo`=@img WHERE `Studentid`= @id", connect.GetConnection);

            // @fn, @ln, @bd, @ph, @gd, @adr, @img
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = firstname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lastname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = birthdate;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = photo;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.openConnect();
                return false;
            }

        }

        public bool deleteStudent(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM 'student' WHERE `Studentid`= @id", connect.GetConnection);

            // id
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

        public DataTable getStudentlist(MySqlCommand command)
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        // Execute Query
        public string exeCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connect.GetConnection);
            connect.openConnect();
            string count = command.ExecuteScalar().ToString();
            connect.closeConnect();
            return count;
        }
        
        public string totalStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student");
        }

        public DataTable searchStudent(string searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` WHERE CONTACT(`Studentfirstname`,`Studentlastname`,`Address`) LIKE '%"+ searchdata +"%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
