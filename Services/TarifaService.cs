using Data.Entities;
using Data.Repositories;
using Common.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class TarifaService
    {
        private readonly ITarifaRepository _repository;
        private readonly IEstacionamientoRepository _estacionamientorepository;

        public TarifaService(ITarifaRepository repository, IEstacionamientoRepository estacionamientorepository)
        {
            _repository = repository;
            _estacionamientorepository = estacionamientorepository;
        }

        // Obtener todas las tarifas como DTOs
        public List<TarifaDto> GetAllTarifas()
        {
            return _repository.GetAllTarifas().Select(t => new TarifaDto
            {
                Id = t.Id,
                Descripcion = t.Descripcion,
                Valor = t.Valor
            }).ToList();
        }

        // Obtener una tarifa por su ID y mapear a DTO
        public TarifaDto GetTarifaById(int id)
        {
            var tarifa = _repository.GetTarifaById(id);
            return tarifa == null ? null : new TarifaDto
            {
                Id = tarifa.Id,
                Descripcion = tarifa.Descripcion,
                Valor = tarifa.Valor
            };
        }

        // Agregar una nueva tarifa desde el DTO
        public int AddTarifa(TarifaDto tarifaDto)
        {
            var tarifa = new Tarifa
            {
                Descripcion = tarifaDto.Descripcion,
                Valor = tarifaDto.Valor
            };
            return _repository.AddTarifa(tarifa);
        }

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
