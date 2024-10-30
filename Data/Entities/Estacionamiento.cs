using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Estacionamiento
    {
        public int Id { get; set; }
        public string Patente { get; set; }
        public DateTime HoraIngreso { get; set; }
        public DateTime? HoraEgreso { get; set; }
        public decimal Costo { get; set; }
        public int IdUsuarioIngreso { get; set; }
        public int IdUsuarioEgreso { get; set; }
        public int IdCochera { get; set; }
        public bool Eliminado { get; set; }
    }
}
