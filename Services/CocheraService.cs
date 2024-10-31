using Data.Entities;
using Data.Repositories;
using System.Collections.Generic;
using Common.Dtos;

namespace Services
{
    public class CocheraService
    {
        private readonly ICocheraRepository _repository;

        public CocheraService(ICocheraRepository repository)
        {
            _repository = repository;
        }

        // Obtener todas las cocheras
        public List<CocheraDto> GetAllCocheras()
        {
            return _repository.GetAllCocheras()
                              .Select(cochera => new CocheraDto
                              {
                                  Id = cochera.Id,
                                  Descripcion = cochera.Descripcion,
                                  Deshabilitada = cochera.Deshabilitada,
                                  Eliminada = cochera.Eliminada,
                                  // Agrega aquí las demás propiedades según tu DTO
                              }).ToList();
        }

        // Obtener una cochera por ID
        public CocheraDto GetCocheraById(int id)
        {
            var cochera = _repository.GetCocheraById(id);
            if (cochera == null) return null;

            return new CocheraDto
            {
                Id = cochera.Id,
                Descripcion = cochera.Descripcion,
                Deshabilitada = cochera.Deshabilitada,
                Eliminada = cochera.Eliminada,
                // Agrega aquí las demás propiedades según tu DTO
            };
        }

        // Agregar una nueva cochera
        public int AddCochera(CocheraDto cocheraDto)
        {
            var cochera = new Cochera
            {
                Descripcion = cocheraDto.Descripcion,
                Deshabilitada = cocheraDto.Deshabilitada,
                Eliminada = cocheraDto.Eliminada,
                // Agrega aquí las demás propiedades necesarias
            };

            return _repository.AddCochera(cochera);
        }


        // Actualizar la descripción de una cochera
        public void UpdateCochera(int id, string descripcion)
        {
            _repository.UpdateCochera(id, descripcion);
        }

        // Deshabilitar una cochera
        public void DisableCochera(int id)
        {
            _repository.DisableCochera(id);
        }

        // Habilitar una cochera
        public void EnableCochera(int id)
        {
            _repository.EnableCochera(id);
        }

        // Eliminar (soft delete) una cochera
        public void SoftDeleteCochera(int id)
        {
            _repository.SoftDeleteCochera(id);
        }

        // Restaurar una cochera eliminada
        public void UndeleteCochera(int id)
        {
            _repository.UndeleteCochera(id);
        }
    }
}