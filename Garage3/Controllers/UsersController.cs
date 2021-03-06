﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models;
using AutoMapper;
using Garage3.Models.ViewModels;
using Garage3.Filter;
using Garage3.Util;

namespace Garage3.Controllers
{
    public class UsersController : Controller
    {
        private readonly Garage3Context _context;
        private readonly IMapper mapper;

        public UsersController(Garage3Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Users
        public async Task<IActionResult> Index(string fullname, string sortOrder)
        {
            var model = await mapper.ProjectTo<UserListViewModel>(_context.Users).ToListAsync();

            ViewData["VCountSortParm"] = sortOrder == "VCount" ? "vcount_desc" : "VCount";
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "email_desc" : "Email";
            ViewData["NameSortParm"] = sortOrder == "name_desc" ? "Name" : "name_desc";
            ViewData["AgeSortParm"] = sortOrder == "Age" ? "age_desc" : "Age";

            var u = string.IsNullOrWhiteSpace(fullname) ?
                            model :
                          model.Where(m => m.FullName.ToLower().Contains(fullname.ToLower()));

            switch (sortOrder)
            {
                case "VCount":
                    u = u.OrderBy(s => s.VehicleCount);
                    break;
                case "vcount_desc":
                    u = u.OrderByDescending(s => s.VehicleCount);
                    break;
                case "Age":
                    u = u.OrderBy(s => s.Age);
                    break;
                case "name_desc":
                    u = u.OrderByDescending(s => s.FullName);
                    break;
                case "Email":
                    u = u.OrderBy(s => s.Email);
                    break;
                case "age_desc":
                    u = u.OrderByDescending(s => s.Age);
                    break;
                case "email_desc":
                    u = u.OrderByDescending(s => s.Email);
                    break;
                default:
                    u = u.OrderBy(s => s.FullName);
                    break;
                    
            }


            return View(u);
        }



        // GET: Users/Details/5
       
        [IdRequiredFilter]
        [ModelNotNullFilter]
        public async Task<IActionResult> Details(int? id)
        {
            var user = await mapper.ProjectTo<UserDetailsViewModel>(_context.Users).FirstOrDefaultAsync(s => s.Id == id);
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Add()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserAddViewModel viewModel)
        {
            var capitalized = CapitalizationFormatting.CapitalizeFirst(viewModel.FirstName, viewModel.LastName);
            viewModel.FirstName = capitalized[0];
            viewModel.LastName = capitalized[1];
            viewModel.Email = viewModel.Email.ToLower();

            if (ModelState.IsValid)
            {
                var user = mapper.Map<User>(viewModel);

                _context.Add(user);
                await _context.SaveChangesAsync();
                TempData["message"] = $"User: {user.FullName} has been added!";
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Users/Edit/5
        [IdRequiredFilter]
        [ModelNotNullFilter]
        public async Task<IActionResult> Edit(int? id)
        {    
            var user = await _context.Users.FindAsync(id);    
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Age,Email")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        [IdRequiredFilter]
        [ModelNotNullFilter]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public IActionResult CheckEmail(string email)
        {
            if (_context.Users.Any(s => s.Email == email))
            {
                return Json($"{email} is in use");
            }

            return Json(true);
        }
    }
}
