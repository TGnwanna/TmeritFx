using TmeritFX.Models;

namespace TmeritFX.IHelper
{
    public interface IEmailHelper
    {
        bool SendMailToApplicant(ApplicationUser userDetail, string linkToClick);
        bool NotifyAdminOfNewInvestment(string username, string adminEmail, decimal InvestmentAmount);
        bool NotifyInvestorOfConfirmedInvestment(string username, string email);
        bool VerificationEmail(string applicantEmail, string linkToClick);
        Task<UserVerification> CreateUserToken(string userEmail);
        bool Gratitude(string applicantEmail);
        void ForgotPasswordTemplateEmailer(ApplicationUser userEmail, string linkToClick);
        Task PasswordResetedTemplateEmailerAsync(ApplicationUser userEmail, string linkToClick);
        bool ChangePasswordMailTemlate(ApplicationUser userDetail, string linkToClick);
        bool ChangePasswordAlert(ApplicationUser userDetail, string linkToClick);
    }
}
