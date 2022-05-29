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
    public class PilotsController : Controller
    {
        private readonly Laba2Context _context;

        public PilotsController(Laba2Context context)
        {
            _context = context;
        }

        // GET: api/Pilots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PilotShort>>> GetPilots()
        {
            if (_context.Pilots == null) return NotFound();
            var pilots = await _context.Pilots.ToListAsync();
            List<PilotShort> Spilots = new List<PilotShort>();
            foreach (var pilot in pilots)
            {
                Spilots.Add(new PilotShort {
                    Id = pilot.Id,
                    Name = pilot.Name,
                    BirthDate = pilot.BirthDate,
                    Experience = pilot.Experience,
                    Licences = (_context.Licences
                            .Where(x => x.PilotId == pilot.Id).ToList())
                            .Select(x => new LicenceRead { Id = x.Id, Name = x.Name, EndDate = x.EndDate }).ToList(),
                    Flights = (_context.Flights.Where(x => x.PilotId == pilot.Id).ToList())
                           .Select(x => x.Id).ToList()
                });
            }            
            return Spilots;

        }

        // GET: api/Pilots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PilotShort>> GetPilot(int? id)
        {
            if (id == null || _context.Pilots == null)
            {
                return NotFound();
            }

            var pilot = await _context.Pilots.FindAsync(id);
            if (pilot == null)
            {
                return NotFound();
            }
            var Spilots = new PilotShort
            {
                Id = pilot.Id,
                Name = pilot.Name,
                BirthDate = pilot.BirthDate,
                Experience = pilot.Experience,
                Licences = (_context.Licences
                            .Where(x => x.PilotId == pilot.Id).ToList())
                            .Select(x => new LicenceRead { Id = x.Id, Name = x.Name, EndDate = x.EndDate }).ToList(),
                Flights = (_context.Flights.Where(x => x.PilotId == pilot.Id).ToList())
                           .Select(x => x.Id).ToList()

            };            

            return Spilots;
        }

        [HttpPost]
        public async Task<ActionResult<PilotShort>> PostPilot(PilotWrite Wpilot)
        {
            if (!ModelState.IsValid) return NotFound();
            if (Wpilot.Experience < 0) return BadRequest("Exp must be >= 0");
            var pilot = new Pilot
            {               
                Name = Wpilot.Name,
                BirthDate = Wpilot.BirthDate,
                Experience = Wpilot.Experience
            };
            _context.Add(pilot);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetPilot", new {id = pilot.Id});

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pilot>> PutPilot([FromRoute] int? id, PilotWrite Wpilot)
        {
            if (id == null) return BadRequest("No Id");
            
            if (_context.Pilots == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) return NotFound();
            if (Wpilot.Experience < -1) return BadRequest("Exp must be >= 0");
            var pilot = await _context.Pilots.FindAsync(id);
            if (pilot == null) return NotFound();
            pilot.Name = Wpilot.Name;
            pilot.BirthDate = Wpilot.BirthDate;
            pilot.Experience = Wpilot.Experience;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pilot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PilotExists(pilot.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("GetPilot", new { id = pilot.Id });
            }
            return NotFound();
        }
      
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteConfirmed(int id)
        {
            if (_context.Pilots == null)
            {
                return Problem("Entity set 'Laba2Context.Pilots'  is null.");
            }
            var pilot = await _context.Pilots.FindAsync(id);
            if (pilot != null)
            {
                _context.Pilots.Remove(pilot); 
                await _context.SaveChangesAsync();
                return "pilot with id = " + id.ToString() + " was deleted successfully";
            }


            return NotFound();
        }
        
        private bool PilotExists(int id)
        {
          return (_context.Pilots?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
