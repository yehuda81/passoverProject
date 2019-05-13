using System;
using System.Data;
using System.Data.SqlClient;

namespace passoverProject2
{
    internal class Product
    {
        public int id { get; set; }
        public string productName { get; set; }
        public int supplierId { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

        public Product(int id, string productName, int supplierId, int price, int quantity)
        {
            this.id = id;
            this.productName = productName;
            this.supplierId = supplierId;
            this.price = price;
            this.quantity = quantity;
            AddProduct();
        }
        private void AddProduct()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO PRODUCTS VALUES({id},'{productName}',{supplierId},{price},{quantity})";
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            Console.WriteLine("Product added");
            Console.ReadLine();
            Program.menu();
        }
       

    }
}