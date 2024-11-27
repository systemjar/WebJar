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

        public CuentaController(IGenericUnitOfWork<Cuenta> unitOfWork, ICuentasUnitOfWork cuentasUnitOfWork) : base(unitOfWork)
        {
            _cuentasUnitOfWork = cuentasUnitOfWork;
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _cuentasUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _cuentasUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }
    }
}