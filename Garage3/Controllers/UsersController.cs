using System;
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
        public async Task<IActionResult> Index()
        {
            var model = await mapper.ProjectTo<UserListViewModel>(_context.Users).Take(20).ToListAsync();
            return View(model);
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
    }
}
