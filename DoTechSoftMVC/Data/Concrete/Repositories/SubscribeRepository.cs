using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;

namespace DoTechSoftMVC.Data.Concrete.Repositories
{
    public class SubscribeRepository : Repository<Subscribe>, ISubscribeRepository
    {
        public SubscribeRepository(Context context) : base(context)
        {
        }
    }
}
