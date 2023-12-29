using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNews.Data;
using Newtonsoft.Json;

namespace HackerNews.Business
{
    public class NewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public NewsService(INewsRepository newsRepository, IHttpClientFactory httpClientFactory)
        {
            _newsRepository = newsRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<NewsCard>> GetNewsCardsAsync(string filter, int page = 1, string searchQuery = null)
        {
            var apiUrl = GetApiUrl(filter, page, searchQuery);

            using var httpClient = _httpClientFactory.CreateClient();

            try
            {
                var response = await httpClient.GetStringAsync(apiUrl);
                var result = JsonConvert.DeserializeObject<HackerNewsApiResponse>(response);

                // Transform the API response to NewsCard objects
                var newsCards = result.Hits.Select(hit => new NewsCard
                {
                    Id = hit.ObjectId,
                    Title = hit.Title,
                    Url = hit.Url,
                    Points = hit.Points,
                    Author = hit.Author,
                    Text = hit.Text
                }).ToList();

                return newsCards;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"HTTP Request Exception: {ex.Message}");
            }
        }

        private string GetApiUrl(string filter, int page, string searchQuery)
        {
            // Base API URL
            var apiUrl = "https://hn.algolia.com/api/v1/search?tags=story&hitsPerPage=20&page=" + page;

            // Apply filter based on the provided criteria
            switch (filter.ToLower())
            {
                case "newest":
                    apiUrl = "https://hn.algolia.com/api/v1/search_by_date?tags=story&hitsPerPage=20&page=" + page;
                    break;

                case "hot":
                    // Sorting by votes (score) in descending order
                    apiUrl += "&tags=story&numericFilters=points>1&analytics=true&analyticsTags=vote";
                    break;

                case "show":
                    apiUrl = "https://hn.algolia.com/api/v1/search?tags=show_hn&hitsPerPage=20&page=" + page;
                    break;

                case "ask":
                    apiUrl = "https://hn.algolia.com/api/v1/search?tags=ask_hn&hitsPerPage=20&page=";
                    break;

                case "default":
                    break;

                case "search":
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        apiUrl += $"&query={Uri.EscapeDataString(searchQuery)}";
                    }
                    break;

                default:
                    throw new ArgumentException("Invalid filter specified.", nameof(filter));
            }

            return apiUrl;
        }
    }
}