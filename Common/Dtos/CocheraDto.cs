using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class CocheraDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Deshabilitada { get; set; }
        public bool Eliminada { get; set; }
    }

}
