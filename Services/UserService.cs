using Data.Repositories;
using Data.Entities;
using System.Collections.Generic;
using Common.Dtos;

namespace Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        //Autenticación del usuario
        public User Authenticate(string username, string password)
        {
            return _repository.Authenticate(username, password);
        }

        //Obtener todos los usuarios
        public List<UserDto> GetAllUsers()
        {
            return _repository.GetAllUsers()
                .Select(u => new UserDto
                {
                    Username = u.Username,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Password = u.Password,
                    Eliminado = u.Eliminado,
                    EsAdmin = u.EsAdmin
                }).ToList();
        }

        //Obtener un usuario por su ID
        public User GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }

        // Obtener un usuario por su username
        public User GetUserByUsername(string username)
        {
            return _repository.GetUserByUsername(username);
        }

        // Agregar un nuevo usuario
        public int AddUser(User user)
        {
            return _repository.AddUser(user);
        }

        // Actualizar un usuario existente
        public void UpdateUser(User user)
        {
            _repository.UpdateUser(user);
        }

        // Eliminar lógicamente un usuario (soft delete)
        public void SoftDeleteUser(string username)
        {
            _repository.SoftDeleteUser(username);
        }

        // Restaurar un usuario eliminado (undelete)
        public void UndeleteUser(string username)
        {
            _repository.UndeleteUser(username);
        }
    }
}
