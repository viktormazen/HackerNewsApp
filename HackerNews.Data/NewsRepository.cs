using System.Collections.Generic;

namespace HackerNews.Data
{
    public class NewsRepository : INewsRepository
    {
        public List<NewsCard> GetNewsCards()
        {
            return new List<NewsCard>();
        }
    }
}
