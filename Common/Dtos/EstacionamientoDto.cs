using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class EstacionamientoDto
    {
        public int Id { get; set; }
        public string Patente { get; set; }
        public string HoraIngreso { get; set; }
        public string HoraEgreso { get; set; }
        public decimal Costo { get; set; }
        public int IdUsuarioIngreso { get; set; }
        public int IdUsuarioEgreso { get; set; }
        public int IdCochera { get; set; }
        public bool Eliminado { get; set; }
    }
}
