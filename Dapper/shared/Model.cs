
using System.Collections.Generic;

namespace shared
{
    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public IEnumerable<Post> Posts {get;set;}
    }
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
    }
}