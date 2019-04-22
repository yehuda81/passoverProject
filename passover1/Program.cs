using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace passover1
{
    class Program
    {
        static void Main(string[] args)
        {
            int x;
            int y;
            do
            {
                Console.WriteLine("Insert 2 numbers please:");
                x = Convert.ToInt32(Console.ReadLine());
                y = Convert.ToInt32(Console.ReadLine());
                SqlCommand cmd = new SqlCommand("ADDX");
                SqlCommand cmd2 = new SqlCommand("ADDY");
                cmd.Parameters.Add(new SqlParameter("@x", x));
                cmd2.Parameters.Add(new SqlParameter("@y", y));
                cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=Calculator;Integrated Security=True");
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd2.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=Calculator;Integrated Security=True");
                cmd2.Connection.Open();
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.ExecuteNonQuery();
                cmd2.Connection.Close();
            }
            while (x > 0 && y > 0);
            SqlCommand cmdR = new SqlCommand("SHOW_RESULT");
            cmdR.Connection = new SqlConnection(@"Data Source=LAPTOP-SPGETJPN;Initial Catalog=Calculator;Integrated Security=True");
            cmdR.Connection.Open();
            cmdR.CommandType = CommandType.StoredProcedure;
            cmdR.ExecuteNonQuery();
            cmdR.CommandType = CommandType.Text;
            cmdR.CommandText = "SELECT * FROM Table_R";
            SqlDataReader reader = cmdR.ExecuteReader();
            while (reader.Read() == true)
            {
                x = Convert.ToInt32(reader["X"]);
                y = Convert.ToInt32(reader["Y"]);                
                int result = x + y;
                if ((string)reader["Operation"] == "-") 
                {
                    result = x - y;
                }
                if ((string)reader["Operation"] == "*")
                {
                    result = x * y;
                }
                if ((string)reader["Operation"] == "/")
                {
                    if (y == 0)
                    {
                        continue;
                    }
                    result = x / y;
                }

                Console.WriteLine($" {reader["X"]} {reader["Operation"]} {reader["Y"]} {result}");
            }
            cmdR.Connection.Close(); 
            
        }
    }
}
