using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using WeeklyScheduler.src.Data_Access;
using WeeklyScheduler.src.Models;

namespace WeeklyScheduler
{
    public class WeeklyScheduleVM : BaseViewModel
    {
        /// <summary>
        /// List of Employees and their daily schedules for the week.
        /// </summary>
        public ObservableCollection<EmployeeSchedule> EmployeeSchedules { get; set; }

        /// <summary>
        /// Collection of weekly dates starting at sunday ending at saturday. Used to re-label data grid headers.
        /// </summary>
        public ObservableCollection<DateTime> WeekDates { get; set; }

        /// <summary>
        /// The date of that current weeks Sunday.
        /// </summary>
        public DateTime WeekOfDate { get; set; }


        private Dictionary<int, EmployeeSchedule> dictionary;


        public WeeklyScheduleVM()
        {
            EmployeeSchedules = new ObservableCollection<EmployeeSchedule>();
            WeekDates = new ObservableCollection<DateTime>();


            // CreateDummyData();

            GetWeekRange(DateTime.Now);
            GetEmployeeSchedules();

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
            GetEmployeeSchedules();
        }

        public void MoveFowardOneWeek()
        {
            var CurrentSunday = WeekDates[0];
            GetWeekRange(CurrentSunday.AddDays(7));
            GetEmployeeSchedules();
        }

        public void MoveBackOneWeek()
        {
            var CurrentSunday = WeekDates[0];
            GetWeekRange(CurrentSunday.AddDays(-7));
            GetEmployeeSchedules();
        }

        public void AddDaySchedule(DayScheduleVM vm)
        {
            //  Console.WriteLine("{0}, {1} to {2} on day {3}", EmpList.ElementAt(vm.EmployeeIndex).Name, vm.StartTime, vm.EndTime, WeekDates.ElementAt(vm.DayIndex - 1));

            var currentDay = WeekDates.ElementAt(vm.DayIndex - 1);
            var currentEmployee = EmployeeSchedules.ElementAt(vm.EmployeeIndex).employee;

            Schedule schdl = new Schedule(currentDay.Month, currentDay.Day, currentDay.Year, vm.StartTime, vm.EndTime);

            //get new schedule id from database
            var scheduleId = ScheduleTableDB.AddSchedule(schdl);

            //add employee id and schedule id to linking table
            EmployeeScheduleTableDB.AddEmployeeSchedule(currentEmployee.EmployeeId, scheduleId);
        }

        public void GetEmployeeSchedules()
        {
            var schdl_list = EmployeeScheduleTableDB.GetAllEmployeeSchedules();
            dictionary = new Dictionary<int, EmployeeSchedule>();

            //add all employees to dictionary
            foreach (var schdl in schdl_list)
            {
                Employee emp = EmployeeTableDB.GetEmployee(schdl.Item1);

                if (!dictionary.ContainsKey(emp.EmployeeId))
                    dictionary.Add(emp.EmployeeId, new EmployeeSchedule(emp));
            }

            //add all employee schedules to dictionary
            foreach (var schdl in schdl_list)
            {
                Schedule schedule = ScheduleTableDB.GetScheduleById(schdl.Item2);

                if (dictionary.ContainsKey(schdl.Item1))
                {

                    //place schedule in correct day slot
                    for(int i = 0; i < WeekDates.Count; i++)
                    {
                        if(WeekDates[i].Day == schedule.Day && WeekDates[i].Month == schedule.Month && WeekDates[i].Year == schedule.Year)
                        {
                            dictionary[schdl.Item1].days.RemoveAt(i);
                            dictionary[schdl.Item1].days.Insert(i, schedule);
                        }
                        else
                        {
                           // dictionary[schdl.Item1].days.Add(new Schedule());
                        }
                    }


                }
            }

            //empty out current schedules
            EmployeeSchedules.Clear();

            foreach (var emp in dictionary)
            {
                EmployeeSchedules.Add(emp.Value);
            }
        }

        public void CreateDummyData()
        {
            Random rand = new Random();
            foreach (var employee in EmployeeTableDB.GetAllEmployees())
            {
                EmployeeSchedule schdl = new EmployeeSchedule(employee);
                for (int i = 0; i < 7; i++)
                {
                    Schedule sun = new Schedule(1, 1, 1, rand.Next(12).ToString(), rand.Next(12).ToString());
                    schdl.days.Add(sun);

                }
                EmployeeSchedules.Add(schdl);
            }
        }

    }
}
