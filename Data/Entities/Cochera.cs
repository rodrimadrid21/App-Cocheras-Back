using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Cochera
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Deshabilitada { get; set; }  // Cambiado a bool
        public bool Eliminada { get; set; }
    }
}
