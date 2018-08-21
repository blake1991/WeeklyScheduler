using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WeeklyScheduler.src.Data_Access;
using WeeklyScheduler.src.Models;

namespace WeeklyScheduler
{
    public class WeeklyScheduleVM : BaseViewModel
    {
        public ObservableCollection<Employee> EmpList { get; set; }

        public string SampleText { get; set; }

        public ObservableCollection<EmployeeSchedule> testEmp { get; set; }

        /// <summary>
        /// Collection of weekly dates starting at sunday ending at saturday
        /// </summary>
        public ObservableCollection<string> WeekDates { get; set; }




        public WeeklyScheduleVM()
        {
            EmpList = EmployeeTableDB.GetAllEmployees();
            testEmp = new ObservableCollection<EmployeeSchedule>();
            WeekDates = new ObservableCollection<string>();


            EmployeeSchedule temp;

            //dummy data
            for (int i = 0; i < 5; i++)
            {
                temp = new EmployeeSchedule();
                Employee e = new Employee();
                e.EmployeeId = i;
                e.Name = "Employee" + i;

                temp.employee = e;

                Schedule sch = new Schedule() { ScheduleId = 1, Day = 2, Month = 3, Year = 2018, StartTime = "9", EndTime = "5" };
                ObservableCollection<Schedule> days = new ObservableCollection<Schedule>();

                days.Add(sch);
                temp.days = days;


                testEmp.Add(temp);


            }


            SetDaysOfWeek();
            var testGet = ScheduleTableDB.GetScheduleById(1);

        }

        public void Refresh()
        {
            EmpList = EmployeeTableDB.GetAllEmployees();
        }

        public void SetDaysOfWeek()
        {
            DateTime today = DateTime.Now;

            //go back to sunday
            TimeSpan fullday = new TimeSpan((int)today.DayOfWeek, 0, 0, 0);
            today = today.Subtract(fullday);

            for (int i = 0; i < 7; i++)
            {
                today = today.AddDays(1);
                string DayDate = today.ToString("M/d");
                WeekDates.Add(DayDate);
            }
        }

        public void MoveFowardOneWeek()
        {
            
        }


    }
}
