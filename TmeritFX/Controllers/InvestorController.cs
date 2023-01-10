using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TmeritFX.DataBase_Connection;
using TmeritFX.Enum;
using TmeritFX.IHelper;
using TmeritFX.Models;
using TmeritFX.ViewModel;

namespace TmeritFX.Controllers
{
    public class InvestorController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;
        private readonly IEmailHelper _emailHelper;
        private readonly IUserHelper _userHelper;
        private readonly IInvestorHelper _investorHelper;
        private readonly IAdminHelper _adminHelper;
        private const string Admin_Email = "breezeblake95@gmail.com";

        public InvestorController(AppDbContext db, IEmailHelper emailHelper, IUserHelper userHelper, IInvestorHelper investorHelper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IAdminHelper adminHelper)
        {
            _db = db;
            _emailHelper = emailHelper;
            _userHelper = userHelper;
            _investorHelper = investorHelper;
            _signInManager = signInManager;
            _userManager = userManager;
            _adminHelper = adminHelper;
        }
        [HttpGet]
        public IActionResult Dashboard()
        {
            var user = User.Identity.Name;
            if (user != null)
            {
                var model = new GeneralViewModel();
                model.Investors = _userHelper.FindByUserNameAsync(user).Result;
                model.myBonus = _userHelper.GetUsersBonus(user);
                model.myInvestment = _userHelper.GetUsersInvestment(user);
                model.TotalROI = _investorHelper.ROIServices(user);
                model.TotalWithdrawal = _userHelper.GetAllUsersWithdrawal(user);
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }
        [HttpGet]
        public IActionResult Plans()
        {
            var user = User.Identity.Name;
            if (user != null)
            {
                var model = new GeneralViewModel();
                model.Investors = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }
        [HttpPost]
        public async Task<JsonResult> InvestmentPost(string investmentData)
        {
            try
            {
                var newInvestmentData = JsonConvert.DeserializeObject<InvestmentViewModel>(investmentData);
                var username = (User.Identity.Name);
                if(newInvestmentData.ActionType == GeneralAction.CREATE)
                {
                    if (newInvestmentData != null && username != null)
                    {
                        var newInvestment = _investorHelper.InvestmentService(newInvestmentData, username).Result;
                        if(newInvestment != null)
                        {
                            _emailHelper.NotifyAdminOfNewInvestment(username, Admin_Email, newInvestmentData.Amount);
                            return Json(new { isError = false, msg = "Submitted Successfully" });
                        }
                    }
                    return Json(new { isError = true, msg = "Submitted Failed" });
                }
                else if (newInvestmentData.ActionType == GeneralAction.ACTIVATE)
                {
                    if (newInvestmentData.Id != null)
                    {
                        var investmentToActivate = _adminHelper.ConfirmClientInvestment(newInvestmentData.Id); 
                        if (investmentToActivate != null)
                        {
                            var investor = _userManager.Users.Where(u => u.Id == investmentToActivate.UserId).FirstOrDefault();
                            _emailHelper.NotifyInvestorOfConfirmedInvestment(investor.Name, investor.Email);
                            return Json(new { isError = false, msg = "Investment Activated Successfully" });
                        }
                    }

                    return Json(new { isError = true, msg = "Activation Failed" });
                }
                    return Json(new { isError = true, msg = "Failed" });

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult History()
        {
            var user = User.Identity.Name;
            if (user != null)
            {
                var model = new GeneralViewModel();
                model.Investors = _userHelper.FindByUserNameAsync(user).Result;
                model.History = _userHelper.GetClientInvestmentHistory(user);
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }
        [HttpGet]
        public IActionResult Referrals()
        {
            var user = User.Identity.Name;
            if (user != null)
            {
                var model = new GeneralViewModel();
                model.Investors = _userHelper.FindByUserNameAsync(user).Result;
                model.Bonus = _userHelper.GetAllClientsBonus(user);
                model.myBonus = _userHelper.GetUsersBonus(user);
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }
        [HttpGet]
        public IActionResult Withdrawal()
       {
            var user = User.Identity.Name;
            if (user != null)
            {
                var model = new GeneralViewModel();
                model.myBonus = _userHelper.GetUsersBonus(user);
                model.Investors = _userHelper.FindByUserNameAsync(user).Result;
                model.myInvestment = _userHelper.GetUsersCompletedInvestment(user);
                model.TotalROI = _userHelper.GetUsersCompletedRIO(user);
                model.TotalWithdrawal = _userHelper.GetUsersCompletedWithdrawal(user);
                model.Withdrawals = _userHelper.GetListOfAllWithdrawalOrder(user);
                model.TotalPendingWithdrawal = _userHelper.GetUsersPendingWithdrawal(user);
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }
        [HttpPost]
        public JsonResult WithdrawalPost(String withdrawalData)
        {
            if(withdrawalData != null)
            {
                var newWithdrawal = JsonConvert.DeserializeObject<WithdrawalViewModel>(withdrawalData);
                var user = User.Identity.Name;
                var investment = _investorHelper.WithdrawalServices(newWithdrawal, user);
                if(investment != null)
                {
                    return Json(new { isError = false, msg = "Withdrawal Request Placed Successfully" });
                }
            }
            return Json(new { isError = true, msg = "Failed" });
        }
    }
}
