using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace passoverProject2
{
    class Program
    {
        static void Main(string[] args)
        {
            menu();
        }
        public static void menu()
        {
            Console.WriteLine("******MENU******");
            Console.WriteLine("1.Select Customer");
            Console.WriteLine("2.New Customer");
            Console.WriteLine("3.Select Supplier");
            Console.WriteLine("4.New Supplier");
            Console.WriteLine("5.Log Table");
            int choise = 0;
            try
            {
                choise = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong typing, Please try again");
                menu();
            }
            switch (choise)
            {
                case 1: // Select Customer
                    string user = EUser();
                    int pass = EPass();                    
                    if (GetCustomer(user, pass) == false)
                    {
                        Console.WriteLine("Invaild user or password");
                        menu();
                    }
                    else
                    {
                        CustomerMenu(user);
                    }
                    break;
                case 2: // New Customer
                    Console.WriteLine("User:");
                    user = Console.ReadLine();
                    Console.WriteLine("Password:");
                    pass = Convert.ToInt32(Console.ReadLine());
                    if (CheakIfCustomerUserExist(user,pass))
                    {
                        Console.WriteLine("User almoust exist");
                        Console.WriteLine();
                        menu();                        
                    }
                    else
                    {
                        Customer c = new Customer(user, pass, EFirstNAme(), ELastNAme(), ECredit());
                    }                    
                    break;
                case 3: // Select Supplier
                    user = EUser();
                    pass = EPass();
                    if (GetSupplier(user, pass) == null)
                    {
                        Console.WriteLine("Invaild user or password");
                        menu();
                    }
                    else
                    {                        
                        Supplier s = GetSupplier(user, pass);
                        s.SupplierMenu(s.id);
                    }
                    break;
                case 4: // New Supplier
                    Console.WriteLine("User:");
                    user = Console.ReadLine();
                    Console.WriteLine("Password:");
                    pass = Convert.ToInt32(Console.ReadLine());                   
                    if (CheakIfSupplierUserExist(user, pass))
                    {
                        Console.WriteLine("User almoust exist");
                        Console.WriteLine();
                        menu();
                    }
                    else
                    {
                        Console.WriteLine("Id:");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Company:");
                        string company = Console.ReadLine();
                        Supplier s = new Supplier(id, user, pass,company);
                    }
                    break;
                case 5:
                    menu();
                    break;
                default:
                    menu();
                    break;
            }                  
        }

        internal static bool CheakIfProductExistInThisSupplier(string productName, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select SUPLLIER_ID FROM PRODUCTS WHERE PRODUCT_NAME = '{productName}'";
            try
            {
                int result = (int)cmd.ExecuteScalar();
                cmd.Connection.Close();
                if (result == id)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CheakIfSupplierUserExist(string user, int pass)
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

       

        public static void SelectProductsById(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM PRODUCTS WHERE SUPLLIER_ID = {id}";
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            while (reader.Read() == true)
            {
                Console.Write("Product: ");
                Console.Write(reader["PRODUCT_NAME"]);
                Console.Write(" Price: ");               
                Console.Write(reader["PRICE"]);
                Console.Write(" Quantity: ");
                Console.Write(reader["QUANTITY"]);
                Console.WriteLine();
            }
        }
            

        public static bool CheakIfProductExistInOtherSupplier(string productName, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select SUPLLIER_ID FROM PRODUCTS WHERE PRODUCT_NAME = '{productName}'";
            try
            {
                int result = (int)cmd.ExecuteScalar();
                cmd.Connection.Close();
                if (result != id)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public static Supplier GetSupplier(string user, int pass)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM SUPPLIERS WHERE [USER] = '{user}' AND PASSWORD = {pass}";
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            while (reader.Read() == true)
            {
                Console.Write($"Welcome Supllier number: {reader["ID"]} ");                          
                Console.WriteLine();
                Supplier supplier = new Supplier((int)reader["ID"], user, pass, (string)reader["COMPANY"]);
                return supplier;
            }
            return null;
        }
        public static bool CheakIfCustomerUserExist(string user, int pass)
        {            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select COUNT ([USER]) FROM CUSTOMERS WHERE [USER] = '{user}'";
            int count = (int)cmd.ExecuteScalar();
            cmd.Connection.Close();
            if (count > 0)
            {
                return true;
            }  
                return false;
        }

        public static bool GetCustomer(string user, int pass)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM CUSTOMERS WHERE [USER] = '{user}' AND PASSWORD = {pass}";
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);            
            while (reader.Read() == true)
            {
                Console.Write("Welcome ");
                Console.Write(reader["FIRST_NAME"]);
                Console.Write(" ");
                Console.Write(reader["LAST_NAME"]);
                Console.WriteLine();
                cmd.Connection.Close();
                return true;
            }
            cmd.Connection.Close();
            return false;
        }
        private static void CustomerMenu(string user)
        {
            Console.WriteLine("Select a number please:");
            Console.WriteLine("1.My Shoppings");
            Console.WriteLine("2.Products List");
            Console.WriteLine("3.Order Producte");
            Console.WriteLine("4.Back to Main Menu");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            int choice = 0;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {               
                CustomerMenu(user);
            }
            
            switch (choice)
            {
                case 1:
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT PRODUCT_NAME,AMOUNT,PRICE,TOTAL_PRICE FROM ORDERS AS O JOIN CUSTOMERS AS C on o.CUSTOMER_ID = c.ID " +
                    $" inner join PRODUCTS AS P ON  P.ID = O.PRODUCT_NUM WHERE C.[USER] = '{user}'";
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    Console.WriteLine("Product   Price  Quantity  Total price");
                    while (reader.Read() == true)
                    {
                        Console.Write(reader["PRODUCT_NAME"]);
                        Console.Write(" ");
                        Console.Write(reader["AMOUNT"]);
                        Console.Write("       ");
                        Console.Write(reader["PRICE"]);
                        Console.Write("          ");
                        Console.WriteLine(reader["TOTAL_PRICE"]);
                    }
                    CustomerMenu(user);
                    break;
                case 2:
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT * FROM PRODUCTS";
                    SqlDataReader reader2 = cmd.ExecuteReader(CommandBehavior.Default);
                    Console.WriteLine("Product   Price  Quantity");
                    while (reader2.Read() == true)
                    {                       
                        Console.Write(reader2["PRODUCT_NAME"]);
                        Console.Write(" ");
                        Console.Write(reader2["PRICE"]);
                        Console.Write("     ");
                        Console.WriteLine(reader2["QUANTITY"]);
                    }
                    CustomerMenu(user);
                    cmd.Connection.Close();
                    break;
                case 3:
                    Console.WriteLine("Product name: ");
                    string productName = Console.ReadLine();
                    cmd.CommandText = $"SELECT QUANTITY FROM PRODUCTS WHERE PRODUCT_NAME = '{productName}'";
                    try
                    {
                        int q = (int)cmd.ExecuteScalar();
                        if (q > 0)
                        {
                            Console.WriteLine($"Insert amount: ");
                            int amount = Convert.ToInt32(Console.ReadLine());
                            if (amount > 0 && amount < q)
                            {                                
                                q = q - amount;
                                cmd.CommandText = $"UPDATE  PRODUCTS set QUANTITY = {q} where PRODUCT_NAME = '{productName}'";
                                cmd.ExecuteNonQuery();
                                cmd.CommandText = $"SELECT ID FROM CUSTOMERS WHERE [USER] = '{user}'";
                                int id = (int)cmd.ExecuteScalar();
                                cmd.CommandText = $"SELECT ID FROM PRODUCTS WHERE PRODUCT_NAME = '{productName}'";
                                int productID = (int)cmd.ExecuteScalar();
                                cmd.CommandText = $"SELECT PRICE FROM PRODUCTS WHERE PRODUCT_NAME = '{productName}'";
                                int price = (int)cmd.ExecuteScalar() * amount;
                                cmd.CommandText = $"INSERT INTO ORDERS VALUES ({id},{productID},{amount},{price})";
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("order succses");
                                Console.ReadLine();
                                CustomerMenu(user);
                            }
                            else
                            {
                                Console.WriteLine($"you can order amount between 0 to {q} ");
                                CustomerMenu(user);
                            }
                        }
                        else
                        {
                            Console.WriteLine("The product not in the stock now");
                            menu();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Product not exist");
                        CustomerMenu(user);
                    }

                    cmd.Connection.Close();
                    break;
                case 4:
                    menu();
                    break;
                default:
                    break;
            }
        }

        private static int ECredit()
        {
            Console.WriteLine("Credit Card:");
            int c = Convert.ToInt32(Console.ReadLine());
            return c;
        }

        private static string ELastNAme()
        {
            Console.WriteLine("Last Name:");
            string l = Console.ReadLine();
            return l;
        }

        private static string EFirstNAme()
        {
            Console.WriteLine("First Name:");
            string f = Console.ReadLine();
            return f;
        }

        private static int EPass()
        {
            Console.WriteLine("Password:");
            try
            {
                int pass = Convert.ToInt32(Console.ReadLine());
                return pass;
            }
            catch (Exception)
            {
                Console.WriteLine("A password can contain only numbers");
                return 0;
            }            
            
        }

        private static string EUser()
        {
            Console.WriteLine("User:");
            string user = Console.ReadLine();            
            return user;
        }
    }
}
