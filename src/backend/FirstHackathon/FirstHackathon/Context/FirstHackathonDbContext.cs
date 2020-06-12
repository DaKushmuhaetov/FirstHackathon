using FirstHackathon.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstHackathon.Context
{
    public sealed class FirstHackathonDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public FirstHackathonDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<House> Houses { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(builder =>
            {
                builder.ToTable("People");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                builder.Property(o => o.Name)
                    .IsRequired(true);
                builder.Property(o => o.Surname)
                     .IsRequired(true);

                builder.Property(o => o.Login)
                    .IsRequired(true);
                builder.Property(o => o.Password)
                     .IsRequired(true);
            });

            modelBuilder.Entity<House>(builder =>
            {
                builder.ToTable("Houses");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                builder.Property(o => o.Address)
                    .IsRequired();

                builder.Property(o => o.Login)
                    .IsRequired(true);
                builder.Property(o => o.Password)
                     .IsRequired(true);

                builder.HasMany(o => o.People)
                    .WithOne(p => p.House)
                    .IsRequired(false);

                builder.HasMany(o => o.Meetings)
                    .WithOne(p => p.House)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Meeting>(builder =>
            {
                builder.ToTable("Meetings");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                builder.Property(o => o.Title)
                    .IsRequired(true);

                builder.Property(o => o.MeetingDate)
                    .IsRequired(true);

                builder.Property(o => o.Description)
                    .IsRequired(false);

                builder.Property(o => o.Image)
                    .IsRequired(false);
            });
        }
    }
}
