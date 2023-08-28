using System.ComponentModel.DataAnnotations;

namespace infrastructure.models;

public class Articles
{
        public int ArticleId { get; set; }
        [MinLength(5)]
        [MaxLength(30)]
        public string Headline { get; set; }
        [MaxLength(1000)]
        public string Body { get; set; }
        public string Author { get; set; }
        public string ArticleImgUrl { get; set; }
}