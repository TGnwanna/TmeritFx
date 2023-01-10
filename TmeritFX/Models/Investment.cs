using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmeritFX.Enum;

namespace TmeritFX.Models
{
    public class Investment
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Clients { get; set; }
        public string Plan { get; set; }
        public string HashID { get; set; }
        public decimal Amount { get; set; }
        public decimal Returns { get; set; }
        public InvestmentStatus Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
    }
}
