using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace AdoNetConsoleApp
{
    class Example2
    {
        public static void Use()
        {
            ReadFileFromDatabase();
        }

        private static void ReadFileFromDatabase()
        {
            string connectionString = @"Data Source=FOREVEREST;Initial Catalog=usersdb;Integrated Security=True";
            List<Image> images = new List<Image>();

            //List<Image> images = new List<Image>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Images";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //int id = reader.GetInt32(0);
                    string filename = reader.GetString(0);
                    string title = reader.GetString(1);
                    byte[] data = (byte[])reader.GetValue(2);

                    Image image = new Image(0, filename, title, data);
                    images.Add(image);
                }
            }
            // сохраним первый файл из списка
            if (images.Count > 0)
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(images[0].FileName, FileMode.OpenOrCreate))
                {
                    fs.Write(images[0].Data, 0, images[0].Data.Length);
                    Console.WriteLine("Изображение '{0}' сохранено", images[0].Title);
                }
            }
        }
    }
}
