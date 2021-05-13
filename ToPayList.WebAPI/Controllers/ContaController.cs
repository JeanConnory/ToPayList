using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToPayList.WebAPI.Data;
using ToPayList.WebAPI.Model;

namespace ToPayList.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly DataContext _context;

        public ContaController(DataContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Get()
        {
            try
            {
                var contas = await _context.Contas
                                    .Where(c => c.Ativo)
                                    .ToListAsync();

                return Ok(contas);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Banco de dados: {ex}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Conta conta)
        {
            try
            {
                _context.Add(conta);
                await _context.SaveChangesAsync();

                return Created($"/api/conta/{conta.Id}", conta);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Banco de dados: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Conta conta)
        {
            try
            {
                if (id != conta.Id)
                    return BadRequest();

                _context.Entry(conta).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Created($"/api/conta/{conta.Id}", conta);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Banco de dados: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var conta = await _context.Contas.FindAsync(id);

                if (conta == null)
                    return NotFound();

                _context.Contas.Remove(conta);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Banco de dados: {ex}");
            }
        }
    }
}