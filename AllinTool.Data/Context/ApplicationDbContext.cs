using AllinTool.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AllinTool.Data.Context
    {
    public class ApplicationDbContext : DbContext
        {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }
        public DbSet<GeographicData> GeographicData { get; set; }
        public DbSet<BankDetail> BankDetails { get; set; }
        public DbSet<TimeZones> TimeZoneTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<GeographicData>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.City).HasColumnName("CITY");
                entity.Property(e => e.Community).HasColumnName("COMMUNITY");
                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("COUNTRY");
                entity.Property(e => e.County).HasColumnName("COUNTY");
                entity.Property(e => e.Latitude).HasColumnName("LATITUDE");
                entity.Property(e => e.Longitude).HasColumnName("LONGITUDE");
                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .HasColumnName("POSTAL_CODE");
                entity.Property(e => e.State).HasColumnName("STATE");
            });

            modelBuilder.Entity<BankDetail>(entity =>
            {
                entity.HasKey(e => e.Ifsc);

                entity.Property(e => e.Ifsc)
                    .HasMaxLength(50)
                    .HasColumnName("IFSC");
                entity.Property(e => e.Address).HasColumnName("ADDRESS");
                entity.Property(e => e.Bank).HasColumnName("BANK");
                entity.Property(e => e.Branch).HasColumnName("BRANCH");
                entity.Property(e => e.Centre)
                    .HasMaxLength(50)
                    .HasColumnName("CENTRE");
                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("CITY");
                entity.Property(e => e.District)
                    .HasMaxLength(50)
                    .HasColumnName("DISTRICT");
                entity.Property(e => e.Imps)
                    .HasMaxLength(50)
                    .HasColumnName("IMPS");
                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .HasColumnName("STATE");
            });

            modelBuilder.Entity<TimeZones>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("time_zone");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(50)
                    .HasColumnName("Country_Code");
                entity.Property(e => e.Offset)
                    .HasMaxLength(50)
                    .HasColumnName("offset");
                entity.Property(e => e.Time_Zone)
                    .HasMaxLength(50)
                    .HasColumnName("Time_Zone");
            });


            }

        }
    }