using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;

namespace DoTechSoftMVC.Data.Concrete.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(Context context) : base(context)
        {
        }
    }
}
