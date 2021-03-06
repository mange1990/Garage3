﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models;
using Garage3.Models.ViewModels;
using AutoMapper;
using Garage3.Filter;
using Garage3.Util;

namespace Garage3.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage3Context _context;
        private readonly IMapper mapper;

        public VehiclesController(Garage3Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var garage3Context = _context.Vehicles.Include(v => v.User).Include(v => v.VehicleType);
            return View(await garage3Context.ToListAsync());
        }

        // GET: Vehicles/Details/5
        [IdRequiredFilter]
        [ModelNotNullFilter]
        public async Task<IActionResult> Details(int? id)
        {
            

            var vehicle = await mapper.ProjectTo<VehicleDetailsViewModel>(_context.Vehicles).FirstOrDefaultAsync(s => s.Id == id);

            

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Add()
        {
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            //ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name");
            //var model = new VehicleAddViewModel
            //{
            //    VehicleTypes = _context.VehicleTypes.Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() })
            //};
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(VehicleAddViewModel viewModel, int id)
        {
            var capitalized = CapitalizationFormatting.CapitalizeFirst(viewModel.Color, viewModel.VehicleModel);
            viewModel.Color = capitalized[0];
            viewModel.VehicleModel = capitalized[1];
            viewModel.Manufacturer = viewModel.Manufacturer.ToUpper();
            viewModel.RegistrationNumber = viewModel.RegistrationNumber.ToUpper();

            if (ModelState.IsValid)
            {
                var vehicle = mapper.Map<Vehicle>(viewModel);
                vehicle.UserId = id;
                
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = vehicle.Id });
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vehicle.UserId);
            //ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(viewModel);
        }

        // GET: Vehicles/Edit/5
        [IdRequiredFilter]
        [ModelNotNullFilter]
        public async Task<IActionResult> Edit(int? id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vehicle.UserId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Wheels,RegistrationNumber,Manufacturer,Color,VehicleModel,VehicleTypeId,UserId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vehicle.UserId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        [IdRequiredFilter]
        [ModelNotNullFilter]
        public async Task<IActionResult> Delete(int? id)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.User)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }

        public IActionResult CheckRegNr(string registrationNumber)
        {
            if (_context.Vehicles.Any(s => s.RegistrationNumber == registrationNumber))
            {
                return Json($"{registrationNumber} is in use");
            }

            return Json(true);
        }

    }
}
