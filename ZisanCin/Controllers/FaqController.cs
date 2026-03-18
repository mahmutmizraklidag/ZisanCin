using Microsoft.AspNetCore.Mvc;
using ZisanCin.Data;

namespace ZisanCin.Controllers
{
    public class FaqController : Controller
    {
        private readonly DatabaseContext _context;

        public FaqController(DatabaseContext context)
        {
            _context = context;
        }
        [Route("sikca-sorulan-sorular")]
        public IActionResult Index()
        {
            var faq = _context.Faqs.ToList();
            return View(faq);
        }
    }
}
