using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using address.Models;

namespace address.Data
{
    public class AddressSystemContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Houses> Houses { get; set; }
        public DbSet<Streets> Streets { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Localities> Localities { get; set; }
        public DbSet<Areas> Areas { get; set; }
        public DbSet<Regions> Regions { get; set; }

        public AddressSystemContext(DbContextOptions<AddressSystemContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
