using infrastructure;
using infrastructure.models;

namespace service;

public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }
    
    public Articles CreateArticle(Articles articles)
    {
        return _repository.CreateArticle(articles);
    }

    public IEnumerable<NewsFeedItem> GetFeed()
    {
        return _repository.GetFeed();
    }

    public Articles GetSpecificArticle(int articleId)
    {
        return _repository.GetSpecificArticle(articleId);
    }

    public void DeleteArticle(int articleId)
    {
        _repository.DeleteArticle(articleId);
    }

    public Articles UpdateArticle(int articleId, Articles articlesDto)
    {
        return _repository.UpdateArticle(articleId, articlesDto);
    }

    public IEnumerable<Articles> SearchArticles(string query, int pageSize)
    {
        return _repository.SearchArticles(query, pageSize);
    }
}