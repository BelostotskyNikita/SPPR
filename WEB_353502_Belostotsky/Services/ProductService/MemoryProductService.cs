using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WEB_353502_Belostotsky.Domain.Models;
using WEB_353502_Belostotsky.Domain.Entities;
using WEB_353502_Belostotsky.UI.Services.CategoryService;

namespace WEB_353502_Belostotsky.UI.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        private List<ClothingItem> _clothingItems;
        private List<Category> _categories;
        private readonly int _itemsPerPage;

        public MemoryProductService(
            IConfiguration config,
            ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            _itemsPerPage = int.Parse(config["ItemsPerPage"] ?? "3");

            SetupData();
        }

        /// <summary>
        /// Инициализация списков с тестовыми данными
        /// </summary>
        private void SetupData()
        {
            _clothingItems = new List<ClothingItem>
            {
                new ClothingItem { Id = 1, Name = "Куртка зимняя",
                    Description = "Теплая куртка для холодной погоды",
                    Size = "M", Price = 3500, Image = "Images/jacket.png",
                    Category = _categories.Find(c => c.NormalizedName.Equals("outerwear"))
                },
                new ClothingItem { Id = 2, Name = "Пальто классическое",
                    Description = "Элегантное пальто для офиса",
                    Size = "L", Price = 4500, Image = "Images/coat.png",
                    Category = _categories.Find(c => c.NormalizedName.Equals("outerwear"))
                },
                new ClothingItem { Id = 3, Name = "Платье вечернее",
                    Description = "Красивое платье для особых случаев",
                    Size = "S", Price = 2800, Image = "Images/dress.png",
                    Category = _categories.Find(c => c.NormalizedName.Equals("dresses"))
                },
                new ClothingItem { Id = 4, Name = "Кроссовки спортивные",
                    Description = "Удобные кроссовки для бега",
                    Size = "42", Price = 3200, Image = "Images/sneakers.png",
                    Category = _categories.Find(c => c.NormalizedName.Equals("shoes"))
                },
                new ClothingItem { Id = 5, Name = "Сумка кожаная",
                    Description = "Стильная кожаная сумка",
                    Size = "One Size", Price = 1800, Image = "Images/bag.png",
                    Category = _categories.Find(c => c.NormalizedName.Equals("accessories"))
                },
                new ClothingItem { Id = 6, Name = "Джинсы классические",
                    Description = "Удобные джинсы на каждый день",
                    Size = "32", Price = 2200, Image = "Images/jeans.png",
                    Category = _categories.Find(c => c.NormalizedName.Equals("pants"))
                },
                new ClothingItem { Id = 7, Name = "Футболка базовая", 
                    Description = "Универсальная футболка",
                    Size = "XL", Price = 800, Image = "Images/shirt.png",
                    Category = _categories.Find(c => c.NormalizedName.Equals("t-shirts"))
                }
            };
        }
        /// <summary>
        /// Получение списка продуктов с фильтрацией по категории и разбиением на страницы
        /// </summary>
        public Task<ResponseData<ListModel<ClothingItem>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var filteredClothingItems = _clothingItems
                .Where(d => string.IsNullOrEmpty(categoryNormalizedName) || categoryNormalizedName == "Все" || d.Category.NormalizedName.Equals(categoryNormalizedName))
                .ToList();

            var totalPages = (int)Math.Ceiling((double)filteredClothingItems.Count / _itemsPerPage);

            if (pageNo < 1 || pageNo > totalPages)
            {
                pageNo = 1; 
            }

            var paginatedClothingItems = filteredClothingItems
                .Skip((pageNo - 1) * _itemsPerPage)  
                .Take(_itemsPerPage) 
                .ToList();

            var result = new ListModel<ClothingItem>
            {
                Items = paginatedClothingItems,
                CurrentPage = pageNo,
                TotalPages = totalPages 
            };

            return Task.FromResult(ResponseData<ListModel<ClothingItem>>.Success(result));
        }


    }
}
