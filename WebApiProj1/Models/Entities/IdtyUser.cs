using Microsoft.AspNetCore.Identity;

namespace WebApiProj1.Models.Entities
{
    public class IdtyUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
