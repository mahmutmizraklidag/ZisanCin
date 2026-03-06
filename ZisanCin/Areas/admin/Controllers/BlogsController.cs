using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZisanCin.Data;
using ZisanCin.Entities;
using ZisanCin.Utils;

namespace ZisanCin.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class BlogsController : Controller
    {
        private readonly DatabaseContext _context;

        public BlogsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: admin/Blogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());
        }



        // GET: admin/Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Blogs.AnyAsync(p => p.Slug == blog.Slug))
                {
                    ModelState.AddModelError("Slug", "Bu slug zaten kullanılıyor.");

                    return View(blog);
                }
                if (Image is not null) blog.Image = await FileHelper.FileLoaderAsync(Image);
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: admin/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: admin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog, IFormFile? Image)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return View(blog);

            //if (await _context.Blogs.AnyAsync(p => p.Slug == blog.Slug))
            //{
            //    ModelState.AddModelError("Slug", "Bu slug zaten kullanılıyor.");

            //    return View(blog);
            //}

            var dbBlog = await _context.Blogs.FindAsync(id);
            if (dbBlog is null) return NotFound();
            dbBlog.Title = blog.Title;
            dbBlog.Description = blog.Description;
            dbBlog.Slug = blog.Slug;
            dbBlog.CreatedAt = blog.CreatedAt;
            dbBlog.Keywords = blog.Keywords;
            dbBlog.MetaDescription = blog.MetaDescription;
            dbBlog.MetaTitle = blog.MetaTitle;

            if (Image is not null)
            {
                if (!string.IsNullOrEmpty(dbBlog.Image))
                {
                    FileHelper.DeleteFile(dbBlog.Image);
                }
                dbBlog.Image = await FileHelper.FileLoaderAsync(Image);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Blogs", new { area = "Admin" });
        }

        // GET: admin/Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: admin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                if (!string.IsNullOrEmpty(blog.Image))
                {
                    FileHelper.DeleteFile(blog.Image);
                }
                _context.Blogs.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
