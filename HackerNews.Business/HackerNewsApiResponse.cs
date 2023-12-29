using System.Collections.Generic;

namespace HackerNews.Business
{
    public class HackerNewsApiResponse
    {
        public IEnumerable<Hit> Hits { get; set; }

        public class Hit
        {
            public int ObjectId { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public int Points { get; set; }
            public string Author { get; set; }
            public string Text { get; set; }
        }
    }
}
