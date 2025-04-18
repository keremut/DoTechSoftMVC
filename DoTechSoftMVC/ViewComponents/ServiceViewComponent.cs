using DoTechSoftMVC.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DoTechSoftMVC.ViewComponents
{
    public class ServiceViewComponent : ViewComponent
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceViewComponent(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IViewComponentResult>  InvokeAsync()
        {
            var list = await _serviceRepository.GetAllAsync();
            var services = list.Where(x => x.IsActive == true);
            return View("Default",services);
        }


    }
}
