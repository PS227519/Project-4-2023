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
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        int id;
        private Product product = new ();
        public Product Product
        {
            get { return product; }
            set { product = value; }
        }
    
        private StonksDB db = new StonksDB();
        public Edit(Product product)
        {
            InitializeComponent();
            this.product.Id= product.Id;
            this.product.Price = product.Price;
            this.product.Description = product.Description;
            this.product.Name = product.Name;
            DataContext = this;
        }
        private void UpdatePizza_Click(object sender, RoutedEventArgs e)
        {
            
            if (db.UpdateProducts(product.Name, product.Description, product.PriceInEuro, product.Id))//runt de functie update friend en geeft de friendid en wat je hebt ingevuld mee
            {
                MessageBox.Show($"Pizza has edited");//laat een message zien en laat zien welke je hebt veranderd
            }
            else
            {
                MessageBox.Show($"Failed to edit");//laat een message zien en welke het niet gelukt is te veranderen
            }
            Employee employee = new Employee();
            employee.Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
            this.Close();
        }
    }
}
