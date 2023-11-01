using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// async use

namespace AdoNetConsoleApp
{
    internal class Example3
    {
        public static void Use()
        {
            ConnectWithDB().GetAwaiter();
        }

        private static async Task ConnectWithDB()
        {
            string connectionString = @"Data Source=FOREVEREST;Initial Catalog=usersdb;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Подключение открыто");
            }
            Console.WriteLine("Подключение закрыто...");
        }
    }
}
