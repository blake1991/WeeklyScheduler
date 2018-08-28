using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyScheduler
{
    class NameInputVM : BaseViewModel
    {
        public string Name { get; set; }
        public string DialogText { get; set; } 

        public NameInputVM()
        {
            this.Name = "";
            this.DialogText = "Enter name:";
        }
    }
}
