using Dapper;
using infrastructure.models;
using Npgsql;

namespace infrastructure;

public class Repository
{
    private readonly NpgsqlDataSource _dataSource;

    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    public Articles CreateArticle(Articles articles)
    {
        var sql = $@"INSERT INTO news.articles (headline, body, author, articleimgurl) " +
                  "VALUES (@Headline, @Body, @Author, @ArticleImgUrl) " +
                  "RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Articles>(sql, articles);
        }
    }

    public IEnumerable<Articles> GetAllArticles()
    {
        var sql = "SELECT * FROM news.articles;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Articles>(sql);
        }
    }

    public IEnumerable<NewsFeedItem> GetFeed()
    {
        var sql = "SELECT articleid, headline, body, articleimgurl " +
                  "FROM news.articles " +
                  "ORDER BY acticleid DESC ;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<NewsFeedItem>(sql);
        }
    }
}