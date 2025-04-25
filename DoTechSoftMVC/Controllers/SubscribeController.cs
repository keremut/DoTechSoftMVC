using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoTechSoftMVC.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly ISubscribeRepository _subscribeRepository;
        public SubscribeController(ISubscribeRepository subscribeRepository)
        {
            _subscribeRepository = subscribeRepository;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _subscribeRepository.GetAllAsync();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Subscribe entity)
        {
            await _subscribeRepository.AddAsync(entity);
            return Json(new { success = true, message = "Abone olundu." });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subscribe = await _subscribeRepository.GetByIdAsync(id);
            if (subscribe != null)
            {
                await _subscribeRepository.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
