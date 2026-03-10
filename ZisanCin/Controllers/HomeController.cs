using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ZisanCin.Data;
using ZisanCin.Entities;
using ZisanCin.Models;

namespace ZisanCin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalculateBmiAjax(BodyMassIndex model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        k => k.Key,
                        v => v.Value.Errors.First().ErrorMessage
                    );

                return Json(new
                {
                    success = false,
                    message = "Lütfen form alanlarını kontrol ediniz.",
                    errors
                });
            }

            var heightMeter = model.Height / 100.0;
            var bmi = model.Weight / (heightMeter * heightMeter);

            string result;
            if (bmi < 18.5)
                result = "Zayıf";
            else if (bmi <= 24.9)
                result = "İdeal";
            else if (bmi <= 29.9)
                result = "Fazla Kilolu";
            else if (bmi <= 34.9)
                result = "1. Derece Obezite";
            else if (bmi <= 39.9)
                result = "2. Derece Obezite";
            else
                result = "3. Derece Obezite (Morbid Obezite)";

            model.Result = Math.Round(bmi, 2);
            model.Status = result;
            model.CreateDate = DateTime.Now;

            _context.BodyMassIndex.Add(model);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                bmi = model.Result.ToString("0.00"),
                status = model.Status,
                message = "VKİ hesaplandı ve kayıt başarıyla alındı."
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
