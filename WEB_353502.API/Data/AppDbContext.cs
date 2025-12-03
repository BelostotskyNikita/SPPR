using Microsoft.EntityFrameworkCore;
using WEB_353502.Domain.Entities;

namespace WEB_353502.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet свойства для сущностей Category и ClothingItem
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClothingItem> ClothingItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка отношения один-ко-многим между Category и ClothingItem
            modelBuilder.Entity<ClothingItem>()
                .HasOne(ci => ci.Category)
                .WithMany(c => c.ClothingItems)
                .HasForeignKey(ci => ci.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // или Restrict в зависимости от требований

            // Дополнительные настройки при необходимости
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.NormalizedName)
                .IsUnique();

            modelBuilder.Entity<ClothingItem>()
                .Property(ci => ci.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}