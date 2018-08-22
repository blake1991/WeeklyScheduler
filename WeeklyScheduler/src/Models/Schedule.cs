namespace WeeklyScheduler
{
    public class Schedule
    {
        public int ScheduleId { get; set; } = -1;
        public int Month { get; set; } = 0;
        public int Day { get; set; } = 0;
        public int Year { get; set; } = 0;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;


        public override string ToString()
        {
            return StartTime + " to " + EndTime;
        }
    }
}
