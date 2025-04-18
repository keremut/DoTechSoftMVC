using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoTechSoftMVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutRepository _aboutRepository;
        public AboutController(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }
        public async Task<IActionResult> Index()
        {
            var aboutList = await _aboutRepository.GetAllAsync();
            return View(aboutList);
        }

        [HttpPost]
        public async Task<IActionResult> Add(About entity, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // 1. Dosya adı ve yolu belirle
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/adminweb/images/about", fileName);

                // 2. Dosyayı kaydet
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // 3. Model'e resim yolu yaz
                entity.Image = "~/adminweb/images/about/" + fileName;
            }

            //id bos ise yeni kayit
            if (entity.AboutId == 0) {
                await _aboutRepository.AddAsync(entity);
                return RedirectToAction("Index");
            }
            else
            {
                //id dolu ise guncelleme
                var model = await _aboutRepository.GetByIdAsync(entity.AboutId);
                if (model != null)
                {
                    model.Title1 = entity.Title1;
                    model.Title2 = entity.Title2;
                    model.Desc1 = entity.Desc1;
                    model.Desc2 = entity.Desc2;
                    if (!string.IsNullOrEmpty(entity.Image))
                        model.Image = entity.Image;

                    await _aboutRepository.UpdateAsync(model);
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
            var entity = await _aboutRepository.GetByIdAsync(id);
            if (entity != null)
            {
                await _aboutRepository.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
