using System.Collections.ObjectModel;
using System.ComponentModel;
using WeeklyScheduler.src.Data_Access;
using WeeklyScheduler.src.Models;

namespace WeeklyScheduler
{
    public class WeeklyScheduleVM : BaseViewModel
    {
        public ObservableCollection<Employee> EmpList{ get; set; }

        public string SampleText{ get; set; }

        public ObservableCollection<EmployeeSchedule> testEmp { get; set; } 

        public WeeklyScheduleVM()
        {
            EmpList = EmployeeTableDB.GetAllEmployees();
            testEmp = new ObservableCollection<EmployeeSchedule>();


            EmployeeSchedule temp;


            for(int i = 0; i<5; i++)
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



            var testGet = ScheduleTableDB.GetScheduleById(1);
            
        }

        public void Refresh()
        {
            EmpList = EmployeeTableDB.GetAllEmployees();
        }


    }
}
