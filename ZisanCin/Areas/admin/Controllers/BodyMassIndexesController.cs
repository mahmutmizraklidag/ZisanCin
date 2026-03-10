using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ZisanCin.Data;

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
            var list = await _context.BodyMassIndex
                .AsNoTracking()
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();

            return View(list);
        }

        // AJAX: Detay getir
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var bodyMassIndex = await _context.BodyMassIndex
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (bodyMassIndex == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Kayıt bulunamadı."
                });
            }

            return Json(new
            {
                success = true,
                data = new
                {
                    id = bodyMassIndex.Id,
                    name = bodyMassIndex.Name,
                    phone = string.IsNullOrWhiteSpace(bodyMassIndex.Phone) ? "-" : bodyMassIndex.Phone,
                    age = bodyMassIndex.Age,
                    gender = bodyMassIndex.Gender,
                    height = bodyMassIndex.Height.ToString("0.##"),
                    weight = bodyMassIndex.Weight.ToString("0.##"),
                    result = bodyMassIndex.Result.ToString("0.00"),
                    status = string.IsNullOrWhiteSpace(bodyMassIndex.Status) ? "-" : bodyMassIndex.Status,
                    createDate = bodyMassIndex.CreateDate.ToString("dd.MM.yyyy HH:mm")
                }
            });
        }

        // AJAX: Sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var bodyMassIndex = await _context.BodyMassIndex.FindAsync(id);

            if (bodyMassIndex == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Silinecek kayıt bulunamadı."
                });
            }

            _context.BodyMassIndex.Remove(bodyMassIndex);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = "Kayıt başarıyla silindi."
            });
        }
    }
}