using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZisanCin.Data;
using ZisanCin.Utils;

namespace ZisanCin.Controllers;

public class SeoController : Controller
{
    private readonly DatabaseContext _context;

    public SeoController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet("sitemap.xml")]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> Sitemap(CancellationToken cancellationToken)
    {
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var urls = new List<(string Location, DateTime? LastModified)>
        {
            (SeoDefaults.AbsoluteUrl("/"), null),
            (SeoDefaults.AbsoluteUrl("/hakkimizda"), null),
            (SeoDefaults.AbsoluteUrl("/hizmetlerimiz"), null),
            (SeoDefaults.AbsoluteUrl("/blog"), null),
            (SeoDefaults.AbsoluteUrl("/sikca-sorulan-sorular"), null),
            (SeoDefaults.AbsoluteUrl("/iletisim"), null)
        };

        var services = await _context.Services.AsNoTracking()
            .Where(item => item.Slug != null && item.Slug != string.Empty)
            .Select(item => item.Slug)
            .ToListAsync(cancellationToken);

        urls.AddRange(services
            .Where(slug => !string.IsNullOrWhiteSpace(slug))
            .Select(slug => (SeoDefaults.AbsoluteUrl($"/hizmetlerimiz/{Uri.EscapeDataString(slug.Trim())}"), (DateTime?)null)));

        var blogs = await _context.Blogs.AsNoTracking()
            .Where(item => item.Slug != null && item.Slug != string.Empty)
            .Select(item => new { item.Slug, item.CreatedAt })
            .ToListAsync(cancellationToken);

        urls.AddRange(blogs
            .Where(blog => !string.IsNullOrWhiteSpace(blog.Slug))
            .Select(blog => (
                SeoDefaults.AbsoluteUrl($"/blog/{Uri.EscapeDataString(blog.Slug.Trim())}"),
                blog.CreatedAt == default ? (DateTime?)null : blog.CreatedAt)));

        var documents = await _context.PdfWrites.AsNoTracking()
            .Where(item => item.Slug != null && item.Slug != string.Empty)
            .Select(item => item.Slug)
            .ToListAsync(cancellationToken);

        urls.AddRange(documents
            .Where(slug => !string.IsNullOrWhiteSpace(slug))
            .Select(slug => (SeoDefaults.AbsoluteUrl($"/yazilar/{Uri.EscapeDataString(slug.Trim())}"), (DateTime?)null)));

        var document = new XDocument(
            new XElement(ns + "urlset",
                urls.DistinctBy(item => item.Location, StringComparer.OrdinalIgnoreCase).Select(item =>
                    new XElement(ns + "url",
                        new XElement(ns + "loc", item.Location),
                        item.LastModified.HasValue
                            ? new XElement(ns + "lastmod", item.LastModified.Value.ToUniversalTime().ToString("yyyy-MM-dd"))
                            : null))));

        var xml = $"<?xml version=\"1.0\" encoding=\"utf-8\"?>{document.ToString(SaveOptions.DisableFormatting)}";
        return Content(xml, "application/xml", Encoding.UTF8);
    }

    [HttpGet("robots.txt")]
    [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
    public IActionResult Robots()
    {
        const string content = """
            User-agent: *
            Allow: /
            Disallow: /admin/
            Disallow: /home/error
            Disallow: /home/privacy

            Sitemap: https://diyetcin.com/sitemap.xml
            """;

        return Content(content, "text/plain", Encoding.UTF8);
    }
}
