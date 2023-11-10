using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServerCopy.Model
{
    public class Form
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDay { get; set; }
        public string Number { get; set; }
        public string Mail { get; set; }

        public override string ToString()
        {
            return $"{Name} {LastName}";
        }
    }
}
