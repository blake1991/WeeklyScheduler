namespace WeeklyScheduler
{
    /// <summary>
    /// A work schedule for a single day. 
    /// </summary>
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }


        public override string ToString()
        {
            StartTime = StartTime.ToLower();
            EndTime = EndTime.ToLower();

            if (StartTime.Equals("off") || EndTime.Equals("off"))
                return "off";
            else if (!(StartTime == string.Empty) || !(EndTime == string.Empty))
                return StartTime + " to " + EndTime;
            else
                return "";
        }

        public Schedule()
        {
            ScheduleId = -1;
            Month = 0;
            Day = 0;
            Year = 0;
            StartTime = string.Empty;
            EndTime = string.Empty;
        }

        public Schedule(int Month, int Day, int Year, string StartTime, string EndTime)
        {
            this.Month = Month;
            this.Day = Day;
            this.Year = Year;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }
    }
}
