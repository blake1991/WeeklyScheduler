using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyScheduler
{
    public class ScheduleTableDB
    {
        public static Schedule GetScheduleById(int id)
        {
            Schedule schdl = new Schedule();

            SQLiteConnection conn = SchedulerDBContext.GetConnection();

            string selectStatement = "SELECT ScheduleId, month, day, year, starttime, endtime FROM Schedule WHERE ScheduleId = @ScheduleId";

            SQLiteCommand selectCommand = new SQLiteCommand(selectStatement, conn);
            selectCommand.Parameters.AddWithValue("@ScheduleId", id);

            try
            {
                conn.Open();
                SQLiteDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    schdl.ScheduleId = Convert.ToInt32(reader["ScheduleId"].ToString());
                    schdl.Month = Convert.ToInt32(reader["month"].ToString());
                    schdl.Day = Convert.ToInt32(reader["day"].ToString());
                    schdl.StartTime = (reader["starttime"].ToString());
                    schdl.EndTime = (reader["endtime"].ToString());
                }
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return schdl;
        }

        public static int AddSchedule(Schedule schdl)
        {
            SQLiteConnection conn = SchedulerDBContext.GetConnection();

            string insertStatement = "INSERT INTO Schedule" +
                "(month, day, year, starttime, endtime)" +
                "VALUES (@month, @day, @year, @starttime, @endtime)";

            string lastInsert = "SELECT last_insert_rowid()";
            SQLiteCommand lastInsertCommand = new SQLiteCommand(lastInsert, conn);


            SQLiteCommand insertCommand = new SQLiteCommand(insertStatement, conn);
            insertCommand.Parameters.AddWithValue("@month", schdl.Month);
            insertCommand.Parameters.AddWithValue("@day", schdl.Day);
            insertCommand.Parameters.AddWithValue("@year", schdl.Year);
            insertCommand.Parameters.AddWithValue("@starttime", schdl.StartTime);
            insertCommand.Parameters.AddWithValue("@endtime", schdl.EndTime);

            try
            {
                conn.Open();
                insertCommand.ExecuteNonQuery();
                int id = Convert.ToInt32(lastInsertCommand.ExecuteScalar());
                return id;
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
