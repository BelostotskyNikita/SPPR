using WEB_353502_Belostotsky.Domain.Entities;
using WEB_353502_Belostotsky.Domain.Models;

namespace WEB_353502_Belostotsky.UI.Services.CategoryService
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
