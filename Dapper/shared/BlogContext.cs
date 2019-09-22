using System.Threading.Tasks;

namespace shared
{
    public class BlogContext
    {
        private readonly IUnitOfWork _uow;
        private readonly IBlogGw _blogGw;
        private readonly IPostGw _postGw;
        public BlogContext(IUnitOfWork uow, IBlogGw blogGw, IPostGw postGw)
        {
            _uow = uow;
            _blogGw = blogGw;
            _postGw = postGw;
        }

        public async Task CreateBlog(Blog blog)
        {
            await _blogGw.Create(blog);
            foreach (var post in blog.Posts)
                await _postGw.Create(post);

            _uow.SaveChanges();
        }
    }
}
