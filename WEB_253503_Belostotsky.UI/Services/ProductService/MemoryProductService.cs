using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_2535503_Belostotsky.Domain.Models;
using WEB_2535503_Belostotsky.Domain.Entities;
using WEB_253503_Belostotsky.UI.Services.CategoryService;

namespace WEB_253503_Belostotsky.UI.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        private List<ClothingItem> _clothingItems;
        private List<Category> _categories;
        private readonly int _itemsPerPage;

        public MemoryProductService(ICategoryService categoryService, IConfiguration config)
        {
            // Получаем категории через categoryService
            _categories = categoryService.GetCategoryListAsync().Result.Data;

            // Получаем количество элементов на странице из конфигурации
            _itemsPerPage = int.Parse(config["ItemsPerPage"] ?? "3");

            // Заполняем коллекции данными
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
            }
        };
        }

        /// <summary>
        /// Получение списка продуктов с фильтрацией по категории и разбиением на страницы
        /// </summary>
        public Task<ResponseData<ListModel<ClothingItem>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var filteredClothingItems = _clothingItems.Where(d => categoryNormalizedName == null || d.Category.NormalizedName.Equals(categoryNormalizedName)).ToList();
            var result = new ListModel<ClothingItem>
            {
                Items = filteredClothingItems,
                CurrentPage = pageNo,
                TotalPages = 1 // В этой реализации пагинация не учитывается, но можно добавить
            };
            return Task.FromResult(ResponseData<ListModel<ClothingItem>>.Success(result));
        }
    }
}
