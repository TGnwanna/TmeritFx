using TmeritFX.Models;
using TmeritFX.ViewModel;

namespace TmeritFX.IHelper
{
    public interface IAccountsHelper
    {
        Task<ApplicationUser> AccountRegisterationService(UserViewModel registrationData);
        string GetUserDashboardPage(ApplicationUser userr);
        Task<ApplicationUser> AuthenticateUser(UserViewModel loginDetail);
        string GetUserLayout(string username);
    }
}
