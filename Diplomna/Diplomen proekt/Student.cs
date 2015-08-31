using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomen_proekt
{
    public class Student:Person
    {
        public string Password { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Level { get; set; }
        public string Birthday { get; set; }

        public Student(string name, string family, string email, byte[] photo,
                       string password, string city, string phone, string level, string birthday)
            : base(name, family, email, photo)
        {
            this.Password = password;
            this.City = city;
            this.Phone = phone;
            this.Level = level;
            this.Birthday = birthday;
        }
    }
}
