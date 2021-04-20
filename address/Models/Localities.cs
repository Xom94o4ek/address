using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace address.Models
{
    public class Localities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocalityId { get; set; }
        public string LocalityName { get; set; }
        public int AreaId { get; set; }
        public Areas Areas { get; set; }
    }
}
