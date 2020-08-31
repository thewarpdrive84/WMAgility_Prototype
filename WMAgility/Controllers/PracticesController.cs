using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMAgility.Data;
using WMAgility.Models;

namespace WMAgility.Controllers
{
    public class PracticesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PracticesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Practices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Practices.Include(p => p.Dog);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Practices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practice = await _context.Practices
                .Include(p => p.Dog)
                .FirstOrDefaultAsync(m => m.PracticeID == id);
            if (practice == null)
            {
                return NotFound();
            }

            return View(practice);
        }

        // GET: Practices/Create
        public IActionResult Create()
        {
            ViewData["CanineId"] = new SelectList(_context.Dogs, "Id", "DogName");
            return View();
        }

        // POST: Practices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PracticeID,Date,Rounds,Fails,Notes,CanineId")] Practice practice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(practice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CanineId"] = new SelectList(_context.Dogs, "Id", "DogName", practice.CanineId);
            return View(practice);
        }

        // GET: Practices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practice = await _context.Practices.FindAsync(id);
            if (practice == null)
            {
                return NotFound();
            }
            ViewData["CanineId"] = new SelectList(_context.Dogs, "Id", "DogName", practice.CanineId);
            return View(practice);
        }

        // POST: Practices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PracticeID,Date,Rounds,Fails,Notes,CanineId")] Practice practice)
        {
            if (id != practice.PracticeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(practice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PracticeExists(practice.PracticeID))
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
            ViewData["CanineId"] = new SelectList(_context.Dogs, "Id", "DogName", practice.CanineId);
            return View(practice);
        }

        // GET: Practices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practice = await _context.Practices
                .Include(p => p.Dog)
                .FirstOrDefaultAsync(m => m.PracticeID == id);
            if (practice == null)
            {
                return NotFound();
            }

            return View(practice);
        }

        // POST: Practices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var practice = await _context.Practices.FindAsync(id);
            _context.Practices.Remove(practice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PracticeExists(int id)
        {
            return _context.Practices.Any(e => e.PracticeID == id);
        }
    }
}
