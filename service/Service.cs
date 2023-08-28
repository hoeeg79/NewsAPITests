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

    public IEnumerable<Articles> GetAllAritcles()
    {
        return _repository.GetAllArticles();
    }

    public IEnumerable<NewsFeedItem> GetFeed()
    {
        return _repository.GetFeed();
    }
}