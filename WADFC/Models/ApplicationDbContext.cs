using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace WADFC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Fight> Fights { get; set; }
    }
}
