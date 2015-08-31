using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomen_proekt
{
    public class Manager:Person
    {
        public string Password { get; set; }

        public Manager(string name, string family, string email, byte[] photo,
                       string password):base(name, family, email, photo)
        {
            this.Password = password;
        }
    }
}
