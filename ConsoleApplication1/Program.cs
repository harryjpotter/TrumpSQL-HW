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
            string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\rotem_000\\Documents\\GitHub\\TrumpSQL-HW\\ConsoleApplication1\\Reporters.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            Console.WriteLine("What would you like to do Mr. Trump ?");
            string x;
            while (((x=Console.ReadLine()).Equals("Quit")) == false && ((x = Console.ReadLine()).Equals("quit")))
            {
                Console.WriteLine("What would you like to do Mr. Trump ?");
                if (x == "InsertReporter")
                    InsertReporter(conn);
                if (x == "InsertNews")
                    InsertNews(conn);
                if (x == "ListReporters")
                    ListReporters(conn);
                if (x == "ListNewById")
                    ListNewById(conn);
            }

            conn.Close();
        }

        private static void CreateReportersTable(SqlConnection conn)
        {
            SqlCommand create_table_command = conn.CreateCommand();
            create_table_command.CommandText = "CREATE TABLE Reporters (Id int IDENTITY(1,1) PRIMARY KEY,  FirstName varchar(255),  LastName varchar(255), Address varchar(255));";
            create_table_command.ExecuteNonQuery();
        }


        private static void CreateFakeNewsTable(SqlConnection conn)
        {
            SqlCommand create_table_command = conn.CreateCommand();
            create_table_command.CommandText = "CREATE TABLE FakeNews (Id int IDENTITY(1,1) PRIMARY KEY, Title varchar(255),ReporterId int FOREIGN KEY REFERENCES Reporters(Id));";
            create_table_command.ExecuteNonQuery();
        }


        private static void InsertReporter(SqlConnection conn)
        {
            SqlCommand insert_into_command = conn.CreateCommand();
            Console.WriteLine("Please Enter a First Name:");
            string FirstName = Console.ReadLine();
            Console.WriteLine("Please Enter a Last Name:");
            string LastName = Console.ReadLine();
            Console.WriteLine("Please Enter an Address:");
            string Address = Console.ReadLine();
            insert_into_command.CommandText = "INSERT INTO Reporters VALUES (FirstName, LastName, Address);";
            insert_into_command.ExecuteNonQuery();
        }


        private static void InsertNews(SqlConnection conn)
        {
            SqlCommand insert_into_command = conn.CreateCommand();
            Console.WriteLine("Please Enter a Title:");
            string Title = Console.ReadLine();
            Console.WriteLine("Please Enter a Reporter ID:");
            string ReporterID = Console.ReadLine();
            insert_into_command.CommandText = "INSERT INTO FakeNews VALUES (Title, ReporterID);";
            insert_into_command.ExecuteNonQuery();
        }


        private static void ListReporters(SqlConnection conn)
        {
            SqlCommand ListReporters = conn.CreateCommand();
            ListReporters.CommandText = "SELECT * FROM Reporters;";
            SqlDataReader reader = ListReporters.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("listReporters:");
                Console.Write("{0} : {1}, ", reader.GetName(0), reader.GetInt32(0));
                Console.Write("{0} : {1}, ", reader.GetName(1), reader.GetString(1));
                Console.Write("{0} : {1}. " + "\n", reader.GetName(2), reader.GetString(2));
            }
            reader.Close();
        }


        private static void ListNewById(SqlConnection conn)
        {
            Console.WriteLine("Please enter a Reporter ID:");
            int id = int.Parse(Console.ReadLine());
            SqlCommand ListNewById = conn.CreateCommand();
            ListNewById.CommandText = "SELECT * FROM FakeNews WHERE ReporterId = 1;";
            SqlDataReader reader = ListNewById.ExecuteReader();
            while (reader.Read())
            {
                Console.Write("{0} : {1}, ", reader.GetName(0), reader.GetInt32(0));
                Console.Write("{0} : {1}. " + "\n", reader.GetName(1), reader.GetString(1));
            }
            reader.Close();
        }

        private static void findReporterByNews(SqlConnection conn)
        {
          SqlCommand findReporterByNews = conn.CreateCommand(); // not finished!!!
          findReporterByNews.CommandText = "SELECT ReporterId FROM FakeNews WHERE Title = 'Trump is bad';";
          SqlDataReader reader = findReporterByNews.ExecuteReader();
          while (reader.Read())
          {
              //Console.Write("{0} : {1}",reader.GetName(0), reader.GetInt32(0) + "\n");
          }
          reader.Close();
          
        }
    }
}
