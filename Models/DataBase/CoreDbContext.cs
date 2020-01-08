using Microsoft.EntityFrameworkCore;
using System;

namespace DepartmentRating.DataBase
{
    public class CoreDbContext : DbContext
    {
        //TODO: изменить хардкод CrmRatingConnectionString
        public CoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Offence> Offences { get; set; }
        public DbSet<EventLog> EventLog { get; set; }
        public DbSet<Main> Mains { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                AdminId = 1,
                Date = DateTime.Now,
                Name = "n7701-00-057",
                Owner = "System"
            });
        }
    }
}
