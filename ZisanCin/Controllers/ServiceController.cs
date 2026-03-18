using Microsoft.AspNetCore.Mvc;
using ZisanCin.Data;
using ZisanCin.Entities;

namespace ZisanCin.Controllers
{
    public class ServiceController : Controller
    {
        private readonly DatabaseContext _context;

        public ServiceController(DatabaseContext context)
        {
            _context = context;
        }
        [Route("hizmetlerimiz")]
        public IActionResult Index()
        {
            var services = _context.Services.OrderByDescending(s => s.Id).ToList();
            return View(services);
        }
        [Route("hizmetlerimiz/{slug}")]
        public IActionResult Detail(string slug)
        {
            var service = _context.Services.FirstOrDefault(s => s.Slug == slug);
            return View(service);
        }
    }
}
