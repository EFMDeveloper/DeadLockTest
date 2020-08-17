using DeadLockTestJulisyAmador.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeadLockTestJulisyAmador.DataContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {         
        }

        public DbSet<Position> Positions { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>(opt =>
            {
                opt.HasKey(o => o.Id);
                opt.Property(o => o.Description).HasMaxLength(60);
            });

            modelBuilder.Entity<Person>(opt =>
            {
                opt.HasKey(o => o.Id);

                opt.Property(o => o.FirstName).HasMaxLength(60);
                opt.Property(o => o.LastName).HasMaxLength(60);


                opt.HasOne(o => o.Position)
                .WithMany(o => o.Person)
                .HasForeignKey(o => o.PositionId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
