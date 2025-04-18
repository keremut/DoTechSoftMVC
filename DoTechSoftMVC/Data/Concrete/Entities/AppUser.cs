using Microsoft.AspNetCore.Identity;

namespace DoTechSoftMVC.Data.Concrete.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
