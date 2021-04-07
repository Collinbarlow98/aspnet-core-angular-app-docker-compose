using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToHApi.Models;

namespace ToHApi.Controllers
{
    [Route("api/tohHeroes")]
    [ApiController]
    public class TohHeroesController : ControllerBase
    {
        private readonly TohContext _context;

        public TohHeroesController(TohContext context)
        {
            _context = context;
        }

        // GET: api/TohHeroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TohHero>>> GetTohHeroes()
        {
            return await _context.TohHeroes.ToListAsync();
        }

        // GET: api/tohHeros/search/superman
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<TohHero>>> Search(string name)
        {
            var tohHeroes = _context.TohHeroes.Where(e => e.name.Contains(name));   

            return await tohHeroes.ToListAsync();
        }

        // GET: api/TohHeroes/hero/5
        [HttpGet("hero/{id}")]
        public async Task<ActionResult<TohHero>> GetTohHero(long id)
        {
            var tohHero = await _context.TohHeroes.FindAsync(id);

            if (tohHero == null)
            {
                return NotFound();
            }

            return tohHero;
        }

        // PUT: api/TohHeroes/hero/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("hero/{id}")]
        public async Task<IActionResult> PutTohHero(long id, TohHero tohHero)
        {
            if (id != tohHero.Id)
            {
                return BadRequest();
            }

            _context.Entry(tohHero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TohHeroExists(id))
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

        // POST: api/TohHeroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TohHero>> PostTohHero(TohHero tohHero)
        {
            _context.TohHeroes.Add(tohHero);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTohHero), new { id = tohHero.Id }, tohHero);
        }

        // DELETE: api/TohHeroes/hero/5
        [HttpDelete("hero/{id}")]
        public async Task<IActionResult> DeleteTohHero(long id)
        {
            var tohHero = await _context.TohHeroes.FindAsync(id);
            if (tohHero == null)
            {
                return NotFound();
            }

            _context.TohHeroes.Remove(tohHero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpOptions]
        public IActionResult PreflightRoute()
        {
            return NoContent();
        }

        [HttpOptions("hero/{id}")]
        public IActionResult PreflightRoute(int id)
        {
            return NoContent();
        }

        [HttpOptions("search/{search}")]
        public IActionResult PreflightRoute(string search)
        {
            return NoContent();
        }

        private bool TohHeroExists(long id)
        {
            return _context.TohHeroes.Any(e => e.Id == id);
        }
    }
}
