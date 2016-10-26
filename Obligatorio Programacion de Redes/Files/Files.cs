using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Programacion_de_Redes.Files
{
    class Files
    {
        public string path { get; set; }
        public string name { get; set; }
        public List<string> readers { get; set; }
        public string writer { get; set; }



        public void finishWriting()
        {
            this.writer = "";
        }

        public bool isBeingEdited()
        {
            return this.writer != "";
        }

        public bool isBeingRead()
        {
            return readers.Count != 0;
        }

        public Files()
        {
            readers = new List<string>();
            this.writer = "";
        }





    }
}