using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Files
{
    public class ClientFile
    {
        public string name { get; set; }
        public string path { get; set; }

        public bool editable { get; set; }
        public bool readable { get; set; }

        public ClientFile(string name,string path)
        {
            this.name = name;
            this.path = path;
        }
        public override bool Equals(object obj)
        {
            return name.Equals(((ClientFile)obj).name);
        }

        public string getStatus()
        {
            if (editable && readable)
                return "Habilitado para Lectura y escritura";
            if (editable)
                return "Habilitado para escritura";
            if (readable)
                return "Habilitado para Lectura";
            else
                return "No esta habilitado, por favor elimine el archivo";
            
        }

    }
}