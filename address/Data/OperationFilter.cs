using System.Collections.Generic;
using System.Linq;
using address.Models;
using address.ModelsData;

namespace address.Data
{
    public class OperationFilter
    {
        /// <summary>
        /// Получение данных регионов
        /// </summary>
        /// <param name="db">БД</param>
        public IQueryable<Regions> RegionsFiltered(AddressSystemContext db)
        {
            var regions = from m in db.Regions
                          select m;
            return regions;
        }
        /// <summary>
        /// Получение данных районов
        /// </summary>
        /// <param name="db">БД</param>
        public IQueryable<DataAreas> AreasFiltered(AddressSystemContext db)
        {
            var areas = from p in db.Areas
                        join c in db.Regions on p.RegionId equals c.RegionId
                        orderby p.AreaId
                        select new DataAreas { AreaId = p.AreaId, AreaName = p.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };
            return areas;
        }
        /// <summary>
        /// Получение данных населенных пунктов
        /// </summary>
        /// <param name="db">БД</param>
        public IQueryable<DataLocalities> LocalitiesFiltered(AddressSystemContext db)
        {
            var localities = from p in db.Localities
                             join b in db.Areas on p.AreaId equals b.AreaId
                             join c in db.Regions on b.RegionId equals c.RegionId
                             orderby p.LocalityId
                             select new DataLocalities { LocalityId = p.LocalityId, LocalityName = p.LocalityName, AreaId = p.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };
            return localities;
        }
        /// <summary>
        /// Получение данных ЭПС
        /// </summary>
        /// <param name="db">БД</param>
        public IQueryable<DataDistricts> DistrictsFiltered(AddressSystemContext db)
        {
            var districts = from p in db.Districts
                            join a in db.Localities on p.LocalityId equals a.LocalityId
                            join b in db.Areas on a.AreaId equals b.AreaId
                            join c in db.Regions on b.RegionId equals c.RegionId
                            orderby p.DistrictId
                            select new DataDistricts { DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };
            return districts;
        }
        /// <summary>
        /// Получение данных улиц
        /// </summary>
        /// <param name="db">БД</param>
        public IQueryable<DataStreets> StreetsFiltered(AddressSystemContext db)
        {
            var streets = from d in db.Streets
                          join p in db.Districts on d.DistrictId equals p.DistrictId
                          join a in db.Localities on p.LocalityId equals a.LocalityId
                          join b in db.Areas on a.AreaId equals b.AreaId
                          join c in db.Regions on b.RegionId equals c.RegionId
                          orderby d.StreetId
                          select new DataStreets { StreetId = d.StreetId, StreetName = d.StreetName, DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };
            return streets;
        }
        /// <summary>
        /// Получение данных домов
        /// </summary>
        /// <param name="db">БД</param>
        public IQueryable<DataHouses> HousesFiltered(AddressSystemContext db)
        {
            var houses = from e in db.Houses
                         join d in db.Streets on e.StreetId equals d.StreetId
                         join p in db.Districts on d.DistrictId equals p.DistrictId
                         join a in db.Localities on p.LocalityId equals a.LocalityId
                         join b in db.Areas on a.AreaId equals b.AreaId
                         join c in db.Regions on b.RegionId equals c.RegionId
                         orderby e.HouseId
                         select new DataHouses { HouseId = e.HouseId, HouseNum = e.HouseNum, Index = e.Index, Floors = e.Floors, Entrances = e.Entrances, Flats = e.Flats, StreetId = d.StreetId, StreetName = d.StreetName, DistrictId = p.DistrictId, DistrictName = p.DistrictName, LocalityId = a.LocalityId, LocalityName = a.LocalityName, AreaId = b.AreaId, AreaName = b.AreaName, RegionName = c.RegionName, RegionId = c.RegionId };
            return houses;
        }
    }
}
