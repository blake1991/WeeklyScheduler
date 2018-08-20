using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace WeeklyScheduler
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
                string ScheduleTable = "CREATE TABLE IF NOT EXISTS Schedule (ScheduleId INTEGER PRIMARY KEY," +
                    "month INTEGER," +
                    "day INTEGER," +
                    "year INTEGER," +
                    "starttime varchar(10)," +
                    "endtime varchar(10))";

                string linkingTable = "CREATE TABLE IF NOT EXISTS EmployeeSchedule (EmployeeId INTEGER," +
                    "ScheduleId INTEGER)";


                try
                {
                    connection.Open();

                    SQLiteCommand createEmployeeCommand = new SQLiteCommand(EmployeeTable, connection);
                    SQLiteCommand createScheduleCommand = new SQLiteCommand(ScheduleTable, connection);
                    SQLiteCommand createLinkingCommand = new SQLiteCommand(linkingTable, connection);

                    createEmployeeCommand.ExecuteNonQuery();
                    createScheduleCommand.ExecuteNonQuery();
                    createLinkingCommand.ExecuteNonQuery();
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
