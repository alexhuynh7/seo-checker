using System.ComponentModel;

namespace SEOAutoWebApi.Infrastructure.Enums
{
    public enum BrowserType
    {
        [Description("All")]
        All = 0,
        [Description("Google")]
        Google = 1,
        [Description("Bing")]
        Bing = 2,
    }
}
