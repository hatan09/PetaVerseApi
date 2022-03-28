using PetaVerseApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PetaVerseApi.Core.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public virtual DbSet<Animal>        Animals         { get; set; } = null!;
        public virtual DbSet<UserAnimal>    UserAnimals     { get; set; } = null!;
        public virtual DbSet<Status>        Statuses        { get; set; } = null!;
        public virtual DbSet<Species>       Species         { get; set; } = null!;
        public virtual DbSet<Temperament>   Temperaments    { get; set; } = null!;
        public virtual DbSet<Shedding>      Sheddings       { get; set; } = null!;
        public virtual DbSet<PetShorts>     PetShorts       { get; set; } = null!;
        


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

            builder.Entity<Animal>(entity => 
            {
                entity.Property(a => a.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasOne(a => a.Species)
                      .WithMany(s => s.Animals)
                      .HasForeignKey(a => a.SpeciesId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(a => a.Breed)
                      .WithMany(b => b.Animals)
                      .HasForeignKey(a => a.BreedId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Status>(entity =>
            {
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
