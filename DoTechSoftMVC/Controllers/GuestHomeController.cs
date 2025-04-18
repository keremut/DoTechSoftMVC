using Microsoft.AspNetCore.Mvc;

namespace DoTechSoftMVC.Controllers
{
    public class GuestHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
