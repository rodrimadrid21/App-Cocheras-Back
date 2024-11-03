using Common.Dtos;
using Data.context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data.Repositories
{
    public class IEstacionamientoRepository
    {
  
        private readonly ApplicationContext _context;
        private readonly ITarifaRepository _tarifaRepository; // Inyección de dependencia
        private readonly ICocheraRepository _cocheraRepository;

        public IEstacionamientoRepository(ApplicationContext context, ITarifaRepository tarifaRepository, ICocheraRepository cocheraRepository)
        {
            _context = context;
            _tarifaRepository = tarifaRepository; // Asignamos el repositorio de tarifas
            _cocheraRepository = cocheraRepository;
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
            var estacionamiento = _context.Estacionamientos
                                  .FirstOrDefault(e => e.Id == id);
            if (estacionamiento != null)
            {
                estacionamiento.Eliminado = true;
                _context.SaveChanges();
                _cocheraRepository.EnableCochera(estacionamiento.IdCochera);
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
        public List<Estacionamiento> GetUltimasTransacciones(int cantidad)
        {
            // Usa Include para cargar la información de la Cochera asociada
            return _context.Estacionamientos
                   .Include(e => e.Cochera)
                   .OrderByDescending(e => e.HoraEgreso)
                   .Take(cantidad)
                   .ToList();

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

        public decimal CalcularCosto(double minutosEstacionados)
        {
            // Obtener tarifas usando el repositorio de tarifas
            var tarifaMediaHora = _tarifaRepository.GetAllTarifas()
                .FirstOrDefault(t => t.Descripcion == "MEDIA HORA");
            var tarifaUnaHora = _tarifaRepository.GetAllTarifas()
                .FirstOrDefault(t => t.Descripcion == "UNA HORA");
            var tarifaValorHora = _tarifaRepository.GetAllTarifas()
                .FirstOrDefault(t => t.Descripcion == "VALOR HORA");

            // Validar que las tarifas no sean nulas antes de usarlas
            if (tarifaMediaHora == null || tarifaUnaHora == null || tarifaValorHora == null)
            {
                throw new InvalidOperationException("Las tarifas requeridas no están definidas en la base de datos.");
            }

            // Calcular el costo según el tiempo estacionado
            if (minutosEstacionados <= 30)
            {
                return tarifaMediaHora.Valor;
            }
            else if (minutosEstacionados <= 60)
            {
                return tarifaUnaHora.Valor;
            }
            else
            {
                // Usamos tarifaValorHora para cada hora adicional después de la primera
                return (decimal)(minutosEstacionados / 60) * tarifaValorHora.Valor;
            }
        }
    }
    
}
