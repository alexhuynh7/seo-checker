using MediatR;
using SEOAutoWebApi.Cache;
using SEOAutoWebApi.Infrastructure.Domain;
using SEOAutoWebApi.Infrastructure.Extensions;
using SEOAutoWebApi.Models;

namespace SEOAutoWebApi.Features
{
    public class GetSupportBrowsersRequest
    {
        public class GetSupportBrowsersResponse : IRequest<BaseResponseModel>
        {
            public List<SupportBrowserModel> SupportBrowsers { get; set; }
        }

        public sealed class Handler
        {
            private readonly ICacheService _cacheService;

            public Handler(ICacheService cacheService)
            {
                _cacheService = cacheService;
            }

            public async Task<BaseResponseModel> Handle()
            {
                var keyCache = string.Format(KeyCacheConstants.SupportBrowsers);
                var res = _cacheService.GetCache<IEnumerable<SupportBrowserModel>>(keyCache);

                if (res != null && res.Count() > 0)
                {
                    return BaseResponseModel.ReturnData(res);
                }
                var supportBrowsers = SearchConstants.SUPPORT_BROWSERS.Select(browser => new SupportBrowserModel { BrowserType = browser, BrowserName = browser.ToName() });
                _cacheService.SetCache(keyCache, supportBrowsers);
                return BaseResponseModel.ReturnData(supportBrowsers);
            }
        }
    }
}
