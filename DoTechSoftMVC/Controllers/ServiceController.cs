using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;
using DoTechSoftMVC.Data.Concrete.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoTechSoftMVC.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<IActionResult> Index()
        {
            var services = await _serviceRepository.GetAllAsync();   
            return View(services);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Service entity, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // 1. Dosya adı ve yolu belirle
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/adminweb/images/service", fileName);

                // 2. Dosyayı kaydet
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // 3. Model'e resim yolu yaz
                entity.Icon = "~/adminweb/images/service/" + fileName;
            }

            //id bos ise yeni kayit
            if (entity.ServiceId == 0)
            {
                await _serviceRepository.AddAsync(entity);
                return RedirectToAction("Index");
            }
            else
            {
                //id dolu ise guncelleme
                var model = await _serviceRepository.GetByIdAsync(entity.ServiceId);
                if (model != null)
                {
                    model.Title = entity.Title;
                    model.Desc = entity.Desc;
                    model.IsActive = entity.IsActive;
                    model.LongDesc = entity.LongDesc;  
                    if (!string.IsNullOrEmpty(entity.Icon))
                        model.Icon = entity.Icon;

                    await _serviceRepository.UpdateAsync(model);
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
            var model = await _serviceRepository.GetByIdAsync(id);
            if (model != null)
            {
                await _serviceRepository.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
