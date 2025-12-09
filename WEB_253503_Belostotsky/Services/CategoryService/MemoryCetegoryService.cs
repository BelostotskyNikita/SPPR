using WEB_2535503_Belostotsky.Domain.Entities;
using WEB_2535503_Belostotsky.Domain.Models;

namespace WEB_253503_Belostotsky.UI.Services.CategoryService
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
{
            new Category {Id=1, Name="Верхняя одежда", NormalizedName="outerwear"},
            new Category {Id=2, Name="Платья", NormalizedName="dresses"},
            new Category {Id=3, Name="Брюки", NormalizedName="pants"},
            new Category {Id=4, Name="Футболки", NormalizedName="t-shirts"},
            new Category {Id=5, Name="Обувь", NormalizedName="shoes"},
            new Category {Id=6, Name="Аксессуары", NormalizedName="accessories"}
};
            var result = ResponseData<List<Category>>.Success(categories);
            return Task.FromResult(result);
        }
    }
}
