using Microsoft.AspNetCore.Mvc;

namespace DoTechSoftMVC.Controllers
{
    public class _AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
