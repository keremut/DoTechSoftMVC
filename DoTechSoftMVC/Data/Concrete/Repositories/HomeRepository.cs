using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;

namespace DoTechSoftMVC.Data.Concrete.Repositories
{
    public class HomeRepository : Repository<Home>, IHomeRepository
    {
        public HomeRepository(Context context) : base(context)
        {
        }
    }

}
