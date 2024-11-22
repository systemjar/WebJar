using Microsoft.AspNetCore.Mvc;
using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.Entities;

namespace WebJar.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : GenericController<Empresa>
    {
        public EmpresaController(IGenericUnitOfWork<Empresa> unitOfWork) : base(unitOfWork)
        {
        }
    }
}