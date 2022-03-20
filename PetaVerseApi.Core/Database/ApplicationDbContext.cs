using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PetaVerseApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PetaVerseApi.Core.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<UserAnimal> UserAnimals { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Species> Species { get; set; } = null!;
        public virtual DbSet<Temperament> Temperaments { get; set; } = null!;
        public virtual DbSet<Shedding> Sheddings { get; set; } = null!;
        public virtual DbSet<PetShorts> PetShorts { get; set; } = null!;
        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.Guid).HasDefaultValueSql("NEWID()");

                entity.HasIndex(e => e.Guid).IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.HasOne(ur => ur.Role).WithMany(r => r!.UserRoles).HasForeignKey(ur => ur.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ur => ur.User).WithMany(u => u!.UserRoles).HasForeignKey(ur => ur.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<UserAnimal>(entity =>
            {
                entity.HasOne(ua => ua.Animal).WithMany(a => a.UserAnimals).HasForeignKey(ua => ua.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ua => ua.User).WithMany(u => u.UserAnimals).HasForeignKey(ua => ua.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Animal>(entity => 
            {
                entity.Property(a => a.Name).IsRequired().HasMaxLength(50);
            });

            builder.Entity<Status>(entity =>
            {
                entity.Property(s => s.Toppic).IsRequired().HasMaxLength(50);

                entity.Property(s => s.Title).IsRequired().HasMaxLength(200);

            });
        }
    }
}
