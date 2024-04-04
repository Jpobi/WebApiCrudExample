using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPICrud.Data;
using webAPICrud.Models;

namespace webAPICrud.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly DataContext _context;
        
        public ClienteController(ILogger<ClienteController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name ="GetAll")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpGet("{id}", Name="Get")]
        public async Task<ActionResult<Cliente>> Get(long id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if(cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        [HttpGet("{nombre}", Name="Search")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Search(string nombre)
        {
            return await _context.Clientes.Where(c => c.Nombre.Contains(nombre)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Insert(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return new CreatedAtRouteResult("Get", new {id = cliente.Id}, cliente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, Cliente cliente)
        {
            if(id != cliente.Id)
            {
                return BadRequest();
            }
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> Delete(long id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if(cliente == null)
            {
                return NotFound();
            }
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}
