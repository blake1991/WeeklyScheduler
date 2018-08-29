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
        public ObservableCollection<Employee> EmpList { get; set; }

        public string SampleText { get; set; }

        //temporary name
        public ObservableCollection<EmployeeSchedule> testEmp { get; set; }

        /// <summary>
        /// Collection of weekly dates starting at sunday ending at saturday
        /// </summary>
        public ObservableCollection<myDatetime> WeekDates { get; set; }

        public Schedule testGet { get; set; }

        public WeeklyScheduleVM()
        {
            // EmpList = EmployeeTableDB.GetAllEmployees();
            testEmp = new ObservableCollection<EmployeeSchedule>();
            WeekDates = new ObservableCollection<myDatetime>();

            Refresh();

            SetDaysOfWeek();

        }

        public void Refresh()
        {
            EmpList = EmployeeTableDB.GetAllEmployees();

            //dummy foreach to test add employee input box
            foreach (var employee in EmpList)
            {
                EmployeeSchedule WeeklySchedule = new EmployeeSchedule();
                WeeklySchedule.employee = employee;

                WeeklySchedule.days.Add(new Schedule() { Day = 2, Month = 3, Year = 2018, StartTime = "9", EndTime = "5" });
                WeeklySchedule.days.Add(new Schedule() { Day = 2, Month = 3, Year = 2018, StartTime = "8", EndTime = "3" });
                WeeklySchedule.days.Add(new Schedule() { Day = 2, Month = 3, Year = 2018, StartTime = "off", EndTime = "off" });
                WeeklySchedule.days.Add(new Schedule() { Day = 2, Month = 3, Year = 2018, StartTime = "11", EndTime = "7" });
                WeeklySchedule.days.Add(new Schedule() { Day = 2, Month = 3, Year = 2018, StartTime = "9", EndTime = "5" });
                WeeklySchedule.days.Add(new Schedule() { Day = 2, Month = 3, Year = 2018, StartTime = "off", EndTime = "off" });
                WeeklySchedule.days.Add(new Schedule() { Day = 2, Month = 3, Year = 2018, StartTime = "9", EndTime = "5" });
                testEmp.Add(WeeklySchedule);
            }
        }

        private void SetDaysOfWeek()
        {
            //get today
            DateTime today = DateTime.Now;

            //go back to previous sunday
            TimeSpan fullday = new TimeSpan((int)today.DayOfWeek, 0, 0, 0);
            today = today.Subtract(fullday);

            //add sunday date to list
            WeekDates.Add(new myDatetime(today));

            //start at sunday and create monday through saturday
            for (int i = 0; i < 6; i++)
            {
                today = today.AddDays(1);
                WeekDates.Add(new myDatetime(today));
            }
        }

        public void MoveToCurrentWeek()
        {
            //create new list to proc notifychanged property
            WeekDates = new ObservableCollection<myDatetime>();
            //rebuild current week list
            SetDaysOfWeek();

            //TODO: get new date range from database
        }

        public void MoveFowardOneWeek()
        {
            ObservableCollection<myDatetime> newWeekDates = new ObservableCollection<myDatetime>();
            foreach (var date in WeekDates)
            {
                date.AddWeek();
                newWeekDates.Add(date);
            }
            WeekDates = newWeekDates;
            //TODO: get new date range from database
        }

        public void MoveBackOneWeek()
        {
            ObservableCollection<myDatetime> newWeekDates = new ObservableCollection<myDatetime>();
            foreach (var date in WeekDates)
            {
                date.SubtractWeek();
                newWeekDates.Add(date);
            }
            WeekDates = newWeekDates;
            //TODO: get new date range from database
        }

        public void AddDaySchedule(DayScheduleVM vm)
        {
          //  Console.WriteLine("{0}, {1} to {2} on day {3}", EmpList.ElementAt(vm.EmployeeIndex).Name, vm.StartTime, vm.EndTime, WeekDates.ElementAt(vm.DayIndex - 1));

            var currentDay = WeekDates.ElementAt(vm.DayIndex - 1).day;
            var currentEmployee = EmpList.ElementAt(vm.EmployeeIndex);

            Schedule schdl = new Schedule(currentDay.Month, currentDay.Day, currentDay.Year, vm.StartTime, vm.EndTime);
            var scheduleId = ScheduleTableDB.AddSchedule(schdl);

            EmployeeScheduleTableDB.AddEmployeeSchedule(currentEmployee.EmployeeId, scheduleId);

            var dbdump = EmployeeScheduleTableDB.GetAllEmployeeSchedules();
        }

    }
}
