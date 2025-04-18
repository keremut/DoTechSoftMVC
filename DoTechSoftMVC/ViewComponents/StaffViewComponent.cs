using DoTechSoftMVC.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DoTechSoftMVC.ViewComponents
{
    public class StaffViewComponent : ViewComponent
    {
        private readonly IStaffRepository _staffRepository;
        public StaffViewComponent(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var staff = await _staffRepository.GetAllAsync();
            var staffList = staff.Where(x => x.IsActive == true);
            return View("Default", staffList);
        }
    }
}
