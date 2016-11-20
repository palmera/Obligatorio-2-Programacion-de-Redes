using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Models
{
    [Serializable]
    public class Administrator : IEquatable<Administrator>
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public Administrator(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }
        private Administrator() { }

        public bool Equals(Administrator other)
        {
            return other.Name == this.Name && other.Password == this.Password;
        }
    }
    
}
