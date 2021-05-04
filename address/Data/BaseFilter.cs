using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using address.Models;

namespace address.Data
{
    public class BaseFilter
    {
        /// <summary>
        /// Получение списка регионов
        /// </summary>
        /// <param name="db">БД</param>
        /// <param name="RegionNum">Выбранный регион по умолчанию</param>
        public SelectList RegionsList(AddressSystemContext db, int RegionNum)
        {
            SelectList regions = new SelectList(db.Regions, "RegionId", "RegionName", RegionNum);
            return regions;
        }
        /// <summary>
        /// Получение списка районов исходя из региона
        /// </summary>
        /// <param name="db">БД</param>
        /// <param name="regions">Список регионов</param>
        public SelectList AreasList(AddressSystemContext db, SelectList regions)
        {
            IQueryable<Areas> FilterAreas = db.Areas.Where(c => c.RegionId == Convert.ToInt32(regions.SelectedValue));
            int AreaNum = FilterAreas.Count() == 0 ? 0 : FilterAreas.First().AreaId;
            SelectList areas = new SelectList(FilterAreas, "AreaId", "AreaName", AreaNum);
            return areas;
        }
        /// <summary>
        /// Получение списка районов исходя из региона
        /// </summary>
        /// <param name="db">БД</param>
        /// <param name="OurRegId">Выбранный регион</param>
        /// <param name="OurAreaId">Выбранный район по умолчанию</param>
        public SelectList AreasList(AddressSystemContext db, int OurRegId, int OurAreaId)
        {
            SelectList areas = new SelectList(db.Areas.Where(c => c.RegionId == OurRegId), "AreaId", "AreaName", OurAreaId);
            return areas;
        }
        /// <summary>
        /// Получение списка населенных пунктов исходя из районов
        /// </summary>
        /// <param name="db">БД</param>
        /// <param name="areas">Список районов</param>
        public SelectList LocalitiesList(AddressSystemContext db, SelectList areas)
        {
            IQueryable<Localities> FilterLocalities = db.Localities.Where(c => c.AreaId == Convert.ToInt32(areas.SelectedValue));
            int LocalitiesNum = FilterLocalities.Count() == 0 ? 0 : FilterLocalities.First().LocalityId;
            SelectList localities = new SelectList(FilterLocalities, "LocalityId", "LocalityName", LocalitiesNum);
            return localities;
        }
        /// <summary>
        /// Получение списка населенных пунктов исходя из районов
        /// </summary>
        /// <param name="db">БД</param>
        /// <param name="OurAreaId">Выбранный район</param>
        /// <param name="OurLocalityId">Выбранный населенный пункт по умолчанию</param>
        public SelectList LocalitiesList(AddressSystemContext db, int OurAreaId, int OurLocalityId)
        {
            SelectList localities = new SelectList(db.Localities.Where(c => c.AreaId == OurAreaId), "LocalityId", "LocalityName", OurLocalityId);
            return localities;
        }
        /// <summary>
        /// Получение списка ЭПС пунктов исходя из населенных пунктов
        /// </summary>
        /// <param name="db">БД</param>
        /// <param name="localities">Список населенных пунктов</param>
        public SelectList DistrictsList(AddressSystemContext db, SelectList localities)
        {
            IQueryable<Districts> FilterDistricts = db.Districts.Where(c => c.LocalityId == Convert.ToInt32(localities.SelectedValue));
            int DistrictsNum = FilterDistricts.Count() == 0 ? 0 : FilterDistricts.First().DistrictId;
            SelectList districts = new SelectList(FilterDistricts, "DistrictId", "DistrictName", DistrictsNum);
            return districts;
        }
        /// <summary>
        /// Получение списка ЭПС пунктов исходя из населенных пунктов
        /// </summary>
        /// <param name="db">БД</param>
        /// <param name="OurLocalityId">Выбранный населенный пункт</param>
        /// <param name="OuDistrictId">Выбранный ЭПС по умолчанию</param>
        public SelectList DistrictsList(AddressSystemContext db, int OurLocalityId, int OuDistrictId)
        {
            SelectList districts = new SelectList(db.Districts.Where(c => c.LocalityId == OurLocalityId), "DistrictId", "DistrictName", OuDistrictId);
            return districts;
        }
        /// <summary>
        /// Получение списка улиц исходя из ЭПС
        /// </summary>
        /// <param name="db">БД</param>
        /// <param name="districts">Список ЭПС</param>
        public SelectList StreetsList(AddressSystemContext db, SelectList districts)
        {
            IQueryable<Streets> FilterStreets = db.Streets.Where(c => c.DistrictId == Convert.ToInt32(districts.SelectedValue));
            int StreetsNum = FilterStreets.Count() == 0 ? 0 : FilterStreets.First().StreetId;
            SelectList streets = new SelectList(FilterStreets, "StreetId", "StreetName", StreetsNum);
            return streets;
        }
        /// <summary>
        /// Получение списка улиц исходя из ЭПС
        /// </summary>
        /// <param name="db">БД</param>
        /// <param name="OuDistrictId">Выбранный ЭПС</param>
        /// <param name="OuStreetId">Выбранная улица по умолчанию</param>
        public SelectList StreetsList(AddressSystemContext db, int OuDistrictId, int OuStreetId)
        {
            SelectList streets = new SelectList(db.Streets.Where(c => c.DistrictId == OuDistrictId), "StreetId", "StreetName", OuStreetId);
            return streets;
        }
        /// <summary>
        /// Получение списка домов исходя из улиц
        /// </summary>
        /// <param name="db">БД</param>
        /// <param name="streets">Список улиц</param>
        public SelectList HousesList(AddressSystemContext db, SelectList streets)
        {
            IQueryable<Houses> FilterHouses = db.Houses.Where(c => c.StreetId == Convert.ToInt32(streets.SelectedValue));
            int HousesNum = FilterHouses.Count() == 0 ? 0 : FilterHouses.First().HouseId;
            SelectList houses = new SelectList(FilterHouses, "HouseId", "HouseNum", HousesNum);
            return houses;
        }
    }
}
