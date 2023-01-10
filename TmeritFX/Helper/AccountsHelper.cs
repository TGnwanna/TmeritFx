using Microsoft.AspNetCore.Identity;
using TmeritFX.DataBase_Connection;
using TmeritFX.IHelper;
using TmeritFX.Models;
using TmeritFX.ViewModel;

namespace TmeritFX.Helper
{
    public class AccountsHelper : IAccountsHelper
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserHelper _userHelper;
        private readonly AppDbContext _db;
        private readonly IEmailHelper _emailHelper;

        public AccountsHelper(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserHelper userHelper, AppDbContext db, IEmailHelper emailHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userHelper = userHelper;
            _db = db;
            _emailHelper = emailHelper;
        }
        public async Task<ApplicationUser> AccountRegisterationService(UserViewModel registrationData)
        {
            if (registrationData != null)
            {
                var newAccount = new ApplicationUser();
                {
                    newAccount.Name = registrationData.Name;
                    newAccount.Email = registrationData.Email;
                    newAccount.UserName = registrationData.Email;
                    newAccount.AccessKey = registrationData.Password;
                    newAccount.IsSuperAdmin = false;
                    newAccount.IsActive = true;
                    newAccount.IsAdmin = false;
                    newAccount.DateCreated = DateTime.Now;
                    newAccount.RefID = registrationData.RefID;
                }
                var result = await _userManager.CreateAsync(newAccount, registrationData.Password);
                if (result.Succeeded)
                {
                    return newAccount;
                }
                return null;
            }
            return null;

        }

        public string GetUserDashboardPage(ApplicationUser userr)
        {
            var userRole = _userManager.GetRolesAsync(userr).Result.FirstOrDefault();
            if (userRole != null)
            {
                if (userRole == "SuperAdmin")
                {
                    return "/SuperAdmin/Dashboard";

                }
                else if (userRole == "Admin")
                {
                    return "/Admin/Dashboard";
                }
                else
                {
                    return "/Investor/Dashboard";
                }
            }
            return null;
        }

        public string GetUserLayout(string username)
        {
            var accountType = _userHelper.FindByUserNameAsync(username).Result;
            var userRole = _userManager.GetRolesAsync(accountType).Result.FirstOrDefault();
            if (userRole != null)
            {
                if (userRole == "Admin")
                {
                    return "~/Views/Shared/_AdminLayout.cshtml";
                }
                else
                {
                    return "~/Views/Shared/_InvestorLayout.cshtml";
                }
            }
            return null;
        }

        public async Task<ApplicationUser> AuthenticateUser(UserViewModel loginDetail)
        {
            var user = await _userManager.FindByEmailAsync(loginDetail.Email);
            if (user != null)
            {
                var logger = _signInManager.PasswordSignInAsync(user.UserName, loginDetail.Password, true, false).Result;
                if (logger.Succeeded)
                {
                    return user;
                }
            }
            return null;
        }

    }
}
