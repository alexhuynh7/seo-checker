using MediatR;
using SEOAutoWebApi.Cache;
using SEOAutoWebApi.Infrastructure.Enums;
using SEOAutoWebApi.Infrastructure.Extensions;
using SEOAutoWebApi.Infrastructure.Interface;
using SEOAutoWebApi.Models;

namespace SEOAutoWebApi.Features
{
    public static class SearchRequest
    {
        public class Command : IRequest<BaseResponseModel>
        {
            public string Keyword { get; set; }
            public string Url { get; set; }
            public BrowserType BrowserType { get; set; }
        }

        public sealed class Handler : IRequestHandler<SearchRequest.Command, BaseResponseModel>
        {
            private readonly ICacheService _cacheService;
            private readonly ISearchServiceFactory _searchServiceFactory;

            public Handler(ICacheService cacheService, ISearchServiceFactory searchServiceFactory)
            {
                _cacheService = cacheService;
                _searchServiceFactory = searchServiceFactory;
            }

            public async Task<BaseResponseModel> Handle(SearchRequest.Command request, CancellationToken cancellationToken)
            {
                var invalidRequest = RequestValidation(request);
                if (invalidRequest != null)
                {
                    return invalidRequest;
                }
                var keyCache = string.Format(KeyCacheConstants.SearchKey, request.Keyword, request.Url, request.BrowserType);
                var res = _cacheService.GetCache<List<RankingResultModel>>(keyCache);

                if (res != null)
                {
                    return BaseResponseModel.ReturnData(res);
                }

                var rankingResults = new List<RankingResultModel>();
                if (request.BrowserType == BrowserType.All)
                {
                    foreach (BrowserType browser in Enum.GetValues(typeof(BrowserType)))
                    {
                        if (browser != BrowserType.All)
                        {
                            var service = _searchServiceFactory.GetSearchService(browser);
                            if (service != null)
                            {
                                var result = await service.SearchAsync(request.Keyword, request.Url);
                                rankingResults.Add(new RankingResultModel()
                                {
                                    BrowserName = browser.ToName(),
                                    Position = result
                                });
                            }
                        }
                    }
                }
                else
                {
                    var searchService = _searchServiceFactory.GetSearchService(request.BrowserType);
                    var result = await searchService.SearchAsync(request.Keyword, request.Url);
                    rankingResults.Add(new RankingResultModel()
                    {
                        BrowserName = request.BrowserType.ToName(),
                        Position = result
                    });
                }
                _cacheService.SetCache(keyCache, rankingResults);
                return BaseResponseModel.ReturnData(rankingResults);
            }

            private static BaseResponseModel? RequestValidation(SearchRequest.Command request)
            {
                if (string.IsNullOrEmpty(request.Keyword))
                {
                    return BaseResponseModel.ReturnError("Keyword must not be empty.");
                }
                else if (string.IsNullOrEmpty(request.Url))
                {
                    return BaseResponseModel.ReturnError("Url must not be empty.");
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
