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

            string selectStatement = "SELECT month, day, year, starttime, endtime FROM Schedule WHERE ScheduleId = @ScheduleId";

            SQLiteCommand selectCommand = new SQLiteCommand(selectStatement, conn);
            selectCommand.Parameters.AddWithValue("@ScheduleId", id);

            try
            {
                conn.Open();
                SQLiteDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    schdl = new Schedule();
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
    }
}
