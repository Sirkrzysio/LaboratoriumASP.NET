using Microsoft.EntityFrameworkCore;
using WebAPP.Models;
using WebApp.Models.Services;

namespace WebApp.Models;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity?> Contacts { get; set; }
    
    public DbSet<OrganizationEntity> Organizations { get; set; }
    
    private string DbPath { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "contacts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>()
            .HasOne<OrganizationEntity>(c => c.Organization)
            .WithMany(o => o.Contacts)
            .HasForeignKey(c => c.OrganizationId);

        modelBuilder.Entity<OrganizationEntity>()
            .ToTable("organizations")
            .HasData(
                new OrganizationEntity()
                {
                    Id = 203,
                    Name = "XD2",
                    NIP = "123321123",
                    Regon = "4432234432",
                    
                },
                new OrganizationEntity()
                {
                    Id = 303,
                    Name = "xpp",
                    NIP = "5345345345",
                    Regon = "4412322112",
                    
                }
            );

        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(o => o.Address)
            .HasData(
                new
                {
                    City="Jezusuf", Street="Błotna 13", OrganizationEntityId = 203
                },
                new
                {
                    City="Górełecko", Street="Zachalapana 5", OrganizationEntityId = 303
                }
            );
            
        modelBuilder.Entity<ContactEntity>().ToTable("contacts").HasData(
            new ContactEntity() {
                Id = 1, 
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "",
                PhoneNumber = "123 456 789",
                BirthDate = new DateOnly(1990, 1, 1),
                Created = DateTime.Now,
                OrganizationId = 203 
            },
            new ContactEntity() {
                Id = 2,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "", 
                PhoneNumber = "987 654 321",
                BirthDate = new DateOnly(1995, 5, 5),
                Created = DateTime.Now,
                OrganizationId = 203
            }, new ContactEntity() {
                Id = 3, 
                FirstName = "Piotr", 
                LastName = "Wiśniewski",
                Email = "", 
                PhoneNumber = "456 789 123",
                BirthDate = new DateOnly(2000, 10, 10),
                Created = DateTime.Now,
                OrganizationId = 203
                
            }
        );
    }
}



