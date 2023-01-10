using System.ComponentModel;

namespace TmeritFX.Enum
{
    public enum InvestmentStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("Completed")]
        Completed = 2,
        [Description("Pending")]
        Pending = 3,
    }
}
