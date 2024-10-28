using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Common.Dtos;
using Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EstacionamientoAustralApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // Registro de un nuevo usuario
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest("El nombre de usuario y la contraseña son obligatorios.");
            }

            var existingUser = _userService.GetUserByUsername(userDto.Username);
            if (existingUser != null)
            {
                return BadRequest("El nombre de usuario ya está en uso.");
            }

            var newUser = new User
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Nombre = userDto.Nombre,
                Apellido = userDto.Apellido,
                EsAdmin = userDto.EsAdmin
            };

            var userId = _userService.AddUser(newUser);
            return Ok(new { message = "Usuario registrado con éxito.", userId });
        }

        // Obtener todos los usuarios
        [HttpGet("all")]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            if (users == null || !users.Any())
            {
                return NotFound("No hay usuarios registrados.");
            }

            var userDtos = users.Select(user => new UserDto
            {
                Username = user.Username,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                EsAdmin = user.EsAdmin
            }).ToList();

            return Ok(userDtos);
        }

        // Obtener un usuario por ID
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"No se encontró un usuario con el ID {id}");
            }

            var userDto = new UserDto
            {
                Username = user.Username,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                EsAdmin = user.EsAdmin
            };

            return Ok(userDto);
        }
        // Obtener un usuario por username
        [HttpGet("by-username/{username}")]
        public ActionResult<UserDto> GetUserByUsername(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound($"No se encontró un usuario con el username '{username}'");
            }

            var userDto = new UserDto
            {
                Username = user.Username,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                EsAdmin = user.EsAdmin
            };

            return Ok(userDto);
        }


        // Actualizar un usuario
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"No se encontró un usuario con el ID {id}");
            }

            user.Nombre = userDto.Nombre;
            user.Apellido = userDto.Apellido;
            user.Password = userDto.Password;
            user.EsAdmin = userDto.EsAdmin;

            _userService.UpdateUser(user);
            return Ok("Usuario actualizado con éxito.");
        }

        // Soft Delete de un usuario
        [HttpDelete("soft/{username}")]
        public IActionResult SoftDeleteUser(string username)
        {
            _userService.SoftDeleteUser(username);
            return Ok("Usuario eliminado lógicamente con éxito.");
        }

        // Restaurar un usuario eliminado
        [HttpPut("undelete/{username}")]
        public IActionResult UndeleteUser(string username)
        {
            _userService.UndeleteUser(username);
            return Ok("Usuario restaurado con éxito.");
        }
    }
}
