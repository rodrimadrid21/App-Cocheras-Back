using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

namespace EstacionamientoAustralApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifaController : ControllerBase
    {
        private readonly TarifaService _tarifaService;

        public TarifaController(TarifaService tarifaService)
        {
            _tarifaService = tarifaService;
        }

        // Obtener todas las tarifas
        [HttpGet]
        public ActionResult<List<Tarifa>> GetAllTarifas()
        {
            var tarifas = _tarifaService.GetAllTarifas();
            if (tarifas == null || tarifas.Count == 0)
            {
                return NotFound("No se encontraron tarifas.");
            }

            return Ok(tarifas);
        }

        // Obtener una tarifa por su ID
        [HttpGet("{id}")]
        public ActionResult<Tarifa> GetTarifaById(int id)
        {
            var tarifa = _tarifaService.GetTarifaById(id);
            if (tarifa == null)
            {
                return NotFound($"Tarifa con ID {id} no encontrada.");
            }

            return Ok(tarifa);
        }

        // Agregar una nueva tarifa
        [HttpPost]
        public ActionResult<int> AddTarifa([FromBody] Tarifa tarifa)
        {
            if (tarifa == null || tarifa.Descripcion == null)
            {
                return BadRequest("La descripción de la tarifa es obligatoria.");
            }

            int tarifaId = _tarifaService.AddTarifa(tarifa);
            return Ok(new { message = "Tarifa agregada con éxito.", tarifaId });
        }

        // Actualizar el valor de una tarifa
        [HttpPut("{id}")]
        public ActionResult UpdateTarifa(int id, [FromBody] decimal valor)
        {
            var tarifa = _tarifaService.GetTarifaById(id);
            if (tarifa == null)
            {
                return NotFound($"Tarifa con ID {id} no encontrada.");
            }

            _tarifaService.UpdateTarifa(id, valor);
            return Ok(new { message = "Tarifa actualizada con éxito." });
        }

        // Eliminar una tarifa por su ID
        [HttpDelete("{id}")]
        public ActionResult DeleteTarifa(int id)
        {
            var tarifa = _tarifaService.GetTarifaById(id);
            if (tarifa == null)
            {
                return NotFound($"Tarifa con ID {id} no encontrada.");
            }

            _tarifaService.DeleteTarifa(id);
            return Ok(new { message = "Tarifa eliminada con éxito." });
        }
    }
}
