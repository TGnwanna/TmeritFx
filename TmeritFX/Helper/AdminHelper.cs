using Microsoft.AspNetCore.Identity;
using TmeritFX.DataBase_Connection;
using TmeritFX.Enum;
using TmeritFX.IHelper;
using TmeritFX.Models;
using TmeritFX.ViewModel;

namespace TmeritFX.Helper
{
    public class AdminHelper: IAdminHelper
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;

        public AdminHelper(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppDbContext db)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
        }

        public ApplicationUser DeactivateClientAccount(UserViewModel collectedData)
        {
            var clientToDisable = _userManager.Users.Where(c => c.Id == collectedData.Id).FirstOrDefault();
            if (clientToDisable != null)
            {
                clientToDisable.IsActive = false;

                _db.Users.Update(clientToDisable);
                _db.SaveChanges();

                return clientToDisable;
            }
            return null;
        }
        public ApplicationUser ActivateClientAccount(UserViewModel collectedData)
        {
            var clientToActivate = _userManager.Users.Where(c => c.Id == collectedData.Id).FirstOrDefault();
            if (clientToActivate != null)
            {
                clientToActivate.IsActive = true;

                _db.Users.Update(clientToActivate);
                _db.SaveChanges();

                return clientToActivate;
            }
            return null;
        }
        public Investment ConfirmClientInvestment(int? Id)
        {
            try
            {
                if (Id != null)
                {
                    var InvestmentoActivate = _db.Investments.Where(c => c.Id == Id).FirstOrDefault();
                    if (InvestmentoActivate != null)
                    {
                        var firstInvestmentCheck = _db.Investments.Where(i => i.UserId == InvestmentoActivate.UserId && i.Status == InvestmentStatus.Active || i.Status == InvestmentStatus.Completed).FirstOrDefault();
                        if (firstInvestmentCheck == null)
                        {
                            var userThatInvested = _userManager.FindByIdAsync(InvestmentoActivate.UserId).Result;
                            if(userThatInvested != null)
                            {
                                decimal tenPercent = 0;
                                userThatInvested.Bonus = (InvestmentoActivate.Amount * tenPercent);
                                userThatInvested.DateFunded = DateTime.Now;

                                _db.ApplicationUser.Update(userThatInvested);
                                _db.SaveChanges();


                                InvestmentoActivate.Status = InvestmentStatus.Active;

                                _db.Investments.Update(InvestmentoActivate);
                                _db.SaveChanges();
                                return InvestmentoActivate;
                            }
                        }
                    }

                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Investment> GetPendingInvestmentList()
        {
            var pendingInvestment = _db.Investments.Where(i => i.Status == InvestmentStatus.Pending).ToList();
            return pendingInvestment;
        }
    }
}
