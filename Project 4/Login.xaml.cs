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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        int id;
        public Login()
        {
            InitializeComponent();
            TBemail.Focus();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            StonksDB stonksDB = new StonksDB();
            string Email = TBemail.Text; //de string Email is wat je hebt ingevuld bij de email textbox
            string Password = TBpassword.Password.ToString(); //de string password is wat je hebt ingevuld bij het wachtwoord
            int id = stonksDB.Inloggen(Email, Password); //het id is de uitkomst van de functie inloggen geeft ook de 2 vars mee
            this.id = id;
            if (id > 0) //als het id groter is dan 0 runt deze if
            {
                MessageBox.Show($"Welcome home sir");//iron man style welkom
                Employee login = new Employee();
                login.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show($"INTRUDER ALERT!!!!");//als het wachtwoord en email niet kloppen ben je een intruder....
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }
    }
}
