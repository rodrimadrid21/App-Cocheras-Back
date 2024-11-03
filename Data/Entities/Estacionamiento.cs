using System;

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

        // Agregamos la propiedad de navegación hacia Cochera para poder acceder a sus detalles
        public Cochera Cochera { get; set; } // Nueva propiedad de navegación
    }
}
