using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using address.Data;
using address.Models;

namespace address.Data
{
    public class BaseFilter
    {
        public SelectList RegionsList(AddressSystemContext db, int RegionNum)
        {
            SelectList regions = new SelectList(db.Regions, "RegionId", "RegionName", RegionNum);
            return regions;
        }
        public SelectList AreasList(AddressSystemContext db, SelectList regions)
        {
            IQueryable<Areas> FilterAreas = db.Areas.Where(c => c.RegionId == Convert.ToInt32(regions.SelectedValue));
            int AreaNum = FilterAreas.Count() == 0 ? 0 : FilterAreas.First().AreaId;
            SelectList areas = new SelectList(FilterAreas, "AreaId", "AreaName", AreaNum);
            return areas;
        }
        public SelectList AreasList(AddressSystemContext _context, int OurRegId, int OurAreaId)
        {
            SelectList areas = new SelectList(_context.Areas.Where(c => c.RegionId == OurRegId), "AreaId", "AreaName", OurAreaId);
            return areas;
        }
        public SelectList LocalitiesList(AddressSystemContext db, SelectList areas)
        {
            IQueryable<Localities> FilterLocalities = db.Localities.Where(c => c.AreaId == Convert.ToInt32(areas.SelectedValue));
            int LocalitiesNum = FilterLocalities.Count() == 0 ? 0 : FilterLocalities.First().LocalityId;
            SelectList localities = new SelectList(FilterLocalities, "LocalityId", "LocalityName", LocalitiesNum);
            return localities;
        }
        public SelectList LocalitiesList(AddressSystemContext _context, int OurAreaId, int OurLocalityId)
        {
            SelectList localities = new SelectList(_context.Localities.Where(c => c.AreaId == OurAreaId), "LocalityId", "LocalityName", OurLocalityId);
            return localities;
        }
        public SelectList DistrictsList(AddressSystemContext db, SelectList localities)
        {
            IQueryable<Districts> FilterDistricts = db.Districts.Where(c => c.LocalityId == Convert.ToInt32(localities.SelectedValue));
            int DistrictsNum = FilterDistricts.Count() == 0 ? 0 : FilterDistricts.First().DistrictId;
            SelectList districts = new SelectList(FilterDistricts, "DistrictId", "DistrictName", DistrictsNum);
            return districts;
        }
        public SelectList DistrictsList(AddressSystemContext _context, int OurLocalityId, int OuDistrictId)
        {
            SelectList districts = new SelectList(_context.Districts.Where(c => c.LocalityId == OurLocalityId), "DistrictId", "DistrictName", OuDistrictId);
            return districts;
        }
        public SelectList StreetsList(AddressSystemContext db, SelectList districts)
        {
            IQueryable<Streets> FilterStreets = db.Streets.Where(c => c.DistrictId == Convert.ToInt32(districts.SelectedValue));
            int StreetsNum = FilterStreets.Count() == 0 ? 0 : FilterStreets.First().StreetId;
            SelectList streets = new SelectList(FilterStreets, "StreetId", "StreetName", StreetsNum);
            return streets;
        }
        public SelectList StreetsList(AddressSystemContext _context, int OuDistrictId, int OuStreetId)
        {
            SelectList streets = new SelectList(_context.Streets.Where(c => c.DistrictId == OuDistrictId), "StreetId", "StreetName", OuStreetId);
            return streets;
        }
        public SelectList HousesList(AddressSystemContext db, SelectList streets)
        {
            IQueryable<Houses> FilterHouses = db.Houses.Where(c => c.StreetId == Convert.ToInt32(streets.SelectedValue));
            int HousesNum = FilterHouses.Count() == 0 ? 0 : FilterHouses.First().HouseId;
            SelectList houses = new SelectList(FilterHouses, "HouseId", "HouseNum", HousesNum);
            return houses;
        }
    }
}
