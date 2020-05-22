using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models;
using Microsoft.Extensions.Configuration;
using Garage3.Models.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Garage3.Filter;

namespace Garage3.Controllers
{
    public class ParkingPlacesController : Controller
    {
        private readonly Garage3Context _context;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public ParkingPlacesController(Garage3Context context, IConfiguration config, IMapper mapper)
        {
            _context = context;
            this.config = config;
            this.mapper = mapper;
        }

        // GET: ParkingPlaces
        public async Task<IActionResult> Index(string regNr, string vehicleType)
        {
            var model = await mapper.ProjectTo<ParkingPlaceListViewModel>(_context.ParkingPlaces).Take(20).ToListAsync();

            var parkingPlace = string.IsNullOrWhiteSpace(regNr) ?
                          model :
                        model.Where(m => m.VehicleRegistrationNumber.Contains(regNr));
           
            parkingPlace = string.IsNullOrWhiteSpace(vehicleType) ?
                         parkingPlace :
                       parkingPlace.Where(m => m.VehicleType == int.Parse(vehicleType));


            return View(parkingPlace);
        }

        // GET: ParkingPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingPlace = await _context.ParkingPlaces
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingPlace == null)
            {
                return NotFound();
            }

            return View(parkingPlace);
        }

        // GET: ParkingPlaces/Create
        public IActionResult Create()
        {
            //ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Color");
            return View();
        }

        // POST: ParkingPlaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id)
        {
            var model = new ParkingPlace
            {
                VehicleId = id
            };

            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Color", parkingPlace.VehicleId);
            return View();
        }

        // GET: ParkingPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingPlace = await _context.ParkingPlaces.FindAsync(id);
            if (parkingPlace == null)
            {
                return NotFound();
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Color", parkingPlace.VehicleId);
            return View(parkingPlace);
        }

        // POST: ParkingPlaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Arrival,VehicleId")] ParkingPlace parkingPlace)
        {
            if (id != parkingPlace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingPlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingPlaceExists(parkingPlace.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Color", parkingPlace.VehicleId);
            return View(parkingPlace);
        }

        // GET: ParkingPlaces/Delete/5
        [IdRequiredFilter]
        [ModelNotNullFilter]
        public async Task<IActionResult> Delete(int? id)
        {
            var parkingPlace = await mapper.ProjectTo<ParkingplaceDeleteViewModel>(_context.ParkingPlaces).FirstOrDefaultAsync(p => p.Id == id);
            return View(parkingPlace);
        }

        // POST: ParkingPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkingPlace = await _context.ParkingPlaces.FindAsync(id);
            _context.ParkingPlaces.Remove(parkingPlace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingPlaceExists(int id)
        {
            return _context.ParkingPlaces.Any(e => e.Id == id);
        }

        public DateTime GetTime() => DateTime.Now;

        //public bool CheckCapacity()
        //{
        //    int calculationResult = 0;
        //    var value = calculationResult < int.Parse(config["ParkingCapacity"]) ? true : false;
        //    return value;
        //}

    }
}
