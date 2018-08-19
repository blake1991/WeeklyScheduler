using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WeeklyScheduler.src.Models;
using WeeklyScheduler.src.Data_Access;
using System.Data.SQLite;

namespace WeeklyScheduler.src.Data_Access
{
    class EmployeeTableDB
    {
        /// <summary>
        /// Returns an ObservableCollection of all employees
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Employee> GetAllEmployees()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            SQLiteConnection conn = SchedulerDBContext.GetConnection();

            string selectStatement = "SELECT * FROM Employee";

            SQLiteCommand selectCommand = new SQLiteCommand(selectStatement, conn);

            try
            {
                conn.Open();
                SQLiteDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    Employee e = new Employee();
                    e.EmployeeId = Convert.ToInt32(reader["EmployeeId"].ToString());
                    e.Name = reader["Name"].ToString();
                    employees.Add(e);
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
            return employees;
        }

        public static Employee GetEmployee(int id)
        {
            SQLiteConnection conn = SchedulerDBContext.GetConnection();

            string selectStatement = "SELECT * FROM Employee WHERE EmployeeId = @EmployeeId";

            SQLiteCommand selectCommand = new SQLiteCommand(selectStatement, conn);
            selectCommand.Parameters.AddWithValue("@EmployeeId", id);

            try
            {
                conn.Open();
                SQLiteDataReader reader = selectCommand.ExecuteReader();
                Employee e = new Employee();

                while (reader.Read())
                {
                    e.EmployeeId = Convert.ToInt32(reader["EmployeeId"].ToString());
                    e.Name = reader["Name"].ToString();
                }

                return e;

            }
            catch (SQLiteException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Adds an employee to the database
        /// </summary>
        /// <param name="e"></param>
        public static int AddEmployee(Employee e)
        {
            SQLiteConnection conn = SchedulerDBContext.GetConnection();

            string insert = "INSERT INTO Employee" +
                "(Name)" +
                "VALUES(@Name)";

            string lastInsert = "SELECT last_insert_rowid()";
            SQLiteCommand selectCommand = new SQLiteCommand(lastInsert, conn);


            SQLiteCommand insertCommand = new SQLiteCommand(insert, conn);
            insertCommand.Parameters.AddWithValue("@Name", e.Name);

            try
            {
                conn.Open();
                insertCommand.ExecuteNonQuery();
                return Convert.ToInt32(selectCommand.ExecuteScalar());
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
