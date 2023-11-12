/* Author:  Auto-generated
 * Editor:  Eric Robinson L00709820
 * Date:    11/10/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     5
 * Purpose: 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABooksEFClasses.Models;

namespace MMABooksRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly MMABooksContext _context;

        public StatesController(MMABooksContext context)
        {
            _context = context;
        }

        // GET: api/States
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetStates()
        // Returns all states
        {
          if (_context.States == null)
          {
              return NotFound();
          }

            return await _context.States.ToListAsync();
        }

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(string id)
        // Returns 1 state
        {
          if (_context.States == null)
          {
              return NotFound();
          }
            var state = await _context.States.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }

        // PUT: api/States/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutState(string id, State state)
        // Updates 1 state record.
        {
            if (id != state.StateCode)
            {
                return BadRequest();
            }

            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // What does this mean?
            // See: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.nocontent?view=aspnetcore-7.0
            return NoContent(); 
         }

        // POST: api/States
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<State>> PostState(State state)
        // Creates 1 state record.
        {
          if (_context.States == null)
          {
              return Problem("Entity set 'MMABooksContext.States'  is null.");
          }
            _context.States.Add(state);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StateExists(state.StateCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetState", new { id = state.StateCode }, state);
        }

        // DELETE: api/States/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(string id)
        // Deletes 1 state.
        {
            if (_context.States == null)
            {
                return NotFound();
            }
            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return NoContent(); // What does this mean?
        }

        private bool StateExists(string id)
        {
            return (_context.States?.Any(e => e.StateCode == id)).GetValueOrDefault();
        }

    } // end class StatesController : ControllerBase
} // end namespace MMABooksRestAPI.Controllers
