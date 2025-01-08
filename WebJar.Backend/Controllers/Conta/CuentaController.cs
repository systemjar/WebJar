using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Backend.Controllers.Conta
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class CuentaController : GenericController<Cuenta>
    {
        private readonly ICuentasUnitOfWork _cuentasUnitOfWork;

        public CuentaController(IGenericUnitOfWork<Cuenta> unitOfWork, ICuentasUnitOfWork cuentasUnitOfWork) : base(unitOfWork)
        {
            _cuentasUnitOfWork = cuentasUnitOfWork;
        }

        [HttpGet("{Id}")]
        public override async Task<IActionResult> GetAsync(int Id)
        {
            var response = await _cuentasUnitOfWork.GetAsync(Id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound();
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> GetAsync(int empresaId, int cuentaId)
        {
            var response = await _cuentasUnitOfWork.GetAsync(empresaId, cuentaId);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound();
        }

        [HttpGet("codigo")]
        public async Task<IActionResult> GetAsync([FromQuery] int empresaId, [FromQuery] string codigoCuenta)
        {
            var response = await _cuentasUnitOfWork.GetAsync(empresaId, codigoCuenta);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound();
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _cuentasUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _cuentasUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _cuentasUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}