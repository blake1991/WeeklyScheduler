using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeeklyScheduler.Dialog
{
    /// <summary>
    /// Interaction logic for NameInputDialog.xaml
    /// </summary>
    public partial class NameInputDialog : Window
    {
        public NameInputDialog()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void NameInputDialog_ContentRendered(object sender, EventArgs e)
        {
            NameInput.Focus();
        }
    }
}
