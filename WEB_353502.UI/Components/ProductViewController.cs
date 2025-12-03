using Microsoft.AspNetCore.Mvc;

namespace WEB_353502.UI.Components
{
    public class ProductViewController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductViewController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var productResponse = await _productService.GetProductListAsync(null);
            if (!productResponse.Successfull)
                return NotFound(productResponse.ErrorMessage);
            return View(productResponse.Data.Items);
        }
    }
}
