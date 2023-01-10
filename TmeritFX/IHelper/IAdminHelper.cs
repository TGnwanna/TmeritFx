using TmeritFX.Models;
using TmeritFX.ViewModel;

namespace TmeritFX.IHelper
{
    public interface IAdminHelper
    {
        ApplicationUser DeactivateClientAccount(UserViewModel collectedData);
        ApplicationUser ActivateClientAccount(UserViewModel collectedData);
        Investment ConfirmClientInvestment(int? Id);
        List<Investment> GetPendingInvestmentList();
    }
}
