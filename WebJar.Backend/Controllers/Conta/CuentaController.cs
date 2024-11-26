using Microsoft.AspNetCore.Mvc;
using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Backend.Controllers.Conta
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentaController : GenericController<Cuenta>
    {
        public CuentaController(IGenericUnitOfWork<Cuenta> unitOfWork) : base(unitOfWork)
        {
        }
    }
}