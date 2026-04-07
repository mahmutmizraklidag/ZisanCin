using Microsoft.AspNetCore.Mvc;
using ZisanCin.Data;

namespace ZisanCin.Controllers
{
    public class BlogController : Controller
    {
        private readonly DatabaseContext _context;

        public BlogController(DatabaseContext context)
        {
            _context = context;
        }
        [Route("blog")]
        public IActionResult Index()
        {
            var blogs = _context.Blogs.OrderByDescending(x=>x.CreatedAt).ToList();
            return View(blogs);
        }
        [Route("blog/{slug}")]
        public IActionResult Detail(string slug)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Slug == slug);
            return View(blog);
        }
    }
}
