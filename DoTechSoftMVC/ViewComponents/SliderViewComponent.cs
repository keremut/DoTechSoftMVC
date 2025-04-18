using DoTechSoftMVC.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DoTechSoftMVC.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly IHomeRepository _homeRepository;
        public SliderViewComponent(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
         
        {
            var list = await _homeRepository.GetAllAsync();
            return View("Default", list);
        }
    }
}
