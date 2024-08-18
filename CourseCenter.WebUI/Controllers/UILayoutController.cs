using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult layout()
        {
            return View();
        }
    }
}
