using TmeritFX.Models;
using TmeritFX.ViewModel;

namespace TmeritFX.IHelper
{
    public interface IInvestorHelper
    {
        Task<Investment> InvestmentService(InvestmentViewModel newInvestmentData, string username);
        decimal ROIServices(string username);
        Withdrawal WithdrawalServices(WithdrawalViewModel withdrawalData, string username);
    }
}
