using System.Collections.Generic;

namespace HackerNews.Data
{
    public interface INewsRepository
    {
        List<NewsCard> GetNewsCards();
    }
}
