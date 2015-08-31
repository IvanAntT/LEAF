using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomen_proekt
{
    public class Teacher:Person
    {
        public string Leveltch {get; set;}
        public string Qualification {get; set;}
        public string EGN {get; set;}
        public string Salary {get; set;}

        public Teacher(string name, string family, string email, byte[] photo, string leveltch,
                       string qualification, string egn, string salary):base(name,family,email,photo)
        {
            this.EGN = egn;
            this.Qualification = qualification;
            this.Leveltch = leveltch;
            this.Salary = salary;
        }
    }
}
