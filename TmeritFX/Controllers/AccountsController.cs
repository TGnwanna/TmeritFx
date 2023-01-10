using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TmeritFX.DataBase_Connection;
using TmeritFX.IHelper;
using TmeritFX.Models;
using TmeritFX.ViewModel;

namespace TmeritFX.Controllers
{
    public class AccountsController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;
        private readonly IEmailHelper _emailHelper;
        private readonly IUserHelper _userHelper;
        private readonly IAccountsHelper _accountsHelper;

        public AccountsController(AppDbContext db, IEmailHelper emailHelper, IUserHelper userHelper, IAccountsHelper accountsHelper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _emailHelper = emailHelper;
            _userHelper = userHelper;
            _accountsHelper = accountsHelper;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register(string RefID)
        {
            ViewBag.RefID = RefID;
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // ADMIN REGISTRAION POST 
        [HttpPost]
        public async Task<JsonResult> Registeration(string userRegistrationData)
        {
            try
            {
                var newAccountData = JsonConvert.DeserializeObject<UserViewModel>(userRegistrationData);

                //We did all the validations
                if (newAccountData != null)
                {

                    var emailCheck = await _userHelper.FindByEmailAsync(newAccountData.Email);
                    if (emailCheck != null)
                    {
                        return Json(new { isError = true, msg = "Email already exist" });
                    }
                    else
                    {
                        var newAccountCreated = _accountsHelper.AccountRegisterationService(newAccountData).Result;
                        var updatedAccountCreated = AddRefLink(newAccountCreated.Id).Result;
                        if (updatedAccountCreated != null)
                        {
                            var userToken = await _emailHelper.CreateUserToken(updatedAccountCreated.Email);
                            var addToRole = _userManager.AddToRoleAsync(updatedAccountCreated, "Investor").Result;
                            if (addToRole.Succeeded & userToken != null)
                            {
                                string linkToClick = HttpContext.Request.Scheme.ToString() + "://"
                                + HttpContext.Request.Host.ToString() + "/Accounts/EmailVerified?token=" + userToken.Token;
                                _emailHelper.VerificationEmail(updatedAccountCreated.Email, linkToClick);
                                return Json(new { isError = false, msg = "Registeration successful, Check your mail to complete application" });

                            }
                        }
                    }
                }
                return Json(new { isError = true, msg = "Application Failed" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApplicationUser> AddRefLink(string userId)
        {
            string refLinkCreated = HttpContext.Request.Scheme.ToString() + "://"
                + HttpContext.Request.Host.ToString() + "/Accounts/Register?RefID=" + userId;
            var user = await _userHelper.FindByIdAsync(userId);
            if (user != null)
            {
                user.RefLink = refLinkCreated;
            }

            _db.Update(user);
            _db.SaveChanges();

            return user;
        }
        public IActionResult EmailVerified(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                Guid userToken = Guid.Parse(token);

                var userVerification = _db.UserVerifications.Where(u => u.Token == userToken).Include(us => us.User).FirstOrDefault();
                if (userVerification == null || userVerification.Token == Guid.Empty)
                {
                    return RedirectToAction("Login");
                }
                if (userVerification.User.EmailConfirmed)
                {
                    return View();
                }
                if (userVerification.Used)
                {
                    return View();
                }
                else
                {
                    userVerification.Used = true;
                    userVerification.DateUsed = DateTime.Now;
                    userVerification.User.EmailConfirmed = true;

                    _db.Update(userVerification);
                    _db.Update(userVerification.User);

                    var sendemail = _emailHelper.Gratitude(userVerification.User.Email);
                    _db.SaveChanges();
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Login(string loginData)
        {
            var userDetails = JsonConvert.DeserializeObject<UserViewModel>(loginData);
            var user = await _userHelper.FindByEmailAsync(userDetails.Email);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    return Json(new { isNotVerified = true, msg = "Email Unverifed!!! Please Verify email to continue" });
                }
                else if (!user.IsActive)
                {
                    return Json(new { isDeactivated = true, msg = "Account Deactivated!!! Please contact support" });
                }
                else if (user.EmailConfirmed && user.IsActive)
                {
                    var currentUser = _accountsHelper.AuthenticateUser(userDetails).Result;
                    if (currentUser != null)
                    {
                        var dashboard = _accountsHelper.GetUserDashboardPage(user);
                        return Json(new { isError = false, msg = "Welcome! " + currentUser.Name, dashboard = dashboard });
                    }
                }
            }
            return Json(new { isError = true, msg = "Login Failed" });
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ChangePassword()
        {
            //ViewBag.Layout = _accountsHelper.GetUserLayout(User.Identity.Name);
            var model = new GeneralViewModel();
            model.Investors = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> ChangePasswordPost(string userPasswordDetails)
        {
            if (userPasswordDetails != null)
            {
                var passDetails = JsonConvert.DeserializeObject<UserViewModel>(userPasswordDetails);

                var currentUser = _userManager.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                var result = await _userManager.ChangePasswordAsync(currentUser, passDetails.OldPassword, passDetails.NewPassword);
                if (result.Succeeded)
                {
                    currentUser.AccessKey = passDetails.NewPassword;

                    _db.ApplicationUser.Update(currentUser);
                    _db.SaveChanges();

                    string loginLink = HttpContext.Request.Scheme.ToString() + "://"
                        + HttpContext.Request.Host.ToString() + "/Accounts/ChangePassword";

                    _emailHelper.ChangePasswordAlert(currentUser, loginLink);
                    var linkToClick = "/Accounts/ChangePassword";
                    return Json(new { isError = false, msg = "Password Changed Successfully", url = linkToClick });
                }
                return Json(new { isError = true, msg = "Failed" });
            }
            return Json(new { isError = true, msg = "Input the required fields" });
        }
    }
}
