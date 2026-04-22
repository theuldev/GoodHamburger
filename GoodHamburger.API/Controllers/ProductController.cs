using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
