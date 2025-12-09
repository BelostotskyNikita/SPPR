using System.Collections.Generic;
using System.Threading.Tasks;
using WEB_353502_Belostotsky.Domain.Entities;
using WEB_353502_Belostotsky.Domain.Models;

namespace WEB_353502_Belostotsky.API.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ResponseData<List<Category>>> GetCategoryListAsync();
        Task<ResponseData<Category>> GetCategoryByIdAsync(int id);
        Task<ResponseData<Category>> CreateCategoryAsync(Category category);
        Task<ResponseData<string>> UpdateCategoryAsync(int id, Category category);
        Task<ResponseData<string>> DeleteCategoryAsync(int id);
    }
}
