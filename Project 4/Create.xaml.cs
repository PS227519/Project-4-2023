using Project_4.Models;
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

namespace Project_4
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        int Category = 1;
        int id;
        private StonksDB db = new StonksDB();
        public Create()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
            this.Close();
        }

        private void BTCreate_Click(object sender, RoutedEventArgs e)
        {
            if (db.InsertPizza(TBNaam.Text, TBDescription.Text, TBPrice.Text, Category, id)) 
            {
                MessageBox.Show(TBNaam.Text + " Has been Created");
                Employee employee = new Employee();
                employee.Show();
                this.Close();
            }
        }
    }
}
