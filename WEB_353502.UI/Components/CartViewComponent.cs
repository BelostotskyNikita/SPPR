using Microsoft.AspNetCore.Mvc;
using WEB_353502.UI.Models;

namespace WEB_353502.UI.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = new CartViewModel
            {
                ItemsCount = 0,
                TotalAmount = 0.0m
            };

            return View(cart);
        }
    }
}