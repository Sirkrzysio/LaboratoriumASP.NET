using Microsoft.EntityFrameworkCore;
using WebApp.Models.Services;

namespace WebApp.Models;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity?> Contacts { get; set; }
    
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

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<ContactEntity>().ToTable("contacts").HasData(
            new ContactEntity() {
                Id = 1, 
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "",
                PhoneNumber = "123 456 789",
                BirthDate = new DateOnly(1990, 1, 1),
                Created = DateTime.Now
            },
            new ContactEntity() {
                Id = 2,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "", 
                PhoneNumber = "987 654 321",
                BirthDate = new DateOnly(1995, 5, 5),
                Created = DateTime.Now
            }, new ContactEntity() {
                Id = 3, 
                FirstName = "Piotr", 
                LastName = "Wiśniewski",
                Email = "", 
                PhoneNumber = "456 789 123",
                BirthDate = new DateOnly(2000, 10, 10),
                Created = DateTime.Now
            }
        );
    }
}



