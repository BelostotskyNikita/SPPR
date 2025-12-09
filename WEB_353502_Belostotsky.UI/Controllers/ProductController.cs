using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEB_353502_Belostotsky.UI.Services.ProductService;
using WEB_353502_Belostotsky.UI.Services.CategoryService;

namespace WEB_353502_Belostotsky.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? category)
        {
            var productResponse = await _productService.GetProductListAsync(category);
            if (!productResponse.Successfull)
                return NotFound(productResponse.ErrorMessage);

            return View(productResponse.Data);
        }
    }
}
