using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyScheduler
{
    public class DayScheduleVM
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        /// <summary>
        /// Current Row
        /// </summary>
        public int EmployeeIndex { get; set; }

        /// <summary>
        /// Current Column
        /// </summary>
        public int DayIndex { get; set; }

        public DayScheduleVM()
        {
            StartTime = "";
            EndTime = "";
        }
    }
}
