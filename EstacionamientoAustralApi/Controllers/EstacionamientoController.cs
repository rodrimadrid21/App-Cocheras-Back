using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

namespace EstacionamientoAustralApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionamientoController : ControllerBase
    {
        private readonly EstacionamientoService _estacionamientoService;

        public EstacionamientoController(EstacionamientoService estacionamientoService)
        {
            _estacionamientoService = estacionamientoService;
        }

        // Obtener todos los estacionamientos no eliminados
        [HttpGet]
        public ActionResult<List<Estacionamiento>> GetAllEstacionamientos()
        {
            var estacionamientos = _estacionamientoService.GetAllEstacionamientos();
            if (estacionamientos == null || estacionamientos.Count == 0)
            {
                return NotFound("No se encontraron estacionamientos.");
            }

            return Ok(estacionamientos);
        }

        // Obtener un estacionamiento por su ID
        [HttpGet("{id}")]
        public ActionResult<Estacionamiento> GetEstacionamientoById(int id)
        {
            var estacionamiento = _estacionamientoService.GetEstacionamientoById(id);
            if (estacionamiento == null)
            {
                return NotFound($"Estacionamiento con ID {id} no encontrado.");
            }

            return Ok(estacionamiento);
        }

        // Agregar un nuevo estacionamiento
        [HttpPost]
        public ActionResult<int> AddEstacionamiento([FromBody] Estacionamiento estacionamiento)
        {
            if (estacionamiento == null || estacionamiento.Patente == null || estacionamiento.IdCochera == 0)
            {
                return BadRequest("Faltan datos obligatorios (Patente, IdCochera).");
            }

            int estacionamientoId = _estacionamientoService.AddEstacionamiento(estacionamiento);
            return Ok(new { message = "Estacionamiento agregado con éxito.", estacionamientoId });
        }

        // Actualizar un estacionamiento existente
        [HttpPut("{id}")]
        public ActionResult UpdateEstacionamiento(int id, [FromBody] Estacionamiento estacionamiento)
        {
            var existingEstacionamiento = _estacionamientoService.GetEstacionamientoById(id);
            if (existingEstacionamiento == null)
            {
                return NotFound($"Estacionamiento con ID {id} no encontrado.");
            }

            estacionamiento.Id = id;  // Asegurarse de que el ID es el mismo
            _estacionamientoService.UpdateEstacionamiento(estacionamiento);
            return Ok(new { message = "Estacionamiento actualizado con éxito." });
        }

        // Eliminar un estacionamiento (soft delete)
        [HttpDelete("{id}")]
        public ActionResult DeleteEstacionamiento(int id)
        {
            var estacionamiento = _estacionamientoService.GetEstacionamientoById(id);
            if (estacionamiento == null)
            {
                return NotFound($"Estacionamiento con ID {id} no encontrado.");
            }

            _estacionamientoService.DeleteEstacionamiento(id);
            return Ok(new { message = "Estacionamiento eliminado con éxito." });
        }

        // Abrir una cochera
        [HttpPost("abrir")]
        public ActionResult<int> AbrirEstacionamiento([FromBody] AbrirEstacionamientoDto abrirDto)
        {
            if (abrirDto == null || string.IsNullOrEmpty(abrirDto.Patente) || abrirDto.IdCochera == 0 || abrirDto.IdUsuarioIngreso == 0)
            {
                return BadRequest("Faltan datos obligatorios para abrir el estacionamiento (Patente, IdUsuarioIngreso, IdCochera).");
            }

            int estacionamientoId = _estacionamientoService.AbrirEstacionamiento(abrirDto.Patente, abrirDto.IdUsuarioIngreso, abrirDto.IdCochera);
            return Ok(new { message = "Cochera abierta con éxito.", estacionamientoId });
        }

        // Cerrar una cochera
        [HttpPost("cerrar")]
        public ActionResult CerrarEstacionamiento([FromBody] CerrarEstacionamientoDto cerrarDto)
        {
            if (cerrarDto == null || string.IsNullOrEmpty(cerrarDto.Patente) || cerrarDto.IdUsuarioEgreso == 0)
            {
                return BadRequest("Faltan datos obligatorios para cerrar el estacionamiento (Patente, IdUsuarioEgreso).");
            }

            _estacionamientoService.CerrarEstacionamiento(cerrarDto.Patente, cerrarDto.IdUsuarioEgreso);
            return Ok(new { message = "Cochera cerrada con éxito." });
        }
    }

    // DTOs utilizados para abrir y cerrar estacionamientos
    public class AbrirEstacionamientoDto
    {
        public string Patente { get; set; }
        public int IdUsuarioIngreso { get; set; }
        public int IdCochera { get; set; }
    }

    public class CerrarEstacionamientoDto
    {
        public string Patente { get; set; }
        public int IdUsuarioEgreso { get; set; }
    }
}
