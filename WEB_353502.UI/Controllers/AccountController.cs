using Microsoft.AspNetCore.Mvc;

namespace WEB_353502.UI.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult LogOut()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
