using Microsoft.AspNetCore.Mvc;
using WEB_353502.Domain.Entities;
using WEB_353502.Domain.Models;
using WEB_353502.UI.Services.CategoryService;

namespace WEB_353502.UI.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        List<ClothingItem> _clothingItems;
        List<Category> _categories;
        private readonly IConfiguration _config;

        public MemoryProductService(
            [FromServices] IConfiguration config,
            ICategoryService categoryService)
        {
            _config = config;
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            SetupData();
        }

        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _clothingItems = new List<ClothingItem>
            {
                new ClothingItem
                {
                    Id = 1,
                    Name="Футболка мужская",
                    Description="Хлопковая футболка черного цвета, удобная и стильная",
                    Price = 1500,
                    Size = "M",
                    Color = "Черный",
                    Material = "Хлопок 100%",
                    ImagePath="/Images/tshirt.jpg",
                    MimeType = "image/jpeg",
                    CategoryId = 1,
                    Category = _categories.Find(c=>c.NormalizedName.Equals("mens-clothing"))
                },
                new ClothingItem
                {
                    Id = 2,
                    Name="Джинсы женские",
                    Description="Скинни джинсы синего цвета, модный крой",
                    Price = 3500,
                    Size = "S",
                    Color = "Синий",
                    Material = "Деним",
                    ImagePath="/Images/jeans.jpg",
                    MimeType = "image/jpeg",
                    CategoryId = 2,
                    Category = _categories.Find(c=>c.NormalizedName.Equals("womens-clothing"))
                },
                new ClothingItem
                {
                    Id = 3,
                    Name="Кроссовки детские",
                    Description="Спортивные кроссовки для детей, удобные и легкие",
                    Price = 2800,
                    Size = "32",
                    Color = "Белый",
                    Material = "Текстиль",
                    ImagePath="/Images/sneakers.jpg",
                    MimeType = "image/jpeg",
                    CategoryId = 3,
                    Category = _categories.Find(c=>c.NormalizedName.Equals("kids-clothing"))
                },
                new ClothingItem
                {
                    Id = 4,
                    Name="Кожаная сумка",
                    Description="Элегантная кожаная сумка, идеально для офиса",
                    Price = 4500,
                    Size = "Универсальный",
                    Color = "Коричневый",
                    Material = "Натуральная кожа",
                    ImagePath="/Images/bag.jpg",
                    MimeType = "image/jpeg",
                    CategoryId = 6,
                    Category = _categories.Find(c=>c.NormalizedName.Equals("accessories"))
                },
                new ClothingItem
                {
                    Id = 5,
                    Name="Рубашка мужская",
                    Description="Классическая рубашка белого цвета",
                    Price = 2200,
                    Size = "L",
                    Color = "Белый",
                    Material = "Хлопок",
                    ImagePath="/Images/shirt.jpg",
                    MimeType = "image/jpeg",
                    CategoryId = 1,
                    Category = _categories.Find(c=>c.NormalizedName.Equals("mens-clothing"))
                },
                new ClothingItem
                {
                    Id = 6,
                    Name="Платье вечернее",
                    Description="Элегантное вечернее платье черного цвета",
                    Price = 5200,
                    Size = "M",
                    Color = "Черный",
                    Material = "Шелк",
                    ImagePath="/Images/dress.jpg",
                    MimeType = "image/jpeg",
                    CategoryId = 2,
                    Category = _categories.Find(c=>c.NormalizedName.Equals("womens-clothing"))
                },
                new ClothingItem
                {
                    Id = 7,
                    Name="Кроссовки мужские",
                    Description="Спортивные кроссовки для бега",
                    Price = 3800,
                    Size = "42",
                    Color = "Синий",
                    Material = "Кожа/Текстиль",
                    ImagePath="/Images/mens-sneakers.jpg",
                    MimeType = "image/jpeg",
                    CategoryId = 4,
                    Category = _categories.Find(c=>c.NormalizedName.Equals("mens-shoes"))
                },
                new ClothingItem
                {
                    Id = 8,
                    Name="Туфли женские",
                    Description="Элегантные туфли на каблуке",
                    Price = 4100,
                    Size = "37",
                    Color = "Красный",
                    Material = "Лаковая кожа",
                    ImagePath="/Images/heels.jpg",
                    MimeType = "image/jpeg",
                    CategoryId = 5,
                    Category = _categories.Find(c=>c.NormalizedName.Equals("womens-shoes"))
                },
                new ClothingItem
                {
                    Id = 9,
                    Name="Кепка бейсболка",
                    Description="Стильная бейсболка с регулировкой",
                    Price = 800,
                    Size = "Универсальный",
                    Color = "Синий",
                    Material = "Хлопок",
                    ImagePath="/Images/cap.jpg",
                    MimeType = "image/jpeg",
                    CategoryId = 6,
                    Category = _categories.Find(c=>c.NormalizedName.Equals("accessories"))
                }
            };
        }

        public Task<ResponseData<ListModel<ClothingItem>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            try
            {
                var items = _clothingItems ?? new List<ClothingItem>();

                // Фильтрация по категории
                var filteredItems = items
                    .Where(d => categoryNormalizedName == null ||
                               d.Category?.NormalizedName?.Equals(categoryNormalizedName) == true)
                    .ToList();

                // Получаем размер страницы из конфигурации
                var pageSize = _config.GetValue<int>("ItemsPerPage", 3);

                // Вычисляем общее количество страниц
                var totalItems = filteredItems.Count;
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                // Корректируем номер страницы
                if (pageNo < 1) pageNo = 1;
                if (pageNo > totalPages && totalPages > 0) pageNo = totalPages;

                // Выбираем нужную страницу
                var pagedItems = filteredItems
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var result = new ListModel<ClothingItem>
                {
                    Items = pagedItems,
                    CurrentPage = pageNo,
                    TotalPages = totalPages
                };

                return Task.FromResult(ResponseData<ListModel<ClothingItem>>.Success(result));
            }
            catch (Exception ex)
            {
                return Task.FromResult(ResponseData<ListModel<ClothingItem>>.Error(ex.Message));
            }
        }

        public Task<ResponseData<ClothingItem>> GetProductByIdAsync(int id)
        {
            try
            {
                var item = _clothingItems.FirstOrDefault(i => i.Id == id);
                if (item == null)
                {
                    return Task.FromResult(ResponseData<ClothingItem>.Error("Товар не найден"));
                }
                return Task.FromResult(ResponseData<ClothingItem>.Success(item));
            }
            catch (Exception ex)
            {
                return Task.FromResult(ResponseData<ClothingItem>.Error(ex.Message));
            }
        }

        public Task UpdateProductAsync(int id, ClothingItem product, IFormFile? formFile)
        {
            throw new NotImplementedException("Метод UpdateProductAsync еще не реализован");
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException("Метод DeleteProductAsync еще не реализован");
        }

        public Task<ResponseData<ClothingItem>> CreateProductAsync(ClothingItem product, IFormFile? formFile)
        {
            throw new NotImplementedException("Метод CreateProductAsync еще не реализован");
        }
    }
}