using DoTechSoftMVC.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoTechSoftMVC.Controllers
{
    public class GuestAboutController : Controller
    {
        private readonly IAboutRepository _aboutRepository;
        public GuestAboutController(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutRepository.GetAllAsync();
            var about = abouts.FirstOrDefault();
            return View(about);
        }
    }
}
