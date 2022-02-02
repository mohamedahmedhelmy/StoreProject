using Microsoft.AspNetCore.Identity;

namespace Domains
{
    public class ApplicationUser :IdentityUser
    {
        public string City { get; set; }
    }
}
