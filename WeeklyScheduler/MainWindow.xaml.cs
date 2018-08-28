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
using WeeklyScheduler.Dialog;
using System.Windows.Controls.Primitives;

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
            NameInputDialog input = new NameInputDialog();
            NameInputVM nameVM = new NameInputVM();
            input.DataContext = nameVM;

            // open dialog box to prompt for new employee input
            if (input.ShowDialog() == true)
            {
                Employee emp = new Employee();
                emp.Name = nameVM.Name;
                EmployeeTableDB.AddEmployee(emp);
            }

            vm.Refresh();
        }

        private void MoveForwardOneWeek_click(object sender, RoutedEventArgs e)
        {
            vm.MoveFowardOneWeek();
        }

        private void MoveBackOneWeek_click(object sender, RoutedEventArgs e)
        {
            vm.MoveBackOneWeek();
        }

        private void MoveToCurrentWeek_click(object sender, RoutedEventArgs e)
        {
            vm.MoveToCurrentWeek();
        }

        private void test_editending(object sender, DataGridCellEditEndingEventArgs e)
        {
            Console.WriteLine("edit ending");
            Console.WriteLine(sender.GetType());
            var noclue = sender as DataGrid;
            Console.WriteLine(noclue.ActualHeight);

        }

        /// <summary>
        /// Find the row and column index of a clicked cell in a datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while (dep != null && !(dep is DataGridCell))
            {
                dep = VisualTreeHelper.GetParent(dep);
                Console.WriteLine(dep);
            }

            if (dep == null)
                return;

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;

                // navigate further up the tree
                while ((dep != null) && !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                DataGridRow row = dep as DataGridRow;

                //TODO: these are the row/column indices
                //use these to update the collection with a new time range provided through a dialog box
                Console.WriteLine("Row: {0} Column: {1}", row.GetIndex(), cell.Column.DisplayIndex);
                
            }
        }
    }
}
