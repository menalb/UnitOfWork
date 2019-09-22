using System.Data;
using System.Threading.Tasks;

namespace shared
{
    public interface IPostGw
    {
        Task Create(Post post);
    }
    public class PostGw : BaseUoWGw, IPostGw
    {
        public PostGw(IDbConnection connection) : base(connection) { }

        public PostGw(IUnitOfWork uow) : base(uow) { }

        public Task Create(Post post)
        {
            return ExecuteAsync(
                "INSERT INTO Post VALUES(@Id,@Title,@Content,@BlogId)",
                new { post.Id, post.Title, post.Content, post.BlogId });
        }
    }
}
