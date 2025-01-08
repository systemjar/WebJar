using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebJar.Backend.UnitOfWork.Implementations.Conta;
using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Backend.Controllers.Conta
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PolizaController : GenericController<Poliza>
    {
        private readonly IPolizasUnitOfWork _polizasUnitOfWork;

        public PolizaController(IGenericUnitOfWork<Poliza> unitOfWork, IPolizasUnitOfWork polizasUnitOfWork) : base(unitOfWork)
        {
            _polizasUnitOfWork = polizasUnitOfWork;
        }

        [HttpGet("{Id}")]
        public override async Task<IActionResult> GetAsync(int Id)
        {
            var response = await _polizasUnitOfWork.GetAsync(Id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound();
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _polizasUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("existe")]
        public async Task<IActionResult> GetAsync([FromQuery] int empresaId, [FromQuery] string documento, [FromQuery] int tipoId)
        {
            var response = await _polizasUnitOfWork.GetAsync(empresaId, documento, tipoId);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound();
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _polizasUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _polizasUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}