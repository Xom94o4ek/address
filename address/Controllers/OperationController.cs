using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using address.Data;
using address.Models;

namespace address.Controllers
{
    public class OperationController : Controller
    {
        private readonly AddressSystemContext _context;

        public OperationController(AddressSystemContext context)
        {
            _context = context;
        }

        // GET: Operation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Houses.ToListAsync());
        }

        // GET: Operation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houses = await _context.Houses
                .FirstOrDefaultAsync(m => m.HouseId == id);
            if (houses == null)
            {
                return NotFound();
            }

            return View(houses);
        }

        // GET: Operation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseId,HouseNum,Index,Floors,Entrances,Flats,StreetId")] Houses houses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(houses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(houses);
        }

        // GET: Operation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houses = await _context.Houses.FindAsync(id);
            if (houses == null)
            {
                return NotFound();
            }
            return View(houses);
        }

        // POST: Operation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HouseId,HouseNum,Index,Floors,Entrances,Flats,StreetId")] Houses houses)
        {
            if (id != houses.HouseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(houses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HousesExists(houses.HouseId))
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
            return View(houses);
        }

        // GET: Operation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houses = await _context.Houses
                .FirstOrDefaultAsync(m => m.HouseId == id);
            if (houses == null)
            {
                return NotFound();
            }

            return View(houses);
        }

        // POST: Operation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var houses = await _context.Houses.FindAsync(id);
            _context.Houses.Remove(houses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HousesExists(int id)
        {
            return _context.Houses.Any(e => e.HouseId == id);
        }
    }
}
