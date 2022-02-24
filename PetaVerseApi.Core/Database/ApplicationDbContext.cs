using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PetaVerseApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PetaVerseApi.Core.Database
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<UserAnimal> UserAnimals { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;

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
                entity.HasOne(ur => ur.Role).WithMany(r => r!.UserRoles).HasForeignKey(ur => ur.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ur => ur.User).WithMany(u => u!.UserRoles).HasForeignKey(ur => ur.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<UserAnimal>(entity =>
            {
                entity.HasOne(ua => ua.Animal).WithMany(a => a.UserAnimals).HasForeignKey(ua => ua.AnimalId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ua => ua.User).WithMany(u => u.UserAnimals).HasForeignKey(ua => ua.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Animal>(entity => 
            {
                entity.Property(a => a.Name).IsRequired().HasMaxLength(50);

                entity.Property(a => a.Species).IsRequired().HasMaxLength(30);
            });

            builder.Entity<Post>(entity =>
            {
                entity.Property(p => p.Toppic).IsRequired().HasMaxLength(50);

                entity.Property(p => p.Title).IsRequired().HasMaxLength(200);

            });
        }
    }
}
