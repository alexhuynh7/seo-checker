﻿using SEOAutoWebApi.Infrastructure.Enums;

namespace SEOAutoWebApi.Infrastructure.Domain
{
    public class SearchConstants
    {
        public const int PageSize = 10;
        public const int TotalPage = 10;
        public const string GoogleBaseUrl = "https://www.google.com.au";
        public const string BingBaseUrl = "https://www.bing.com";
        public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";
        public static readonly List<BrowserType> SUPPORT_BROWSERS = new()
        {
            BrowserType.All,
            BrowserType.Google,
            BrowserType.Bing,
        };
    }
}
