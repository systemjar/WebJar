using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebJar.Backend.Data;
using WebJar.Shared.Entities;

namespace WebJar.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoContaController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoContaController(DataContext context)
        {
            _context = context;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var tipoConta = await _context.TiposConta.FirstOrDefaultAsync(c => c.Id == id);
            if (tipoConta == null)
            {
                return NotFound();
            }
            _context.Remove(tipoConta);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.TiposConta.ToListAsync());
        }

        // id es un parametro pasado por ruta
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var tipoConta = await _context.TiposConta.FirstOrDefaultAsync(c => c.Id == id);
            if (tipoConta == null)
            {
                return NotFound();
            }
            return Ok(tipoConta);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(TipoConta tipoConta)
        {
            _context.Add(tipoConta);
            await _context.SaveChangesAsync();
            return Ok(tipoConta);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(TipoConta tipoConta)
        {
            _context.Update(tipoConta);
            await _context.SaveChangesAsync();
            return Ok(tipoConta);
        }
    }
}