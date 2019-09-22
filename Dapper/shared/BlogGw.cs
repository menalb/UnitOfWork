using System.Data;
using System.Threading.Tasks;

namespace shared
{
    public interface IBlogGw
    {
        Task Create(Blog blog);
    }
    public class BlogGw : BaseUoWGw, IBlogGw
    {
        public BlogGw(IDbConnection connection) : base(connection) { }

        public BlogGw(IUnitOfWork uow) : base(uow) { }

        public Task Create(Blog blog)
        {
            return ExecuteAsync(
                "INSERT INTO Blog VALUES(@Id,@Url)",
                new { blog.Id, blog.Url });
        }
    }
}
