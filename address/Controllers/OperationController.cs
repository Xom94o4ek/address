using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using address.Data;
using address.Models;
using address.ModelsData;

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
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Houses.ToListAsync());
        }*/
        public async Task<IActionResult> RegIndex(string id)
        {
            var regions = from m in _context.Regions
                         select m;

            if (!String.IsNullOrEmpty(id))
            {
                regions = regions.Where(s => s.RegionName.Contains(id));
            }

            return View(await regions.ToListAsync());
        }
        public async Task<IActionResult> AreaIndex(string id)
        {
            var areas = from p in _context.Areas
                         join c in _context.Regions on p.RegionId equals c.RegionId
                         select new DataAreas { AreaId = p.AreaId, AreaName = p.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };

            if (!String.IsNullOrEmpty(id))
            {
                areas = areas.Where(s => s.AreaName.Contains(id));
            }

            return View(await areas.ToListAsync());
        }

        // GET: Operation/Details/5
        /*public async Task<IActionResult> Details(int? id)
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
        }*/
        public async Task<IActionResult> RegDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regions = await _context.Regions
                .FirstOrDefaultAsync(m => m.RegionId == id);
            if (regions == null)
            {
                return NotFound();
            }

            return View(regions);
        }
        public async Task<IActionResult> AreaDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areas = await (from p in _context.Areas
                        join c in _context.Regions on p.RegionId equals c.RegionId
                        select new DataAreas { AreaId = p.AreaId, AreaName = p.AreaName, RegionName = c.RegionName, RegionId = c.RegionId }).FirstOrDefaultAsync(m => m.AreaId == id);

            if (areas == null)
            {
                return NotFound();
            }

            return View(areas);
        }

        // GET: Operation/Create
        /*public IActionResult Create()
        {
            return View();
        }*/
        public IActionResult RegCreate()
        {
            return View();
        }

        public IActionResult AreaCreate()
        {
            SelectList regions = new SelectList(_context.Regions, "RegionId", "RegionName", 1);
            ViewBag.Regions = regions;
            return View();
        }

        // POST: Operation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
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
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegCreate([Bind("RegionName")] Regions regions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RegIndex));
            }
            return View(regions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AreaCreate([Bind("AreaName", "RegionId")] Areas areas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(areas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AreaIndex));
            }
            return View(areas);
        }

        // GET: Operation/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
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
        }*/
        public async Task<IActionResult> RegEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regions = await _context.Regions.FindAsync(id);
            if (regions == null)
            {
                return NotFound();
            }
            return View(regions);
        }

        public async Task<IActionResult> AreaEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var areas = await _context.Areas.FindAsync(id);
            SelectList regions = new SelectList(_context.Regions, "RegionId", "RegionName", areas.RegionId);
            ViewBag.Regions = regions;
            if (areas == null)
            {
                return NotFound();
            }
            return View(areas);
        }

        // POST: Operation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
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
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegEdit(int id, [Bind("RegionId,RegionName")] Regions regions)
        {
            if (id != regions.RegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionsExists(regions.RegionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(RegIndex));
            }
            return View(regions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AreaEdit(int id, [Bind("AreaId,AreaName,RegionId")] Areas areas)
        {
            if (id != areas.AreaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(areas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionsExists(areas.AreaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AreaIndex));
            }
            return View(areas);
        }
        // GET: Operation/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
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
        }*/
        public async Task<IActionResult> RegDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regions = await _context.Regions
                .FirstOrDefaultAsync(m => m.RegionId == id);
            if (regions == null)
            {
                return NotFound();
            }

            return View(regions);
        }
        // POST: Operation/Delete/5
        [HttpPost, ActionName("RegDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegDeleteConfirmed(int id)
        {
            var regions = await _context.Regions.FindAsync(id);
            _context.Regions.Remove(regions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(RegIndex));
        }

        public async Task<IActionResult> AreaDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areas = await _context.Areas
                .FirstOrDefaultAsync(m => m.AreaId == id);
            if (areas == null)
            {
                return NotFound();
            }

            return View(areas);
        }
        // POST: Operation/Delete/5
        [HttpPost, ActionName("AreaDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AreaDeleteConfirmed(int id)
        {
            var areas = await _context.Areas.FindAsync(id);
            _context.Areas.Remove(areas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AreaIndex));
        }

        private bool RegionsExists(int id)
        {
            return _context.Regions.Any(e => e.RegionId == id);
        }
    }
}
