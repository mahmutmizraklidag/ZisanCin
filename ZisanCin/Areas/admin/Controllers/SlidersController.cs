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
    public class SlidersController : Controller
    {
        private readonly DatabaseContext _context;

        public SlidersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: admin/Sliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }



        // GET: admin/Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image is not null) slider.Image = await FileHelper.FileLoaderAsync(Image);
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: admin/Sliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: admin/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Slider slider, IFormFile? Image)
        {
            if (id != slider.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {

                return View(slider);
            }

            var dbService = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbService is null)
            {
                return NotFound();
            }

            if (Image is not null)
            {
                if (!string.IsNullOrEmpty(dbService.Image))
                {
                    FileHelper.DeleteFile(dbService.Image);
                }
                dbService.Image = await FileHelper.FileLoaderAsync(Image);
            }
            dbService.Title = slider.Title;
            dbService.Description = slider.Description;
            _context.Update(dbService);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Sliders", new { area = "Admin" });
        }

        // GET: admin/Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: admin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                if (!string.IsNullOrEmpty(slider.Image))
                {
                    FileHelper.DeleteFile(slider.Image);
                }
                _context.Sliders.Remove(slider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool SliderExists(int id)
        {
            return _context.Sliders.Any(e => e.Id == id);
        }
    }
}
