using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4.Models
{
    public class Product
    {
        private ulong id;
        public ulong Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name = null!;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string? description;
        public string? Description
        {
            get { return description; }
            set { description = value; }
        }
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public decimal PriceInEuro
        {
            get { return price / 100.0m; }
            set { price = value * 100.0m; }
        }


        private string createAdd;
        public string CreateAdd
        {
            get { return createAdd; }
            set { createAdd = value; }
        }
        private string updateAdd;
        public string UpdateAdd
        {
            get { return updateAdd; }
            set
            {
                updateAdd = value;
            }
        }
    }

}
