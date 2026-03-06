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

namespace ZisanCin.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class BodyMassIndexesController : Controller
    {
        private readonly DatabaseContext _context;

        public BodyMassIndexesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: admin/BodyMassIndexes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BodyMassIndex.ToListAsync());
        }

        // GET: admin/BodyMassIndexes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bodyMassIndex = await _context.BodyMassIndex
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bodyMassIndex == null)
            {
                return NotFound();
            }

            return View(bodyMassIndex);
        }

        // GET: admin/BodyMassIndexes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/BodyMassIndexes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Age,Gender,Height,Weight,Result,Status")] BodyMassIndex bodyMassIndex)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bodyMassIndex);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bodyMassIndex);
        }

        // GET: admin/BodyMassIndexes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bodyMassIndex = await _context.BodyMassIndex.FindAsync(id);
            if (bodyMassIndex == null)
            {
                return NotFound();
            }
            return View(bodyMassIndex);
        }

        // POST: admin/BodyMassIndexes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Age,Gender,Height,Weight,Result,Status")] BodyMassIndex bodyMassIndex)
        {
            if (id != bodyMassIndex.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bodyMassIndex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BodyMassIndexExists(bodyMassIndex.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bodyMassIndex);
        }

        // GET: admin/BodyMassIndexes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bodyMassIndex = await _context.BodyMassIndex
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bodyMassIndex == null)
            {
                return NotFound();
            }

            return View(bodyMassIndex);
        }

        // POST: admin/BodyMassIndexes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bodyMassIndex = await _context.BodyMassIndex.FindAsync(id);
            if (bodyMassIndex != null)
            {
                _context.BodyMassIndex.Remove(bodyMassIndex);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BodyMassIndexExists(int id)
        {
            return _context.BodyMassIndex.Any(e => e.Id == id);
        }
    }
}
