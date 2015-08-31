using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomen_proekt
{
    abstract public class Person
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public byte[] Photo {get; set;}

        public Person (string name, string family, string email, byte[] photo)
        {
            this.Name = name;
            this.Family = family;
            this.Email = email;
            this.Photo = photo;
        }
        
    
    }
}
