using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TmeritFX.Models
{
    public class Referrals
    {
        [Key]
        public int Id { get; set; }
        public string Referral { get; set; }
        public string Referee { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser Reff { get; set; }
    }
}
