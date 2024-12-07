using Microsoft.AspNetCore.Mvc;
using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Backend.Controllers.Conta
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoContaController : GenericController<TipoConta>
    {
        private readonly ITiposContaUnitOfWork _tiposContaUnitOfWork;

        public TipoContaController(IGenericUnitOfWork<TipoConta> unitOfWork, ITiposContaUnitOfWork tiposContaUnitOfWork) : base(unitOfWork)
        {
            _tiposContaUnitOfWork = tiposContaUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _tiposContaUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _tiposContaUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}