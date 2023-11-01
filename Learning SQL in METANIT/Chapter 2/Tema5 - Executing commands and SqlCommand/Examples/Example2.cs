using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetConsoleApp
{
    class Example2
    {
        public static void Use()
        {
            string connectionString = @"Data Source=FOREVEREST;Initial Catalog=usersdb;Integrated Security=True";
            string sqlExpression = "SELECT * FROM Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                foreach (var item in command.ExecuteReader())
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
