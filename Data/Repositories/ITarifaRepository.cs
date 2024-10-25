using Data.context;
using Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class ITarifaRepository
    {
        private readonly ApplicationContext _context;

        public ITarifaRepository(ApplicationContext context)
        {
            _context = context;
        }

        // Obtener todas las tarifas
        public List<Tarifa> GetAllTarifas()
        {
            return _context.Tarifas.ToList();
        }

        // Obtener tarifa por ID
        public Tarifa GetTarifaById(int id)
        {
            return _context.Tarifas.FirstOrDefault(t => t.Id == id);
        }

        // Agregar una nueva tarifa
        public int AddTarifa(Tarifa tarifa)
        {
            _context.Tarifas.Add(tarifa);
            _context.SaveChanges();
            return tarifa.Id;
        }

        // Actualizar tarifa existente
        public void UpdateTarifa(int id, decimal valor)
        {
            var tarifa = _context.Tarifas.FirstOrDefault(t => t.Id == id);
            if (tarifa != null)
            {
                tarifa.Valor = valor;  // Solo actualizamos el valor de la tarifa
                _context.SaveChanges();
            }
        }

        // Eliminar tarifa (eliminación física en este caso)
        public void DeleteTarifa(int id)
        {
            var tarifa = _context.Tarifas.Find(id);
            if (tarifa != null)
            {
                _context.Tarifas.Remove(tarifa);
                _context.SaveChanges();
            }
        }
    }
}
