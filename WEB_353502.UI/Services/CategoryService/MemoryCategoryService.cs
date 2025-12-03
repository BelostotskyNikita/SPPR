using WEB_353502.Domain.Entities;
using WEB_353502.Domain.Models;

namespace WEB_353502.UI.Services.CategoryService
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category {Id=1, Name="Мужская одежда", NormalizedName="mens-clothing"},
                new Category {Id=2, Name="Женская одежда", NormalizedName="womens-clothing"},
                new Category {Id=3, Name="Детская одежда", NormalizedName="kids-clothing"},
                new Category {Id=4, Name="Мужская обувь", NormalizedName="mens-shoes"},
                new Category {Id=5, Name="Женская обувь", NormalizedName="womens-shoes"},
                new Category {Id=6, Name="Аксессуары", NormalizedName="accessories"}
            };
            var result = ResponseData<List<Category>>.Success(categories);
            return Task.FromResult(result);
        }
    }
}
