using SEOAutoWebApi.Infrastructure.Enums;

namespace SEOAutoWebApi.Infrastructure.Interface
{
    public interface ISearchServiceFactory
    {
        ISearchService GetSearchService(BrowserType browserType);
    }
}
