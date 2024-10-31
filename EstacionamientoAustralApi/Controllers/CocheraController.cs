using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Common.Dtos;
using System.Collections.Generic;

namespace EstacionamientoAustralApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CocheraController : ControllerBase
    {
        private readonly CocheraService _cocheraService;

        public CocheraController(CocheraService cocheraService)
        {
            _cocheraService = cocheraService;
        }

        // Obtener todas las cocheras no eliminadas
        [HttpGet]
        public ActionResult<List<CocheraDto>> GetAllCocheras()
        {
            var cocheras = _cocheraService.GetAllCocheras();
            if (cocheras == null || cocheras.Count == 0)
            {
                return NotFound("No se encontraron cocheras.");
            }

            return Ok(cocheras);
        }

        // Obtener una cochera por ID
        [HttpGet("{id}")]
        public ActionResult<CocheraDto> GetCocheraById(int id)
        {
            var cochera = _cocheraService.GetCocheraById(id);
            if (cochera == null)
            {
                return NotFound($"Cochera con ID {id} no encontrada.");
            }

            return Ok(cochera);
        }

        // Agregar una nueva cochera
        [HttpPost]
        public ActionResult<int> AddCochera([FromBody] CocheraDto cochera)
        {
            if (string.IsNullOrEmpty(cochera.Descripcion))
            {
                return BadRequest("La descripción de la cochera es obligatoria.");
            }

            int cocheraId = _cocheraService.AddCochera(cochera);
            return Ok(new { message = "Cochera agregada con éxito.", cocheraId });
        }


        // Actualizar la descripción de una cochera
        [HttpPut("{id}")]
        public ActionResult UpdateCochera(int id, [FromBody] string descripcion)
        {
            var cochera = _cocheraService.GetCocheraById(id);
            if (cochera == null)
            {
                return NotFound($"Cochera con ID {id} no encontrada.");
            }

            _cocheraService.UpdateCochera(id, descripcion);
            return Ok(new { message = "Cochera actualizada con éxito." });
        }

        // Deshabilitar una cochera
        [HttpPost("disable/{id}")]
        public ActionResult DisableCochera(int id)
        {
            var cochera = _cocheraService.GetCocheraById(id);
            if (cochera == null)
            {
                return NotFound($"Cochera con ID {id} no encontrada.");
            }

            _cocheraService.DisableCochera(id);
            return Ok(new { message = "Cochera deshabilitada con éxito." });
        }

        // Habilitar una cochera
        [HttpPost("enable/{id}")]
        public ActionResult EnableCochera(int id)
        {
            var cochera = _cocheraService.GetCocheraById(id);
            if (cochera == null)
            {
                return NotFound($"Cochera con ID {id} no encontrada.");
            }

            _cocheraService.EnableCochera(id);
            return Ok(new { message = "Cochera habilitada con éxito." });
        }

        // Eliminar (soft delete) una cochera
        [HttpDelete("{id}")]
        public ActionResult SoftDeleteCochera(int id)
        {
            var cochera = _cocheraService.GetCocheraById(id);
            if (cochera == null)
            {
                return NotFound($"Cochera con ID {id} no encontrada.");
            }

            _cocheraService.SoftDeleteCochera(id);
            return Ok(new { message = "Cochera eliminada con éxito." });
        }

        // Restaurar una cochera eliminada
        [HttpPost("undelete/{id}")]
        public ActionResult UndeleteCochera(int id)
        {
            var cochera = _cocheraService.GetCocheraById(id);
            if (cochera == null)
            {
                return NotFound($"Cochera con ID {id} no encontrada.");
            }

            _cocheraService.UndeleteCochera(id);
            return Ok(new { message = "Cochera restaurada con éxito." });
        }
    }
}
