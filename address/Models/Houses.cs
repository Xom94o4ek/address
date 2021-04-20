using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace address.Models
{
    public class Houses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HouseId { get; set; }
        public string HouseNum { get; set; }
        public int Index { get; set; }
        public int Floors { get; set; }
        public int Entrances { get; set; }
        public int Flats { get; set; }

        public int StreetId { get; set; }
        public Streets Streets { get; set; }
    }
}
