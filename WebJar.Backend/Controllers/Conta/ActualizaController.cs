using Microsoft.AspNetCore.Mvc;
using WebJar.Backend.Repositories.Interfaces.Conta;

namespace WebJar.Backend.Controllers.Conta
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActualizaController : ControllerBase
    {
        private readonly IActualizarContaRepository _actualizarContaRepository;

        public ActualizaController(IActualizarContaRepository actualizarContaRepository)
        {
            _actualizarContaRepository = actualizarContaRepository;
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> PostAsync([FromQuery] int empresaId, [FromQuery] int elMes, [FromQuery] int elYear)
        {
            try
            {
                // Llamar al método del repositorio para actualizar las cuentas
                await _actualizarContaRepository.ActualizarConta(empresaId, elMes, elYear);

                // Devolver una respuesta exitosa
                return Ok("Las cuentas se actualizaron correctamente.");
            }
            catch (Exception ex)
            {
                // Devolver un error en caso de excepción
                return BadRequest($"Error al actualizar las cuentas: {ex.Message}");
            }
        }
    }
}