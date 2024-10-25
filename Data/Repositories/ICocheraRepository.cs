using Data.context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class ICocheraRepository
    {
        private readonly ApplicationContext _context;

        public ICocheraRepository(ApplicationContext context)
        {
            _context = context;
        }

        // Obtener todas las cocheras que no estén eliminadas
        public List<Cochera> GetAllCocheras()
        {
            return _context.Cocheras.Where(c => !c.Eliminada).ToList();
        }

        // Obtener cochera por ID si no está eliminada
        public Cochera GetCocheraById(int id)
        {
            return _context.Cocheras.FirstOrDefault(c => c.Id == id && !c.Eliminada);
        }

        // Agregar una nueva cochera
        public int AddCochera(Cochera cochera)
        {
            _context.Cocheras.Add(cochera);
            _context.SaveChanges();
            return cochera.Id;
        }

        // Actualizar la descripción de una cochera existente
        public void UpdateCochera(int id, string descripcion)
        {
            var cochera = _context.Cocheras.Find(id);
            if (cochera != null && !cochera.Eliminada)
            {
                cochera.Descripcion = descripcion;
                _context.SaveChanges();
            }
        }

        // Deshabilitar una cochera (marcarla como deshabilitada)
        public void DisableCochera(int id)
        {
            var cochera = _context.Cocheras.Find(id);
            if (cochera != null && !cochera.Eliminada)
            {
                cochera.Deshabilitada = true;
                _context.SaveChanges();
            }
        }

        // Habilitar una cochera (quitar la deshabilitación)
        public void EnableCochera(int id)
        {
            var cochera = _context.Cocheras.Find(id);
            if (cochera != null && !cochera.Eliminada)
            {
                cochera.Deshabilitada = false;
                _context.SaveChanges();
            }
        }

        // Eliminar una cochera (soft delete, marcando como eliminada)
        public void SoftDeleteCochera(int id)
        {
            var cochera = _context.Cocheras.Find(id);
            if (cochera != null)
            {
                cochera.Eliminada = true;
                _context.SaveChanges();
            }
        }

        // Restaurar una cochera eliminada
        public void UndeleteCochera(int id)
        {
            var cochera = _context.Cocheras.Find(id);
            if (cochera != null && cochera.Eliminada)
            {
                cochera.Eliminada = false;
                _context.SaveChanges();
            }
        }
    }
}
