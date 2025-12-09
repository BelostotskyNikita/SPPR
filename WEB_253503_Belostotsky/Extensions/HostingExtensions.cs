using WEB_253503_Belostotsky.UI.Services.ProductService;
using WEB_253503_Belostotsky.UI.Services.CategoryService;

namespace WEB_253503_Belostotsky.UI.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
            builder.Services.AddScoped<IProductService, MemoryProductService>();
        }
    }
}
