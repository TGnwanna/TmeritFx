using TmeritFX.Models;

namespace TmeritFX.IHelper
{
    public interface IUserHelper
    {
        Task<ApplicationUser> FindByUserNameAsync(string userName);
        Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(string Id);
        List<ApplicationUser> GetAllClientsList();
        Task<UserVerification> CreateUserToken(string userEmail);
        Task<UserVerification> GetUserToken(Guid token);
        Task<bool> MarkTokenAsUsed(UserVerification userVerification);
        decimal GetUsersBonus(string username);
        decimal GetUsersInvestment(string username);
        decimal GetUsersCompletedInvestment(string username);
        decimal GetUsersPendingWithdrawal(string username);
        decimal GetAllUsersWithdrawal(string username);
        decimal GetUsersCompletedRIO(string username);
        List<Investment> GetClientInvestmentHistory(string username); 
        List<ApplicationUser> GetAllClientsBonus(string username);
        decimal GetUsersCompletedWithdrawal(string username);
        List<Withdrawal> GetListOfAllWithdrawalOrder(string username);
    }
}
