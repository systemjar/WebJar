using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebJar.Backend.Data;
using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Backend.Controllers.Conta
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoContaController : GenericController<TipoConta>
    {
        public TipoContaController(IGenericUnitOfWork<TipoConta> unitOfWork) : base(unitOfWork)
        {
        }
    }
}