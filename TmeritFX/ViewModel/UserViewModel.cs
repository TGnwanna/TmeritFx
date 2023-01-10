using TmeritFX.Enum;

namespace TmeritFX.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RefLink { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        //To store user password
        public string AccessKey { get; set; }
        public GeneralAction ActionType { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public DateTime DateCreated { get; set; }
        public string RefID { get; set; }
        public double Bonus { get; set; }
        public DateTime DateFunded { get; set; }
    }
}
