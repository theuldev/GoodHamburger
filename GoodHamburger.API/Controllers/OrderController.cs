using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
