using DoTechSoftMVC.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DoTechSoftMVC.ViewComponents
{
    public class WhyUsViewComponent : ViewComponent
    {
        private readonly IWhyUsRepository _whyUsRepository;

        public WhyUsViewComponent(IWhyUsRepository whyUsRepository)
        {
            _whyUsRepository = whyUsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await _whyUsRepository.GetAllAsync();
            return View("Default", list);
        }
    }
}
