using System.ComponentModel.DataAnnotations.Schema;
using TmeritFX.Enum;

namespace TmeritFX.Models
{
    public class Withdrawal
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Clients { get; set; }
        public decimal Amount { get; set; }
        public string Address { get; set; }
        public WithdrawalStatus Status { get; set; }
        public DateTime Date { get; set; }
    }
}
