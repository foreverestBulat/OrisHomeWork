﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AdoNetConsoleApp
{
    class Example1
    {
        static string connectionString = @"Data Source=FOREVEREST;Initial Catalog=usersdb;Integrated Security=True";
        public static void Use()
        {
            Console.Write("Введите имя пользователя:");
            string name = Console.ReadLine();

            Console.Write("Введите возраст пользователя:");
            int age = Int32.Parse(Console.ReadLine());

            AddUser(name, age);
            Console.WriteLine();
            GetUsers();

            Console.Read();
        }
        // добавление пользователя
        private static void AddUser(string name, int age)
        {
            // название процедуры
            string sqlExpression = "sp_InsertUser";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = name
                };
                // добавляем параметр
                command.Parameters.Add(nameParam);
                // параметр для ввода возраста
                SqlParameter ageParam = new SqlParameter
                {
                    ParameterName = "@age",
                    Value = age
                };
                command.Parameters.Add(ageParam);

                var result = command.ExecuteScalar();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();

                Console.WriteLine("Id добавленного объекта: {0}", result);
            }
        }

        // вывод всех пользователей
        private static void GetUsers()
        {
            // название процедуры
            string sqlExpression = "sp_GetUsers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int age = reader.GetInt32(2);
                        Console.WriteLine("{0} \t{1} \t{2}", id, name, age);
                    }
                }
                reader.Close();
            }
        }
    }
}
