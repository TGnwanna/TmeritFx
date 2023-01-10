using System.ComponentModel.DataAnnotations.Schema;
using TmeritFX.Enum;
using TmeritFX.Models;

namespace TmeritFX.ViewModel
{
    public class InvestmentViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Plan { get; set; }
        public string HashID { get; set; }
        public decimal Amount { get; set; }
        public decimal Returns { get; set; }
        public InvestmentStatus Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public GeneralAction ActionType { get; set; }
    }
}
