using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;
using DoTechSoftMVC.Data.Concrete.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoTechSoftMVC.Controllers
{
    public class WhyUsController : Controller
    {
        private readonly IWhyUsRepository _whyUsRepository;
        public WhyUsController(IWhyUsRepository whyUsRepository)
        {
            _whyUsRepository = whyUsRepository; 
        }
        public async Task<IActionResult> Index()
        {
            var list = await _whyUsRepository.GetAllAsync();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Add(WhyUs entity, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // 1. Dosya adı ve yolu belirle
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/adminweb/images/whyus", fileName);

                // 2. Dosyayı kaydet
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // 3. Model'e resim yolu yaz
                entity.Icon = "~/adminweb/images/whyus/" + fileName;
            }

            //id bos ise yeni kayit
            if (entity.WhyUsId == 0)
            {
                await _whyUsRepository.AddAsync(entity);
                return RedirectToAction("Index");
            }
            else
            {
                //id dolu ise guncelleme
                var model = await _whyUsRepository.GetByIdAsync(entity.WhyUsId);
                if (model != null)
                {
                    model.Title = entity.Title;
                    model.Desc = entity.Desc;
                    if (!string.IsNullOrEmpty(entity.Icon))
                        model.Icon = entity.Icon;

                    await _whyUsRepository.UpdateAsync(model);
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
            var model = await _whyUsRepository.GetByIdAsync(id);
            if (model != null)
            {
                await _whyUsRepository.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
