using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;
using DoTechSoftMVC.Data.Concrete.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoTechSoftMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;
        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> HomePage()
        {
            var homeList = await _homeRepository.GetAllAsync();
            return View(homeList);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Home entity, IFormFile ImageFile)
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
            if (entity.HomeId == 0)
            {
                await _homeRepository.AddAsync(entity);
                return RedirectToAction("HomePage");
            }
            else
            {
                //id dolu ise guncelleme
                var model = await _homeRepository.GetByIdAsync(entity.HomeId);
                if (model != null)
                {
                    model.Title = entity.Title;
                    model.Desc = entity.Desc;
                    if (!string.IsNullOrEmpty(entity.Image))
                        model.Image = entity.Image;

                    await _homeRepository.UpdateAsync(model);
                    return RedirectToAction("HomePage");
                }
                else
                {
                    return NotFound();
                }
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _homeRepository.GetByIdAsync(id);
            if (model != null)
            {
                await _homeRepository.DeleteAsync(id);
                return RedirectToAction("HomePage");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
