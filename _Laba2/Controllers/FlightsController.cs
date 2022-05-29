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
    public class FlightsController : Controller
    {
        private readonly Laba2Context _context;

        public FlightsController(Laba2Context context)
        {
            _context = context;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightShort>>> GetFlights()
        {
            if (_context.Flights == null) return NotFound();
            var flights = await _context.Flights.ToListAsync();
            List<FlightShort> Sflights = new List<FlightShort>();
            foreach (var flight in flights)
            {
                var tmpPilot = await _context.Pilots.FirstOrDefaultAsync(x => x.Id == flight.PilotId);
                var tmpDir = await _context.Directions.FirstOrDefaultAsync(x => x.Id == flight.PilotId);
                var tmpPlane = await _context.Planes.FirstOrDefaultAsync(x => x.Id == flight.PilotId);
                Sflights.Add(new FlightShort
                {
                    Id = flight.Id,
                    PlaneId = flight.PlaneId,
                    PilotId = flight.PilotId,
                    DirectionId = flight.DirectionId,
                    TakeOffTime = flight.TakeOffTime,
                    FlightLenghtInMinutes = flight.FlightLenghtInMinutes,
                    Pilot = (tmpPilot == null) ? null : new PilotWrite { Name = tmpPilot.Name, BirthDate = tmpPilot.BirthDate, Experience = tmpPilot.Experience },
                    Direction = (tmpDir == null) ? null : new DirectionWrite {
                        CountryFrom = tmpDir.CountryFrom,
                        CountryTo = tmpDir.CountryTo,
                        CityFrom = tmpDir.CityFrom,
                        CityTo = tmpDir.CityTo,
                        TerminalFrom = tmpDir.TerminalFrom,
                        TerminalTo = tmpDir.TerminalTo,
                        RunwayFrom = tmpDir.RunwayFrom,
                        RunwayTo = tmpDir.RunwayTo
                    },
                    Plane = new PlaneWrite { Model = tmpPlane.Model, MaxPassAmount = tmpPlane.MaxPassAmount }
                });
            }
            return Sflights;

        }
        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightShort>> GetFlight(int? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            var tmpPilot = await _context.Pilots.FirstOrDefaultAsync(x => x.Id == flight.PilotId);
            var tmpDir = await _context.Directions.FirstOrDefaultAsync(x => x.Id == flight.PilotId);
            var tmpPlane = await _context.Planes.FirstOrDefaultAsync(x => x.Id == flight.PilotId);
            var Sflight = new FlightShort
            {
                Id = flight.Id,
                PlaneId = flight.PlaneId,
                PilotId = flight.PilotId,
                DirectionId = flight.DirectionId,
                TakeOffTime = flight.TakeOffTime,
                FlightLenghtInMinutes = flight.FlightLenghtInMinutes,
                Pilot = (tmpPilot == null) ? null : new PilotWrite { Name = tmpPilot.Name, BirthDate = tmpPilot.BirthDate, Experience = tmpPilot.Experience },
                Direction = (tmpDir == null) ? null : new DirectionWrite
                {
                    CountryFrom = tmpDir.CountryFrom,
                    CountryTo = tmpDir.CountryTo,
                    CityFrom = tmpDir.CityFrom,
                    CityTo = tmpDir.CityTo,
                    TerminalFrom = tmpDir.TerminalFrom,
                    TerminalTo = tmpDir.TerminalTo,
                    RunwayFrom = tmpDir.RunwayFrom,
                    RunwayTo = tmpDir.RunwayTo
                },
                
                Plane = (tmpPlane == null) ? null : new PlaneWrite { Model = tmpPlane.Model, MaxPassAmount = tmpPlane.MaxPassAmount }
            };
            return Sflight;
        }


        // POST: api/Flights
        [HttpPost]
        public async Task<ActionResult<FlightShort>> PostFlight(FlightWrite Wflight)
        {
            if (!ModelState.IsValid) return NotFound();

            var flight = new Flight
            {
                PlaneId = Wflight.PlaneId,
                PilotId = Wflight.PilotId,
                DirectionId = Wflight.DirectionId,
                TakeOffTime = Wflight.TakeOffTime,
                FlightLenghtInMinutes = Wflight.FlightLenghtInMinutes, 
            };

            _context.Add(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetFlight", new { id = flight.Id });

        }


        // PUT: api/Flights/5
        [HttpPut("{id}")]
        public async Task<ActionResult<FlightShort>> PutFlight([FromRoute] int? id, FlightWrite Wflight)
        {
            if (id == null) return BadRequest("No Id");
            if (_context.Flights == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) return NotFound();
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return NotFound();

            flight.PlaneId = Wflight.PlaneId;
            flight.PilotId = Wflight.PilotId;
            flight.DirectionId = Wflight.DirectionId;
            flight.TakeOffTime = Wflight.TakeOffTime;
            flight.FlightLenghtInMinutes = Wflight.FlightLenghtInMinutes; 


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                 return RedirectToAction("GetFlight", new { id = flight.Id });
            }
            return NotFound();
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteFlights(int id)
        {
            if (_context.Flights == null)
            {
                return Problem("Entity set 'Laba2Context.Flights'  is null.");
            }
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
                return "flight with id = " + id.ToString() + " was deleted successfully";
            }           
           
            return NotFound();
        }

        private bool FlightExists(int id)
        {
          return (_context.Flights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
