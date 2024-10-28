using Microsoft.AspNetCore.Mvc;
using Services;
using Common.Dtos;
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
        public ActionResult<List<TarifaDto>> GetAllTarifas()
        {
            var tarifas = _tarifaService.GetAllTarifas();
            return tarifas.Count > 0 ? Ok(tarifas) : NotFound("No se encontraron tarifas.");
        }

        // Obtener una tarifa por su ID
        [HttpGet("{id}")]
        public ActionResult<TarifaDto> GetTarifaById(int id)
        {
            var tarifa = _tarifaService.GetTarifaById(id);
            return tarifa == null ? NotFound($"Tarifa con ID {id} no encontrada.") : Ok(tarifa);
        }

        // Agregar una nueva tarifa
        [HttpPost]
        public ActionResult<int> AddTarifa([FromBody] TarifaDto tarifaDto)
        {
            if (tarifaDto == null || string.IsNullOrWhiteSpace(tarifaDto.Descripcion))
                return BadRequest("La descripción de la tarifa es obligatoria.");

            int tarifaId = _tarifaService.AddTarifa(tarifaDto);
            return Ok(new { message = "Tarifa agregada con éxito.", tarifaId });
        }

        // Actualizar el valor de una tarifa
        [HttpPut("{id}")]
        public ActionResult UpdateTarifa(int id, [FromBody] decimal valor)
        {
            var tarifa = _tarifaService.GetTarifaById(id);
            if (tarifa == null)
                return NotFound($"Tarifa con ID {id} no encontrada.");

            _tarifaService.UpdateTarifa(id, valor);
            return Ok(new { message = "Tarifa actualizada con éxito." });
        }

        // Eliminar una tarifa por su ID
        [HttpDelete("{id}")]
        public ActionResult DeleteTarifa(int id)
        {
            var tarifa = _tarifaService.GetTarifaById(id);
            if (tarifa == null)
                return NotFound($"Tarifa con ID {id} no encontrada.");

            _tarifaService.DeleteTarifa(id);
            return Ok(new { message = "Tarifa eliminada con éxito." });
        }
    }
}
