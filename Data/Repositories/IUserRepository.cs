using Data.context;
using Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class IUserRepository
    {
        private readonly ApplicationContext _context;

        public IUserRepository(ApplicationContext context)
        {
            _context = context;
        }

        // Autenticación de usuario
        public User? Authenticate(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password && !u.Eliminado);
        }

        // Obtener todos los usuarios que no estén eliminados
        public List<User> GetAllUsers()
        {
            return _context.Users.Where(u => !u.Eliminado).ToList();
        }

        // Obtener usuario por ID
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id && !u.Eliminado);
        }

        // Obtener usuario por Username
        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && !u.Eliminado);
        }

        // Agregar un nuevo usuario
        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        // Actualizar usuario existente
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        // Eliminación lógica de usuario (soft delete)
        public void SoftDeleteUser(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.Eliminado = true;
                _context.SaveChanges();
            }
        }

        // Restaurar usuario (undelete)
        public void UndeleteUser(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Eliminado);
            if (user != null)
            {
                user.Eliminado = false;
                _context.SaveChanges();
            }
        }
    }
}
