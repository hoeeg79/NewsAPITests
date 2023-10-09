
using Bogus;
using Dapper;
using infrastructure.models;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Tests;

namespace PlaywrightTests;

public class Tests : PageTest
{
    [SetUp]
    public void Setup()
    {
        Helper.TriggerRebuild();
    }
    
    
    
    [TestCase("Bob")]
    [TestCase("Rob")]
    public async Task CreateArticleTest(string teststring)
    {
        // Arrange
        await Page.GotoAsync("http://localhost:4200/app/feed");

        await Page.GetByTestId("create-button").ClickAsync();

        await Page.GetByLabel("insert title for article please").ClickAsync();
        await Page.GetByLabel("insert title for article please").FillAsync("Article 5");
        await Page.GetByLabel("insert author for the article please").ClickAsync();
        await Page.GetByLabel("insert author for the article please").FillAsync(teststring);
        await Page.GetByLabel("insert article body please").ClickAsync();
        await Page.GetByLabel("insert article body please").FillAsync("Tekst for artikel 5, dette  er meget informatict");
        await Page.GetByLabel("insert cover-image-url for book please").ClickAsync();
        await Page.GetByLabel("insert cover-image-url for book please").FillAsync("https://asset.dr.dk/imagescaler/?protocol=https&server=www.dr.dk&file=%2Fimages%2Fother%2F2019%2F10%2F16%2Fc_yongqing_bao_-_wildlife_photographer_of_the_year.jpg&scaleAfter=crop&quality=70&w=720&h=405");
        await Page.GetByRole(AriaRole.Button, new() { Name = "send" }).ClickAsync();
        // Act
        var itemToBeThere = Page.Locator("ion-title div");
        
        // Assert
        await Expect(itemToBeThere).ToBeVisibleAsync();
    }

    [Test]
    public async Task FailCreateArticle()
    {
        // Arrange
        await Page.GotoAsync("http://localhost:4200/app/feed");

        await Page.GetByTestId("create-button").ClickAsync();

        await Page.GetByLabel("insert title for article please").ClickAsync();
        await Page.GetByLabel("insert title for article please").FillAsync("Article 5");
        await Page.GetByLabel("insert author for the article please").ClickAsync();
        await Page.GetByLabel("insert author for the article please").FillAsync("bob");
        await Page.GetByLabel("insert article body please").ClickAsync();
        await Page.GetByLabel("insert article body please").FillAsync("Tekst for artikel 5, dette  er meget informatict");
        await Page.GetByLabel("insert cover-image-url for book please").ClickAsync();
        await Page.GetByLabel("insert cover-image-url for book please").FillAsync("https://asset.dr.dk/imagescaler/?protocol=https&server=www.dr.dk&file=%2Fimages%2Fother%2F2019%2F10%2F16%2Fc_yongqing_bao_-_wildlife_photographer_of_the_year.jpg&scaleAfter=crop&quality=70&w=720&h=405");
       var element =  Page.GetByRole(AriaRole.Button, new() { Name = "send" });
       await Expect(element).ToBeDisabledAsync();
    }

    [Test]
    public async Task DeleteArticle(){
        // Arrange
        var article = new Articles()
        {
            ArticleId = 1,
            Headline = "hello world",
            Body = "Mock bodu",
            Author = "Rob",
            ArticleImgUrl = "\"https://asset.dr.dk/imagescaler/?protocol=https&server=www.dr.dk&file=%2Fimages%2Fother%2F2019%2F10%2F16%2Fc_yongqing_bao_-_wildlife_photographer_of_the_year.jpg&scaleAfter=crop&quality=70&w=720&h=405\""
        };
        var sql = $@" 
            insert into news.articles (headline, body, author, articleimgurl) VALUES (@headline, @body, @author, @articleimgurl);
            ";
        using (var conn = Helper.DataSource.OpenConnection())
        {
            conn.Execute(sql, article);
        }
        
        // Act
        Page.GetByTestId("delete-button").ClickAsync();
        var itemToBeThere = Page.Locator("ion-title div");
        
        // Assert
        await Expect(itemToBeThere).Not.ToBeVisibleAsync();
    }

    [Test]
    public async Task SearchTester()
    {
        var expected = new List<object>();
        for (var i = 1; i < 10; i++)
        {
            var article = new Articles()
            {
                ArticleId = i,
                Headline = new Faker().Random.Words(2),
                Body = "asdasdl qqq asdlkjasdlk",
                Author = new Faker().Random.Word(),
                ArticleImgUrl = new Faker().Random.Word()
            };
            expected.Add(article);
            var sql = $@" 
            insert into news.articles (headline, body, author, articleimgurl) VALUES (@headline, @body, @author, @articleimgurl);
            ";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, article);
            }
        }
        await Page.GotoAsync("http://localhost:4200/app/feed");
        
        
        
    }
}