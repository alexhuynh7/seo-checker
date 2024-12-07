using SEOAutoWebApi.Infrastructure.Enums;
using SEOAutoWebApi.Infrastructure.Interface;

namespace SEOAutoWebApi.Infrastructure.Services
{
    public class SearchServiceFactory : ISearchServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SearchServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISearchService GetSearchService(BrowserType browserType)
        {
            return browserType switch
            {
                BrowserType.Google => _serviceProvider.GetRequiredService<GoogleSearchService>(),
                BrowserType.Bing => _serviceProvider.GetRequiredService<BingSearchService>(),
                _ => throw new ArgumentException("Invalid search engine type")
            };
        }
    }
}
