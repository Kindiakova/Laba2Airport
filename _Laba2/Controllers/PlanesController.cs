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
    public class PlanesController : Controller
    {
        private readonly Laba2Context _context;

        public PlanesController(Laba2Context context)
        {
            _context = context;
        }
        // GET: api/Planes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaneShort>>> GetPlanes()
        {
            if (_context.Planes == null) return NotFound();
            var planes = await _context.Planes.ToListAsync();
            List<PlaneShort> Splanes = new List<PlaneShort>();

            foreach (var plane in planes)
            {
                Splanes.Add(new PlaneShort
                {
                    Id = plane.Id,
                    Model = plane.Model,
                    MaxPassAmount = plane.MaxPassAmount,
                    Flights = (_context.Flights.Where(x => x.PlaneId == plane.Id).ToList())
                           .Select(x => x.Id).ToList()
                });
            }

            return Splanes;

        }


        // GET: api/Planes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaneShort>> GetPlane(int? id)
        {
            if (id == null || _context.Planes == null)
            {
                return NotFound();
            }

            var plane = await _context.Planes.FindAsync(id);
            if (plane == null)
            {
                return NotFound();
            }
            var Splane = new PlaneShort
            {
                Id = plane.Id,
                Model = plane.Model,
                MaxPassAmount = plane.MaxPassAmount,
                Flights = (_context.Flights.Where(x => x.PlaneId == plane.Id).ToList())
                           .Select(x => x.Id).ToList()
            };
            return Splane;
        }

        [HttpPost]
        public async Task<ActionResult<PlaneShort>> PostPlane(PlaneWrite Wplane)
        {
            if (!ModelState.IsValid) return NotFound();
            var plane = new PlaneShort
            {
                Model = Wplane.Model,
                MaxPassAmount = Wplane.MaxPassAmount
            };
            _context.Add(plane);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetPlane", new { id = plane.Id }); 

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PlaneShort>> PutPlane([FromRoute] int? id, PlaneWrite Wplane)
        {
            if (id == null) return BadRequest("No Id");
            if (_context.Planes == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid) return NotFound();
            var plane = await _context.Planes.FindAsync(id);
            if (plane == null) return NotFound();

            plane.Model = Wplane.Model;
            plane.MaxPassAmount = Wplane.MaxPassAmount;
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaneExists(plane.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("GetPlane", new { id = plane.Id });
            }
            return NotFound();
        }


        // POST: Planes/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeletePlanes(int id)
        {
            if (_context.Planes == null)
            {
                return Problem("Entity set 'Laba2Context.Planes'  is null.");
            }
            var plane = await _context.Planes.FindAsync(id);
            if (plane != null)
            {
                _context.Planes.Remove(plane);
                await _context.SaveChangesAsync();
                return "plane with id = " + id.ToString() + " was deleted successfully";
            }
            return NotFound();
        }
        
        private bool PlaneExists(int id)
        {
          return (_context.Planes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
