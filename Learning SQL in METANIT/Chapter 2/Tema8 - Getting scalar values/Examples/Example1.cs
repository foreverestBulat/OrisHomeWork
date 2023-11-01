using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdoNetConsoleApp
{
    class Example1
    {
        public static void Use()
        {
            string connectionString = @"Data Source=FOREVEREST;Initial Catalog=usersdb;Integrated Security=True";

            string sqlExpression = "SELECT COUNT(*) FROM Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                object count = command.ExecuteScalar();

                command.CommandText = "SELECT MIN(Age) FROM Users";
                object minAge = command.ExecuteScalar();

                Console.WriteLine("В таблице {0} объектов", count);
                Console.WriteLine("Минимальный возраст: {0}", minAge);
            }
        }
    }
}
