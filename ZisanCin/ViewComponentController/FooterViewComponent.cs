using Microsoft.AspNetCore.Mvc;

namespace ZisanCin.ViewComponentController
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
