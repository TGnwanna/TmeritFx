using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TmeritFX.DataBase_Connection;
using TmeritFX.Enum;
using TmeritFX.IHelper;
using TmeritFX.Models;

namespace TmeritFX.Helper
{
    public class UserHelper : IUserHelper
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;

        public UserHelper(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppDbContext db)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
        }
        public async Task<ApplicationUser> FindByUserNameAsync(string userName)
        {
            return await _userManager.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber)
        {
            return await _userManager.Users.Where(s => s.PhoneNumber == phoneNumber)?.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.Users.Where(s => s.Email == email)?.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByIdAsync(string Id)
        {
            return await _userManager.Users.Where(s => s.Id == Id)?.FirstOrDefaultAsync();
        }
        public List<ApplicationUser> GetAllClientsList()
        {
            var allClients = _db.ApplicationUser.Where(b => !b.IsAdmin && !b.IsSuperAdmin).ToList();
            if (allClients.Any())
            {
                return allClients;
            }
            return allClients;
        }
        public async Task<UserVerification> CreateUserToken(string userEmail)
        {
            try
            {
                var user = await FindByEmailAsync(userEmail);
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
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UserVerification> GetUserToken(Guid token)
        {
            return await _db.UserVerifications.Where(t => t.Used != true && t.Token == token)?.Include(s => s.User).FirstOrDefaultAsync();

        }
        public async Task<bool> MarkTokenAsUsed(UserVerification userVerification)
        {
            try
            {
                var VerifiedUser = _db.UserVerifications.Where(s => s.UserId == userVerification.User.Id && s.Used != true).FirstOrDefault();
                if (VerifiedUser != null)
                {
                    userVerification.Used = true;
                    userVerification.DateUsed = DateTime.Now;
                    _db.Update(userVerification);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal GetUsersBonus(string username)
        {
            var user = FindByUserNameAsync(username).Result;
            var myBonus = ((decimal)_userManager.Users.Where(b => b.RefID == user.Id).Sum(s => s.Bonus));
            return myBonus;
        }
        public decimal GetUsersInvestment(string username)
        {
            var user = FindByUserNameAsync(username).Result;
            var myInvestment = ((decimal)_db.Investments.Where(i => i.UserId == user.Id && i.Status != InvestmentStatus.Pending).Sum(s => s.Amount));
            return myInvestment;
        }
        public decimal GetUsersCompletedRIO(string username)
        {
            var user = FindByUserNameAsync(username).Result;
            var myTotalReturn = ((decimal)_db.Investments.Where(i => i.UserId == user.Id && i.Status == InvestmentStatus.Completed).Sum(s => s.Returns));
            return myTotalReturn;
        }
        public decimal GetUsersCompletedWithdrawal(string username)
        {
            var user = FindByUserNameAsync(username).Result;
            var myTotalWithdrawal = ((decimal)_db.Withdrawals.Where(i => i.UserId == user.Id && i.Status == WithdrawalStatus.APPROVED).Sum(s => s.Amount));
            return myTotalWithdrawal;
        }        
        public decimal GetAllUsersWithdrawal(string username)
        {
            var user = FindByUserNameAsync(username).Result;
            var myTotalWithdrawal = ((decimal)_db.Withdrawals.Where(i => i.UserId == user.Id).Sum(s => s.Amount));
            return myTotalWithdrawal;
        }
        public decimal GetUsersPendingWithdrawal(string username)
        {
            var user = FindByUserNameAsync(username).Result;
            var myTotalWithdrawal = ((decimal)_db.Withdrawals.Where(i => i.UserId == user.Id && i.Status == WithdrawalStatus.PENDING).Sum(s => s.Amount));
            return myTotalWithdrawal;
        }
        public decimal GetUsersCompletedInvestment(string username)
        {
            var user = FindByUserNameAsync(username).Result;
            var myInvestment = ((decimal)_db.Investments.Where(i => i.UserId == user.Id && i.Status == InvestmentStatus.Completed).Sum(s => s.Amount));
            return myInvestment;
        }
        public List<Investment> GetClientInvestmentHistory(string username)
        {
            var user = _userManager.Users.Where(u => u.UserName == username).FirstOrDefault();
            var accHistory = _db.Investments.Where(i => i.UserId == user.Id).ToList();
            if (accHistory.Any())
            {
                return accHistory;
            }
            return accHistory;
        }
        public List<ApplicationUser> GetAllClientsBonus(string username)
        {
            var user = _userManager.Users.Where(u => u.UserName == username).FirstOrDefault();
            var refBonus = _db.ApplicationUser.Where(b => b.RefID == user.Id && b.Bonus != 0).ToList();
            if (refBonus.Any())
            {
                return refBonus;
            }
            return refBonus;
        }
        public List<Withdrawal> GetListOfAllWithdrawalOrder(string username)
        {
            var user = _userManager.Users.Where(u => u.UserName == username).FirstOrDefault();
            var withdrawalOrder = _db.Withdrawals.ToList();
            if (withdrawalOrder.Any())
            {
                return withdrawalOrder;
            }
            return withdrawalOrder;
        }
    }
}