using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Class10Controller : ControllerBase
    {
        private readonly StudentDbContext _context;

        public Class10Controller(StudentDbContext context)
        {
            _context = context;
        }

        // GET: api/Class10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class10>>> GetClass10s()
        {
            return await _context.Class10s.ToListAsync();
        }

        // GET: api/Class10/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class10>> GetClass10(int id)
        {
            var class10 = await _context.Class10s.FindAsync(id);

            if (class10 == null)
            {
                return NotFound();
            }

            return class10;
        }

        // PUT: api/Class10/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass10(int id, Class10 class10)
        {
            if (id != class10.AdmissionNum)
            {
                return BadRequest();
            }

            _context.Entry(class10).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Class10Exists(id))
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

        // POST: api/Class10
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class10>> PostClass10(Class10 class10)
        {
            _context.Class10s.Add(class10);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass10", new { id = class10.AdmissionNum }, class10);
        }

        // DELETE: api/Class10/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass10(int id)
        {
            var class10 = await _context.Class10s.FindAsync(id);
            if (class10 == null)
            {
                return NotFound();
            }

            _context.Class10s.Remove(class10);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Class10Exists(int id)
        {
            return _context.Class10s.Any(e => e.AdmissionNum == id);
        }
    }
}
