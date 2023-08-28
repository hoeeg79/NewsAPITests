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
        return _service.GetAllArticles();
    }
    
    [HttpPost]
    [Route("/api/articles")]
    public Articles PostArticle(Articles articles)
    {
        return _service.CreateArticle(articles);
    }


    [HttpGet]
    [Route("/api/articles/{id}")]
    public Articles GetSpecificArticle([FromRoute] int articleId)
    {
        return _service.GetSpecificArticle(articleId);
    }

    [HttpDelete]
    [Route("/api/articles/{id}")]
    public void DeleteArticle([FromRoute] int articleId)
    {
        _service.DeleteArticle(articleId);
    }

    [HttpPut]
    [Route("/api/articles/{id}")]
    public Articles UpdateArticle([FromRoute] int articleId, [FromBody] Articles articlesDTO)
    {
        return _service.UpdateArticle(articleId, articlesDTO);
    }
}