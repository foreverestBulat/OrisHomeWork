using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetConsoleApp
{
    class Example6
    {
        public static void Use()
        {
            string connectionString = @"Data Source=FOREVEREST;Initial Catalog=usersdb;Integrated Security=True";

            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите возраст:");
            int age = Int32.Parse(Console.ReadLine());

            //string connectionString = @"Data Source=FOREVEREST;Initial Catalog=usersdb;Integrated Security=True";
            //string sqlExpression = "SELECT * FROM Users";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(sqlExpression, connection);
            //    foreach (var item in command.ExecuteReader())
            //    {
            //        Console.WriteLine();
            //    }
            //}

            //string sqlExpressionCount = "SELECT * FROM Users";

            //int count;

            string sqlExpression = String.Format("INSERT INTO Users (id, Name, Age) VALUES ( 4,'{0}', {1})", name, age);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                connection.Open();

                //count = (int)new SqlCommand(sqlExpression, connection).ExecuteScalar();
                //count = countCommand.Exe;
                // добавление
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов: 4", number);

                // обновление ранее добавленного объекта
                Console.WriteLine("Введите новое имя:");
                name = Console.ReadLine();
                sqlExpression = String.Format("UPDATE Users SET Name='{0}' WHERE Age={1}", name, age);
                command.CommandText = sqlExpression;
                number = command.ExecuteNonQuery();
                Console.WriteLine("Обновлено объектов: {0}", number);
            }
            Console.Read();
        }
    }
}