using Microsoft.AspNetCore.Mvc;
using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Backend.Controllers.Conta
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentaController : GenericController<Cuenta>
    {
        private readonly ICuentasUnitOfWork _cuentasUnitOfWork;

        public CuentaController(ICuentasUnitOfWork cuentasUnitOfWork, IGenericUnitOfWork<Cuenta> genericUnitOfWork) : base(genericUnitOfWork)
        {
            _cuentasUnitOfWork = cuentasUnitOfWork;
        }

        [HttpGet("pornit")]
        public async Task<IActionResult> GetCuentaAsync([FromQuery] string nit)
        {
            var response = await _cuentasUnitOfWork.GetCuentaAsync(nit);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }
    }
}