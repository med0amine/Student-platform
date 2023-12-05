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

namespace Student_platform
{
    /*
     *  in this class i create the connection beween the application and mySQL database
     *  i need to install wampp and mySQL to connector this project
     *  i need to create the student database
     */
    internal class DBconnect
    {
        // to create connection
        MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;user=root; database=studentdb");

        // to get connection
        public MySqlConnection GetConnection
        {
            get 
            { 
                return connect; 
            }
        }

        // create a function to open connection

        public void openConnect()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }

        // create a function to close connection
        
        public void closeConnect()
        {
            if (connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }

    }
}
