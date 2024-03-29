﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetaVerseApi.Core.Database;

#nullable disable

namespace PetaVerseApi.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220802080239_Petaverse_Milestone1")]
    partial class Petaverse_Milestone1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BreedId")
                        .HasColumnType("int");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PetAvatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PetColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SixDigitCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.AnimalPetaverseMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int>("PetaverMediaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("PetaverMediaId");

                    b.ToTable("AnimalPetaverseMedia");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BreedDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BreedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Coat")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaximumLifeSpan")
                        .HasColumnType("int");

                    b.Property<double>("MaximumSize")
                        .HasColumnType("float");

                    b.Property<double>("MaximumWeight")
                        .HasColumnType("float");

                    b.Property<int>("MinimumLifeSpan")
                        .HasColumnType("int");

                    b.Property<double>("MinimumWeight")
                        .HasColumnType("float");

                    b.Property<double>("MinimunSize")
                        .HasColumnType("float");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Breeds");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.PetaverseMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("MediaUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeUpload")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PetaverseMedias");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.PetShorts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsSpam")
                        .HasColumnType("bit");

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<int?>("MediaId")
                        .HasColumnType("int");

                    b.Property<int?>("PetId")
                        .HasColumnType("int");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MediaId");

                    b.HasIndex("PetId");

                    b.HasIndex("PublisherId");

                    b.ToTable("PetShorts");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Shedding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sheddings");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TopLovedPetOfTheWeek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Species");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Toppic")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Temperament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Temperaments");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CoverImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Guid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PetaverseProfileImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.UserAnimal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnimals");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Animal", b =>
                {
                    b.HasOne("PetaVerseApi.Core.Entities.Breed", "Breed")
                        .WithMany("Animals")
                        .HasForeignKey("BreedId");

                    b.Navigation("Breed");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.AnimalPetaverseMedia", b =>
                {
                    b.HasOne("PetaVerseApi.Core.Entities.Animal", "Animal")
                        .WithMany("AnimalPetaverseMedias")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetaVerseApi.Core.Entities.PetaverseMedia", "PetaverseMedia")
                        .WithMany("AnimalPetaverseMedias")
                        .HasForeignKey("PetaverMediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("PetaverseMedia");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Breed", b =>
                {
                    b.HasOne("PetaVerseApi.Core.Entities.Species", "Species")
                        .WithMany("Breeds")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Species");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.PetShorts", b =>
                {
                    b.HasOne("PetaVerseApi.Core.Entities.PetaverseMedia", "Media")
                        .WithMany()
                        .HasForeignKey("MediaId");

                    b.HasOne("PetaVerseApi.Core.Entities.Animal", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId");

                    b.HasOne("PetaVerseApi.Core.Entities.User", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId");

                    b.Navigation("Media");

                    b.Navigation("Pet");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Status", b =>
                {
                    b.HasOne("PetaVerseApi.Core.Entities.User", "User")
                        .WithMany("Statuses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.UserAnimal", b =>
                {
                    b.HasOne("PetaVerseApi.Core.Entities.Animal", "Animal")
                        .WithMany("UserAnimals")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetaVerseApi.Core.Entities.User", "User")
                        .WithMany("UserAnimals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.UserRole", b =>
                {
                    b.HasOne("PetaVerseApi.Core.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetaVerseApi.Core.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Animal", b =>
                {
                    b.Navigation("AnimalPetaverseMedias");

                    b.Navigation("UserAnimals");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Breed", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.PetaverseMedia", b =>
                {
                    b.Navigation("AnimalPetaverseMedias");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("PetaVerseApi.Core.Entities.User", b =>
                {
                    b.Navigation("Statuses");

                    b.Navigation("UserAnimals");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
