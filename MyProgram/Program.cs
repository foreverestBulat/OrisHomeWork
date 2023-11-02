using MyProgram.Classes;
using MyProgram.Mapper;
using System.Net.Sockets;

namespace ny_program
{
    public class Program
    {
        public static void Main()
        {
            var student = new Student()
            {
                name = "Leisan",
                fio = "Nonckaya Leisan",
                address = "Kazan",
                temp = 37,
                balls = 99
            };

            var students = new Student[]
            {
                student,
                new Student()
                {
                    name = "Bulat",
                    fio = "Subukhankulov Bulat Ilhatovich",
                    address = "Kazan",
                    temp = 36,
                    balls = 90
                },
                new Student()
                {
                    name = "Ivan",
                    fio = "Ivanov Ivan Ivanovich",
                    address = "Moscow",
                    temp = 38,
                    balls = 57
                }
            };

            var group = new Group()
            {
                group = "11-208",
                students = students
            };

            Console.WriteLine(Mapper.DeductStudent("Leisan"));
            Console.WriteLine(Mapper.GetAddress(student));
            Console.WriteLine(Mapper.GetRole(student));
            Console.WriteLine(Mapper.GetBallsAllStudents(group));
        }
    }
}