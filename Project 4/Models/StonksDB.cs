using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project_4.Models
{
    public class StonksDB
    {
        
        private string connString = ConfigurationManager.ConnectionStrings["Project4"].ConnectionString;
        public string GetProducts(ICollection<Product> products)
        {
            
            if (products == null) { 
                throw new ArgumentException("Ongeldig argument bij gebruik van GetProducts"); 
            }
            string methodResult = "unknown"; 
            using (MySqlConnection conn = new(connString)){
                try{
                    conn.Open(); 
                    MySqlCommand sql = conn.CreateCommand(); 
                    sql.CommandText = @"
                         SELECT p.id, p.name, p.description, p.price, p.category_id, p.created_at, p.updated_at
                           FROM products p"; 
                    
                    MySqlDataReader reader = sql.ExecuteReader(); 
                    while (reader.Read()) {
                        Product product = new Product()
                        {
                            Id = (ulong)reader["id"],
                            Name = (string)reader["name"],
                            Description = reader["description"] == DBNull.Value 
                                       ? null 
                                       : (string)reader["description"],
                            
                            Price = (decimal)reader["price"],
                            CreateAdd = reader["created_at"]== DBNull.Value ? null :  reader["created_at"].ToString(),
                            UpdateAdd = reader["updated_at"]==DBNull.Value ? null : reader["updated_at"] .ToString(),
                        };
                        products.Add(product);
                    }
                    methodResult = "ok";
                }catch (Exception e) { 
                    Console.Error.WriteLine(nameof(GetProducts)); 
                    Console.Error.WriteLine(e.Message); 
                    methodResult = e.Message; 
                }
            }
            return methodResult;
        }
        public int Inloggen(string Email, string Password)
        {
            int id = 0;
            using (MySqlConnection conn = new(connString))
                try
                {
                    conn.Open();
                    MySqlCommand command = conn.CreateCommand();
                    command.CommandText = "SELECT id FROM `users` WHERE `Email` = @Email AND `Password` = @Password"; //selecteerd het playerid van de tabel account als het wacht woord en het e-mail overeenkomen
                    command.Parameters.AddWithValue("@Email", Email); //maakt parameters aan, eigenlijk is dit er gwn om te zorgen dat je var's in een command kunt gebruiken
                    command.Parameters.AddWithValue("@Password", Password);
                    id = int.Parse(command.ExecuteScalar().ToString()); //de id is de uitkomst van de command in een string
                }
                catch (Exception)
                {

                }
                finally
                {
                    conn.Close();
                }
            return id;
        }
        public DataTable SelectPizza()
        {
            DataTable result = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connString))
                try
                {
                    conn.Open();//opent een connectie
                    MySqlCommand command = conn.CreateCommand(); //maakt een command aan die geconneced is met de database
                    command.CommandText = "SELECT * FROM Products ;"; // selecteerd alles van de tabel friends
                    MySqlDataReader reader = command.ExecuteReader(); //voert het command out
                    result.Load(reader); // laat de uitkomst van de command zien op het scherm
                }
                catch (Exception)
                {
                    //Problem with the database

                }
                finally
                {
                    conn.Close(); //sluit de connectie
                }
            return result;
        }
        public bool UpdateProducts(string PizzaName, string Description, decimal Price, ulong Pizzaid)
        {

                bool succes = false;
                DataTable result = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(connString))
                try
                {
                    conn.Open();
                    MySqlCommand command = conn.CreateCommand();
                    command.CommandText = "UPDATE `Products` SET `Name` = @PizzaName, `Description` = @Description, `Price` = @Price *100 WHERE `id` = @id";//Update de tabel friends door de playername, nickname en favorite te veranderen waar friends . friendsid @id is
                    command.Parameters.AddWithValue("@PizzaName", PizzaName);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@Price",Price);
                    command.Parameters.AddWithValue("@id", Pizzaid);
                    int nrOfRowsAffected = command.ExecuteNonQuery();
                    succes = (nrOfRowsAffected != 0);
                }
                catch (Exception e)
                {
                    //Problem with the database
                }
                finally
                {
                    conn.Close();
                }
                return succes;
        }
        public bool DeletePizza(string id)
        {
            bool succes = false;
            DataTable result = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connString))
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM `Products` WHERE `id` = @id;"; //verwijderd alles van friends waar het friendsid @id is
                command.Parameters.AddWithValue("@id", id);
                int nrOfRowsAffected = command.ExecuteNonQuery();
                succes = (nrOfRowsAffected != 0);
            }
            catch (Exception)
            {
                //Problem with the database
            }
            finally
            {
                conn.Close();
            }
            return succes;
        }
        internal bool InsertPizza(string Name, string description, string price, int category, int id)
        {
            bool succes = false; //maakt een bool succes aan
            DataTable result = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connString))
                try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO `Products` (`id`, `name`, `description`, `price`,`image`,`category_id`) VALUES (@Id, @Name, @Description, @Price, NULL, @category) ";// voegt een playerid, playername, Nickname, favorite, friendsid toe aan de tabel friends
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@category", category);
                    int nrOfRowsAffected = command.ExecuteNonQuery(); //de int is de uitkomst van de command
                succes = (nrOfRowsAffected != 0);// zet de bool succes weer op true als de command goed uitgevoerd wordt
            }
            catch (Exception)
            {
                //Problem with the database
            }
            finally
            {
                conn.Close();
            }
            return succes;
        }

    }
}
