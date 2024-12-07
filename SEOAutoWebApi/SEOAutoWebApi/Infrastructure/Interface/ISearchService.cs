namespace SEOAutoWebApi.Infrastructure.Interface
{
    public interface ISearchService
    {
        Task<string> SearchAsync(string keyword, string url);
    }
}
