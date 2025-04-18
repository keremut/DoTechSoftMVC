using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;

namespace DoTechSoftMVC.Data.Concrete.Repositories
{
    public class StaffRepository : Repository<Staff>, IStaffRepository
    {
        public StaffRepository(Context context) : base(context)
        {
        }

    }

}
