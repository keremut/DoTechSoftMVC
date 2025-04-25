using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;
using DoTechSoftMVC.Data.Concrete.Repositories;
using DoTechSoftMVC.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoTechSoftMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactRepository.GetAllAsync();
            return View(contacts);
        }


        [HttpPost]
        public async Task<IActionResult> Add(Contact entity)
        {
            await _contactRepository.AddAsync(entity);
            return Json(new { success = true, message = "Mesajınız başarıyla gönderildi!" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact != null)
            {
                await _contactRepository.DeleteAsync(id);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead([FromBody] MarkAsReadRequest request)
        {
            int id = request.Id;
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact != null)
            {
                if (!contact.IsRead)
                {
                    contact.IsRead = true;
                    await _contactRepository.UpdateAsync(contact);
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}
