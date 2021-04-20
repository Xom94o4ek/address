using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace address.Models
{
    public class Streets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StreetId { get; set; }
        public string StreetName { get; set; }
        public int DistrictId { get; set; }
        public Districts Districts { get; set; }
    }
}
