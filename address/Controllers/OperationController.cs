using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using address.Data;
using address.Models;
using address.Log;

namespace address.Controllers
{
    public class OperationController : Controller
    {
        private readonly AddressSystemContext db;
        Logger Log = new Logger();
        BaseFilter BaseFilter = new BaseFilter();
        OperationFilter OperationFilter = new OperationFilter();
        public OperationController(AddressSystemContext context)
        {
            db = context;
        }

        /// <summary>
        /// Вывод/Поиск регионов
        /// </summary>
        /// <param name="id">Название региона</param>
        public async Task<IActionResult> RegIndex(string id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var regions = OperationFilter.RegionsFiltered(db);

                if (!String.IsNullOrEmpty(id))
                {
                    regions = regions.Where(s => s.RegionName.Contains(id)).OrderBy(m => m.RegionId);
                }

                return View(await regions.ToListAsync());
            }
        }
        /// <summary>
        /// Вывод/Поиск районов
        /// </summary>
        /// <param name="id">Название района</param>
        public async Task<IActionResult> AreaIndex(string id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var areas = OperationFilter.AreasFiltered(db);
                if (!String.IsNullOrEmpty(id))
                {
                    areas = areas.Where(s => s.AreaName.Contains(id));
                }

                return View(await areas.ToListAsync());
            }
        }
        /// <summary>
        /// Вывод/Поиск населенных пунктов
        /// </summary>
        /// <param name="id">Название населенного пункта</param>
        public async Task<IActionResult> LocIndex(string id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var localities = OperationFilter.LocalitiesFiltered(db);
                if (!String.IsNullOrEmpty(id))
                {
                    localities = localities.Where(s => s.LocalityName.Contains(id));
                }

                return View(await localities.ToListAsync());
            }
        }
        /// <summary>
        /// Вывод/Поиск ЭПС
        /// </summary>
        /// <param name="id">Название ЭПС</param>
        public async Task<IActionResult> DisIndex(string id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var districts = OperationFilter.DistrictsFiltered(db);

                if (!String.IsNullOrEmpty(id))
                {
                    districts = districts.Where(s => s.DistrictName.Contains(id));
                }

                return View(await districts.ToListAsync());
            }
        }
        /// <summary>
        /// Вывод/Поиск улиц
        /// </summary>
        /// <param name="id">Название улицы</param>
        public async Task<IActionResult> StrIndex(string id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var streets = OperationFilter.StreetsFiltered(db);

                if (!String.IsNullOrEmpty(id))
                {
                    streets = streets.Where(s => s.StreetName.Contains(id));
                }

                return View(await streets.ToListAsync());
            }
        }
        /// <summary>
        /// Вывод/Поиск домов
        /// </summary>
        /// <param name="id">Номер дома</param>
        public async Task<IActionResult> HouseIndex(string id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var houses = OperationFilter.HousesFiltered(db);

                if (!String.IsNullOrEmpty(id))
                {
                    houses = houses.Where(s => s.StreetName.Contains(id));
                }

                return View(await houses.ToListAsync());
            }
        }
        /// <summary>
        /// Вывод информации о регионе
        /// </summary>
        /// <param name="id">id региона</param>
        public async Task<IActionResult> RegDetails(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var regions = await OperationFilter.RegionsFiltered(db).FirstOrDefaultAsync(m => m.RegionId == id);
                if (regions == null)
                {
                    return NotFound();
                }

                return View(regions);
            }
        }
        /// <summary>
        /// Вывод информации о районе
        /// </summary>
        /// <param name="id">id района</param>
        public async Task<IActionResult> AreaDetails(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var areas = await OperationFilter.AreasFiltered(db).FirstOrDefaultAsync(m => m.AreaId == id);

                if (areas == null)
                {
                    return NotFound();
                }

                return View(areas);
            }
        }
        /// <summary>
        /// Вывод информации о населенном пункте
        /// </summary>
        /// <param name="id">id населенного пункта</param>
        public async Task<IActionResult> LocDetails(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var localities = await OperationFilter.LocalitiesFiltered(db).FirstOrDefaultAsync(m => m.LocalityId == id);

                if (localities == null)
                {
                    return NotFound();
                }

                return View(localities);
            }
        }
        /// <summary>
        /// Вывод информации об ЭПС
        /// </summary>
        /// <param name="id">id ЭПС</param>
        public async Task<IActionResult> DisDetails(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var districts = await OperationFilter.DistrictsFiltered(db).FirstOrDefaultAsync(m => m.DistrictId == id);

                if (districts == null)
                {
                    return NotFound();
                }

                return View(districts);
            }
        }
        /// <summary>
        /// Вывод информации о улице
        /// </summary>
        /// <param name="id">id улицы</param>
        public async Task<IActionResult> StrDetails(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var streets = await OperationFilter.StreetsFiltered(db).FirstOrDefaultAsync(m => m.StreetId == id);

                if (streets == null)
                {
                    return NotFound();
                }

                return View(streets);
            }
        }
        /// <summary>
        /// Вывод информации о доме
        /// </summary>
        /// <param name="id">id дома</param>
        public async Task<IActionResult> HouseDetails(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var houses = await OperationFilter.HousesFiltered(db).FirstOrDefaultAsync(m => m.HouseId == id);

                if (houses == null)
                {
                    return NotFound();
                }

                return View(houses);
            }
        }
        /// <summary>
        /// Создание региона
        /// </summary>
        public IActionResult RegCreate()
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }
        /// <summary>
        /// Создание района
        /// </summary>
        public IActionResult AreaCreate()
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Regions = BaseFilter.RegionsList(db, 1);
                return View();
            }
        }
        /// <summary>
        /// Создание населенного пункта
        /// </summary>
        public IActionResult LocCreate()
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Regions = BaseFilter.RegionsList(db, 1);
                ViewBag.Areas = BaseFilter.AreasList(db, ViewBag.Regions);
                return View();
            }
        }
        /// <summary>
        /// Создание ЭПС
        /// </summary>
        /// <param name="id">id ЭПС</param>
        public IActionResult DisCreate()
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Regions = BaseFilter.RegionsList(db, 1);
                ViewBag.Areas = BaseFilter.AreasList(db, ViewBag.Regions);
                ViewBag.Localities = BaseFilter.LocalitiesList(db, ViewBag.Areas);
                return View();
            }
        }
        /// <summary>
        /// Создание улицы
        /// </summary>
        public IActionResult StrCreate()
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Regions = BaseFilter.RegionsList(db, 1);
                ViewBag.Areas = BaseFilter.AreasList(db, ViewBag.Regions);
                ViewBag.Localities = BaseFilter.LocalitiesList(db, ViewBag.Areas);
                ViewBag.Districts = BaseFilter.LocalitiesList(db, ViewBag.Localities);
                return View();
            }
        }
        /// <summary>
        /// Создание дома
        /// </summary>
        public IActionResult HouseCreate()
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Regions = BaseFilter.RegionsList(db, 1);
                ViewBag.Areas = BaseFilter.AreasList(db, ViewBag.Regions);
                ViewBag.Localities = BaseFilter.LocalitiesList(db, ViewBag.Areas);
                ViewBag.Districts = BaseFilter.LocalitiesList(db, ViewBag.Localities);
                ViewBag.Streets = BaseFilter.StreetsList(db, ViewBag.Districts);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegCreate([Bind("RegionName")] Regions regions)
        {
                if (ModelState.IsValid)
                {
                    db.Add(regions);
                    await db.SaveChangesAsync();
                    Log.Info($"[Region:Create][User:{User.Identity.Name}]", regions.RegionName);
                    return RedirectToAction(nameof(RegIndex));
                }
                return View(regions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AreaCreate([Bind("AreaName", "RegionId")] Areas areas)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Add(areas);
                    await db.SaveChangesAsync();
                    Log.Info($"[Area:Create][User:{User.Identity.Name}]", areas.AreaName, areas.RegionId);
                    return RedirectToAction(nameof(AreaIndex));
                }
                return View(areas);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocCreate([Bind("LocalityName", "AreaId")] Localities localities)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Add(localities);
                    await db.SaveChangesAsync();
                    Log.Info($"[Locality:Create][User:{User.Identity.Name}]", localities.LocalityName, localities.AreaId);
                    return RedirectToAction(nameof(LocIndex));
                }
                return View(localities);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisCreate([Bind("DistrictName", "LocalityId")] Districts districts)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Add(districts);
                    await db.SaveChangesAsync();
                    Log.Info($"[District:Create][User:{User.Identity.Name}]", districts.DistrictName, districts.LocalityId);
                    return RedirectToAction(nameof(DisIndex));
                }
                return View(districts);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StrCreate([Bind("StreetName", "DistrictId")] Streets streets)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Add(streets);
                    await db.SaveChangesAsync();
                    Log.Info($"[Street:Create][User:{User.Identity.Name}]", streets.StreetName,streets.DistrictId);
                    return RedirectToAction(nameof(StrIndex));
                }
                return View(streets);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HouseCreate([Bind("HouseNum", "Index", "Floors", "Entrances", "Flats", "StreetId")] Houses houses)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Add(houses);
                    await db.SaveChangesAsync();
                    Log.Info($"[House:Create][User:{User.Identity.Name}]", houses.HouseNum, houses.Index, houses.Floors, houses.Entrances, houses.Flats, houses.StreetId);
                    return RedirectToAction(nameof(HouseIndex));
                }
                return View(houses);
            }
        }
        /// <summary>
        /// Редактирование региона
        /// </summary>
        /// <param name="id">id региона</param>
        public async Task<IActionResult> RegEdit(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var regions = await db.Regions.FindAsync(id);
                if (regions == null)
                {
                    return NotFound();
                }
                return View(regions);
            }
        }
        /// <summary>
        /// Редактирование района
        /// </summary>
        /// <param name="id">id района</param>
        public async Task<IActionResult> AreaEdit(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                var areas = await db.Areas.FindAsync(id);
                ViewBag.Regions = BaseFilter.RegionsList(db, areas.RegionId);
                if (areas == null)
                {
                    return NotFound();
                }
                return View(areas);
            }
        }
        /// <summary>
        /// Редактирование населенного пункта
        /// </summary>
        /// <param name="id">id населенного пункта</param>
        public async Task<IActionResult> LocEdit(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                var localities = await db.Localities.FindAsync(id);
                int OurAreaId = localities.AreaId;
                int OurRegId = (await db.Areas.FindAsync(OurAreaId)).RegionId;
                ViewBag.Areas = BaseFilter.AreasList(db, OurRegId, OurAreaId);
                ViewBag.Regions = BaseFilter.RegionsList(db, OurRegId);
                if (localities == null)
                {
                    return NotFound();
                }
                return View(localities);
            }
        }
        /// <summary>
        /// Редактирование ЭПС
        /// </summary>
        /// <param name="id">id ЭПС</param>
        public async Task<IActionResult> DisEdit(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                var districts = await db.Districts.FindAsync(id);
                int OurLocalityId = districts.LocalityId;
                int OurAreaId = (await db.Localities.FindAsync(OurLocalityId)).AreaId;
                int OurRegId = (await db.Areas.FindAsync(OurAreaId)).RegionId;

                ViewBag.Localities = BaseFilter.LocalitiesList(db, OurAreaId, OurLocalityId);
                ViewBag.Areas = BaseFilter.AreasList(db, OurRegId, OurAreaId);
                ViewBag.Regions = BaseFilter.RegionsList(db, OurRegId);
                if (districts == null)
                {
                    return NotFound();
                }
                return View(districts);
            }
        }
        /// <summary>
        /// Редактирование улицы
        /// </summary>
        /// <param name="id">id улицы</param>
        public async Task<IActionResult> StrEdit(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                var streets = await db.Streets.FindAsync(id);
                int OuDistrictId = streets.DistrictId;
                int OurLocalityId = (await db.Districts.FindAsync(OuDistrictId)).LocalityId;
                int OurAreaId = (await db.Localities.FindAsync(OurLocalityId)).AreaId;
                int OurRegId = (await db.Areas.FindAsync(OurAreaId)).RegionId;

                ViewBag.Districts = BaseFilter.DistrictsList(db, OurLocalityId, OuDistrictId);
                ViewBag.Localities = BaseFilter.LocalitiesList(db, OurAreaId, OurLocalityId);
                ViewBag.Areas = BaseFilter.AreasList(db, OurRegId, OurAreaId);
                ViewBag.Regions = BaseFilter.RegionsList(db, OurRegId);
                if (streets == null)
                {
                    return NotFound();
                }
                return View(streets);
            }
        }
        /// <summary>
        /// Редактирование дома
        /// </summary>
        /// <param name="id">id дома</param>
        public async Task<IActionResult> HouseEdit(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                var houses = await db.Houses.FindAsync(id);
                int OuStreetId = houses.StreetId;
                int OuDistrictId = (await db.Streets.FindAsync(OuStreetId)).DistrictId; ;
                int OurLocalityId = (await db.Districts.FindAsync(OuDistrictId)).LocalityId;
                int OurAreaId = (await db.Localities.FindAsync(OurLocalityId)).AreaId;
                int OurRegId = (await db.Areas.FindAsync(OurAreaId)).RegionId;

                ViewBag.Streets = BaseFilter.StreetsList(db, OuDistrictId, OuStreetId);
                ViewBag.Districts = BaseFilter.DistrictsList(db, OurLocalityId, OuDistrictId);
                ViewBag.Localities = BaseFilter.LocalitiesList(db, OurAreaId, OurLocalityId);
                ViewBag.Areas = BaseFilter.AreasList(db, OurRegId, OurAreaId);
                ViewBag.Regions = BaseFilter.RegionsList(db, OurRegId);
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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                        db.Update(regions);
                        await db.SaveChangesAsync();
                        Log.Info($"[Region:Edit][User:{User.Identity.Name}]", regions.RegionId, regions.RegionName);
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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                        db.Update(areas);
                        await db.SaveChangesAsync();
                        Log.Info($"[Area:Edit][User:{User.Identity.Name}]", areas.AreaId, areas.AreaName, areas.RegionId);
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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                        db.Update(localities);
                        await db.SaveChangesAsync();
                        Log.Info($"[Locality:Edit][User:{User.Identity.Name}]", localities.LocalityId, localities.LocalityName, localities.AreaId);
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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                        db.Update(districts);
                        await db.SaveChangesAsync();
                        Log.Info($"[District:Edit][User:{User.Identity.Name}]", districts.DistrictId, districts.DistrictName, districts.LocalityId);
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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                        db.Update(streets);
                        await db.SaveChangesAsync();
                        Log.Info($"[Street:Edit][User:{User.Identity.Name}]", streets.StreetId, streets.StreetName, streets.DistrictId);
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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                        db.Update(houses);
                        await db.SaveChangesAsync();
                        Log.Info($"[House:Edit][User:{User.Identity.Name}]", houses.HouseId, houses.HouseNum, houses.Index, houses.Floors, houses.Entrances, houses.Flats, houses.StreetId);
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
        /// <summary>
        /// Удаление региона
        /// </summary>
        /// <param name="id">id региона</param>
        public async Task<IActionResult> RegDelete(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var regions = await OperationFilter.RegionsFiltered(db).FirstOrDefaultAsync(m => m.RegionId == id);
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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var regions = await db.Regions.FindAsync(id);
                db.Regions.Remove(regions);
                await db.SaveChangesAsync();
                Log.Info($"[Region:Delete][User:{User.Identity.Name}]", regions.RegionId, regions.RegionName);
                return RedirectToAction(nameof(RegIndex));
            }
        }
        /// <summary>
        /// Удаление района
        /// </summary>
        /// <param name="id">id района</param>
        public async Task<IActionResult> AreaDelete(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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
                var areas = await OperationFilter.AreasFiltered(db).FirstOrDefaultAsync(m => m.AreaId == id);
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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var areas = await db.Areas.FindAsync(id);
                db.Areas.Remove(areas);
                await db.SaveChangesAsync();
                Log.Info($"[Area:Delete][User:{User.Identity.Name}]", areas.AreaId, areas.AreaName, areas.RegionId);
                return RedirectToAction(nameof(AreaIndex));
            }
        }
        /// <summary>
        /// Удаление населенного пункта
        /// </summary>
        /// <param name="id">id населенного пункта</param>
        public async Task<IActionResult> LocDelete(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var localities = await OperationFilter.LocalitiesFiltered(db).FirstOrDefaultAsync(m => m.LocalityId == id);
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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var localities = await db.Localities.FindAsync(id);
                db.Localities.Remove(localities);
                await db.SaveChangesAsync();
                Log.Info($"[Locality:Delete][User:{User.Identity.Name}]", localities.LocalityId, localities.LocalityName, localities.AreaId);
                return RedirectToAction(nameof(LocIndex));
            }
        }
        /// <summary>
        /// Удаление ЭПС
        /// </summary>
        /// <param name="id">id ЭПС</param>
        public async Task<IActionResult> DisDelete(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var districts = await OperationFilter.DistrictsFiltered(db).FirstOrDefaultAsync(m => m.DistrictId == id);
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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var districts = await db.Districts.FindAsync(id);
                db.Districts.Remove(districts);
                await db.SaveChangesAsync();
                Log.Info($"[District:Delete][User:{User.Identity.Name}]", districts.DistrictId, districts.DistrictName, districts.LocalityId);
                return RedirectToAction(nameof(DisIndex));
            }
        }
        /// <summary>
        /// Удаление улицы
        /// </summary>
        /// <param name="id">id улицы</param>
        public async Task<IActionResult> StrDelete(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var streets = await OperationFilter.StreetsFiltered(db).FirstOrDefaultAsync(m => m.StreetId == id);

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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var streets = await db.Streets.FindAsync(id);
                db.Streets.Remove(streets);
                await db.SaveChangesAsync();
                Log.Info($"[Street:Delete][User:{User.Identity.Name}]", streets.StreetId, streets.StreetName, streets.DistrictId);
                return RedirectToAction(nameof(StrIndex));
            }
        }
        /// <summary>
        /// Удаление дома
        /// </summary>
        /// <param name="id">id дома</param>
        public async Task<IActionResult> HouseDelete(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
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

                var houses = await OperationFilter.HousesFiltered(db).FirstOrDefaultAsync(m => m.HouseId == id);

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
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var houses = await db.Houses.FindAsync(id);
                db.Houses.Remove(houses);
                await db.SaveChangesAsync();
                Log.Info($"[House:Delete][User:{User.Identity.Name}]", houses.HouseId, houses.HouseNum, houses.Index, houses.Floors, houses.Entrances, houses.Flats, houses.StreetId);
                return RedirectToAction(nameof(HouseIndex));
            }
        }
        /// <summary>
        /// Проверки существования региона, района и тд
        /// </summary>
        private bool RegionsExists(int id)
        {
            return db.Regions.Any(e => e.RegionId == id);
        }
        private bool AreasExists(int id)
        {
            return db.Areas.Any(e => e.AreaId == id);
        }
        private bool LocalitiesExists(int id)
        {
            return db.Localities.Any(e => e.LocalityId == id);
        }
        private bool DistrictsExists(int id)
        {
            return db.Districts.Any(e => e.DistrictId == id);
        }
        private bool StreetsExists(int id)
        {
            return db.Streets.Any(e => e.StreetId == id);
        }
        private bool HousesExists(int id)
        {
            return db.Houses.Any(e => e.HouseId == id);
        }
    }
}
