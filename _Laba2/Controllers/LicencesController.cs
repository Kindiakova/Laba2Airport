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
    public class LicencesController : Controller
    {
        private readonly Laba2Context _context;

        public LicencesController(Laba2Context context)
        {
            _context = context;
        }

        // GET: Licences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LicenceShort>>> GetLicences()
        {
            if (_context.Licences == null) return NotFound();
            var licences = await _context.Licences.ToListAsync();
            List<LicenceShort> Slicences = new List<LicenceShort>();
            foreach (var licence in licences)
            {
                var tmp = await _context.Pilots.FirstOrDefaultAsync(x => x.Id == licence.PilotId);
                Slicences.Add(new LicenceShort
                {
                    Id = licence.Id,
                    Name = licence.Name,
                    EndDate = licence.EndDate,                   
                    PilotId = licence.PilotId,
                    Pilot = new PilotWrite {Name = tmp.Name, BirthDate = tmp.BirthDate, Experience = tmp.Experience }
                                
                });
            }
            return Slicences;

        }
        // GET: api/Licences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LicenceShort>> GetLicence(int? id)
        {
            if (id == null || _context.Licences == null)
            {
                return NotFound();
            }

            var licence = await _context.Licences.FindAsync(id);
            if (licence == null)
            {
                return NotFound();
            }
            var tmp = await _context.Pilots.FirstOrDefaultAsync(x => x.Id == licence.PilotId);
            var Slicence = new LicenceShort
            {
                Id = licence.Id,
                Name = licence.Name,
                EndDate = licence.EndDate,
                PilotId = licence.PilotId,
                Pilot = new PilotWrite { Name = tmp.Name, BirthDate = tmp.BirthDate, Experience = tmp.Experience }
            };

            return Slicence;
        }

        // POST: api/Licences
        [HttpPost]
        public async Task<ActionResult<LicenceShort>> PostLicence(LicenceWrite Wlicence)
        {
            if (!ModelState.IsValid) return NotFound();
            var licence = new Licence
            {
                Name = Wlicence.Name,
                EndDate = Wlicence.EndDate,
                PilotId = Wlicence.PilotId
            };
            _context.Add(licence);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetLicence", new { id = licence.Id });

        }

        // PUT: api/Licences/5
        [HttpPut("{id}")]
        public async Task<ActionResult<LicenceShort>> PutLicence([FromRoute] int? id, LicenceWrite Wlicence)
        {
            if (id == null) return BadRequest("No Id");
            if (_context.Licences == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return NotFound();
            
            var licence = await _context.Licences.FindAsync(id);
            if (licence == null) return NotFound();

            licence.Name = Wlicence.Name;
            licence.EndDate = Wlicence.EndDate;
            licence.PilotId = Wlicence.PilotId;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(licence);
                    await _context.SaveChangesAsync();
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicenceExists(licence.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("GetLicence", new { id = licence.Id });
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteLicence(int id)
        {
            if (_context.Licences == null)
            {
                return Problem("Entity set 'Laba2Context.Licences'  is null.");
            }
            var licence = await _context.Licences.FindAsync(id);
            if (licence != null)
            {
                _context.Licences.Remove(licence);
                await _context.SaveChangesAsync();
                return "licence with id = " + id.ToString() + " was deleted successfully";
            }


            return NotFound();
        }

        private bool LicenceExists(int id)
        {
          return (_context.Licences?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
