using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZisanCin.Data;

namespace ZisanCin.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class MainController : Controller
    {
        private readonly DatabaseContext _context;

        public MainController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.ContactForms.Take(3).ToList();
            return View(model);
        }
    }
}
