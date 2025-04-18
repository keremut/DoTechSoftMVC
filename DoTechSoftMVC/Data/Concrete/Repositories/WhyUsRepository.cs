using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;

namespace DoTechSoftMVC.Data.Concrete.Repositories
{
    public class WhyUsRepository : Repository<WhyUs>, IWhyUsRepository
    {
        public WhyUsRepository(Context context) : base(context)
        {
        }
    }
}
