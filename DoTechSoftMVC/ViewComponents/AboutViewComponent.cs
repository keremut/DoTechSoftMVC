using DoTechSoftMVC.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DoTechSoftMVC.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly IAboutRepository _aboutRepository;
        public AboutViewComponent(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        public async Task<IViewComponentResult>  InvokeAsync()
        
        {
            var list = await _aboutRepository.GetAllAsync();
            var about = list.FirstOrDefault();
            return View("Default", about);
        }
    }
}
