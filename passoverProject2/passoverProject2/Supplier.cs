using System;
using System.Data;
using System.Data.SqlClient;

namespace passoverProject2
{
    internal class Supplier
    {
        public int id { get; set; }
        public string user { get; set; }
        public int pass { get; set; }
        public string company { get; set; }

        public Supplier(int id, string user, int pass, string company)
        {
            this.id = id;
            this.user = user;
            this.pass = pass;
            this.company = company;
            if (CheakIfSupplierUserExist(user))
            {
                SupplierMenu(id);
            }
            else
            {
                AddSupplier();
            }
            
            
        }

        private void AddSupplier()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO SUPPLIERS VALUES('{user}',{pass},'{company}')";
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            Console.WriteLine("Supplier added");
            Console.ReadLine();
            Program.menu();
        }
        public static bool CheakIfSupplierUserExist(string user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select COUNT ([USER]) FROM SUPPLIERS WHERE [USER] = '{user}'";
            int count = (int)cmd.ExecuteScalar();
            cmd.Connection.Close();
            if (count > 0)
            {
                return true;
            }

            return false;

        }
        public void SupplierMenu(int id)
        {
            Console.WriteLine("1.Add Product");
            Console.WriteLine("2.My Stock");
            Console.WriteLine("3.Back to Menu");

            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1: // Add Product
                    Console.WriteLine("Product name:");
                    string productName = Console.ReadLine();
                    if (Program.CheakIfProductExistInOtherSupplier(productName, this.id))
                    {
                        Console.WriteLine("Product exist in other supplier");
                        Console.WriteLine();
                        SupplierMenu(this.id);
                    }
                    else
                    {
                        if (Program.CheakIfProductExistInThisSupplier(productName, this.id))
                        {
                            Console.WriteLine("quantity:");
                            int quantity = Convert.ToInt32(Console.ReadLine());
                            AddQuantity(id, productName, quantity);
                            SupplierMenu(this.id);
                            
                        }
                        else
                        {
                            Console.WriteLine("id:");
                            int productID = Convert.ToInt32(Console.ReadLine());                            
                                while (CheakIfproductIDExist(productID) == true)
                                {
                                    Console.WriteLine("This number exists in another product");
                                    Console.WriteLine("id:");
                                    productID = Convert.ToInt32(Console.ReadLine());
                            }                            
                            int supllierID = this.id;
                            Console.WriteLine("price:");
                            int price = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("quantity:");
                            int quantity = Convert.ToInt32(Console.ReadLine());
                            Product p = new Product(productID, productName, supllierID, price, quantity);
                        }
                    }
                    break;
                case 2:                    
                    Program.SelectProductsById(this.id);
                    break;
                case 3:
                    Program.menu();
                    break;
                default:
                    Console.WriteLine("");
                    break;
            }
        }

        private bool CheakIfproductIDExist(int productID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT ID FROM PRODUCTS WHERE ID = '{productID}'";            
            try
            {
                int r = (int)cmd.ExecuteScalar();                
                return true;             
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AddQuantity(int id, string productName,int quantity)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();            
            cmd.CommandType = CommandType.Text;           
            cmd.CommandText = $"SELECT QUANTITY FROM PRODUCTS WHERE PRODUCT_NAME = '{productName}'";
            try
            {
                int q = (int)cmd.ExecuteScalar();
                q = q + quantity;
                cmd.CommandText = $"UPDATE  PRODUCTS set QUANTITY = {q} where PRODUCT_NAME = '{productName}'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)            {

                Console.WriteLine("Product does not exist");
            }
           
            cmd.Connection.Close();
            Console.WriteLine("quantity updated");
            Console.ReadLine();
            Program.menu();
        }
    }
}