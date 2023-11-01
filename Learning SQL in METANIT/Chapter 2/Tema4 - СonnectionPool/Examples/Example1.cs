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
            string connectionString2 = @"Data Source=FOREVEREST;Initial Catalog=players;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // создается первый пул
                Console.WriteLine(connection.ClientConnectionId);
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // подключение извлекается из первого пула
                Console.WriteLine(connection.ClientConnectionId);
            }
            using (SqlConnection connection = new SqlConnection(connectionString2))
            {
                connection.Open(); // создается второй пул, т.к. строка подключения отличается
                Console.WriteLine(connection.ClientConnectionId);
            }
        }
    }
}
