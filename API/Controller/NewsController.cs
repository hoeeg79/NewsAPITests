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

    [HttpPost]
    [Route("/api/articles")]
    public Articles PostArticle(Articles articles)
    {
        return _service.CreateArticle(articles);
    }


    [HttpGet]
    [Route("/api/articles/{articleId}")]
    public Articles GetSpecificArticle([FromRoute] int articleId)
    {
        return _service.GetSpecificArticle(articleId);
    }

    [HttpDelete]
    [Route("/api/articles/{articleId}")]
    public void DeleteArticle([FromRoute] int articleId)
    {
        _service.DeleteArticle(articleId);
    }

    [HttpPut]
    [Route("/api/articles/{articleId}")]
    public Articles UpdateArticle([FromRoute] int articleId, [FromBody] Articles articlesDTO)
    {
        return _service.UpdateArticle(articleId, articlesDTO);
    }

    [HttpGet]
    [Route("/api/articles")]
    public IEnumerable<Articles> SearchArticles([FromQuery] string searchterm, [FromQuery] int pagesize)
    {
        if (pagesize > 0 && searchterm.Length>3)
        {
            return _service.SearchArticles(searchterm, pagesize);
        }

        throw new Exception("Your search term were too short, or pagesize were too low.");
    }
}