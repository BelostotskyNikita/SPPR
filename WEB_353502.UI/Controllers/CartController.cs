using Microsoft.AspNetCore.Mvc;

namespace WEB_353502.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
