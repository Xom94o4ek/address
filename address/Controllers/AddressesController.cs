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
    public class AddressesController : Controller
    {
        private readonly AddressSystemContext db;

        public AddressesController(AddressSystemContext context)
        {
            db = context;
        }

        // GET: Addresses
        public IActionResult Index()
        {
            //return View(await db.Houses.ToListAsync());
            int regionSelectedIndex = 1;
            SelectList regions = new SelectList(db.Regions, "RegionId", "RegionName", regionSelectedIndex);
            ViewBag.Regions = regions;

            IQueryable<Areas> FilterAreas = db.Areas.Where(c => c.RegionId == Convert.ToInt32(regions.SelectedValue));
            int AreaNum = FilterAreas.Count() == 0 ? 0 : FilterAreas.First().AreaId;
            SelectList areas = new SelectList(FilterAreas, "AreaId", "AreaName", AreaNum);
            ViewBag.Areas = areas;

            IQueryable<Localities> FilterLocalities = db.Localities.Where(c => c.AreaId == Convert.ToInt32(areas.SelectedValue));
            int LocalitiesNum = FilterLocalities.Count() == 0 ? 0 : FilterLocalities.First().LocalityId;
            SelectList localities = new SelectList(FilterLocalities, "LocalityId", "LocalityName", LocalitiesNum);
            ViewBag.Localities = localities;

            IQueryable<Districts> FilterDistricts = db.Districts.Where(c => c.LocalityId == Convert.ToInt32(localities.SelectedValue));
            int DistrictsNum = FilterDistricts.Count() == 0 ? 0 : FilterDistricts.First().DistrictId;
            SelectList districts = new SelectList(FilterDistricts, "DistrictId", "DistrictName", DistrictsNum);
            ViewBag.Districts = districts;

            IQueryable<Streets> FilterStreets = db.Streets.Where(c => c.DistrictId == Convert.ToInt32(districts.SelectedValue));
            int StreetsNum = FilterStreets.Count() == 0 ? 0 : FilterStreets.First().StreetId;
            SelectList streets = new SelectList(FilterStreets, "StreetId", "StreetName", StreetsNum);
            ViewBag.Streets = streets;

            IQueryable<Houses> FilterHouses = db.Houses.Where(c => c.StreetId == Convert.ToInt32(streets.SelectedValue));
            int HousesNum = FilterHouses.Count() == 0 ? 00 : FilterHouses.First().HouseId;
            SelectList houses = new SelectList(FilterHouses, "HouseId", "HouseNum", HousesNum);
            ViewBag.Houses = houses;
            return View();
        }
        public ActionResult GetAreas(int id)
        {
            return PartialView(db.Areas.Where(c => c.RegionId == id).ToList());
        }
        public ActionResult GetLocalities(int id)
        {
            return PartialView(db.Localities.Where(c => c.AreaId == id).ToList());
        }
        public ActionResult GetDistricts(int id)
        {
            return PartialView(db.Districts.Where(c => c.LocalityId == id).ToList());
        }
        public ActionResult GetStreets(int id)
        {
            return PartialView(db.Streets.Where(c => c.DistrictId == id).ToList());
        }
        public ActionResult GetHouses(int id)
        {
            return PartialView(db.Houses.Where(c => c.StreetId == id).ToList());
        }
        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houses = await db.Houses
                .FirstOrDefaultAsync(m => m.HouseId == id);
            if (houses == null)
            {
                return NotFound();
            }

            return View(houses);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseId,HouseNum,Index,Floors,Entrances,Flats,StreetId")] Houses houses)
        {
            if (ModelState.IsValid)
            {
                db.Add(houses);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(houses);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houses = await db.Houses.FindAsync(id);
            if (houses == null)
            {
                return NotFound();
            }
            return View(houses);
        }

        // POST: Addresses/Edit/5
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
                    db.Update(houses);
                    await db.SaveChangesAsync();
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

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houses = await db.Houses
                .FirstOrDefaultAsync(m => m.HouseId == id);
            if (houses == null)
            {
                return NotFound();
            }

            return View(houses);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var houses = await db.Houses.FindAsync(id);
            db.Houses.Remove(houses);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HousesExists(int id)
        {
            return db.Houses.Any(e => e.HouseId == id);
        }
    }
}
