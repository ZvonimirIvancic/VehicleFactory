using Microsoft.EntityFrameworkCore;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataAccess
{
    public class VehicleFactoryContext : DbContext
    {
        public VehicleFactoryContext(DbContextOptions<VehicleFactoryContext> options) : base(options) { }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleMake>()
                .HasMany(m => m.Models)
                .WithOne(m => m.Make)
                .HasForeignKey(m => m.MakeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VehicleMake>().HasData(
                new VehicleMake { Id = 1, Name = "Mercedes", Abrv = "MRC" },
                new VehicleMake { Id = 2, Name = "BayerMotorischeWerke", Abrv = "BMW" }
            );
        }
    }
}
