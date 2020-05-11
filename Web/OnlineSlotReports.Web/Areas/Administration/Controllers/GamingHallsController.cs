using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineSlotReports.Data;
using OnlineSlotReports.Data.Models;

namespace OnlineSlotReports.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class GamingHallsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamingHallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/GamingHalls
        public async Task<IActionResult> Index()
        {
            return View(await _context.GamingHalls.ToListAsync());
        }

        // GET: Administration/GamingHalls/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamingHall = await _context.GamingHalls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamingHall == null)
            {
                return NotFound();
            }

            return View(gamingHall);
        }

        // GET: Administration/GamingHalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administration/GamingHalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HallName,ImageUrl,Description,PhoneNumber,Adress,Town,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] GamingHall gamingHall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gamingHall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gamingHall);
        }

        // GET: Administration/GamingHalls/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamingHall = await _context.GamingHalls.FindAsync(id);
            if (gamingHall == null)
            {
                return NotFound();
            }
            return View(gamingHall);
        }

        // POST: Administration/GamingHalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("HallName,ImageUrl,Description,PhoneNumber,Adress,Town,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] GamingHall gamingHall)
        {
            if (id != gamingHall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamingHall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamingHallExists(gamingHall.Id))
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
            return View(gamingHall);
        }

        // GET: Administration/GamingHalls/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamingHall = await _context.GamingHalls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamingHall == null)
            {
                return NotFound();
            }

            return View(gamingHall);
        }

        // POST: Administration/GamingHalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var gamingHall = await _context.GamingHalls.FindAsync(id);

            gamingHall.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool GamingHallExists(string id)
        {
            return _context.GamingHalls.Any(e => e.Id == id);
        }
    }
}
