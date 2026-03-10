using Microsoft.AspNetCore.Mvc;

namespace ZisanCin.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
