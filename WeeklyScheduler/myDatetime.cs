using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyScheduler
{
    public class myDatetime : BaseViewModel
    {
        public DateTime day { get; set; }

        public myDatetime(DateTime day)
        {
            this.day = day;
        }

        public override string ToString()
        {
            return day.ToString("ddd M/d");
        }

        public void AddWeek()
        {
            day = day.AddDays(7);
        }

        public void SubtractWeek()
        {
            day = day.AddDays(-7);
        }
    }
}
