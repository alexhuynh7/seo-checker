using MediatR;
using SEOAutoWebApi.Cache;
using SEOAutoWebApi.Infrastructure.Domain;
using SEOAutoWebApi.Infrastructure.Enums;
using SEOAutoWebApi.Infrastructure.Extensions;
using SEOAutoWebApi.Models;

namespace SEOAutoWebApi.Features
{
    public static class GetSupportBrowsersRequest
    {
        public class Command : IRequest<ResponseModel>
        {
            
        }

        public sealed class Handler : IRequestHandler<GetSupportBrowsersRequest.Command, ResponseModel>
        {
            private readonly ICacheService _cacheService;

            public Handler(ICacheService cacheService)
            {
                _cacheService = cacheService;
            }

            public async Task<ResponseModel> Handle(GetSupportBrowsersRequest.Command request, CancellationToken cancellationToken)
            {
                var keyCache = string.Format(KeyCacheConstants.SupportBrowsers);
                var res = _cacheService.GetCache<IEnumerable<SupportBrowserModel>>(keyCache);

                if (res != null && res.Count() > 0)
                {
                    return ResponseModel.ReturnData(res);
                }
                var supportBrowsers = SearchConstants.SUPPORT_BROWSERS.Select(browser => new SupportBrowserModel { BrowserType = browser, BrowserName = browser.ToName() });
                _cacheService.SetCache(keyCache, supportBrowsers);
                return ResponseModel.ReturnData(supportBrowsers);
            }
        }
    }
}
