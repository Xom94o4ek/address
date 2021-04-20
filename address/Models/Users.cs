using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace address.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Group { get; set; } //0 - User; 1 - Admin; 2 - Owner
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }
    }
}
