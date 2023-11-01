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
            int age = 23;
            string name = "T',10);INSERT INTO Users (Name, Age) VALUES('H";
            string sqlExpression = "INSERT INTO Users (Name, Age) VALUES (@name, @age)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметр для имени
                SqlParameter nameParam = new SqlParameter("@name", name);
                // добавляем параметр к команде
                command.Parameters.Add(nameParam);
                // создаем параметр для возраста
                SqlParameter ageParam = new SqlParameter("@age", age);
                // добавляем параметр к команде
                command.Parameters.Add(ageParam);

                int number = command.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов: {0}", number); // 1
            }
        }
    }
}
