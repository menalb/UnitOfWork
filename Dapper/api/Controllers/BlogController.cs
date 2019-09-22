using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shared;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task Create()
        {
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

            await _context.CreateBlog(myblog);
        }
    }
}
