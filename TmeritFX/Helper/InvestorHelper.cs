using Microsoft.AspNetCore.Identity;
using TmeritFX.DataBase_Connection;
using TmeritFX.Enum;
using TmeritFX.IHelper;
using TmeritFX.Models;
using TmeritFX.ViewModel;

namespace TmeritFX.Helper
{
    public class InvestorHelper: IInvestorHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;
        private readonly IUserHelper _userHelper;

        public InvestorHelper(UserManager<ApplicationUser> userManager, AppDbContext db, IUserHelper userHelper)
        {
            _userManager = userManager;
            _db = db;
            _userHelper = userHelper;
        }

        public async Task<Investment> InvestmentService(InvestmentViewModel newInvestmentData, string username)
        {
            if (newInvestmentData != null && username != null)
            {
                var userToInvest = _userManager.FindByEmailAsync(username).Result;
                var dateInv = DateTime.Now;
                DateTime dueDate = new DateTime();
                if (newInvestmentData.Plan == "BASIC")
                {
                    dueDate = dateInv.AddDays(12);
                }
                else if (newInvestmentData.Plan == "CORPORATE")
                {
                    dueDate = dateInv.AddDays(10);
                }
                else if (newInvestmentData.Plan == "PREMIUM")
                {
                    dueDate = dateInv.AddDays(8);
                }
                else if (newInvestmentData.Plan == "EXECUTIVE")
                {
                    dueDate = dateInv.AddDays(6);
                }
                var newInvestment = new Investment
                {
                    UserId = userToInvest.Id,
                    Plan = newInvestmentData.Plan,
                    Amount = newInvestmentData.Amount,
                    Status = InvestmentStatus.Pending,
                    Date = DateTime.Now,
                    DueDate = dueDate,
                    HashID = newInvestmentData.HashID,
                };
                var result = await _db.Investments.AddAsync(newInvestment);
                                   _db.SaveChanges();
                if (result != null)
                {
                    return newInvestment;
                }
                return null;
            }
            return null;

        }
        public decimal ROIServices(string username)
        {
            if (username != null)
            {
                var user = _userHelper.FindByUserNameAsync(username).Result;
                var myActiveInvestment = new List<Investment>();
                myActiveInvestment = _db.Investments.Where(i => i.UserId == user.Id && i.Status == InvestmentStatus.Active).ToList();
                if (myActiveInvestment.Count() != 0)
                {
                    foreach (var inv in myActiveInvestment)
                    {
                        var dateInv = inv.Date;
                        var currentDate = (DateTime.Now).AddDays(-1);
                        var plan = 0;
                        var dailyPercent = 0;
                        if (inv.Plan == "BASIC")
                        {
                            plan = 12;
                            dailyPercent = 8;
                        }
                        else if (inv.Plan == "CORPORATE")
                        {
                            plan = 10;
                            dailyPercent = 10;
                        }
                        else if (inv.Plan == "PREMIUM")
                        {
                            plan = 8;
                            dailyPercent = 15;
                        }
                        else if (inv.Plan == "EXECUTIVE")
                        {
                            plan = 6;
                            dailyPercent = 25;
                        }
                        decimal percentage = ((decimal)dailyPercent / 100);
                        var dailyReturn = inv.Amount * percentage;
                        TimeSpan ts = inv.DueDate - currentDate;
                        int remainDate = ts.Days;
                           var stopper = plan - remainDate;
                        if (remainDate >= 1 && stopper >= 1)
                        {
                            decimal rio = stopper * dailyReturn;
                            inv.Returns = rio;

                            _db.Investments.Update(inv);
                            _db.SaveChanges();
                        }
                    }
                }
                myActiveInvestment = _db.Investments.Where(i => i.UserId == user.Id && i.Status == InvestmentStatus.Completed).ToList();
                if (myActiveInvestment.Count() != 0)
                {
                    foreach (var inv in myActiveInvestment)
                    {
                        var dateInv = inv.Date;
                        var dailyPercent = 0; 
                        var plan = 0;
                        decimal expectedRIO = 0;
                        if (inv.Plan == "BASIC")
                        {
                            plan = 12;
                            dailyPercent = 8;
                            expectedRIO = inv.Amount * dailyPercent;
                            if(inv.Returns != expectedRIO)
                            {
                                decimal percentage = ((decimal)dailyPercent / 100);
                                var dailyReturn = inv.Amount * percentage;

                                decimal rio = plan * dailyReturn;
                                inv.Returns = rio;

                                _db.Investments.Update(inv);
                                _db.SaveChanges();
                            }
                        }
                        else if (inv.Plan == "CORPORATE")
                        {
                            plan = 10;
                            dailyPercent = 10;
                            expectedRIO = inv.Amount * dailyPercent;
                            if (inv.Returns != expectedRIO)
                            {
                                decimal percentage = ((decimal)dailyPercent / 100);
                                var dailyReturn = inv.Amount * percentage;

                                decimal rio = plan * dailyReturn;
                                inv.Returns = rio;

                                _db.Investments.Update(inv);
                                _db.SaveChanges();
                            }
                        }
                        else if (inv.Plan == "PREMIUM")
                        {
                            plan = 8;
                            dailyPercent = 15;
                            expectedRIO = inv.Amount * dailyPercent;
                            if (inv.Returns != expectedRIO)
                            {
                                decimal percentage = ((decimal)dailyPercent / 100);
                                var dailyReturn = inv.Amount * percentage;

                                decimal rio = plan * dailyReturn;
                                inv.Returns = rio;

                                _db.Investments.Update(inv);
                                _db.SaveChanges();
                            }
                        }
                        else if (inv.Plan == "EXECUTIVE")
                        {
                            plan = 6;
                            dailyPercent = 25;
                            expectedRIO = inv.Amount * dailyPercent;
                            if (inv.Returns != expectedRIO)
                            {
                                decimal percentage = ((decimal)dailyPercent / 100);
                                var dailyReturn = inv.Amount * percentage;

                                decimal rio = plan * dailyReturn;
                                inv.Returns = rio;

                                _db.Investments.Update(inv);
                                _db.SaveChanges();
                            }
                        }

                    }
                }

                    var myTotalReturn = ((decimal)_db.Investments.Where(i => i.UserId == user.Id && i.Status != InvestmentStatus.Pending).Sum(s => s.Returns));
                return myTotalReturn;
            }
            return 0;
        }

        public Withdrawal WithdrawalServices(WithdrawalViewModel withdrawalData, string username)
        {
            if(withdrawalData != null && username != null)
            {
                var user = _userHelper.FindByUserNameAsync(username).Result;
                var myWithdrawal = new Withdrawal()
                {
                    UserId = user.Id,
                    Amount = withdrawalData.Amount,
                    Address = withdrawalData.Address,
                    Date = DateTime.Now,
                    Status = WithdrawalStatus.PENDING,
                };

                var newWithdrawal = _db.Withdrawals.Add(myWithdrawal);
                _db.SaveChanges();

                if(newWithdrawal != null)
                {
                    return myWithdrawal;
                }
            }
            return null;
        }
    }
}
