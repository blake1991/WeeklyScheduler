using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace WeeklyScheduler.src.Data_Access
{
    class SchedulerDBContext
    {
        /// <summary>
        /// Returns a SQLiteConnection to database
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection GetConnection()
        {
            SQLiteConnection connection;
            connection = new SQLiteConnection("Data Source=SchedulerDB.sql;Version=3;");
            return connection;
        }


        /// <summary>
        /// Creates database and all associated tables.
        /// </summary>
        public static void CreateDatabase()
        {
            if (!File.Exists("SchedulerDB.sql"))
            {
                SQLiteConnection.CreateFile("SchedulerDB.sql");
                SQLiteConnection connection = GetConnection();

                string EmployeeTable = "CREATE TABLE IF NOT EXISTS Employee (EmployeeId INTEGER PRIMARY KEY, " +
                    "Name varchar(50))";

                try
                {
                    connection.Open();
                    SQLiteCommand createCommand = new SQLiteCommand(EmployeeTable, connection);

                    createCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
