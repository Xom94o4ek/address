using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace address.ModelsData
{
    public class DataDistricts
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int LocalityId { get; set; }
        public string LocalityName { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }
}
