using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba2;
using Laba2.Models;

namespace Laba2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionsController : Controller
    {
        private readonly Laba2Context _context;

        public DirectionsController(Laba2Context context)
        {
            _context = context;
        }

        //GET: api/Directions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectionShort>>> GetDirections()
        {
            if (_context.Directions == null) return NotFound();
            var directions = await _context.Directions.ToListAsync();
            List<DirectionShort> Sdir = new List<DirectionShort>();
            foreach (var direction in directions)
            {
                Sdir.Add(new DirectionShort
                {
                    Id = direction.Id,
                    CountryFrom = direction.CountryFrom,
                    CountryTo = direction.CountryTo,
                    CityFrom = direction.CityFrom,
                    CityTo = direction.CityTo,
                    TerminalFrom = direction.TerminalFrom,
                    TerminalTo = direction.TerminalTo,
                    RunwayFrom = direction.RunwayFrom,
                    RunwayTo = direction.RunwayTo,
                    Flights = (_context.Flights.Where(x => x.DirectionId == direction.Id).ToList())
                           .Select(x => x.Id).ToList()

                }) ;
            }
            return Sdir;

        }
        // GET: api/Directions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectionShort>> GetDirection(int? id)
        {
            if (id == null || _context.Directions == null)
            {
                return NotFound();
            }

            var direction = await _context.Directions.FindAsync(id);
            if (direction == null)
            {
                return NotFound();
            }
            var Sdir = new DirectionShort
            {
                Id = direction.Id,
                CountryFrom = direction.CountryFrom,
                CountryTo = direction.CountryTo,
                CityFrom = direction.CityFrom,
                CityTo = direction.CityTo,
                TerminalFrom = direction.TerminalFrom,
                TerminalTo = direction.TerminalTo,
                RunwayFrom = direction.RunwayFrom,
                RunwayTo = direction.RunwayTo,
                Flights = (_context.Flights.Where(x => x.DirectionId == direction.Id).ToList())
                           .Select(x => x.Id).ToList()

            };
            return Sdir;
        }

        // POST: api/Directions
        [HttpPost]
        public async Task<ActionResult<DirectionShort>> PostDirection(DirectionWrite Wdirection)
        {
            if (!ModelState.IsValid) return NotFound();

            var direction = new Direction
            {
                CountryFrom = Wdirection.CountryFrom,
                CountryTo = Wdirection.CountryTo,
                CityFrom = Wdirection.CityFrom,
                CityTo = Wdirection.CityTo,
                TerminalFrom = Wdirection.TerminalFrom,
                TerminalTo = Wdirection.TerminalTo,
                RunwayFrom = Wdirection.RunwayFrom,
                RunwayTo = Wdirection.RunwayTo
            };

            _context.Add(direction);
            await _context.SaveChangesAsync();
             return RedirectToAction("GetDirection", new { id = direction.Id });
        }

        // PUT: api/Directions/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DirectionShort>> PutDirection([FromRoute] int? id, DirectionWrite Wdirection)
        {
            if (id == null) return BadRequest("No Id");
            if (_context.Directions == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) return NotFound();


            var direction = await _context.Directions.FindAsync(id);
            if (direction == null) return NotFound();

            direction.CountryFrom = Wdirection.CountryFrom;
            direction.CountryTo = Wdirection.CountryTo;
            direction.CityFrom = Wdirection.CityFrom;
            direction.CityTo = Wdirection.CityTo;
            direction.TerminalFrom = Wdirection.TerminalFrom;
            direction.TerminalTo = Wdirection.TerminalTo;
            direction.RunwayFrom = Wdirection.RunwayFrom;
            direction.RunwayTo = Wdirection.RunwayTo;    

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectionExists(direction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("GetDirection", new { id = direction.Id });
            }
            return BadRequest("Model isn`t valid");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteDirection(int id)
        {
            if (_context.Directions == null)
            {
                return Problem("Entity set 'Laba2Context.Places'  is null.");
            }
            var direction = await _context.Directions.FindAsync(id);
            if (direction != null)
            {
                _context.Directions.Remove(direction); 
                await _context.SaveChangesAsync();
                return "direction with id = " + id.ToString() + " was deleted successfully"; ;
            }
            return NotFound();
        }

        private bool DirectionExists(int id)
        {
          return (_context.Directions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
