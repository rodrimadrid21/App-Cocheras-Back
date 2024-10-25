using Data.Entities;
using Data.Repositories;
using System.Collections.Generic;

namespace Services
{
    public class CocheraService
    {
        private readonly ICocheraRepository _repository;

        public CocheraService(ICocheraRepository repository)
        {
            _repository = repository;
        }

        // Obtener todas las cocheras no eliminadas
        public List<Cochera> GetAllCocheras()
        {
            return _repository.GetAllCocheras();
        }

        // Obtener una cochera por ID
        public Cochera GetCocheraById(int id)
        {
            return _repository.GetCocheraById(id);
        }

        // Agregar una nueva cochera
        public int AddCochera(Cochera cochera)
        {
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
