using Microsoft.AspNetCore.Identity;

namespace TmeritFX.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string RefLink { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        //To store user password
        public string AccessKey { get; set; }
        public string RefID { get; set; }
        public decimal Bonus { get; set; }
        public DateTime DateFunded { get; set; }
    }
}
