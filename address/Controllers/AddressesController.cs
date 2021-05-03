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
        BaseFilter BaseFilter = new BaseFilter();

        public AddressesController(AddressSystemContext context)
        {
            db = context;
        }

        // GET: Addresses
        public IActionResult Index()
        {
            //return View(await db.Houses.ToListAsync());
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
                ViewBag.Houses = BaseFilter.HousesList(db, ViewBag.Streets);
                return View();
            }
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
    }
}
