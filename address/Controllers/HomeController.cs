using address.Models;
using address.ModelsData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using address.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace address.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        AddressSystemContext db;
        public HomeController(AddressSystemContext context)
        {
            db = context;
        }

        //[Authorize] //
        public IActionResult Index()
        {
            //return View(db.Users.ToList());
            //return Content(User.Identity.Name);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Search(string RegionName, string LocalityName, string DistrictName, string StreetName, string HouseNum)
        {

            var result = from e in db.Houses
                         join d in db.Streets on e.StreetId equals d.StreetId
                         join p in db.Districts on d.DistrictId equals p.DistrictId
                         join a in db.Localities on p.LocalityId equals a.LocalityId
                         join b in db.Areas on a.AreaId equals b.AreaId
                         join c in db.Regions on b.RegionId equals c.RegionId
                         orderby e.Streets
                         select new DataHouses { HouseId = e.HouseId, HouseNum = e.HouseNum, Index = e.Index, Floors = e.Floors, Entrances = e.Entrances, Flats = e.Flats, StreetId = d.DistrictId, StreetName = d.StreetName, DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };

            if (!String.IsNullOrEmpty(RegionName))
            {
                result = result.Where(s => s.RegionName.Contains(RegionName));
            }

            if (!String.IsNullOrEmpty(LocalityName))
            {
                result = result.Where(s => s.LocalityName.Contains(LocalityName));
            }

            if (!String.IsNullOrEmpty(DistrictName))
            {
                result = result.Where(s => s.DistrictName.Contains(DistrictName));
            }

            if (!String.IsNullOrEmpty(StreetName))
            {
                result = result.Where(s => s.StreetName.Contains(StreetName));
            }

            if (!String.IsNullOrEmpty(HouseNum))
            {
                result = result.Where(s => s.HouseNum.Contains(HouseNum));
            }

            return PartialView(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
