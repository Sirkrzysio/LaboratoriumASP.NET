using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Context;

public partial class UniversityDB : DbContext
{
    public UniversityDB()
    {
    }

    public UniversityDB(DbContextOptions<UniversityDB> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<RankingCriterion> RankingCriteria { get; set; }

    public virtual DbSet<RankingSystem> RankingSystems { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<UniversityRankingYear> UniversityRankingYears { get; set; }

    public virtual DbSet<UniversityYear> UniversityYears { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("data source=c:\\data\\university.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("country");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CountryName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<RankingCriterion>(entity =>
        {
            entity.ToTable("ranking_criteria");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CriteriaName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("criteria_name");
            entity.Property(e => e.RankingSystemId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("ranking_system_id");

            entity.HasOne(d => d.RankingSystem).WithMany(p => p.RankingCriteria).HasForeignKey(d => d.RankingSystemId);
        });

        modelBuilder.Entity<RankingSystem>(entity =>
        {
            entity.ToTable("ranking_system");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.SystemName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("system_name");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.ToTable("university");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CountryId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("country_id");
            entity.Property(e => e.UniversityName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("university_name");

            entity.HasOne(d => d.Country).WithMany(p => p.Universities).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<UniversityRankingYear>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("university_ranking_year");

            entity.Property(e => e.RankingCriteriaId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("ranking_criteria_id");
            entity.Property(e => e.Score)
                .HasDefaultValueSql("NULL")
                .HasColumnName("score");
            entity.Property(e => e.UniversityId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("university_id");
            entity.Property(e => e.Year)
                .HasDefaultValueSql("NULL")
                .HasColumnName("year");

            entity.HasOne(d => d.RankingCriteria).WithMany().HasForeignKey(d => d.RankingCriteriaId);

            entity.HasOne(d => d.University).WithMany().HasForeignKey(d => d.UniversityId);
        });

        modelBuilder.Entity<UniversityYear>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("university_year");

            entity.Property(e => e.NumStudents)
                .HasDefaultValueSql("NULL")
                .HasColumnName("num_students");
            entity.Property(e => e.PctFemaleStudents)
                .HasDefaultValueSql("NULL")
                .HasColumnName("pct_female_students");
            entity.Property(e => e.PctInternationalStudents)
                .HasDefaultValueSql("NULL")
                .HasColumnName("pct_international_students");
            entity.Property(e => e.StudentStaffRatio)
                .HasDefaultValueSql("NULL")
                .HasColumnName("student_staff_ratio");
            entity.Property(e => e.UniversityId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("university_id");
            entity.Property(e => e.Year)
                .HasDefaultValueSql("NULL")
                .HasColumnName("year");

            entity.HasOne(d => d.University).WithMany().HasForeignKey(d => d.UniversityId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
