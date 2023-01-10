using System.ComponentModel;

namespace TmeritFX.Enum
{
    public enum Plans
    {
        [Description("BASIC")]
        BASIC = 1,
        [Description("CORPORATE")]
        CORPORATE,
        [Description("PREMIUM")]
        PREMIUM,
        [Description("EXECUTIVE")]
        EXECUTIVE,
    }
}
