using Microsoft.AspNetCore.Mvc;
using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.Entities;

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