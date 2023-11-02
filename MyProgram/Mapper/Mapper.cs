using MyProgram.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProgram.Mapper
{
    public class Mapper
    {
        public static string DeductStudent(string name)
        {
            return "Здравствуйте, @{name}, вы отчислены".Replace("@{name}", name);
        }

        public static string GetAddress(object obj)
        {
            string phrase = "Здравствуйте, @{name}. Вы прописаны по адресу @{address}";

            var type = obj.GetType();

            foreach (var field in type.GetFields())
            {
                var replace = "@{" + field.Name + "}";
                phrase = phrase.Replace(replace, field.GetValue(obj).ToString());
            }

            return phrase;
        }

        public static string GetRole(object obj)
        {
            string phrase = "Здравствуйте, @{name}. ";

            Type type = obj.GetType();
            double? temp = null;
            foreach (var field in type.GetFields())
            {
                var replace = "@{" + field.Name + "}";
                phrase = phrase.Replace(replace, field.GetValue(obj).ToString());
                if (field.Name == "temp" && field.FieldType == typeof(double))
                    temp = (double)field.GetValue(obj);
            }

            if (temp != null)
                phrase += temp > 37 ? "Выздоравливайте" : "Прогульщица";
            
            return phrase;
        }

        public static string GetBallsAllStudents(object obj)
        {
            string phrase = "Здравствуйте, студенты группы @{group}. Ваши баллы по ОРИС:\n";
            //@{student} @{item.FIO} баллы: {item.balls}"

            var objectStr = new StringBuilder("");

            Type type = obj.GetType();

            foreach (var field in type.GetFields())
            {
                if (field.FieldType.IsArray)
                    foreach (var fieldItem in field.GetValue(obj) as Array)
                    {
                        var typeItem = fieldItem.GetType();
                        foreach (var fieldTypeItem in typeItem.GetFields())
                            if (fieldTypeItem.Name == "fio" || (fieldTypeItem.Name == "balls" && fieldTypeItem.FieldType == typeof(int)))
                            {
                                //objectStr += ("{0}: {1}\n", fieldTypeItem.Name, fieldTypeItem.GetValue(fieldItem));
                                objectStr.Append(fieldTypeItem.Name);
                                objectStr.Append(": ");
                                objectStr.Append(fieldTypeItem.GetValue(fieldItem));
                                objectStr.Append("\n");
                            }
                    }

                var replace = "@{" + field.Name + "}";
                phrase = phrase.Replace(replace, field.GetValue(obj).ToString());
            }

            return phrase + objectStr.ToString();
        }
    }
}
