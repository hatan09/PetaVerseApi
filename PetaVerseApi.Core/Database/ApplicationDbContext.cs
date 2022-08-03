﻿using PetaVerseApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PetaVerseApi.Core.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public virtual DbSet<Animal>            Animals         { get; set; } = null!;
        public virtual DbSet<UserAnimal>        UserAnimals     { get; set; } = null!;
        public virtual DbSet<Status>            Statuses        { get; set; } = null!;
        public virtual DbSet<Species>           Species         { get; set; } = null!;
        public virtual DbSet<Breed>             Breeds          { get; set; } = null!;
        public virtual DbSet<Temperament>       Temperaments    { get; set; } = null!;
        public virtual DbSet<Shedding>          Sheddings       { get; set; } = null!;
        public virtual DbSet<PetShorts>         PetShorts       { get; set; } = null!;
        public virtual DbSet<User>              Users           { get; set; } = null!;
        public virtual DbSet<Role>              Roles           { get; set; } = null!;
        public virtual DbSet<UserRole>          UserRoles       { get; set; } = null!;
        public virtual DbSet<PetaverseMedia>    PetaverseMedias { get; set; } = null!;
        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.Property(u => u.Guid).HasDefaultValueSql("NEWID()");
                entity.HasIndex(u => u.Guid).IsUnique();
                entity.Property(u => u.CreatedAt).HasColumnType("datetime");
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r!.UserRoles)
                      .HasForeignKey(ur => ur.RoleId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur => ur.User)
                      .WithMany(u => u!.UserRoles)
                      .HasForeignKey(ur => ur.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<UserAnimal>(entity =>
            {
                entity.HasOne(ua => ua.Animal)
                      .WithMany(a => a.UserAnimals)
                      .HasForeignKey(ua => ua.AnimalId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ua => ua.User)
                      .WithMany(u => u.UserAnimals)
                      .HasForeignKey(ua => ua.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AnimalPetaverseMedia>(entity =>
            {
                entity.HasOne(apm => apm.Animal)
                      .WithMany(a => a.AnimalPetaverseMedias)
                      .HasForeignKey(apm => apm.AnimalId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(apm => apm.PetaverseMedia)
                      .WithMany(pm => pm.AnimalPetaverseMedias)
                      .HasForeignKey(apm => apm.PetaverMediaId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Animal>(entity => 
            {
                entity.Property(a => a.Name)
                      .IsRequired()
                      .HasMaxLength(50);

            });

            builder.Entity<Status>(entity =>
            {
                entity.HasOne(s => s.User)
                      .WithMany(u => u.Statuses)
                      .HasForeignKey(s => s.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.Property(s => s.Toppic)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(s => s.Title)
                      .IsRequired()
                      .HasMaxLength(200);
            });

            builder.Entity<Breed>(entity => 
            {
                entity.HasOne(b => b.Species)
                      .WithMany(s => s.Breeds)
                      .HasForeignKey(b => b.SpeciesId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
