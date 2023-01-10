using TmeritFX.Config;
using TmeritFX.DataBase_Connection;
using TmeritFX.IHelper;
using TmeritFX.Models;
using TmeritFX.SmtpMailServices;
using TmeritFX.ViewModel;

namespace TmeritFX.Helper
{
    public class EmailHelper : IEmailHelper
    {

        private readonly IUserHelper _userHelper;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _db;
        private readonly IGeneralConfiguration _generalConfiguration;

        public EmailHelper(AppDbContext db, IUserHelper userHelper, IEmailService emailService, IGeneralConfiguration generalConfiguration)
        {
            _db = db;
            _emailService = emailService;
            _userHelper = userHelper;
            _generalConfiguration = generalConfiguration;
        }
        public bool SendMailToApplicant(ApplicationUser userDetail, string linkToClick)
        {
            string toEmail = userDetail.Email;
            string subject = "Registration Completed Successfully";
            string message = "Dear " + userDetail.Name + ", You have successfully completed the registration in our platform" + " Please click on the link below to choose your interview date" + "<br/>" + "<br/>" +
                   "<a  href='" + linkToClick + "?userId=" + userDetail.Id + "' target='_blank'>" + "<button style='color:white; background-color:#018DE4; padding:10px; border:10px;'>Create Now</button>" + "</a>";
            _emailService.SendEmail(toEmail, subject, message);
            return true;
        }
        public bool NotifyAdminOfNewInvestment(string username,  string adminEmail, decimal InvestmentAmount)
        {
            string toEmail = adminEmail;
            string subject = "NEW INVESTMENT NOTIFICATION";
            string message = "Dear Admin " + "<br/>" + "<br/>" +
                "Client" + username + "Have successfully submitted an investmented $" + InvestmentAmount + " in your platform. Please verify investment and do the needfull." + "<br/>" + "<br/>" +
                "Joy anticipated";
            _emailService.SendEmail(toEmail, subject, message);
            return true;
        }
        public bool NotifyInvestorOfConfirmedInvestment(string username, string email)
        {
            string toEmail = email;
            string subject = "INVESTMENT CONFIRMATION NOTIFICATION";
            string message = "Dear " + username + "<br/>" + "<br/>" +
                "Your investment has been confirmed and actively running." + "<br/>" + "<br/>" + "Tips: Did you know that by referring a friend you earn 10% of their first deposit?. Login and share your referral link to earn more" + "<br/>" + "<br/>" +
                "TmeritFX";
            _emailService.SendEmail(toEmail, subject, message);
            return true;
        }
        public bool VerificationEmail(string clientEmail, string linkToClick)
        {
            try
            {
                if (clientEmail != null)
                {
                    string toEmail = clientEmail;
                    string subject = "Verify your account - TmeritFX ";
                    string message = "Thank you for registering! You’re only one click away from accessing your TmeritFX account." + "<br/>" + "<br/>" +
                        "<a  href='" + linkToClick + "' target='_blank'>" + "<button style='color:white; background-color:#FFA76F; padding:10px; border:10px;'>Verify Me Now</button>" + "</a>" + "<br/>" +
                        "If the link does not work copy the link here to your browser: " + linkToClick + "<br/>" +
                        "Need help? We’re here for you.Simply reply to this email to contact us. <br/>" +

                        "Kind regards,<br/>" +
                        "TmeritFX";
                    _emailService.SendEmail(toEmail, subject, message);
                    return true;
                };
                return false;
            }

            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UserVerification> CreateUserToken(string userEmail)
        {
            try
            {
                var user = await _userHelper.FindByEmailAsync(userEmail);
                if (user != null)
                {
                    UserVerification userVerification = new UserVerification()
                    {
                        UserId = user.Id,
                    };
                    await _db.AddAsync(userVerification);
                    await _db.SaveChangesAsync();
                    return userVerification;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool Gratitude(string clientEmail)
        {
            try
            {
                if (clientEmail != null)
                {
                    string toEmail = clientEmail;
                    string subject = "Email Verified Successfully";
                    string message = "Your Email has been verified.Thank you for registering with TmeritFX. <br/>" + "<br/>" +
                    "Need help? We’re always here for you.Simply reply to this email to reach us. <br/>" + "<br/>" +
                    "Kind regards,<br/>" +
                    "<b>Tmerit.</b>";
                    _emailService.SendEmail(toEmail, subject, message);
                    return true;
                };

                return false;
            }

            catch (Exception)
            {
                throw;
            }
        }
        public async void ForgotPasswordTemplateEmailer(ApplicationUser userEmail, string linkToClick)
        {
            var getUserId = await _userHelper.FindByUserNameAsync(userEmail.Name);
            if (userEmail != null)
            {
                string toEmail = userEmail.Email;
                string subject = "Reset your Email - TmeritFX";
                string message = "Hi!" + " " + userEmail.Name + "," + "<br/>" + "<br/>" +
                    "You requested for a password reset. please click on the link below to create a new password," + "<br/>" +
                    "<a  href='" + linkToClick + "' target='_blank'>" + "<button style='color:white; background-color:#FFA76F; padding:10px; border:10px;'>Password Reset</button>" + "</a>" + "<br/>" +
                    "If the link does not work copy the link here to your browser" + "<br/>" +
                    "Need help? We’re here for you.Simply reply to this email to contact us. <br/>" +

                    "Kind regards,<br/>" +
                    "TmeritFX";
                _emailService.SendEmail(toEmail, subject, message);

            }

        }
        public async Task PasswordResetedTemplateEmailerAsync(ApplicationUser userEmail, string linkToClick)
        {
            var getUserId = await _userHelper.FindByUserNameAsync(userEmail.Name);
            if (userEmail != null)
            {
                string toEmail = userEmail.Email;
                string subject = "Reset your Email - TmeritFX ";
                string message = "Hi!" + userEmail.Name + "," + "<br/>" + "<br/>" +
                "You requested for a password reset." + "Your password has been changed.If you did not make this change please email us at support@safecash.com<br/> Regards" + " < br/>" +
                    "<a  href='" + linkToClick + "' target='_blank'>" + "<button style='color:white; background-color:#FFA76F; padding:10px; border:10px;'>Verify Me Now</button>" + "</a>" + "<br/>" +
                   "If you have any trouble with your account, you can always email us at hello@TmeritFX.com," + "<br/>" +
                    "Need help? We’re here for you.Simply reply to this email to contact us. <br/>" +

                    "Kind regards,<br/>" +
                    "TmeritFX";
                _emailService.SendEmail(toEmail, subject, message);

            }
        }
        public bool ChangePasswordAlert(ApplicationUser userDetail, string loginLink)
        {
            string toEmail = userDetail.Email;
            string subject = "PASSWORD CHANGE ALERT";
            string message = "Dear " + userDetail.Name + "," + "<br/>" + "<br/>" + "Your password has been changed successfully. If you did not make this change please email us at admin@TmeritFX.com" + "<br/>" + "<br/>" + "Regards";
            _emailService.SendEmail(toEmail, subject, message);

            ChangePasswordMailTemlate(userDetail, loginLink);
            return true;
        }
        public bool ChangePasswordMailTemlate(ApplicationUser userDetail, string loginLink)
        {
            string toEmail = userDetail.Email;
            string subject = "PASSWORD CHANGED SUCCESSFULLY ";
            string message = "Hi " + userDetail.Name + ", " + "<br/>" + "<br/>" + "You have successfully changed your password. please click on the link below to login with your new password" + "<br/>" + "<br/>" + "Regards" + "<br/>" + "<br/>" +
                   "<a  href='" + loginLink + "' target='_blank'>" + "<button style='color:white; background-color:#018DE4; padding:10px; border:10px;'>Login</button>" + "</a>";
            _emailService.SendEmail(toEmail, subject, message);
            return true;
        }

    }
}