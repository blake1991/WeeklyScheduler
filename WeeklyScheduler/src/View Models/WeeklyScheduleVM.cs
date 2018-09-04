using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WeeklyScheduler.src.Data_Access;
using WeeklyScheduler.src.Models;

namespace WeeklyScheduler
{
    public class WeeklyScheduleVM : BaseViewModel
    {
        public ObservableCollection<Employee> EmployeeList { get; set; }
        public ObservableCollection<EmployeeSchedule> EmployeeSchedules { get; set; }

        /// <summary>
        /// Collection of weekly dates starting at sunday ending at saturday. Used to re-label data grid headers.
        /// </summary>
        //public ObservableCollection<myDatetime> WeekDates { get; set; }
        public ObservableCollection<DateTime> WeekDates { get; set; }

        public DateTime WeekOfDate { get; set; }

        public Schedule testGet { get; set; }

        public WeeklyScheduleVM()
        {
            // EmpList = EmployeeTableDB.GetAllEmployees();
            EmployeeSchedules = new ObservableCollection<EmployeeSchedule>();
            WeekDates = new ObservableCollection<DateTime>();

            Refresh();

            GetWeekRange(DateTime.Now);

        }

        public void Refresh()
        {
            EmployeeList = EmployeeTableDB.GetAllEmployees();
        }

        /// <summary>
        /// Gets the week range containing provided date.
        /// </summary>
        /// <param name="day">Day of week to find week of.</param>
        private void GetWeekRange(DateTime day)
        {
            //clear week dates so property changed attributes go off correctly
            WeekDates.Clear();


            //move back to sunday
            DateTime sunday = day.Subtract(new TimeSpan((int)day.DayOfWeek, 0, 0, 0));
            WeekOfDate = sunday;

            //add sunday to list
            WeekDates.Add(sunday);

            //create list through saturday
            for (int i = 0; i < 6; i++)
            {
                sunday = sunday.AddDays(1);
                WeekDates.Add(sunday);
            }

        }

        public void MoveToCurrentWeek()
        {
            GetWeekRange(DateTime.Now);

            //TODO: get new date range from database
        }

        public void MoveFowardOneWeek()
        {
            var CurrentSunday = WeekDates[0];
            GetWeekRange(CurrentSunday.AddDays(7));

            //TODO: get new date range from database

        }

        public void MoveBackOneWeek()
        {
            var CurrentSunday = WeekDates[0];
            GetWeekRange(CurrentSunday.AddDays(-7));
            //TODO: get new date range from database
        }

        public void AddDaySchedule(DayScheduleVM vm)
        {
            //  Console.WriteLine("{0}, {1} to {2} on day {3}", EmpList.ElementAt(vm.EmployeeIndex).Name, vm.StartTime, vm.EndTime, WeekDates.ElementAt(vm.DayIndex - 1));

            // var currentDay = WeekDates.ElementAt(vm.DayIndex - 1).day;
            //var currentEmployee = EmployeeList.ElementAt(vm.EmployeeIndex);

            //Schedule schdl = new Schedule(currentDay.Month, currentDay.Day, currentDay.Year, vm.StartTime, vm.EndTime);
            //var scheduleId = ScheduleTableDB.AddSchedule(schdl);

            //EmployeeScheduleTableDB.AddEmployeeSchedule(currentEmployee.EmployeeId, scheduleId);
        }

    }
}
