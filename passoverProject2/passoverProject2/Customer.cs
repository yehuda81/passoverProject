using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace passoverProject2
{
    class Customer : ICustomer
    {
        public string user { get; set; }
        public int pass { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int creditCard { get; set; }
        //string user;
        //int pass;
        //string firstName;
        //string lastName;
        //int creditCard;

        public Customer(string user, int pass, string firstName, string lastName, int creditCard)
        {
            this.user = user;
            this.pass = pass;
            this.firstName = firstName;
            this.lastName = lastName;
            this.creditCard = creditCard;
            AddCustomer();                    
        }

        private void AddCustomer()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=passover2;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO CUSTOMERS VALUES('{user}',{pass},'{firstName}','{lastName}',{creditCard})";
            cmd.ExecuteNonQuery(); 
            cmd.Connection.Close();
            Console.WriteLine("Customer added");
            Console.ReadLine();
            Program.menu();
        }

        
        public Customer(string user, int pass)
        {
        }

        public override string ToString()
        {
            return $"Name: {firstName}  { lastName}, user: {user} ";
        }
    }
    }
