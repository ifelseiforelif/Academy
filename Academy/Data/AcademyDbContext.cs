using Academy.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Context;

public class AcademyDbContext:DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public AcademyDbContext(DbContextOptions<AcademyDbContext> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable("Users");
        modelBuilder.Entity<Student>().Property(s => s.Name).HasDefaultValue("no name");
        modelBuilder.Entity<Group>().ToTable(t => t.HasCheckConstraint("constraintName","[Rating] BETWEEN 1 AND 12"));
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //    optionsBuilder.UseSqlServer("Server=DESKTOP-SKISPK7;Database=academyPV521;Trusted_Connection=True;TrustServerCertificate=True;");
    //}
}
