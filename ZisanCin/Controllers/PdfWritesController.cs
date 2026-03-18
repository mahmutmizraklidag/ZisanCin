using Microsoft.AspNetCore.Mvc;
using ZisanCin.Data;

namespace ZisanCin.Controllers
{
    public class PdfWritesController : Controller
    {
        private readonly DatabaseContext _context;

        public PdfWritesController(DatabaseContext context)
        {
            _context = context;
        }
        [Route("yazilar/{slug}")]
        public IActionResult Index(string slug)
        {
            var pdfWrite = _context.PdfWrites.FirstOrDefault(p => p.Slug == slug);
            return View(pdfWrite);
        }
    }
}
