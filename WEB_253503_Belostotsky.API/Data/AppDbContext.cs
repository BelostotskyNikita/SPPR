using Microsoft.EntityFrameworkCore;
using WEB_2535503_Belostotsky.Domain.Entities; 

namespace WEB_253503_Belostotsky.API.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClothingItem> ClothingItems { get; set; }
    }
}
