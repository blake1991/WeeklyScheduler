using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeeklyScheduler.src.Data_Access;
using WeeklyScheduler.src.Models;
using WeeklyScheduler.src;


namespace WeeklyScheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WeeklyScheduleVM vm;

        public MainWindow()
        {
            InitializeComponent();
            SchedulerDBContext.CreateDatabase();

            vm = new WeeklyScheduleVM();
            
            base.DataContext = vm;

        }



        // https://stackoverflow.com/questions/5661034/printing-in-c-sharp-wpf
        // Test code 
        private void PrintSchedule(object sender, RoutedEventArgs e)
        {
            PrintDialog pDialog = new PrintDialog();

            if (pDialog.ShowDialog() == true)
            {

                //print landscape
                pDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;


                FlowDocument fd = new FlowDocument();
                Paragraph p = new Paragraph(new Run("this is a title"));

                //  fd.Blocks.Add(ScheduleDG);
                Table tab = new Table();
                TableRowGroup tg = new TableRowGroup();
                TableRow r = new TableRow();

                fd.PageWidth = pDialog.PrintableAreaWidth;
                fd.PageHeight = pDialog.PrintableAreaHeight;
                fd.BringIntoView();

                fd.ColumnWidth = 800;


                for (int i = 0; i < 10; i++)
                {
                    r.Cells.Add(new TableCell(new Paragraph(new Run("Header"))));
                    r.Cells[i].ColumnSpan = 4;
                    r.Cells[i].Padding = new Thickness(4);

                    r.Cells[i].BorderBrush = Brushes.Black;
                    r.Cells[i].FontWeight = FontWeights.Bold;
                    r.Cells[i].Background = Brushes.DarkGray;
                    r.Cells[i].Foreground = Brushes.White;
                    r.Cells[i].BorderThickness = new Thickness(1, 1, 1, 1);
                }

                tg.Rows.Add(r);
                tab.RowGroups.Add(tg);


                fd.Blocks.Add(p);
                fd.Blocks.Add(tab);


                // pDialog.PrintVisual(ScheduleDG, "GAAAHH!");
                pDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");

            }
        }

        private void OnWindowLoad(object sender, RoutedEventArgs e)
        {
            SchedulerDBContext.CreateDatabase();
        }

        private void AddNewEmployee(object sender, RoutedEventArgs e)
        {
            //TODO: open dialog to add new employee



            //simulates dialog to add new employee
            Employee emp = new Employee();
            emp.Name = ":facepalm:";

            EmployeeTableDB.AddEmployee(emp);
            vm.Refresh();
            //int id = EmployeeTableDB.AddEmployee(emp);
            //vm.EmpList.Add(EmployeeTableDB.GetEmployee(id));
            vm.SampleText = "A button changed this.";
   
        }
    }
}
