using System.ComponentModel;


namespace TmeritFX.Enum
{
    public enum GeneralAction
    {
        [Description("CREATE")]
        CREATE = 1,
        [Description("EDIT")]
        EDIT,
        [Description("DELETE")]
        DELETE,
        [Description("ACTIVATE")]
        ACTIVATE,
        [Description("DEACTIVATE")]
        DEACTIVATE,
    }
}
