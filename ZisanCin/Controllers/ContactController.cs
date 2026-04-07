using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using ZisanCin.Data;
using ZisanCin.Entities;
using ZisanCin.Models;

namespace ZisanCin.Controllers
{
    public class ContactController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GoogleReCaptchaSettings _reCaptchaSettings;

        public ContactController(
            DatabaseContext context,
            IHttpClientFactory httpClientFactory,
            IOptions<GoogleReCaptchaSettings> reCaptchaOptions)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _reCaptchaSettings = reCaptchaOptions.Value;
        }
        [Route("iletisim")]
        public IActionResult Index()
        {
            ViewBag.ReCaptchaSiteKey = _reCaptchaSettings.SiteKey;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactForm entity, [FromForm(Name = "g-recaptcha-response")] string? recaptchaResponse)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(recaptchaResponse))
                        return Json(new { success = false, message = "Lütfen robot olmadığınızı doğrulayın." });

                    var captchaValid = await VerifyReCaptchaAsync(
                        recaptchaResponse,
                        HttpContext.Connection.RemoteIpAddress?.ToString());

                    if (!captchaValid)
                        return Json(new { success = false, message = "reCAPTCHA doğrulaması başarısız oldu." });
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
        private async Task<bool> VerifyReCaptchaAsync(string token, string? remoteIp)
        {
            if (string.IsNullOrWhiteSpace(_reCaptchaSettings.SecretKey))
                return false;

            var client = _httpClientFactory.CreateClient();

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["secret"] = _reCaptchaSettings.SecretKey,
                ["response"] = token,
                ["remoteip"] = remoteIp ?? string.Empty
            });

            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);

            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<GoogleReCaptchaVerifyResponse>(json);

            return result?.Success == true;
        }
    }
}
