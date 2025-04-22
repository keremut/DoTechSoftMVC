using DoTechSoftMVC.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoTechSoftMVC.Controllers
{
    public class GuestServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        public GuestServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _serviceRepository.GetAllAsync();
            var serviceList = services.Where(s => s.IsActive).ToList();
            return View(serviceList);
        }

        public async Task<IActionResult> ServiceDetail(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null || !service.IsActive)
            {
                return NotFound();
            }
            return View(service);
        }
    }
}
