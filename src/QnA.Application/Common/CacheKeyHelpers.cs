namespace QnA.Application.Common;

public static class CacheKeyHelpers
{
    public const string GET_ALL_QUESTIONS = "GET_ALL_QUESTIONS";

    public const string GET_QUESTION_BY_ID = "GET_QUESTION_ID_{0}";


    public static void SetCacheEntry(ICacheEntry cacheEntry)
    {
        cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(3);
        cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20);
    }
}
