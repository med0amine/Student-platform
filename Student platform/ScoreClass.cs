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
    internal class ScoreClass
    {
        DBconnect connect = new DBconnect();
        public bool insertScore(int id, string coursename, double score, string description)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `score`(`Studentid`, `Coursename`, `Score`, `Description`) VALUES (@id,@cn,@sc,@desc)", connect.GetConnection);
            
            //@id,@cn,@sco,@desc
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = coursename;
            command.Parameters.Add("@sc", MySqlDbType.Double).Value = score;
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

        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public bool checkScore(int id, string coursename)
        {
            DataTable table = getList(new MySqlCommand("SELECT * FROM `score` WHERE `Studentid`= '" + id + "' AND `Coursename`= '" + coursename + "'"));
            
            if (table.Rows.Count > 0)
                return true; 
            else
                return false; 
        }

        public bool updateScore(int id, string cn, double sc, string desc)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `score` SET `Score`=@sc,`Description`=@desc WHERE `Studentid`=@id AND `Coursename`=@cn", connect.GetConnection);
            
            //@stid,@sco,@desc
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cn;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@sc", MySqlDbType.Double).Value = sc;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
            
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

        public bool deleteScore(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `score` WHERE `Studentid`=@id", connect.GetConnection);

            //@id
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
    }
}
