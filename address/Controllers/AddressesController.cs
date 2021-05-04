using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// Инициализация страницы управления адресами для администратора
        /// </summary>
        public IActionResult Index()
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
                ViewBag.Houses = BaseFilter.HousesList(db, ViewBag.Streets);
                return View();
            }
        }
        /// <summary>
        /// Получение районов
        /// </summary>
        /// <param name="id">id региона</param>
        public ActionResult GetAreas(int id)
        {
            return PartialView(db.Areas.Where(c => c.RegionId == id).ToList());
        }
        /// <summary>
        /// Получение населенных пунктов
        /// </summary>
        /// <param name="id">id района</param>
        public ActionResult GetLocalities(int id)
        {
            return PartialView(db.Localities.Where(c => c.AreaId == id).ToList());
        }
        /// <summary>
        /// Получение ЭПС
        /// </summary>
        /// <param name="id">id населенного пункта</param>
        public ActionResult GetDistricts(int id)
        {
            return PartialView(db.Districts.Where(c => c.LocalityId == id).ToList());
        }
        /// <summary>
        /// Получение улиц
        /// </summary>
        /// <param name="id">id ЭПС</param>
        public ActionResult GetStreets(int id)
        {
            return PartialView(db.Streets.Where(c => c.DistrictId == id).ToList());
        }
        /// <summary>
        /// Получение домов
        /// </summary>
        /// <param name="id">id улицы</param>
        public ActionResult GetHouses(int id)
        {
            return PartialView(db.Houses.Where(c => c.StreetId == id).ToList());
        }
    }
}
