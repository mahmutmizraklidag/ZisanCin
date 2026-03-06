using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZisanCin.Data;
using ZisanCin.Entities;

namespace ZisanCin.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class FaqsController : Controller
    {
        private readonly DatabaseContext _context;

        public FaqsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: admin/Faqs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Faqs.ToListAsync());
        }

        // GET: admin/Faqs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        // GET: admin/Faqs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Faqs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faq);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Faqs", new { area = "Admin" });
            }
            return View(faq);
        }

        // GET: admin/Faqs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        // POST: admin/Faqs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] Faq faq)
        {
            if (id != faq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faq);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqExists(faq.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Faqs", new { area = "Admin" });
            }
            return View(faq);
        }

        // GET: admin/Faqs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        // POST: admin/Faqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq != null)
            {
                _context.Faqs.Remove(faq);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Faqs", new { area = "Admin" });
        }
        private bool FaqExists(int id)
        {
            return _context.Faqs.Any(e => e.Id == id);
        }
    }
}
