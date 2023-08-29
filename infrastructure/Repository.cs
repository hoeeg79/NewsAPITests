using Dapper;
using infrastructure.models;
using Npgsql;

namespace infrastructure;

public class Repository
{
    private readonly string[] validAuthors = {"Bob", "Rob", "Dob", "Lob"};

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
        if (validAuthors.Contains(articles.Author))
        {
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirst<Articles>(sql, articles);
            }
        }

        throw new Exception("Invalid author given");
    }

    public IEnumerable<NewsFeedItem> GetFeed()
    {
        var sql = "SELECT articleid, headline, LEFT(body,50) body, articleimgurl " +
                  "FROM news.articles " +
                  "ORDER BY articleid DESC ;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<NewsFeedItem>(sql);
        }
    }

    public Articles GetSpecificArticle(int articleId)
    {
        var sql = "SELECT * FROM news.articles " +
                  "WHERE articleid = @articleId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Articles>(sql, articleId);
        }
    }

    public void DeleteArticle(int articleId)
    {
        var sql = "DELETE FROM news.articles " +
                  "WHERE articleid = @articleId;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, articleId);
        }
    }

    public Articles UpdateArticle(int articleId, Articles articlesDto)
    {
        var sql = @"UPDATE news.articles SET 
                         headline = @Headline, 
                         body = @Body, 
                         author = @Author, 
                         articleimgurl = @ArticleImgUrl 
                         WHERE articleid = @articleId 
                         RETURNING *;";

        if (validAuthors.Contains(articlesDto.Author))
        {
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirst<Articles>(sql, new
                {
                    articlesDto.Headline,
                    articlesDto.Body,
                    articlesDto.Author,
                    articlesDto.ArticleImgUrl,
                    articleId
                });
            }
        }

        throw new Exception("Invalid choice of author");
    }

    public IEnumerable<Articles> SearchArticles(string query, int pageSize)
    {
        var sql = @"SELECT * FROM news.articles " +
                  "WHERE body LIKE '%' || @query || '%' OR headline LIKE '%' || @query || '%' LIMIT @pageSize;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Articles>(sql, new {query, pageSize});
        }
    }
}