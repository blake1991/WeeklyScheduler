using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyScheduler
{
    class EmployeeScheduleTableDB
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list of Tuple(EmployeeId, ScheduleId)</returns>
        public static List<Tuple<int, int>> GetAllEmployeeSchedules()
        {
            List<Tuple<int, int>> tList = new List<Tuple<int, int>>();
            SQLiteConnection conn = SchedulerDBContext.GetConnection();

            string selectStatement = "SELECT * FROM EmployeeSchedule";

            SQLiteCommand selectCommand = new SQLiteCommand(selectStatement, conn);

            try
            {
                conn.Open();
                SQLiteDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    Tuple<int, int> t = new Tuple<int, int>(
                        Convert.ToInt32(reader["EmployeeId"].ToString()),
                        Convert.ToInt32(reader["ScheduleId"].ToString())
                        );
                    tList.Add(t);
                }
                return tList;
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

        public static void AddEmployeeSchedule(int EmployeeId, int ScheduleId)
        {
            SQLiteConnection conn = SchedulerDBContext.GetConnection();

            string insertStatement = "INSERT INTO EmployeeSchedule" +
                "(EmployeeId, ScheduleId)" +
                "VALUES (@EmployeeId, @ScheduleId)";

            SQLiteCommand insertCommand = new SQLiteCommand(insertStatement, conn);
            insertCommand.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            insertCommand.Parameters.AddWithValue("@ScheduleId", ScheduleId);

            try
            {
                conn.Open();
                insertCommand.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
