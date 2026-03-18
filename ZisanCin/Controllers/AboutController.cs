using Microsoft.AspNetCore.Mvc;
using ZisanCin.Data;

namespace ZisanCin.Controllers
{
    public class AboutController : Controller
    { private readonly DatabaseContext _context;

        public AboutController(DatabaseContext context)
        {
            _context = context;
        }
        [Route("hakkimizda")]
        public IActionResult Index()
        {
            var aboutInfo = _context.Abouts.FirstOrDefault();
            return View(aboutInfo);
        }
    }
}
