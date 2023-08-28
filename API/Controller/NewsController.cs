using infrastructure.models;
using Microsoft.AspNetCore.Mvc;
using service;

namespace API.Controller;

[ApiController]
public class NewsController
{
    private readonly Service _service;

    public NewsController(Service service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/api/feed")]
    public IEnumerable<NewsFeedItem> GetFeed()
    {
        return _service.GetFeed();
    }
    
    [HttpGet]
    [Route("/api/articles")]
    public IEnumerable<Articles> GetAllArticles()
    {
        return _service.GetAllAritcles();
    }
    
    [HttpPost]
    [Route("/api/articles")]
    public Articles PostArticle(Articles articles)
    {
        return _service.CreateArticle(articles);
    }
}