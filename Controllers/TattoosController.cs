using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppTattoo.Data;
using WebAppTattoo.Models;

namespace WebAppTattoo.Controllers
{
    public class TattoosController : Controller
    {
        private readonly WebAppTattooContext _context;

        public TattoosController(WebAppTattooContext context)
        {
            _context = context;
        }

        // GET: Tattoos
        public async Task<IActionResult> Index()
        {
            var webAppTattooContext = _context.Tattoo.Include(t => t.Client);
            return View(await webAppTattooContext.ToListAsync());
        }

        // GET: Tattoos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tattoo = await _context.Tattoo
                .Include(t => t.Client)
                .FirstOrDefaultAsync(m => m.id == id);
            if (tattoo == null)
            {
                return NotFound();
            }

            return View(tattoo);
        }

        // GET: Tattoos/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
            return View();
        }

        // POST: Tattoos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,SessionDate,ValuePaid,PaymentMethod,PostScript,ClientId")] Tattoo tattoo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tattoo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", tattoo.ClientId);
            return View(tattoo);
        }

        // GET: Tattoos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tattoo = await _context.Tattoo.FindAsync(id);
            if (tattoo == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", tattoo.ClientId);
            return View(tattoo);
        }

        // POST: Tattoos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,SessionDate,ValuePaid,PaymentMethod,PostScript,ClientId")] Tattoo tattoo)
        {
            if (id != tattoo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tattoo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TattooExists(tattoo.id))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", tattoo.ClientId);
            return View(tattoo);
        }

        // GET: Tattoos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tattoo = await _context.Tattoo
                .Include(t => t.Client)
                .FirstOrDefaultAsync(m => m.id == id);
            if (tattoo == null)
            {
                return NotFound();
            }

            return View(tattoo);
        }

        // POST: Tattoos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tattoo = await _context.Tattoo.FindAsync(id);
            if (tattoo != null)
            {
                _context.Tattoo.Remove(tattoo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TattooExists(int id)
        {
            return _context.Tattoo.Any(e => e.id == id);
        }
    }
}
