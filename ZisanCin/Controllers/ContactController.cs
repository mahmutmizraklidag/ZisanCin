using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZisanCin.Data;
using ZisanCin.Entities;

namespace ZisanCin.Controllers
{
    public class ContactController : Controller
    {
        private readonly DatabaseContext _context;

        public ContactController(DatabaseContext context)
        {
            _context = context;
        }
        [Route("iletisim")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactForm entity)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    _context.ContactForms.Add(entity);
                    int result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        //var temp = MailTemplates.ContactFormTemplate(entity);
                        //var temp2 = MailTemplates.ContactFormAutoReplyTemplate(entity);
                        //MailSender mailSender = new MailSender();
                        //await mailSender.SendMailAsync("info@ideiocreative.com", "İdeio Creative Teklif Formu", temp, entity.Name);
                        //await mailSender.SendMailAsync(entity.Email, "İdeio Creative - Talebiniz Alındı", temp2, entity.Name);
                        return Json(new { success = true, message = "Mesajınız gönderildi." });
                    }
                }
                catch
                {
                    return Json(new { success = false, message = "Hata oluştu!" });
                }
            }
            return Json(new { success = false, message = "Form bilgileri hatalı!" });
        }
    }
}
