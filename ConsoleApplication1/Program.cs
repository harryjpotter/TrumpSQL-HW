using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr= "Data Source=.\\SQLEXPRESS;AttachDbFilename=\"c:\\users\\user1\\documents\\visual studio 2010\\Projects\\ConsoleApplication1\\ConsoleApplication1\\Reporters.mdf\";Integrated Security=True;User Instance=True";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            /*SqlCommand insert_into_command = conn.CreateCommand();
            insert_into_command.CommandText = "INSERT INTO Reporters VALUES ('Magyn', 'Kelly', 'Somewhere Avenue 123');";
            insert_into_command.ExecuteNonQuery(); */

            /*SqlCommand insert_into_command = conn.CreateCommand();
            insert_into_command.CommandText = "INSERT INTO FakeNews VALUES ('Trump is bad', 3);";
            insert_into_command.ExecuteNonQuery(); */

            SqlCommand listReporters = conn.CreateCommand();
            listReporters.CommandText = "SELECT * FROM Reporters;";
            SqlDataReader reader = listReporters.ExecuteReader();
            while (reader.Read())
            {
                Console.Write("{0} : {1}, ", reader.GetName(0), reader.GetInt32(0));
                Console.Write("{0} : {1}, ", reader.GetName(1), reader.GetString(1));
                Console.Write("{0} : {1}, " + "\n", reader.GetName(2), reader.GetString(2));
            }
            reader.Close();

            SqlCommand listNewById = conn.CreateCommand();
            listNewById.CommandText = "SELECT * FROM FakeNews WHERE ReporterId = 1;";
            reader = listNewById.ExecuteReader();
            while (reader.Read())
            {
                Console.Write("{0} : {1}, ", reader.GetName(0), reader.GetInt32(0));
                Console.Write("{0} : {1}, " + "\n", reader.GetName(1), reader.GetString(1));
            }
            reader.Close();

            conn.Close();
        }

        private static void CreateFakeNewsTable(SqlConnection conn)
        {
            SqlCommand create_table_command = conn.CreateCommand();
            create_table_command.CommandText = "CREATE TABLE FakeNews (Id int IDENTITY(1,1) PRIMARY KEY, Title varchar(255),ReporterId int FOREIGN KEY REFERENCES Reporters(Id));";
            create_table_command.ExecuteNonQuery();
        }

        private static void CreateReportersTable(SqlConnection conn)
        {
            SqlCommand create_table_command = conn.CreateCommand();
            create_table_command.CommandText = "CREATE TABLE Reporters (Id int IDENTITY(1,1) PRIMARY KEY,  FirstName varchar(255),  LastName varchar(255), Address varchar(255));";
            create_table_command.ExecuteNonQuery();
        }

    }
}
