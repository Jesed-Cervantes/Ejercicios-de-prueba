using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Context;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoMusicasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoMusicasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TodoMusicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoMusica>>> GettodoMusicas()
        {
            return await _context.todoMusicas.ToListAsync();
        }

        // GET: api/TodoMusicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoMusica>> GetTodoMusica(long id)
        {
            var todoMusica = await _context.todoMusicas.FindAsync(id);

            if (todoMusica == null)
            {
                return NotFound();
            }

            return todoMusica;
        }

        // PUT: api/TodoMusicas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoMusica(long id, TodoMusica todoMusica)
        {
            if (id != todoMusica.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoMusica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoMusicaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoMusicas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoMusica>> PostTodoMusica(TodoMusica todoMusica)
        {
            _context.todoMusicas.Add(todoMusica);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoMusica", new { id = todoMusica.Id }, todoMusica);
            return CreatedAtAction(nameof(GetTodoMusica), new { id = todoMusica.Id }, todoMusica);
        }

        // DELETE: api/TodoMusicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoMusica(long id)
        {
            var todoMusica = await _context.todoMusicas.FindAsync(id);
            if (todoMusica == null)
            {
                return NotFound();
            }

            _context.todoMusicas.Remove(todoMusica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoMusicaExists(long id)
        {
            return _context.todoMusicas.Any(e => e.Id == id);
        }
    }
}
