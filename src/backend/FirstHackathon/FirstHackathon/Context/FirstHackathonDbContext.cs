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

                builder.HasOne(o => o.House)
                    .WithMany()
                    .HasForeignKey(_ => _.HouseId)
                    .HasPrincipalKey(_ => _.People)
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
            });
        }
    }
}
