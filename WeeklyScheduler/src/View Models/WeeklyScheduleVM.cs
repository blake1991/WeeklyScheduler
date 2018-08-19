using System.Collections.ObjectModel;
using System.ComponentModel;
using WeeklyScheduler.src.Data_Access;
using WeeklyScheduler.src.Models;

namespace WeeklyScheduler.src.View_Models
{
    public class WeeklyScheduleVM : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> EmpList { get; set; }

        public WeeklyScheduleVM()
        {
            EmpList = EmployeeTableDB.GetAllEmployees();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
