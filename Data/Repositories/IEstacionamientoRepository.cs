using Data.context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class IEstacionamientoRepository
    {
  
        private readonly ApplicationContext _context;
        private readonly ITarifaRepository _tarifaRepository; // Inyección de dependencia

        public IEstacionamientoRepository(ApplicationContext context, ITarifaRepository tarifaRepository)
        {
            _context = context;
            _tarifaRepository = tarifaRepository; // Asignamos el repositorio de tarifas
        }

        public List<Estacionamiento> GetAllEstacionamientos()
        {
            return _context.Estacionamientos.Where(e => !e.Eliminado).ToList();
        }

        public Estacionamiento GetEstacionamientoById(int id)
        {
            return _context.Estacionamientos.FirstOrDefault(e => e.Id == id && !e.Eliminado);
        }

        public int AddEstacionamiento(Estacionamiento estacionamiento)
        {
            _context.Estacionamientos.Add(estacionamiento);
            _context.SaveChanges();
            return estacionamiento.Id;
        }

        public void UpdateEstacionamiento(Estacionamiento estacionamiento)
        {
            _context.Estacionamientos.Update(estacionamiento);
            _context.SaveChanges();
        }

        public void DeleteEstacionamiento(int id)
        {
            var estacionamiento = _context.Estacionamientos.Find(id);
            if (estacionamiento != null)
            {
                estacionamiento.Eliminado = true;
                _context.SaveChanges();
            }
        }

        // Nueva funcionalidad: abrir cochera
        public int AbrirEstacionamiento(string patente, int idUsuarioIngreso, int idCochera)
        {
            // Verificar si la cochera ya está ocupada
            var cocheraOcupada = _context.Estacionamientos
                .Any(e => e.IdCochera == idCochera && e.HoraEgreso == null && !e.Eliminado);

            if (cocheraOcupada)
            {
                throw new InvalidOperationException("La cochera ya está ocupada.");
            }

            // Registrar el ingreso del coche
            var nuevoEstacionamiento = new Estacionamiento
            {
                Patente = patente,
                IdUsuarioIngreso = idUsuarioIngreso,
                IdCochera = idCochera,
                HoraIngreso = DateTime.Now
            };

            _context.Estacionamientos.Add(nuevoEstacionamiento);
            _context.SaveChanges();
            return nuevoEstacionamiento.Id;
        }

        // Nueva funcionalidad: cerrar cochera
        public void CerrarEstacionamiento(string patente, int idUsuarioEgreso)
        {
            // Encontrar el estacionamiento abierto para la patente dada
            var estacionamiento = _context.Estacionamientos
                .FirstOrDefault(e => e.Patente == patente && e.HoraEgreso == null && !e.Eliminado);

            if (estacionamiento == null)
            {
                throw new InvalidOperationException("No hay un estacionamiento activo para la patente dada.");
            }

            // Calcular el tiempo de estacionamiento
            var tiempoEstacionado = DateTime.Now - estacionamiento.HoraIngreso;
            var minutosEstacionados = tiempoEstacionado.TotalMinutes;

            // Definir el costo basado en las tarifas
            decimal costo = CalcularCosto(minutosEstacionados);

            // Registrar el egreso
            estacionamiento.HoraEgreso = DateTime.Now;
            estacionamiento.IdUsuarioEgreso = idUsuarioEgreso;
            estacionamiento.Costo = costo;

            _context.Estacionamientos.Update(estacionamiento);
            _context.SaveChanges();
        }

        private decimal CalcularCosto(double minutosEstacionados)
        {
            // Obtener tarifas usando el repositorio de tarifas
            var tarifaMediaHora = _tarifaRepository.GetAllTarifas()
                .FirstOrDefault(t => t.Descripcion == "Media Hora");
            var tarifaHora = _tarifaRepository.GetAllTarifas()
                .FirstOrDefault(t => t.Descripcion == "1 Hora");


            // Validar que las tarifas no sean nulas antes de usarlas
            if (tarifaMediaHora == null || tarifaHora == null)
            {
                throw new InvalidOperationException("Las tarifas requeridas no están definidas en la base de datos.");
            }

            // Calcular el costo según el tiempo estacionado
            if (minutosEstacionados <= 30)
            {
                return tarifaMediaHora.Valor;
            }
            else
            {
                return (decimal)(minutosEstacionados / 60) * tarifaHora.Valor;
            }
        }
    }
}
