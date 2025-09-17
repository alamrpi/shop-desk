using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDesk.Domain.Entities
{
    // This class extends the default IdentityUser with custom properties.
    // In a pure Clean Architecture, the Domain layer shouldn't reference external frameworks like Identity.
    // However, reinventing the entire Identity system is complex.
    // This is a common and pragmatic compromise to leverage the power of ASP.NET Core Identity.
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
