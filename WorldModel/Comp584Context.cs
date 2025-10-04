using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WorldModel;

public partial class Comp584Context : DbContext
{
    public Comp584Context()
    {
    }

    public Comp584Context(DbContextOptions<Comp584Context> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationBuilder configurationbuilder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json").AddJsonFile("appsettings.Development.json",optional:true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasOne(d => d.CountryIdentifierNavigation).WithMany(p => p.Cities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_Country");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
