﻿using System;
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
        public async Task<IActionResult> Index(string regNr, string vehicleType, string sortOrder)
        {
            var model = await mapper.ProjectTo<ParkingPlaceListViewModel>(_context.ParkingPlaces).ToListAsync();

            ViewData["VTypeSortParm"] = sortOrder == "VType" ? "vtype_desc" : "VType";
            ViewData["RegNrSortParm"] = sortOrder == "RegNr" ? "regnr_desc" : "RegNr";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["ArrivalSortParm"] = sortOrder == "arrival_desc" ? "Arrival" : "arrival_desc";

            var parkingPlace = string.IsNullOrWhiteSpace(regNr) ?
                          model :
                        model.Where(m => m.VehicleRegistrationNumber.Contains(regNr.ToUpper()));
           
            parkingPlace = string.IsNullOrWhiteSpace(vehicleType) ?
                         parkingPlace :
                       parkingPlace.Where(m => m.VehicleType == int.Parse(vehicleType));

            switch (sortOrder)
            {
                case "VType":
                    parkingPlace = parkingPlace.OrderBy(s => s.VehicleType);
                    break;
                case "vtype_desc":
                    parkingPlace = parkingPlace.OrderByDescending(s => s.VehicleType);
                    break;
                case "Name":
                    parkingPlace = parkingPlace.OrderBy(s => s.Username);
                    break;
                case "name_desc":
                    parkingPlace = parkingPlace.OrderByDescending(s => s.Username);
                    break;
                case "RegNr":
                    parkingPlace = parkingPlace.OrderBy(s => s.VehicleRegistrationNumber);
                    break;
                case "arrival_desc":
                    parkingPlace = parkingPlace.OrderByDescending(s => s.Arrival);
                    break;
                case "regnr_desc":
                    parkingPlace = parkingPlace.OrderByDescending(s => s.VehicleRegistrationNumber);
                    break;
                default:
                    parkingPlace = parkingPlace.OrderBy(s => s.Arrival);
                    break;
            }

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
            var found = _context.ParkingPlaces.FirstOrDefault(p => p.VehicleId == id);

            if (found != null)
            {
                TempData["failedParking"] = $"Reg Nr: This vehicle is already parked!";
                return RedirectToAction(nameof(Details), "Vehicles", new { id = id });

            }
            var currentParkingVehicles = _context.ParkingPlaces.Count() + 1;
            if (currentParkingVehicles > int.Parse(config["parkingCapacity"]))
            {
                TempData["parkingFull"] = $"Parking capacity has been reached";
                return RedirectToAction("Details", "Vehicles", new { id = id });
            }

            var model = new ParkingPlace
            {
                VehicleId = id
            };

            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                var vehicleRegNr = _context.Vehicles.Where(e => e.Id == id).Select(e => e.RegistrationNumber).ToList();
                TempData["message"] = $"Reg Nr: {vehicleRegNr[0]} is parked!";

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
            var parkedVehicle = await _context.ParkingPlaces.FindAsync(id);

            var model = await mapper.ProjectTo<ReceiptViewModel>(_context.ParkingPlaces).FirstOrDefaultAsync(e => e.Id == id);

            var checkout = DateTime.Now;
            var realTime = (checkout - model.Arrival).TotalSeconds / 3600;
            var chargeTime = (int)Math.Ceiling(realTime);

            model.CheckOut = checkout;
            model.ParkingTime = chargeTime;
            model.Price = chargeTime * 80;

            _context.ParkingPlaces.Remove(parkedVehicle);
            await _context.SaveChangesAsync();
            return View("Receipt", model);
            //return RedirectToAction(nameof(Index));
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
