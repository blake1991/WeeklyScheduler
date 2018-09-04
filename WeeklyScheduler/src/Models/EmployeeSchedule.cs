using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyScheduler.src.Models
{
    public class EmployeeSchedule
    {
        public Employee employee { get; set; }
        public ObservableCollection<Schedule> days { get; set; }

        public EmployeeSchedule(Employee emp)
        {
            employee = emp;
            days = new ObservableCollection<Schedule>();
        }
    }
}
