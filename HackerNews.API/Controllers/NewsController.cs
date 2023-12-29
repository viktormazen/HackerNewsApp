using System.Threading.Tasks;
using HackerNews.Business;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("default")]
        public async Task<IActionResult> GetDefaultNews(int page = 1)
        {
            var newsCards = await _newsService.GetNewsCardsAsync("default", page);
            return Ok(newsCards);
        }

        [HttpGet("newest")]
        public async Task<IActionResult> GetNewestNews(int page = 1)
        {
            var newsCards = await _newsService.GetNewsCardsAsync("newest", page);
            return Ok(newsCards);
        }

        [HttpGet("hot")]
        public async Task<IActionResult> GetHotNews(int page = 1)
        {
            var newsCards = await _newsService.GetNewsCardsAsync("hot", page);
            return Ok(newsCards);
        }

        [HttpGet("show")]
        public async Task<IActionResult> GetShowNews(int page = 1)
        {
            var newsCards = await _newsService.GetNewsCardsAsync("show", page);
            return Ok(newsCards);
        }

        [HttpGet("ask")]
        public async Task<IActionResult> GetAskNews(int page = 1)
        {
            var newsCards = await _newsService.GetNewsCardsAsync("ask", page);
            return Ok(newsCards);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchNews(string query, int page = 1)
        {
            var newsCards = await _newsService.GetNewsCardsAsync("search", page, query);
            return Ok(newsCards);
        }
    }
}
