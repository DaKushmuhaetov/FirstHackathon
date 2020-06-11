﻿// <auto-generated />
using System;
using FirstHackathon.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FirstHackathon.Migrations
{
    [DbContext(typeof(FirstHackathonDbContext))]
    partial class FirstHackathonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FirstHackathon.Models.House", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("FirstHackathon.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("FirstHackathon.Models.Votes.Variant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VotingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VotingId");

                    b.ToTable("Variants");
                });

            modelBuilder.Entity("FirstHackathon.Models.Votes.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VariantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("VariantId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("FirstHackathon.Models.Votes.Voting", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsClosed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("Votings");
                });

            modelBuilder.Entity("FirstHackathon.Models.Person", b =>
                {
                    b.HasOne("FirstHackathon.Models.House", "House")
                        .WithMany("People")
                        .HasForeignKey("HouseId");
                });

            modelBuilder.Entity("FirstHackathon.Models.Votes.Variant", b =>
                {
                    b.HasOne("FirstHackathon.Models.Votes.Voting", "Voting")
                        .WithMany("Variants")
                        .HasForeignKey("VotingId");
                });

            modelBuilder.Entity("FirstHackathon.Models.Votes.Vote", b =>
                {
                    b.HasOne("FirstHackathon.Models.Person", "Person")
                        .WithMany("Votes")
                        .HasForeignKey("PersonId");

                    b.HasOne("FirstHackathon.Models.Votes.Variant", "Variant")
                        .WithMany("Votes")
                        .HasForeignKey("VariantId");
                });

            modelBuilder.Entity("FirstHackathon.Models.Votes.Voting", b =>
                {
                    b.HasOne("FirstHackathon.Models.House", null)
                        .WithMany("Votings")
                        .HasForeignKey("HouseId");
                });
#pragma warning restore 612, 618
        }
    }
}
