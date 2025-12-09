using WEB_2535503_Belostotsky.Domain.Entities;
using WEB_2535503_Belostotsky.Domain.Models;

namespace WEB_253503_Belostotsky.UI.Services.CategoryService
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
