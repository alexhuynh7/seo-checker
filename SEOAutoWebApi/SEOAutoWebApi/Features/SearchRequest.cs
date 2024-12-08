using FluentValidation;
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
        public class Command : IRequest<ResponseModel>
        {
            public string Keyword { get; set; }
            public string Url { get; set; }
            public BrowserType BrowserType { get; set; }
        }

        public sealed class Handler : IRequestHandler<SearchRequest.Command, ResponseModel>
        {
            private readonly ICacheService _cacheService;
            private readonly ISearchServiceFactory _searchServiceFactory;
            private readonly IValidator<SearchRequest.Command> _validator;

            public Handler(ICacheService cacheService, ISearchServiceFactory searchServiceFactory, IValidator<SearchRequest.Command> validator)
            {
                _cacheService = cacheService;
                _searchServiceFactory = searchServiceFactory;
                _validator = validator;
            }

            public class Validator : AbstractValidator<SearchRequest.Command>
            {
                public Validator()
                {
                    RuleFor(c => c.Keyword).NotEmpty();
                    RuleFor(c => c.Url).NotEmpty();
                }
            }

            public async Task<ResponseModel> Handle(SearchRequest.Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return ResponseModel.ReturnError(validationResult.ToString());
                }
                
                var keyCache = string.Format(KeyCacheConstants.SearchKey, request.Keyword, request.Url, request.BrowserType);
                var res = _cacheService.GetCache<List<RankingResultModel>>(keyCache);

                if (res != null)
                {
                    return ResponseModel.ReturnData(res);
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
                return ResponseModel.ReturnData(rankingResults);
            }
        }
    }
}
