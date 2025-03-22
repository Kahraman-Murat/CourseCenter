using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Contact
{
    public class _ContactSendMessageComponent : ViewComponent
    {
        public IViewComponentResult Invoke() 
        { 
            return View(); 
        }
    }
}
