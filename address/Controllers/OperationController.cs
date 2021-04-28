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
                regions = regions.Where(s => s.RegionName.Contains(id)).OrderBy(m=>m.RegionId);
            }

            return View(await regions.ToListAsync());
        }
        public async Task<IActionResult> AreaIndex(string id)
        {
            var areas = from p in _context.Areas
                         join c in _context.Regions on p.RegionId equals c.RegionId
                        orderby p.AreaId
                        select new DataAreas { AreaId = p.AreaId, AreaName = p.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };

            if (!String.IsNullOrEmpty(id))
            {
                areas = areas.Where(s => s.AreaName.Contains(id));
            }

            return View(await areas.ToListAsync());
        }
        public async Task<IActionResult> LocIndex(string id)
        {
            var localities = from p in _context.Localities
                        join b in _context.Areas on p.AreaId equals b.AreaId
                        join c in _context.Regions on b.RegionId equals c.RegionId
                             orderby p.LocalityId
                             select new DataLocalities { LocalityId = p.LocalityId, LocalityName = p.LocalityName, AreaId = p.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };

            if (!String.IsNullOrEmpty(id))
            {
                localities = localities.Where(s => s.LocalityName.Contains(id));
            }

            return View(await localities.ToListAsync());
        }
        public async Task<IActionResult> DisIndex(string id)
        {
            var districts = from p in _context.Districts
                            join a in _context.Localities on p.LocalityId equals a.LocalityId
                            join b in _context.Areas on a.AreaId equals b.AreaId
                             join c in _context.Regions on b.RegionId equals c.RegionId
                             orderby p.DistrictId
                             select new DataDistricts { DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };


            if (!String.IsNullOrEmpty(id))
            {
                districts = districts.Where(s => s.DistrictName.Contains(id));
            }

            return View(await districts.ToListAsync());
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
        public async Task<IActionResult> LocDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localities = await (from p in _context.Localities
                             join b in _context.Areas on p.AreaId equals b.AreaId
                             join c in _context.Regions on b.RegionId equals c.RegionId
                             select new DataLocalities { LocalityId = p.LocalityId, LocalityName = p.LocalityName, AreaId = p.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId }).FirstOrDefaultAsync(m => m.LocalityId == id);

            if (localities == null)
            {
                return NotFound();
            }

            return View(localities);
        }
        public async Task<IActionResult> DisDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districts = await (from p in _context.Districts
                            join a in _context.Localities on p.LocalityId equals a.LocalityId
                            join b in _context.Areas on a.AreaId equals b.AreaId
                            join c in _context.Regions on b.RegionId equals c.RegionId
                            select new DataDistricts { DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId }).FirstOrDefaultAsync(m => m.DistrictId == id);

            if (districts == null)
            {
                return NotFound();
            }

            return View(districts);
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
            int regionSelectedIndex = 1;
            SelectList regions = new SelectList(_context.Regions, "RegionId", "RegionName", regionSelectedIndex);
            ViewBag.Regions = regions;
            return View();
        }
        public IActionResult LocCreate()
        {
            int regionSelectedIndex = 1;
            SelectList regions = new SelectList(_context.Regions, "RegionId", "RegionName", regionSelectedIndex);
            ViewBag.Regions = regions;

            IQueryable<Areas> FilterAreas = _context.Areas.Where(c => c.RegionId == Convert.ToInt32(regions.SelectedValue));
            int AreaNum = FilterAreas.Count() == 0 ? 0 : FilterAreas.First().AreaId;
            SelectList areas = new SelectList(FilterAreas, "AreaId", "AreaName", AreaNum);
            ViewBag.Areas = areas;
            return View();
        }
        public IActionResult DisCreate()
        {
            int regionSelectedIndex = 1;
            SelectList regions = new SelectList(_context.Regions, "RegionId", "RegionName", regionSelectedIndex);
            ViewBag.Regions = regions;

            IQueryable<Areas> FilterAreas = _context.Areas.Where(c => c.RegionId == Convert.ToInt32(regions.SelectedValue));
            int AreaNum = FilterAreas.Count() == 0 ? 0 : FilterAreas.First().AreaId;
            SelectList areas = new SelectList(FilterAreas, "AreaId", "AreaName", AreaNum);
            ViewBag.Areas = areas;

            IQueryable<Localities> FilterLocalities = _context.Localities.Where(c => c.AreaId == Convert.ToInt32(areas.SelectedValue));
            int LocalitiesNum = FilterLocalities.Count() == 0 ? 0 : FilterLocalities.First().LocalityId;
            SelectList localities = new SelectList(FilterLocalities, "LocalityId", "LocalityName", LocalitiesNum);
            ViewBag.Localities = localities;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocCreate([Bind("LocalityName", "AreaId")] Localities localities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(LocIndex));
            }
            return View(localities);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisCreate([Bind("DistrictName", "LocalityId")] Districts districts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(districts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DisIndex));
            }
            return View(districts);
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

        public async Task<IActionResult> LocEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var localities = await _context.Localities.FindAsync(id);
            int OurAreaId = localities.AreaId;
            int OurRegId = (await _context.Areas.FindAsync(OurAreaId)).RegionId;
            SelectList areas = new SelectList(_context.Areas.Where(c => c.RegionId == OurRegId), "AreaId", "AreaName", OurAreaId);
            ViewBag.Areas = areas;
            SelectList regions = new SelectList(_context.Regions, "RegionId", "RegionName", OurRegId);
            ViewBag.Regions = regions;
            if (areas == null)
            {
                return NotFound();
            }
            return View(localities);
        }
        public async Task<IActionResult> DisEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var districts = await _context.Districts.FindAsync(id);
            int OurLocalityId = districts.LocalityId;
            int OurAreaId = (await _context.Localities.FindAsync(OurLocalityId)).AreaId;
            int OurRegId = (await _context.Areas.FindAsync(OurAreaId)).RegionId;
            SelectList localities = new SelectList(_context.Localities.Where(c => c.AreaId == OurAreaId), "LocalityId", "LocalityName", OurLocalityId);
            ViewBag.Localities = localities;
            SelectList areas = new SelectList(_context.Areas.Where(c => c.RegionId == OurRegId), "AreaId", "AreaName", OurAreaId);
            ViewBag.Areas = areas;
            SelectList regions = new SelectList(_context.Regions, "RegionId", "RegionName", OurRegId);
            ViewBag.Regions = regions;
            if (areas == null)
            {
                return NotFound();
            }
            return View(districts);
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
                    if (!AreasExists(areas.AreaId))
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocEdit(int id, [Bind("LocalityId,LocalityName,AreaId")] Localities localities)
        {
            if (id != localities.LocalityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalitiesExists(localities.LocalityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(LocIndex));
            }
            return View(localities);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisEdit(int id, [Bind("DistrictId,DistrictName,LocalityId")] Districts districts)
        {
            if (id != districts.DistrictId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(districts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistrictsExists(districts.DistrictId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(DisIndex));
            }
            return View(districts);
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
            var areas = await (from p in _context.Areas
                               join c in _context.Regions on p.RegionId equals c.RegionId
                               select new DataAreas { AreaId = p.AreaId, AreaName = p.AreaName, RegionName = c.RegionName, RegionId = c.RegionId }).FirstOrDefaultAsync(m => m.AreaId == id);
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

        public async Task<IActionResult> LocDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localities = await (from p in _context.Localities
                                    join b in _context.Areas on p.AreaId equals b.AreaId
                                    join c in _context.Regions on b.RegionId equals c.RegionId
                                    select new DataLocalities { LocalityId = p.LocalityId, LocalityName = p.LocalityName, AreaId = p.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId }).FirstOrDefaultAsync(m => m.LocalityId == id);
            if (localities == null)
            {
                return NotFound();
            }

            return View(localities);
        }
        // POST: Operation/Delete/5
        [HttpPost, ActionName("LocDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocDeleteConfirmed(int id)
        {
            var localities = await _context.Localities.FindAsync(id);
            _context.Localities.Remove(localities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(LocIndex));
        }

        public async Task<IActionResult> DisDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districts = await (from p in _context.Districts
                                   join a in _context.Localities on p.LocalityId equals a.LocalityId
                                   join b in _context.Areas on a.AreaId equals b.AreaId
                                   join c in _context.Regions on b.RegionId equals c.RegionId
                                   select new DataDistricts { DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId }).FirstOrDefaultAsync(m => m.DistrictId == id);
            if (districts == null)
            {
                return NotFound();
            }

            return View(districts);
        }
        // POST: Operation/Delete/5
        [HttpPost, ActionName("DisDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisDeleteConfirmed(int id)
        {
            var districts = await _context.Districts.FindAsync(id);
            _context.Districts.Remove(districts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DisIndex));
        }

        private bool RegionsExists(int id)
        {
            return _context.Regions.Any(e => e.RegionId == id);
        }
        private bool AreasExists(int id)
        {
            return _context.Areas.Any(e => e.AreaId == id);
        }
        private bool LocalitiesExists(int id)
        {
            return _context.Localities.Any(e => e.LocalityId == id);
        }
        private bool DistrictsExists(int id)
        {
            return _context.Districts.Any(e => e.DistrictId == id);
        }
    }
}
