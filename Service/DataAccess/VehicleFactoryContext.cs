using Microsoft.EntityFrameworkCore;
using Service.Models;

public class VehicleFactoryContext : DbContext
{
    public VehicleFactoryContext(DbContextOptions<VehicleFactoryContext> options) : base(options) { }

    public DbSet<VehicleMake> VehicleMakes { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<VehicleMake>().ToTable("VehicleMake");
        modelBuilder.Entity<VehicleModel>().ToTable("VehicleModel");

        modelBuilder.Entity<VehicleMake>()
            .HasMany(m => m.VehicleModels)
            .WithOne(m => m.Make)
            .HasForeignKey(m => m.MakeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VehicleMake>().HasData(
            new VehicleMake { Id = 1, Name = "Mercedes", Abrv = "MRC" },
            new VehicleMake { Id = 2, Name = "BayerMotorischeWerke", Abrv = "BMW" }
        );
    }
}