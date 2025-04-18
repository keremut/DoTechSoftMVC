using DoTechSoftMVC.Data.Abstract;
using DoTechSoftMVC.Data.Concrete.Entities;

namespace DoTechSoftMVC.Data.Concrete.Repositories
{
    public class AboutRepository: Repository<About>, IAboutRepository
    {
        public AboutRepository(Context context) : base(context)
        {
        }
    }
    
}
