using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_353502.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Lab2";

            var items = new List<ListDemo>
            {
                new ListDemo { Id = 1, Name = "Ёлемент 1" },
                new ListDemo { Id = 2, Name = "Ёлемент 2" },
                new ListDemo { Id = 3, Name = "Ёлемент 3" },
                new ListDemo { Id = 4, Name = "Ёлемент 4" }
            };

            ViewData["SelectList"] = new SelectList(items, "Id", "Name");

            return View();
        }
    }

    public class ListDemo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
