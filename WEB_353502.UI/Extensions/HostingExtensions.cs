using Microsoft.Extensions.DependencyInjection;
using WEB_353502.UI.Services.CategoryService;
using WEB_353502.UI.Services.ProductService;

namespace WEB_353502.UI.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            // Регистрируем сервисы как scoped
            builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
            builder.Services.AddScoped<IProductService, MemoryProductService>();

            builder.Services.AddHttpClient();
        }
    }
}