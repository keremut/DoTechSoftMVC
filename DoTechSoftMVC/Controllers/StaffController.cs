using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoTechSoftMVC.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffRepository _staffRepository;
        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }
        public async Task<IActionResult> Index()
        {
            var staffs = await _staffRepository.GetAllAsync();
            return View(staffs);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Staff entity, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // 1. Dosya adı ve yolu belirle
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/adminweb/images/home", fileName);

                // 2. Dosyayı kaydet
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // 3. Model'e resim yolu yaz
                entity.Image = "~/adminweb/images/home/" + fileName;
            }

            //id bos ise yeni kayit
            if (entity.StaffId == 0)
            {
                await _staffRepository.AddAsync(entity);
                return RedirectToAction("Index");
            }
            else
            {
                //id dolu ise guncelleme
                var model = await _staffRepository.GetByIdAsync(entity.StaffId);
                if (model != null)
                {
                    model.Name = entity.Name;
                    model.Position = entity.Position;
                    model.LinkedIn = entity.LinkedIn;
                    model.Instagram = entity.Instagram;
                    model.YouTube = entity.YouTube;
                    model.IsActive = entity.IsActive;
                    if (!string.IsNullOrEmpty(entity.Image))
                        model.Image = entity.Image;

                    await _staffRepository.UpdateAsync(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _staffRepository.GetByIdAsync(id);
            if (model != null)
            {
                await _staffRepository.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
