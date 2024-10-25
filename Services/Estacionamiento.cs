using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class EstacionamientoService
    {
        private readonly IEstacionamientoRepository _repository;

        public EstacionamientoService(IEstacionamientoRepository repository)
        {
            _repository = repository;
        }

        // Obtener todos los estacionamientos que no han sido eliminados
        public List<Estacionamiento> GetAllEstacionamientos()
        {
            return _repository.GetAllEstacionamientos();
        }

        // Obtener un estacionamiento por su ID
        public Estacionamiento GetEstacionamientoById(int id)
        {
            return _repository.GetEstacionamientoById(id);
        }

        // Agregar un nuevo estacionamiento
        public int AddEstacionamiento(Estacionamiento estacionamiento)
        {
            return _repository.AddEstacionamiento(estacionamiento);
        }

        // Actualizar un estacionamiento existente
        public void UpdateEstacionamiento(Estacionamiento estacionamiento)
        {
            _repository.UpdateEstacionamiento(estacionamiento);
        }

        // Eliminar un estacionamiento (soft delete)
        public void DeleteEstacionamiento(int id)
        {
            _repository.DeleteEstacionamiento(id);
        }

        // Abrir una cochera
        public int AbrirEstacionamiento(string patente, int idUsuarioIngreso, int idCochera)
        {
            return _repository.AbrirEstacionamiento(patente, idUsuarioIngreso, idCochera);
        }

        // Cerrar una cochera y calcular el costo
        public void CerrarEstacionamiento(string patente, int idUsuarioEgreso)
        {
            _repository.CerrarEstacionamiento(patente, idUsuarioEgreso);
        }
    }
}
