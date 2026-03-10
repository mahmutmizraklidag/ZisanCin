using Microsoft.AspNetCore.Mvc;

namespace ZisanCin.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
