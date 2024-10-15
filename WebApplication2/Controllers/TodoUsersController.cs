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
    public class TodoUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TodoUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoUser>>> GetTodoUsers()
        {
            return await _context.TodoUsers.ToListAsync();
        }

        // GET: api/TodoUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoUser>> GetTodoUser(long id)
        {
            var todoUser = await _context.TodoUsers.FindAsync(id);

            if (todoUser == null)
            {
                return NotFound();
            }

            return todoUser;
        }

        // PUT: api/TodoUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoUser(long id, TodoUser todoUser)
        {
            if (id != todoUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoUserExists(id))
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

        // POST: api/TodoUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoUser>> PostTodoUser(TodoUser todoUser)
        {
            _context.TodoUsers.Add(todoUser);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoUser", new { id = todoUser.Id }, todoUser);
            return CreatedAtAction(nameof(GetTodoUser), new { id = todoUser.Id }, todoUser);
        }

        // DELETE: api/TodoUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoUser(long id)
        {
            var todoUser = await _context.TodoUsers.FindAsync(id);
            if (todoUser == null)
            {
                return NotFound();
            }

            _context.TodoUsers.Remove(todoUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoUserExists(long id)
        {
            return _context.TodoUsers.Any(e => e.Id == id);
        }
    }
}
