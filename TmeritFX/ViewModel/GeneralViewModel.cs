using TmeritFX.Models;

namespace TmeritFX.ViewModel
{
    public class GeneralViewModel
    {
        public virtual ApplicationUser Investors { get; set; }
        public virtual List<ApplicationUser> Bonus { get; set; }
        public virtual List<Investment> History { get; set; }
        public decimal myBonus { get; set; }
        public decimal myInvestment { get; set; }
        public virtual List<Investment> CompletedInvestmentList { get; set; }
        public decimal TotalROI { get; set; }
        public virtual List<Withdrawal> Withdrawals { get; set; }
        public decimal TotalWithdrawal { get; set; }
        public decimal TotalPendingWithdrawal { get; set; }
    }
}
