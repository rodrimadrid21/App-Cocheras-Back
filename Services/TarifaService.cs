using Data.Entities;
using Data.Repositories;
using System.Collections.Generic;

namespace Services
{
    public class TarifaService
    {
        private readonly ITarifaRepository _repository;

        public TarifaService(ITarifaRepository repository)
        {
            _repository = repository;
        }

        // Obtener todas las tarifas
        public List<Tarifa> GetAllTarifas()
        {
            return _repository.GetAllTarifas();
        }

        // Obtener una tarifa por su ID
        public Tarifa GetTarifaById(int id)
        {
            return _repository.GetTarifaById(id);
        }

        // Agregar una nueva tarifa
        public int AddTarifa(Tarifa tarifa)
        {
            return _repository.AddTarifa(tarifa);
        }

        // Actualizar el valor de una tarifa existente
        public void UpdateTarifa(int id, decimal valor)
        {
            _repository.UpdateTarifa(id, valor);
        }

        // Eliminar una tarifa existente
        public void DeleteTarifa(int id)
        {
            _repository.DeleteTarifa(id);
        }
    }
}
