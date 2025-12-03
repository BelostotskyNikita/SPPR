using Microsoft.AspNetCore.Mvc;

namespace WEB_353502.UI.Controllers
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

        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            // Получаем список категорий
            var categoryResponse = await _categoryService.GetCategoryListAsync();
            var categories = categoryResponse.Successfull ? categoryResponse.Data : new List<Category>();

            // Получаем имя текущей категории
            var currentCategory = categories.FirstOrDefault(c => c.NormalizedName == category)?.Name ?? "Все категории";

            // Передаем данные в представление
            ViewData["Categories"] = categories;
            ViewData["CurrentCategory"] = currentCategory;
            ViewData["CurrentCategoryNormalizedName"] = category;

            // Получаем товары с фильтрацией по категории и пагинацией
            var productResponse = await _productService.GetProductListAsync(category, pageNo);
            if (!productResponse.Successfull)
            {
                return View(new ListModel<ClothingItem>());
            }
            return View(productResponse.Data ?? new ListModel<ClothingItem>());
        }
    }
}
