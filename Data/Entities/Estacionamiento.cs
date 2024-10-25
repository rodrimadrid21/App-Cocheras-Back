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
        public string Patente { get; set; } // La patente del auto
        public DateTime HoraIngreso { get; set; } // Se puede cambiar a DateTime para manejar mejor las fechas
        public DateTime? HoraEgreso { get; set; } // Se puede cambiar a DateTime para manejar mejor las fechas
        public decimal Costo { get; set; } // Costo del estacionamiento
        public int IdUsuarioIngreso { get; set; } // Usuario que ingresó el vehículo
        public int? IdUsuarioEgreso { get; set; } // Usuario que retiró el vehículo
        public int IdCochera { get; set; } // Id de la cochera ocupada
        public bool Eliminado { get; set; } // Para control de soft delete
    }
}

