using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WEB_2535503_Belostotsky.Domain.Entities;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEB_253503_Belostotsky.API.Data;

public static class DbInitializer
{
    public static async Task SeedData(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        if (context.Categories.Any() || context.ClothingItems.Any())
            return;

        var config = app.Configuration;
        var baseUrl = config["AppSettings:BaseUrl"];

        var categories = new Category[]
        {
            new Category {Id=1, Name="Верхняя одежда", NormalizedName="outerwear"},
            new Category {Id=2, Name="Платья", NormalizedName="dresses"},
            new Category {Id=3, Name="Брюки", NormalizedName="pants"},
            new Category {Id=4, Name="Футболки", NormalizedName="t-shirts"},
            new Category {Id=5, Name="Обувь", NormalizedName="shoes"},
            new Category {Id=6, Name="Аксессуары", NormalizedName="accessories"}
        };

        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();

        var clothingItems = new ClothingItem[]
        {
            new ClothingItem { Name = "Куртка зимняя", Description = "Теплая куртка для холодной погоды", Size = "M", Price = 3500, Image = $"{baseUrl}/Images/jacket.png", CategoryId = categories.First(c => c.NormalizedName == "outerwear").Id },
            new ClothingItem { Name = "Пальто классическое", Description = "Элегантное пальто для офиса", Size = "L", Price = 4500, Image = $"{baseUrl}/Images/coat.png", CategoryId = categories.First(c => c.NormalizedName == "outerwear").Id },
            new ClothingItem { Name = "Платье вечернее", Description = "Красивое платье для особых случаев", Size = "S", Price = 2800, Image = $"{baseUrl}/Images/dress.png", CategoryId = categories.First(c => c.NormalizedName == "dresses").Id },
            new ClothingItem { Name = "Кроссовки спортивные", Description = "Удобные кроссовки для бега", Size = "42", Price = 3200, Image = $"{baseUrl}/Images/sneakers.png", CategoryId = categories.First(c => c.NormalizedName == "shoes").Id },
            new ClothingItem { Name = "Сумка кожаная", Description = "Стильная кожаная сумка", Size = "One Size", Price = 1800, Image = $"{baseUrl}/Images/bag.png", CategoryId = categories.First(c => c.NormalizedName == "accessories").Id },
            new ClothingItem { Name = "Джинсы классические", Description = "Удобные джинсы на каждый день", Size = "32", Price = 2200, Image = $"{baseUrl}/Images/jeans.png", CategoryId = categories.First(c => c.NormalizedName == "pants").Id },
            new ClothingItem { Name = "Футболка базовая", Description = "Универсальная футболка", Size = "XL", Price = 800, Image = $"{baseUrl}/Images/shirt.png", CategoryId = categories.First(c => c.NormalizedName == "t-shirts").Id }
        };

        context.ClothingItems.AddRange(clothingItems);
        await context.SaveChangesAsync();
    }
}
