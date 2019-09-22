using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

using shared;

namespace console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = RegisterServices();

            var context = serviceProvider.GetService<BlogContext>();

            var myblog = new Blog
            {
                Id = 1,
                Url = "www.google.ie",
                Posts = new List<Post>
                        {
                            new Post { Id = 1, Title = "Test_1", Content = "test content 1", BlogId = 1 },
                            new Post { Id = 2, Title = "Test_2", Content = "test content 2", BlogId = 1 }
                        }
            };
            await context.CreateBlog(myblog);
        }

        public static ServiceProvider RegisterServices()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CqrsInPractice;Integrated Security=True";

            return new ServiceCollection()
            .AddTransient<IDbConnection>(ctx => new SqlConnection(connectionString))
            .AddTransient<IUnitOfWork>(ctx =>
            {
                var conn = ctx.GetService<IDbConnection>();
                conn.Open();
                return new DapperUnitOfWork(conn, true);
            })
            .AddTransient<BlogContext>(ctx =>
            {
                var uow = ctx.GetService<IUnitOfWork>();
                return new BlogContext(uow, new BlogGw(uow), new PostGw(uow));
            })
            .BuildServiceProvider();
        }
    }    
}
