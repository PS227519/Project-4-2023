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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project_4.Models;
using System.Collections.ObjectModel;

namespace Project_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal totaalprijs;   
        public decimal Totaalprijs
        {
            get { return totaalprijs; }
            set { totaalprijs = value; }
        }

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; }
        }

        public ObservableCollection<Product> Products { get; set; } = new();
        private StonksDB db= new StonksDB();
        public MainWindow()
        {
            InitializeComponent();
            InitializeProgram();
            DataContext = this;
        }

        private void InitializeProgram()
        {
            string resultaat = db.GetProducts(Products);
            if (resultaat != "ok")
            {
                MessageBox.Show(resultaat);
            }

        }

        private void LoginBT_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void LV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (selectedProduct != null )
            {
                List<string> Pizzas = new List<string>();
                Pizzas.Add(selectedProduct.ToString());
                decimal Pizza = selectedProduct.PriceInEuro;
                MessageBox.Show(SelectedProduct.Name + " has been added");
                LBBestelling.Items.Add(SelectedProduct.Name);
                Optellen(Pizza,Pizzas);
            }
            
        }

        private void Optellen(decimal pizza, List<string> pizzas)
        {
            foreach (string a in pizzas)
            {
                Totaalprijs += pizza;
            }
            TBTotaalprijs.Text = Totaalprijs.ToString();
        }

        private void OrderBT_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Your order is empty please pick something before ordering");
            }
            else
            {
                MessageBox.Show("Your order has been placed");
                MainWindow Home = new MainWindow();
                Home.Show();
                this.Close();
            }
            
        }

        private void LBBestelling_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete "+ LBBestelling.SelectedItem +"?", "MessageBox", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show(LBBestelling.SelectedItem + " has been removed from your order");
                LBBestelling.Items.Remove(LBBestelling.SelectedItem);
            }
                
        }
    }
}
