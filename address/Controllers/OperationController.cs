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
        BaseFilter BaseFilter = new BaseFilter();
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
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var regions = from m in _context.Regions
                              select m;

                if (!String.IsNullOrEmpty(id))
                {
                    regions = regions.Where(s => s.RegionName.Contains(id)).OrderBy(m => m.RegionId);
                }

                return View(await regions.ToListAsync());
            }
        }
        public async Task<IActionResult> AreaIndex(string id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        public async Task<IActionResult> LocIndex(string id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        public async Task<IActionResult> DisIndex(string id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }

        public async Task<IActionResult> StrIndex(string id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var streets = from d in _context.Streets
                              join p in _context.Districts on d.DistrictId equals p.DistrictId
                              join a in _context.Localities on p.LocalityId equals a.LocalityId
                              join b in _context.Areas on a.AreaId equals b.AreaId
                              join c in _context.Regions on b.RegionId equals c.RegionId
                              orderby d.StreetId
                              select new DataStreets { StreetId = d.StreetId, StreetName = d.StreetName, DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };


                if (!String.IsNullOrEmpty(id))
                {
                    streets = streets.Where(s => s.StreetName.Contains(id));
                }

                return View(await streets.ToListAsync());
            }
        }

        public async Task<IActionResult> HouseIndex(string id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var houses = from e in _context.Houses
                             join d in _context.Streets on e.StreetId equals d.StreetId
                             join p in _context.Districts on d.DistrictId equals p.DistrictId
                             join a in _context.Localities on p.LocalityId equals a.LocalityId
                             join b in _context.Areas on a.AreaId equals b.AreaId
                             join c in _context.Regions on b.RegionId equals c.RegionId
                             orderby e.HouseId
                             select new DataHouses { HouseId = e.HouseId, HouseNum = e.HouseNum, Index = e.Index, Floors = e.Floors, Entrances = e.Entrances, Flats = e.Flats, StreetId = d.StreetId, StreetName = d.StreetName, DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };


                if (!String.IsNullOrEmpty(id))
                {
                    houses = houses.Where(s => s.StreetName.Contains(id));
                }

                return View(await houses.ToListAsync());
            }
        }
        public async Task<IActionResult> RegDetails(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        public async Task<IActionResult> AreaDetails(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        public async Task<IActionResult> LocDetails(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        public async Task<IActionResult> DisDetails(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }

        public async Task<IActionResult> StrDetails(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var streets = await (from d in _context.Streets
                                     join p in _context.Districts on d.DistrictId equals p.DistrictId
                                     join a in _context.Localities on p.LocalityId equals a.LocalityId
                                     join b in _context.Areas on a.AreaId equals b.AreaId
                                     join c in _context.Regions on b.RegionId equals c.RegionId
                                     orderby d.StreetId
                                     select new DataStreets { StreetId = d.StreetId, StreetName = d.StreetName, DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId }).FirstOrDefaultAsync(m => m.StreetId == id);

                if (streets == null)
                {
                    return NotFound();
                }

                return View(streets);
            }
        }

        public async Task<IActionResult> HouseDetails(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var houses = await (from e in _context.Houses
                                    join d in _context.Streets on e.StreetId equals d.StreetId
                                    join p in _context.Districts on d.DistrictId equals p.DistrictId
                                    join a in _context.Localities on p.LocalityId equals a.LocalityId
                                    join b in _context.Areas on a.AreaId equals b.AreaId
                                    join c in _context.Regions on b.RegionId equals c.RegionId
                                    orderby e.HouseId
                                    select new DataHouses { HouseId = e.HouseId, HouseNum = e.HouseNum, Index = e.Index, Floors = e.Floors, Entrances = e.Entrances, Flats = e.Flats, StreetId = d.DistrictId, StreetName = d.StreetName, DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId }).FirstOrDefaultAsync(m => m.HouseId == id);

                if (houses == null)
                {
                    return NotFound();
                }

                return View(houses);
            }
        }
        public IActionResult RegCreate()
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        public IActionResult AreaCreate()
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Regions = BaseFilter.RegionsList(_context, 1);
                return View();
            }
        }
        public IActionResult LocCreate()
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Regions = BaseFilter.RegionsList(_context, 1);
                ViewBag.Areas = BaseFilter.AreasList(_context, ViewBag.Regions);
                return View();
            }
        }
        public IActionResult DisCreate()
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Regions = BaseFilter.RegionsList(_context, 1);
                ViewBag.Areas = BaseFilter.AreasList(_context, ViewBag.Regions);
                ViewBag.Localities = BaseFilter.LocalitiesList(_context, ViewBag.Areas);
                return View();
            }
        }
        public IActionResult StrCreate()
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Regions = BaseFilter.RegionsList(_context, 1);
                ViewBag.Areas = BaseFilter.AreasList(_context, ViewBag.Regions);
                ViewBag.Localities = BaseFilter.LocalitiesList(_context, ViewBag.Areas);
                ViewBag.Districts = BaseFilter.LocalitiesList(_context, ViewBag.Localities);
                return View();
            }
        }

        public IActionResult HouseCreate()
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Regions = BaseFilter.RegionsList(_context, 1);
                ViewBag.Areas = BaseFilter.AreasList(_context, ViewBag.Regions);
                ViewBag.Localities = BaseFilter.LocalitiesList(_context, ViewBag.Areas);
                ViewBag.Districts = BaseFilter.LocalitiesList(_context, ViewBag.Localities);
                ViewBag.Streets = BaseFilter.StreetsList(_context, ViewBag.Districts);
                return View();
            }
        }

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
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(areas);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(AreaIndex));
                }
                return View(areas);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocCreate([Bind("LocalityName", "AreaId")] Localities localities)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(localities);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(LocIndex));
                }
                return View(localities);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisCreate([Bind("DistrictName", "LocalityId")] Districts districts)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(districts);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DisIndex));
                }
                return View(districts);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StrCreate([Bind("StreetName", "DistrictId")] Streets streets)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(streets);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(StrIndex));
                }
                return View(streets);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HouseCreate([Bind("HouseNum", "Index", "Floors", "Entrances", "Flats", "StreetId")] Houses houses)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(houses);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(HouseIndex));
                }
                return View(houses);
            }
        }
        public async Task<IActionResult> RegEdit(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }

        public async Task<IActionResult> AreaEdit(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                var areas = await _context.Areas.FindAsync(id);
                ViewBag.Regions = BaseFilter.RegionsList(_context, areas.RegionId);
                if (areas == null)
                {
                    return NotFound();
                }
                return View(areas);
            }
        }

        public async Task<IActionResult> LocEdit(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                var localities = await _context.Localities.FindAsync(id);
                int OurAreaId = localities.AreaId;
                int OurRegId = (await _context.Areas.FindAsync(OurAreaId)).RegionId;
                ViewBag.Areas = BaseFilter.AreasList(_context, OurRegId, OurAreaId);
                ViewBag.Regions = BaseFilter.RegionsList(_context, OurRegId);
                if (localities == null)
                {
                    return NotFound();
                }
                return View(localities);
            }
        }
        public async Task<IActionResult> DisEdit(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                var districts = await _context.Districts.FindAsync(id);
                int OurLocalityId = districts.LocalityId;
                int OurAreaId = (await _context.Localities.FindAsync(OurLocalityId)).AreaId;
                int OurRegId = (await _context.Areas.FindAsync(OurAreaId)).RegionId;

                ViewBag.Localities = BaseFilter.LocalitiesList(_context, OurAreaId, OurLocalityId);
                ViewBag.Areas = BaseFilter.AreasList(_context, OurRegId, OurAreaId);
                ViewBag.Regions = BaseFilter.RegionsList(_context, OurRegId);
                if (districts == null)
                {
                    return NotFound();
                }
                return View(districts);
            }
        }

        public async Task<IActionResult> StrEdit(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                var streets = await _context.Streets.FindAsync(id);
                int OuDistrictId = streets.DistrictId;
                int OurLocalityId = (await _context.Districts.FindAsync(OuDistrictId)).LocalityId;
                int OurAreaId = (await _context.Localities.FindAsync(OurLocalityId)).AreaId;
                int OurRegId = (await _context.Areas.FindAsync(OurAreaId)).RegionId;

                ViewBag.Districts = BaseFilter.DistrictsList(_context, OurLocalityId, OuDistrictId);
                ViewBag.Localities = BaseFilter.LocalitiesList(_context, OurAreaId, OurLocalityId);
                ViewBag.Areas = BaseFilter.AreasList(_context, OurRegId, OurAreaId);
                ViewBag.Regions = BaseFilter.RegionsList(_context, OurRegId);
                if (streets == null)
                {
                    return NotFound();
                }
                return View(streets);
            }
        }

        public async Task<IActionResult> HouseEdit(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                var houses = await _context.Houses.FindAsync(id);
                int OuStreetId = houses.StreetId;
                int OuDistrictId = (await _context.Streets.FindAsync(OuStreetId)).DistrictId; ;
                int OurLocalityId = (await _context.Districts.FindAsync(OuDistrictId)).LocalityId;
                int OurAreaId = (await _context.Localities.FindAsync(OurLocalityId)).AreaId;
                int OurRegId = (await _context.Areas.FindAsync(OurAreaId)).RegionId;

                ViewBag.Streets = BaseFilter.StreetsList(_context, OuDistrictId, OuStreetId);
                ViewBag.Districts = BaseFilter.DistrictsList(_context, OurLocalityId, OuDistrictId);
                ViewBag.Localities = BaseFilter.LocalitiesList(_context, OurAreaId, OurLocalityId);
                ViewBag.Areas = BaseFilter.AreasList(_context, OurRegId, OurAreaId);
                ViewBag.Regions = BaseFilter.RegionsList(_context, OurRegId);
                if (houses == null)
                {
                    return NotFound();
                }
                return View(houses);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegEdit(int id, [Bind("RegionId,RegionName")] Regions regions)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AreaEdit(int id, [Bind("AreaId,AreaName,RegionId")] Areas areas)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocEdit(int id, [Bind("LocalityId,LocalityName,AreaId")] Localities localities)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisEdit(int id, [Bind("DistrictId,DistrictName,LocalityId")] Districts districts)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StrEdit(int id, [Bind("StreetId, StreetName", "DistrictId")] Streets streets)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id != streets.StreetId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(streets);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StreetsExists(streets.StreetId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(StrIndex));
                }
                return View(streets);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HouseEdit(int id, [Bind("HouseId", "HouseNum", "Index", "Floors", "Entrances", "Flats", "StreetId")] Houses houses)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
                    return RedirectToAction(nameof(HouseIndex));
                }
                return View(houses);
            }
        }

        public async Task<IActionResult> RegDelete(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        // POST: Operation/Delete/5
        [HttpPost, ActionName("RegDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegDeleteConfirmed(int id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var regions = await _context.Regions.FindAsync(id);
                _context.Regions.Remove(regions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RegIndex));
            }
        }

        public async Task<IActionResult> AreaDelete(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        // POST: Operation/Delete/5
        [HttpPost, ActionName("AreaDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AreaDeleteConfirmed(int id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var areas = await _context.Areas.FindAsync(id);
                _context.Areas.Remove(areas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AreaIndex));
            }
        }

        public async Task<IActionResult> LocDelete(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        // POST: Operation/Delete/5
        [HttpPost, ActionName("LocDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocDeleteConfirmed(int id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var localities = await _context.Localities.FindAsync(id);
                _context.Localities.Remove(localities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(LocIndex));
            }
        }

        public async Task<IActionResult> DisDelete(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
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
        }
        // POST: Operation/Delete/5
        [HttpPost, ActionName("DisDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisDeleteConfirmed(int id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var districts = await _context.Districts.FindAsync(id);
                _context.Districts.Remove(districts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DisIndex));
            }
        }

        public async Task<IActionResult> StrDelete(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var streets = await (from d in _context.Streets
                                     join p in _context.Districts on d.DistrictId equals p.DistrictId
                                     join a in _context.Localities on p.LocalityId equals a.LocalityId
                                     join b in _context.Areas on a.AreaId equals b.AreaId
                                     join c in _context.Regions on b.RegionId equals c.RegionId
                                     orderby d.StreetId
                                     select new DataStreets { StreetId = d.StreetId, StreetName = d.StreetName, DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId }).FirstOrDefaultAsync(m => m.StreetId == id);

                if (streets == null)
                {
                    return NotFound();
                }

                return View(streets);
            }
        }
        // POST: Operation/Delete/5
        [HttpPost, ActionName("StrDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StrDeleteConfirmed(int id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var streets = await _context.Streets.FindAsync(id);
                _context.Streets.Remove(streets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(StrIndex));
            }
        }

        public async Task<IActionResult> HouseDelete(int? id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var houses = await (from e in _context.Houses
                                    join d in _context.Streets on e.StreetId equals d.StreetId
                                    join p in _context.Districts on d.DistrictId equals p.DistrictId
                                    join a in _context.Localities on p.LocalityId equals a.LocalityId
                                    join b in _context.Areas on a.AreaId equals b.AreaId
                                    join c in _context.Regions on b.RegionId equals c.RegionId
                                    orderby e.HouseId
                                    select new DataHouses { HouseId = e.HouseId, HouseNum = e.HouseNum, Index = e.Index, Floors = e.Floors, Entrances = e.Entrances, Flats = e.Flats, StreetId = d.DistrictId, StreetName = d.StreetName, DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId }).FirstOrDefaultAsync(m => m.HouseId == id);

                if (houses == null)
                {
                    return NotFound();
                }

                return View(houses);
            }
        }
        // POST: Operation/Delete/5
        [HttpPost, ActionName("HouseDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HouseDeleteConfirmed(int id)
        {
            Users Iuser = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var houses = await _context.Houses.FindAsync(id);
                _context.Houses.Remove(houses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HouseIndex));
            }
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
        private bool StreetsExists(int id)
        {
            return _context.Streets.Any(e => e.StreetId == id);
        }
        private bool HousesExists(int id)
        {
            return _context.Houses.Any(e => e.HouseId == id);
        }
    }
}
