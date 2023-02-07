using Project_4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        private Product product = new();
        public Product Product
        {
            get { return product; }
            set { product = value; }
        }
        public ObservableCollection<Product> Products { get; set; } = new();
        private StonksDB db = new StonksDB();
        public Employee()
        {
            InitializeComponent();
            FillDataGrid();
            DataContext= this;

        }
        private void FillDataGrid()
        {
            string resultaaat = db.GetProducts(Products);
            if (resultaaat != "ok")
            {
                MessageBox.Show("Neem contact op met de servicedesk");
            }

        }

        private void CreateBT_Click(object sender, RoutedEventArgs e)
        {
            Create create= new Create();
            create.Show();
            this.Close();
        }

        private void LogoutBT_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
            MessageBox.Show("You have been logged out");
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            Button edtButton = (Button)sender;
            Product edtProduct = (Product)edtButton!.DataContext;
            Edit edit = new Edit(edtProduct);
            edit.Show();
            this.Close();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            Button edtButton = (Button)sender;
            Product edtProduct = (Product)edtButton!.DataContext;

            if (db.DeletePizza(edtProduct.Id.ToString())) //dit verwijdert de student door middel van het id
            {
                MessageBox.Show($"Pizza deleted you didn't like the taste anymore?"); // laat een bericht zien dat id is verwijderd
            }
            else
            {
                MessageBox.Show($"Deleting of pizza failed"); //laat een bericht zien dat het mislukt is
            }

            FillDataGrid();
            Employee employee = new Employee();
            employee.Show();
            this.Close();
            //runt deze functie
        }
    }
}
