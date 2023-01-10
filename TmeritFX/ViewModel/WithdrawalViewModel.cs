using TmeritFX.Enum;
using TmeritFX.Models;

namespace TmeritFX.ViewModel
{
    public class WithdrawalViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string Address { get; set; }
        public WithdrawalStatus Status { get; set; }
        public DateTime Date { get; set; }
    }
}
