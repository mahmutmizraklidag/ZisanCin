using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using ZisanCin.Data;
using ZisanCin.Entities;
using ZisanCin.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace ZisanCin.Areas.admin.Controllers
{
    [Area("admin")]
    public class PdfWritesController : Controller
    {
        private readonly DatabaseContext _context;

        private readonly IWebHostEnvironment _env;
        public PdfWritesController(DatabaseContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: admin/PdfWrites
        public async Task<IActionResult> Index()
        {
            return View(await _context.PdfWrites.ToListAsync());
        }

       

        // GET: admin/PdfWrites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/PdfWrites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PdfWrite pdfWrite,IFormFile? PdfFİlePath)
        {
            if (ModelState.IsValid)
            {
                if (await _context.PdfWrites.AnyAsync(p => p.Slug == pdfWrite.Slug))
                {
                    ModelState.AddModelError("Slug", "Bu slug zaten kullanılıyor.");

                    return View(pdfWrite);
                }
                if (PdfFİlePath is not null) pdfWrite.PdfFİlePath = await FileHelper.UploadCvAsync(PdfFİlePath,_env);
                _context.Add(pdfWrite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pdfWrite);
        }

        // GET: admin/PdfWrites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pdfWrite = await _context.PdfWrites.FindAsync(id);
            if (pdfWrite == null)
            {
                return NotFound();
            }
            return View(pdfWrite);
        }

        // POST: admin/PdfWrites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PdfWrite pdfWrite, IFormFile? PdfFİlePath)
        {
            if (id != pdfWrite.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return View(pdfWrite);
            var dbPdfWrite = await _context.PdfWrites.FindAsync(id);
            if (dbPdfWrite == null)
            {
                return NotFound();
            }
            dbPdfWrite.Name = pdfWrite.Name;
            dbPdfWrite.Slug = pdfWrite.Slug;
            dbPdfWrite.MetaTitle = pdfWrite.MetaTitle;
            dbPdfWrite.MetaDescription = pdfWrite.MetaDescription;
            dbPdfWrite.MetaKeywords = pdfWrite.MetaKeywords;
            if(PdfFİlePath is not null)
            {
                dbPdfWrite.PdfFİlePath = await FileHelper.UploadCvAsync(PdfFİlePath, _env);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "PdfWrites", new { area = "Admin" });
        }

        // GET: admin/PdfWrites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pdfWrite = await _context.PdfWrites
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pdfWrite == null)
            {
                return NotFound();
            }

            return View(pdfWrite);
        }

        // POST: admin/PdfWrites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pdfWrite = await _context.PdfWrites.FindAsync(id);
            if (pdfWrite != null)
            {
                _context.PdfWrites.Remove(pdfWrite);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PdfWriteExists(int id)
        {
            return _context.PdfWrites.Any(e => e.Id == id);
        }
    }
}
