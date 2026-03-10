using Microsoft.AspNetCore.Mvc;

namespace ZisanCin.ViewComponentController
{
    public class NavbarViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
            {
                return View();
        }
    }
}
